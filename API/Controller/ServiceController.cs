using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ServiceController : ControllerBase
	{
		private readonly IServiceService _serviceService;

		public ServiceController(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _serviceService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ServiceDTO>>.SuccessResponse(result, "Fetched all services successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _serviceService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<ServiceDTO>.Failure("Service not found"));
			return Ok(ApiResponse<ServiceDTO>.SuccessResponse(result, "Fetched service successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateServiceDTO dto)
		{
			var result = await _serviceService.AddAsync(dto);
			return Ok(ApiResponse<ServiceDTO>.SuccessResponse(result, "Service created successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceDTO dto)
		{
			dto.Id = id;
			var result = await _serviceService.UpdateNoForeignId(dto);
			return Ok(ApiResponse<ServiceDTO>.SuccessResponse(result, "Service updated successfully"));
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _serviceService.DeleteAsync(id);
			return Ok(ApiResponse<ServiceDTO>.SuccessResponse(result, "Service deleted successfully"));
		}

		[HttpGet("by-user/{userId:guid}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var result = await _serviceService.GetServicesByUserIdAsync(userId);
			return Ok(ApiResponse<IEnumerable<ServiceDTO>>.SuccessResponse(result, "Fetched services by user successfully"));
		}

		[HttpGet("by-category/{categoryId:guid}")]
		public async Task<IActionResult> GetByCategoryId(Guid categoryId)
		{
			var result = await _serviceService.GetServicesByCategoryIdAsync(categoryId);
			return Ok(ApiResponse<IEnumerable<ServiceDTO>>.SuccessResponse(result, "Fetched services by category successfully"));
		}
	}
}
