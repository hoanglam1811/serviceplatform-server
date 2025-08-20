using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class WalletController : ControllerBase
	{
		private readonly IWalletService _walletService;

		public WalletController(IWalletService walletService)
		{
			_walletService = walletService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _walletService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<WalletDTO>>.SuccessResponse(result, "Fetched all wallets successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _walletService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<WalletDTO>.Failure("Wallet not found"));
			return Ok(ApiResponse<WalletDTO>.SuccessResponse(result, "Fetched wallet successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateWalletDTO dto)
		{
			var result = await _walletService.AddAsync(dto);
			return Ok(ApiResponse<WalletDTO>.SuccessResponse(result, "Wallet created successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalletDTO dto)
		{
			dto.Id = id;
			var result = await _walletService.UpdateAsync(dto);
			return Ok(ApiResponse<WalletDTO>.SuccessResponse(result, "Wallet updated successfully"));
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _walletService.DeleteAsync(id);
			return Ok(ApiResponse<WalletDTO>.SuccessResponse(result, "Wallet deleted successfully"));
		}

		[HttpGet("by-user/{userId:guid}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var result = await _walletService.GetByUserIdAsync(userId);
			if (result == null)
				return NotFound(ApiResponse<WalletDTO>.Failure("Wallet not found for this user"));
			return Ok(ApiResponse<WalletDTO>.SuccessResponse(result, "Fetched wallet by user successfully"));
		}
	}
}
