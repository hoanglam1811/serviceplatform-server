using Repository.DTO;
using Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Service.Implement
{
	public class PayOSService : IPayOSService
	{
		private readonly HttpClient _httpClient;
		private readonly string _clientId;
		private readonly string _apiKey;
		private readonly string _checksumKey;

		public PayOSService(string clientId, string apiKey, string checksumKey)
		{
			_clientId = clientId;
			_apiKey = apiKey;
			_checksumKey = checksumKey;

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
			var json = JsonSerializer.Serialize(request);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("v2/payment-requests", content);
			var responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
				throw new Exception($"PayOS error: {response.StatusCode} - {responseContent}");

			return JsonSerializer.Deserialize<PayOSPaymentResponseDTO>(responseContent,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
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
	}
}
