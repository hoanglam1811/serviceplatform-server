using Microsoft.Extensions.Configuration;
using Repository.DTO;
using Service.Service.Interface;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Service.Service.Implement
{
	public class PayOSService : IPayOSService
	{
		private readonly HttpClient _httpClient;
		private readonly string _clientId;
		private readonly string _apiKey;
		private readonly string _checksumKey;
    private readonly IConfiguration _config;

		public PayOSService(IConfiguration config)
		{
      _config = config;
			_clientId = _config["PayOS:ClientId"];
			_apiKey = _config["PayOS:ApiKey"];
			_checksumKey = _config["PayOS:ChecksumKey"];

			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://api-merchant.payos.vn/")
			};
			_httpClient.DefaultRequestHeaders.Add("x-client-id", _clientId);
			_httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		// 1. Tạo Payment Request
		public async Task<PayOSPaymentResponseDTO> CreatePaymentAsync(PayOSPaymentRequestDTO request)
		{
      var orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      var message = $"amount={request.amount}&cancelUrl={request.cancelUrl}&"+
        $"description={request.description}&orderCode={orderCode}&returnUrl={request.returnUrl}";

      request.orderCode = orderCode;
      request.signature = CreateSignature(_checksumKey, message);

			var json = JsonSerializer.Serialize(request);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("v2/payment-requests", content);
			var responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
				throw new Exception($"PayOS error: {response.StatusCode} - {responseContent}");

      using var doc = JsonDocument.Parse(responseContent);
      var dataElement = doc.RootElement.GetProperty("data");

      return JsonSerializer.Deserialize<PayOSPaymentResponseDTO>(dataElement,
				new JsonSerializerOptions {})!;
		}

		// 2. Lấy trạng thái Payment theo orderCode
		public async Task<PayOSPaymentResponseDTO> GetPaymentStatusAsync(string orderCode)
		{
			var response = await _httpClient.GetAsync($"v2/payment-requests/{orderCode}");
			var responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
				throw new Exception($"PayOS error: {response.StatusCode} - {responseContent}");

			return JsonSerializer.Deserialize<PayOSPaymentResponseDTO>(responseContent,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
		}

		// 3. Xác minh webhook callback
		public Task<bool> VerifyWebhookAsync(string signature, string payload)
		{
			using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_checksumKey));
			var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
			var computedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();

			return Task.FromResult(signature == computedSignature);
		}

    public string CreateSignature(string key, string message)
    {

      // Convert key and message to byte arrays
      var keyBytes = Encoding.UTF8.GetBytes(key);
      var messageBytes = Encoding.UTF8.GetBytes(message);

      // Compute HMAC-SHA256
      using var hmac = new HMACSHA256(keyBytes);
      var hashBytes = hmac.ComputeHash(messageBytes);

      // Convert to hex string
      var signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
      return signature;
    }
	}
}
