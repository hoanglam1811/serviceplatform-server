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
		public async Task<ActionResult<IEnumerable<ProviderProfileDTO>>> GetAll()
		{
			var providers = await _providerProfileService.GetAllAsync();
			return Ok(providers);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<ProviderProfileDTO>> GetById(Guid id)
		{
			var provider = await _providerProfileService.GetByIdAsync(id);
			if (provider == null) return NotFound();
			return Ok(provider);
		}

		[HttpGet("user/{userId:guid}")]
		public async Task<ActionResult<ProviderProfileDTO>> GetByUserId(Guid userId)
		{
			var provider = await _providerProfileService.GetByUserIdAsync(userId);
			if (provider == null) return NotFound();
			return Ok(provider);
		}

		[HttpPost]
		public async Task<ActionResult<ProviderProfileDTO>> Create([FromBody] CreateProviderProfileDTO dto)
		{
			var created = await _providerProfileService.AddAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut]
		public async Task<ActionResult<ProviderProfileDTO>> Update([FromBody] UpdateProviderProfileDTO dto)
		{
			var updated = await _providerProfileService.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _providerProfileService.DeleteAsync(id);
			if (deleted == null) return NotFound();
			return NoContent();
		}
	}
}
