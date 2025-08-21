using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;
using API.DTO;
using Service.Service.Implement;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;

		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpPost("create-payos-link")]
		public async Task<IActionResult> CreatePayOSLink([FromBody] CreatePaymentDTO dto)
		{
			var payment = await _paymentService.CreatePayOSPaymentLinkAsync(dto);
			return Ok(new { checkoutUrl = payment.PaymentUrl });
		}

		[HttpPost("webhook/payos")]
		public async Task<IActionResult> PayOSWebhook([FromForm] PayOSWebhookDTO dto)
		{
			var updated = await _paymentService.UpdateStatusAsync(Guid.Parse(dto.orderCode), dto.status);
			if (updated == null) return NotFound();

			return Ok("OK");
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAll()
		{
			var payments = await _paymentService.GetAllAsync();
			return Ok(payments);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<PaymentDTO>> GetById(Guid id)
		{
			var payment = await _paymentService.GetByIdAsync(id);
			if (payment == null) return NotFound();
			return Ok(payment);
		}

		[HttpGet("booking/{bookingId:guid}")]
		public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetByBookingId(Guid bookingId)
		{
			var payments = await _paymentService.GetPaymentsByBookingIdAsync(bookingId);
			return Ok(payments);
		}

		[HttpPatch("{id:guid}/status")]
		public async Task<ActionResult<PaymentDTO>> UpdateStatus(Guid id, [FromQuery] string status)
		{
			var updated = await _paymentService.UpdateStatusAsync(id, status);
			if (updated == null) return NotFound();
			return Ok(updated);
		}
	}
}
