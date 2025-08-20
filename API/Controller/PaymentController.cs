using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

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
