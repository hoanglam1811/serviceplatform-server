using Microsoft.AspNetCore.Mvc;
using Repository.DTO;
using API.DTO;
using Service.Service.Implement;
using Service.Service.Interface;
using Repository.Entities;

namespace API.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;
    private readonly IWebHostEnvironment _env;

	public AuthenticationController(AuthService authService,
        IWebHostEnvironment env,
        JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _env = env;
	}

    [HttpPost("login")]
    public async Task<IActionResult> CustomerLogin([FromBody]LoginDTO dto)
    {
        try{
            var customer = await _authService.LoginCustomerAsync(dto);
            string token = _jwtService.GenerateToken(customer, "Customer");
            Response.Cookies.Append("auth_token", token, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true, // only over HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return Ok(ApiResponse<object>.SuccessResponse(new { 
              id = customer.Id,
              name = customer.FullName,
              role = customer.Role 
            }, "Customer logged in successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
        }
    }

    [HttpPost("login-service-provider")]
    public async Task<IActionResult> LoginClient([FromBody]LoginDTO dto)
    {
        try{
            var serviceProvider = await _authService.LoginServiceProviderAsync(dto);
            string token = _jwtService.GenerateToken(serviceProvider, "ServiceProvider");
            return Ok(ApiResponse<string>.SuccessResponse(token, "ServiceProvider logged in successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
        }
    }

	[HttpPost("login-admin")]
	public async Task<IActionResult> LoginAdmin([FromBody] LoginDTO dto)
	{
		try
		{
			var admin = await _authService.LoginAdminAsync(dto);
			string token = _jwtService.GenerateToken(admin, "Admin");
			return Ok(ApiResponse<string>.SuccessResponse(token, "Admin logged in successfully"));
		}
		catch (Exception ex)
		{
			return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
		}
	}

	[HttpPost("register-customer")]
    public async Task<IActionResult> CustomerRegister([FromForm]RegisterDTO dto)
    {
        var customer = await _authService.RegisterCustomerAsync(dto);
        string token = _jwtService.GenerateToken(customer, "Customer");
        Response.Cookies.Append("auth_token", token, new CookieOptions
        {
            HttpOnly = true,
            //Secure = true, // only over HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });
        return Ok(ApiResponse<object>.SuccessResponse(new { 
          id = customer.Id,
          name = customer.FullName,
          role = customer.Role 
        }, "Customer registered successfully"));
    }

	[HttpPost("register-service-provider")]
	public async Task<IActionResult> ServiceProviderRegister([FromBody] RegisterDTO dto)
	{
		var serviceProvider = await _authService.RegisterCustomerAsync(dto);
		string token = _jwtService.GenerateToken(serviceProvider, "ServiceProvider");
		return Ok(ApiResponse<string>.SuccessResponse(token, "Service Provider registered successfully"));
	}

	[HttpGet("customers")]
	public async Task<IActionResult> GetAllCustomers()
	{
		try
		{
			var customers = await _authService.GetAllCustomersAsync();
			return Ok(ApiResponse<IEnumerable<UserDTO>>.SuccessResponse(customers, "Get all customers successfully"));
		}
		catch (Exception ex)
		{
			return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
		}
	}

	[HttpGet("service-providers")]
	public async Task<IActionResult> GetAllServiceProviders()
	{
		try
		{
			var serviceProviders = await _authService.GetAllCustomersAsync();
			return Ok(ApiResponse<IEnumerable<UserDTO>>.SuccessResponse(serviceProviders, "Get all service-providers successfully"));
		}
		catch (Exception ex)
		{
			return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
		}
	}


    [HttpPost("logout")]
    public IActionResult Logout()
    {
      Response.Cookies.Delete("auth_token");
      return Ok();
    }

	//[HttpGet("admin")]
	//public async Task<IActionResult> GetAdmin()
	//{
	//	try
	//	{
	//		var admins = await _adminService.GetAllAsync();
	//		var admin = admins.FirstOrDefault();

	//		if (admin == null)
	//			return NotFound(ApiResponse<string>.Failure("Admin not found"));

	//		return Ok(ApiResponse<AdminDTO>.SuccessResponse(admin, "Admin info retrieved successfully"));
	//	}
	//	catch (Exception ex)
	//	{
	//		return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
	//	}
	//}

	//[HttpPost("send-reset-password-otp")]
	//public async Task<IActionResult> SendResetPasswordOtp([FromBody] SendResetPasswordOTPRequest request)
	//{
	//	try
	//	{
	//		await _authService.SendResetPasswordOTPAsync(request.Email);
	//		return Ok(ApiResponse<string>.SuccessResponse("OTP sent to email", "OTP sent"));
	//	}
	//	catch (Exception ex)
	//	{
	//		return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
	//	}
	//}

	//[HttpPost("verify-reset-password-otp")]
	//public IActionResult VerifyResetPasswordOtp([FromBody] VerifyOTPRequest request)
	//{
	//	try
	//	{
	//		bool isValid = _authService.VerifyResetPasswordOTP(request.Email, request.OTP);

	//		if (!isValid)
	//			return Ok(ApiResponse<bool>.SuccessResponse(false, "OTP is invalid or expired"));

	//		return Ok(ApiResponse<bool>.SuccessResponse(true, "OTP verified"));
	//	}
	//	catch (Exception ex)
	//	{
	//		return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
	//	}
	//}

	//[HttpPost("reset-password")]
	//public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
	//{
	//	try
	//	{
	//		await _authService.ResetPasswordAsync(request.Email, request.NewPassword);
	//		return Ok(ApiResponse<string>.SuccessResponse("Password reset successfully", "Password changed"));
	//	}
	//	catch (Exception ex)
	//	{
	//		return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
	//	}
	//}

	public class SendResetPasswordOTPRequest
	{
		public string Email { get; set; }
	}

	public class VerifyOTPRequest
	{
		public string Email { get; set; }
		public string OTP { get; set; }
	}

	public class ResetPasswordRequest
	{
		public string Email { get; set; }
		public string NewPassword { get; set; }
	}


}
