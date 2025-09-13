using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization.Metadata;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class PayOSController : ControllerBase
	{
		private readonly IPayOSService _payOSService;

		public PayOSController(IPayOSService payOSService)
		{
			_payOSService = payOSService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreatePayment([FromBody] PayOSPaymentRequestDTO dto)
		{
			var result = await _payOSService.CreatePaymentAsync(dto);
			return Ok(result);
		}

		[HttpGet("status/{orderCode}")]
		public async Task<IActionResult> GetStatus(string orderCode)
		{
			var result = await _payOSService.GetPaymentStatusAsync(orderCode);
			return Ok(result);
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> Webhook([FromBody] PayOSWebhook payload)
		{
			var signature = Request.Headers["x-signature"].ToString();
      var options = new JsonSerializerOptions
      {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };

      // Use custom order resolver
      options.TypeInfoResolver = new DefaultJsonTypeInfoResolver
      {
        Modifiers =
        {
          ti =>
          {
            // Sort properties alphabetically
            ti.Properties.ToList().Sort((p1, p2) => string.Compare(p1.Name, p2.Name, StringComparison.Ordinal));
          }
        }
      };

      string json = JsonSerializer.Serialize(payload.data, options);

			var verified = await _payOSService.VerifyWebhookAsync(signature, json);
      System.Console.WriteLine(verified);
			if (!verified)
				return Unauthorized();

			return Ok(new { success = true });
		}
	}
}
