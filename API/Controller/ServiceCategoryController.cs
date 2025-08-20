using API.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using Service.Service.Interface;

namespace API.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ServiceCategoryController : ControllerBase
	{
		private readonly IServiceCategoryService _serviceCategoryService;

		public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
		{
			_serviceCategoryService = serviceCategoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _serviceCategoryService.GetAllAsync();
			return Ok(ApiResponse<IEnumerable<ServiceCategoryDTO>>.SuccessResponse(result, "Fetched all categories successfully"));
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _serviceCategoryService.GetByIdAsync(id);
			if (result == null)
				return NotFound(ApiResponse<ServiceCategoryDTO>.Failure("Category not found"));

			return Ok(ApiResponse<ServiceCategoryDTO>.SuccessResponse(result, "Fetched category successfully"));
		}

		[HttpGet("by-name/{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _serviceCategoryService.GetByNameAsync(name);
			if (result == null)
				return NotFound(ApiResponse<ServiceCategoryDTO>.Failure("Category not found"));

			return Ok(ApiResponse<ServiceCategoryDTO>.SuccessResponse(result, "Fetched category successfully"));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateServiceCategoryDTO dto)
		{
			var result = await _serviceCategoryService.AddAsync(dto);
			return Ok(ApiResponse<ServiceCategoryDTO>.SuccessResponse(result, "Category created successfully"));
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCategoryDTO dto)
		{
			dto.CategoryId = id;
			var result = await _serviceCategoryService.UpdateAsync(dto);
			return Ok(ApiResponse<ServiceCategoryDTO>.SuccessResponse(result, "Category updated successfully"));
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _serviceCategoryService.DeleteAsync(id);
			return Ok(ApiResponse<ServiceCategoryDTO>.SuccessResponse(result, "Category deleted successfully"));
		}
	}
}
