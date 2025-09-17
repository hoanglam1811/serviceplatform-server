using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProviderProfileController : ControllerBase
	{
		private readonly IProviderProfileService _providerProfileService;

		public ProviderProfileController(IProviderProfileService providerProfileService)
		{
			_providerProfileService = providerProfileService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var providers = await _providerProfileService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ProviderProfileDTO>>
				.SuccessResponse(providers, "Fetched all provider profiles successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var provider = await _providerProfileService.GetByIdAsync(id);
			if (provider == null)
				return Ok(ApiResponse<ProviderProfileDTO>
					.SuccessResponse(null, "Provider profile not found"));

			return Ok(ApiResponse<ProviderProfileDTO>
				.SuccessResponse(provider, "Fetched provider profile successfully"));
		}

		[HttpGet("user/{userId:guid}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var provider = await _providerProfileService.GetByUserIdAsync(userId);
			if (provider == null)
				return Ok(ApiResponse<ProviderProfileDTO>
					.SuccessResponse(null, "Provider profile not found for this user"));

			return Ok(ApiResponse<ProviderProfileDTO>
				.SuccessResponse(provider, "Fetched provider profile successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateProviderProfileDTO dto)
		{
			var created = await _providerProfileService.AddAsync(dto);
			return Ok(ApiResponse<ProviderProfileDTO>
				.SuccessResponse(created, "Provider profile created successfully"));
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProviderProfileDTO dto)
		{
			var updated = await _providerProfileService.UpdateAsync(dto);
			if (updated == null)
				return Ok(ApiResponse<ProviderProfileDTO>
					.SuccessResponse(null, "Provider profile not found to update"));

			return Ok(ApiResponse<ProviderProfileDTO>
				.SuccessResponse(updated, "Provider profile updated successfully"));
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _providerProfileService.DeleteAsync(id);
			if (deleted == null)
				return Ok(ApiResponse<ProviderProfileDTO>
					.SuccessResponse(null, "Provider profile not found to delete"));

			return Ok(ApiResponse<ProviderProfileDTO>
				.SuccessResponse(deleted, "Provider profile deleted successfully"));
		}
	}
}
