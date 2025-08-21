using Microsoft.AspNetCore.Mvc;
using Service.Service.Interface;
using Repository.Entities;
using API.DTO;
using Repository.DTO;

namespace API.Controller;
[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;
    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    //[HttpGet("send-otp")]
    //public async Task<IActionResult> SendOTP([FromQuery] string email)
    //{
    //    try
    //    {
    //        await _emailService.SendOTPAsync(email);
    //        return Ok(ApiResponse<string>.SuccessResponse("OTP sent successfully", "OTP sent successfully"));
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(400, ApiResponse<string>.Failure(ex.Message));
    //    }
    //}

    //[HttpGet("verify-otp")]
    //public async Task<IActionResult> VerifyOTP([FromQuery] string email, [FromQuery] string otp)
    //{
    //    try
    //    {
    //        bool result = await _emailService.ValidateOTPAsync(email, otp);
    //        if(!result)
    //            return Ok(ApiResponse<bool>.SuccessResponse(false, "OTP not valid"));
    //        return Ok(ApiResponse<bool>.SuccessResponse(true, "OTP valid"));
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
    //    }
    //}

    [HttpPost]
    public async Task<IActionResult> SendEmail(SendEmailRequest request)
    {
        try
        {
            await _emailService.SendEmailAsync(request);
            return Ok(ApiResponse<string>.SuccessResponse("Email sent successfully", "Email sent successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
        }
    }

	//[HttpPost("notify-admin-new-coach")]
	//public async Task<IActionResult> NotifyAdminNewCoach([FromBody] CoachProfileDTO coach)
	//{
	//	try
	//	{
	//		await _emailService.NotifyAdminNewCoachAsync(coach);
	//		return Ok(ApiResponse<string>.SuccessResponse("Notification sent to admin", "Success"));
	//	}
	//	catch (Exception ex)
	//	{
	//		return StatusCode(500, ApiResponse<string>.Failure(ex.Message));
	//	}
	//}
}

