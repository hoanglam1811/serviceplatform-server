using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

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
		public async Task<IActionResult> Webhook([FromBody] object payload)
		{
			var signature = Request.Headers["x-signature"].ToString();
			var rawBody = payload.ToString();

			var verified = await _payOSService.VerifyWebhookAsync(signature, rawBody);
			if (!verified)
				return Unauthorized();

			return Ok(new { success = true });
		}
	}
}
