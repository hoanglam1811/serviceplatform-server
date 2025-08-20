using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _transactionService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<TransactionDTO>>.SuccessResponse(result, "Fetched all transactions successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _transactionService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<TransactionDTO>.Failure("Transaction not found"));
			return Ok(ApiResponse<TransactionDTO>.SuccessResponse(result, "Fetched transaction successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateTransactionDTO dto)
		{
			var result = await _transactionService.AddAsync(dto);
			return Ok(ApiResponse<TransactionDTO>.SuccessResponse(result, "Transaction created successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionDTO dto)
		{
			dto.Id = id;
			var result = await _transactionService.UpdateAsync(dto);
			return Ok(ApiResponse<TransactionDTO>.SuccessResponse(result, "Transaction updated successfully"));
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _transactionService.DeleteAsync(id);
			return Ok(ApiResponse<TransactionDTO>.SuccessResponse(result, "Transaction deleted successfully"));
		}

		[HttpGet("by-wallet/{walletId:guid}")]
		public async Task<IActionResult> GetByWalletId(Guid walletId)
		{
			var result = await _transactionService.GetTransactionsByWalletIdAsync(walletId);
			return Ok(ApiResponse<IEnumerable<TransactionDTO>>.SuccessResponse(result, "Fetched transactions by wallet successfully"));
		}
	}
}
