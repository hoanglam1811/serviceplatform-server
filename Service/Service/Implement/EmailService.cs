using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Microsoft.Extensions.Caching.Memory;
using Service.Service.Interface;
using Repository.DTO;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;

    public EmailService(IConfiguration config, IMemoryCache cache)
    {
        _config = config;
        _cache = cache;
    }

	public string GetTemplate()
	{
		var filePath = Path.Combine(AppContext.BaseDirectory, "Service", "Implement", "template.html");
		return File.ReadAllText(filePath);
	}

	public async Task SendEmailAsync(SendEmailRequest request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
        email.To.Add(MailboxAddress.Parse(request.ToEmail));
        email.Subject = request.Subject;

		var body = new TextPart(MimeKit.Text.TextFormat.Html)
		{
			Text = GetTemplate()
		.Replace("{{Content}}", request.Content ?? string.Empty)
		.Replace("{{UserName}}", request.UserName ?? string.Empty)
		};

		var multipart = new Multipart("mixed") { body };

        var memoryStreams = new List<MemoryStream>();

        try
        {
            if (request.File != null && request.File.Any())
            {
                foreach (var formFile in request.File)
                {
                    if (formFile.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await formFile.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var attachment = new MimePart(formFile.ContentType)
                        {
                            Content = new MimeContent(memoryStream, ContentEncoding.Default),
                                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                    ContentTransferEncoding = ContentEncoding.Base64,
                                    FileName = formFile.FileName
                        };

                        multipart.Add(attachment);
                        memoryStreams.Add(memoryStream); 
                    }
                }
            }

            email.Body = multipart;

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), true);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        finally
        {
            foreach (var ms in memoryStreams)
                ms.Dispose();
        }
    }

    //	public async Task SendOTPAsync(string toEmail)
    //	{
    //		string otp = CreateOTP();

    //		var coaches = await _coachProfileService.GetAllAsync();
    //		var coach = coaches.FirstOrDefault(x =>
    //			x.EmailAddress.Equals(toEmail, StringComparison.OrdinalIgnoreCase));

    //		if (coach == null)
    //		{
    //			throw new Exception("Email không tồn tại trong hệ thống.");
    //		}

    //		if (coach.Status?.ToLower() == "verified")
    //		{
    //			throw new Exception("Tài khoản của bạn đã được xác thực.");
    //		}

    //		// Nếu là unverified, tiến hành gửi mã OTP
    //		string htmlContent = $@"
    //		<p>Xin chào <strong>{coach.FullName}</strong>,</p>
    //		<p>Mã OTP xác thực email của bạn là:</p>
    //		<div style='font-size: 28px; font-weight: bold; color: #2d6cdf; padding: 12px 0;'>{otp}</div>
    //		<p>Vui lòng nhập mã này để hoàn tất việc xác thực email của bạn. Mã này có hiệu lực trong <strong>5 phút</strong>.</p>
    //		<hr style='margin: 20px 0;' />
    //		<p style='font-size: 13px; color: #888;'>Nếu bạn không yêu cầu xác thực, hãy bỏ qua email này.</p>
    //		<p style='font-size: 13px; color: #888;'>Trân trọng,<br />Đội ngũ Coaching</p>";

    //		await SendEmailAsync(new SendEmailRequest
    //		{
    //			ToEmail = toEmail,
    //			Subject = "🔐 Mã OTP xác thực email",
    //			UserName = coach.FullName,
    //			Content = htmlContent
    //		});

    //		_cache.Set($"otp:{toEmail}", otp, TimeSpan.FromMinutes(5));
    //	}

    //	public async Task<bool> ValidateOTPAsync(string email, string otp)
    //	{
    //		if (_cache.TryGetValue($"otp:{email}", out string storedOtp))
    //		{
    //			if (storedOtp == otp)
    //			{
    //				_cache.Remove($"otp:{email}");

    //				// Tìm Coach theo email
    //				var coaches = await _coachProfileService.GetAllAsync();
    //				var coach = coaches.FirstOrDefault(x => x.EmailAddress == email);

    //				if (coach != null && coach.Status?.ToLower() == "unverified")
    //				{
    //					var updateDto = new UpdateCoachProfileDTO
    //					{
    //						Id = coach.Id,
    //						Username = coach.Username,
    //						PasswordHashed = coach.PasswordHashed,
    //						Slug = coach.Slug,
    //						FullName = coach.FullName,
    //						LogoUrl = coach.LogoUrl,
    //						AvatarUrl = coach.AvatarUrl,
    //						EmailAddress = coach.EmailAddress,
    //						YoutubeUrl = coach.YoutubeUrl,
    //						InstagramUrl = coach.InstagramUrl,
    //						FacebookUrl = coach.FacebookUrl,
    //						ContactPhone = coach.ContactPhone,
    //						ContactEmail = coach.ContactEmail,
    //						Bio = coach.Bio,
    //						Status = "Verified",
    //						PaymentImg = coach.PaymentImg,
    //						BankImg = coach.BankImg,
    //						Slogan = coach.Slogan
    //					};

    //					await _coachProfileService.UpdateAsync(updateDto);
    //					await SendEmailAsync(new SendEmailRequest
    //					{
    //						ToEmail = coach.EmailAddress,
    //						Subject = "✅ Xác thực Email thành công",
    //						UserName = coach.FullName,
    //						Content = @"
    //                        <p>Xin chào <strong>" + coach.FullName + @"</strong>,</p>
    //                        <p>Chúc mừng! Bạn đã <span style='color:green; font-weight:bold;'>xác thực email thành công</span> và tài khoản huấn luyện viên của bạn đã được kích hoạt.</p>
    //                        <p>Bạn có thể đăng nhập và bắt đầu sử dụng nền tảng Coaching ngay bây giờ.</p>
    //                        <hr style='margin: 20px 0;' />
    //                        <p style='font-size: 14px; color: #555;'>Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ bộ phận hỗ trợ.</p>
    //                        <p style='font-size: 14px; color: #888;'>Trân trọng,<br />Đội ngũ Coaching</p>
    //                    "
    //					});

    //					return true;
    //				}
    //			}
    //		}

    //		return false;
    //	}

    //	public string CreateOTP()
    //	{
    //		const string digits = "0123456789";
    //		var random = new Random();
    //		return new string(Enumerable.Repeat(digits, 6)
    //			.Select(s => s[random.Next(s.Length)]).ToArray());
    //	}

    //	public async Task NotifyAdminNewCoachAsync(CoachProfileDTO coach)
    //	{
    //		var adminEmail = _config["AdminSettings:Email"];
    //		if (string.IsNullOrWhiteSpace(adminEmail))
    //			throw new Exception("Admin email not configured.");

    //		string adminHtml = $@"
    //    <div style='font-family:Segoe UI,Roboto,sans-serif;max-width:700px;margin:0 auto;background:#fff;padding:24px;border-radius:10px;border:1px solid #ddd'>
    //        <h2 style='color:#2d6cdf;'>📢 Một Coach mới đã đăng ký!</h2>
    //        <p>Thông tin chi tiết:</p>
    //<table style=""width:50%; border-collapse:collapse; margin-top:16px; font-size:15px; line-height:1.6; color:#2d6cdf;"">
    //    <tr><td><strong>👤 Họ tên:</strong></td><td>{coach.FullName}</td></tr>
    //    <tr><td><strong>📧 Email:</strong></td><td>{coach.EmailAddress}</td></tr>
    //    <tr><td><strong>📱 Số điện thoại:</strong></td><td>{coach.ContactPhone ?? "(chưa cung cấp)"}</td></tr>
    //    <tr><td><strong>🌐 Username:</strong></td><td>{coach.Username}</td></tr>
    //</table>


    //        <h4 style='margin-top:24px;color:#2d6cdf;'>💳 Ảnh thanh toán</h4>
    //        <div style='margin-top:8px'>
    //            {(string.IsNullOrEmpty(coach.PaymentImg) ? "<p>(Chưa có)</p>" : $"<img src='{coach.PaymentImg}' style='max-width:100%;height:auto;border:1px solid #ccc;border-radius:6px' />")}
    //        </div>

    //        <hr style='margin:24px 0' />
    //        <p style='font-size:13px;color:#888'>Email này được gửi từ hệ thống Coaching Platform.</p>
    //        <p style='font-size:13px;color:#888'>Vui lòng không trả lời email này.</p>
    //    </div>";

    //		await SendEmailAsync(new SendEmailRequest
    //		{
    //			ToEmail = adminEmail,
    //			Subject = "🚀 Huấn luyện viên mới vừa đăng ký",
    //			UserName = "Admin",
    //			Content = adminHtml
    //		});

    //		string coachHtml = $@"
    //    <div style='font-family:Segoe UI,Roboto,sans-serif;max-width:700px;margin:0 auto;background:#fff;padding:24px;border-radius:10px;border:1px solid #ddd'>
    //        <h2 style='color:#2d6cdf;'>🎉 Chào mừng {coach.FullName}!</h2>
    //        <p>Bạn đã <strong>đăng ký thành công</strong> để trở thành huấn luyện viên trên nền tảng <strong>Coaching</strong>.</p>

    //        <p>Chúng tôi sẽ xem xét thông tin và xác thực trong thời gian sớm nhất. Sau khi được duyệt, bạn có thể bắt đầu chia sẻ kiến thức và tạo gói huấn luyện cho khách hàng.</p>

    //        <h4 style='margin-top:24px;color:#2d6cdf;'>🧾 Thông tin đăng ký</h4>
    //<table style=""width:50%; border-collapse:collapse; margin-top:16px; font-size:15px; line-height:1.6; color:#2d6cdf;"">
    //<tr><td><strong>👤 Email:</strong></td><td>{coach.EmailAddress}</td></tr>
    //    <tr><td><strong>Ngày đăng ký:</strong></td><td>{DateTime.Now:dd/MM/yyyy HH:mm}</td></tr>
    //</table>

    //        {(string.IsNullOrEmpty(coach.PaymentImg) ? "" : $@"
    //        <h4 style='margin-top:24px;color:#2d6cdf;'>💳 Ảnh thanh toán bạn đã gửi</h4>
    //        <img src='{coach.PaymentImg}' style='max-width:100%;border-radius:6px;border:1px solid #ccc;margin-top:8px' />
    //        ")}

    //        <hr style='margin:24px 0' />
    //        <p style='font-size:14px;color:#888'>Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi qua email: <strong>support@coaching.vn</strong></p>
    //        <p style='font-size:13px;color:#aaa'>Trân trọng,<br/>Đội ngũ Coaching</p>
    //    </div>";

    //		await SendEmailAsync(new SendEmailRequest
    //		{
    //			ToEmail = coach.EmailAddress,
    //			Subject = "🎓 Xác nhận đăng ký Huấn luyện viên thành công",
    //			UserName = coach.FullName,
    //			Content = coachHtml
    //		});
    //	}

    //	public async Task SendCoachApprovedEmailAsync(CoachProfileDTO coach)
    //	{
    //		string html = $@"
    //<div style='font-family:Segoe UI,Roboto,sans-serif;max-width:700px;margin:0 auto;background:#fff;padding:24px;border-radius:10px;border:1px solid #ddd'>
    //    <h2 style='color:#2d6cdf;'>🎉 Chúc mừng {coach.FullName}!</h2>
    //    <p>Tài khoản huấn luyện viên của bạn đã được <strong style='color:green;'>phê duyệt</strong>.</p>

    //    <table style='width:50%; border-collapse:collapse; margin-top:16px; font-size:15px; line-height:1.6; color:#2d6cdf;'>
    //        <tr><td><strong>👤 Họ tên:</strong></td><td>{coach.FullName}</td></tr>
    //        <tr><td><strong>🌐 Username:</strong></td><td>{coach.Username}</td></tr>
    //        <tr><td><strong>📧 Email:</strong></td><td>{coach.EmailAddress}</td></tr>
    //    </table>

    //    <p style='margin-top:24px'>Bạn đã có thể đăng nhập và sử dụng các tính năng trên nền tảng Coaching.</p>

    //    <hr style='margin:24px 0;' />
    //    <p style='font-size:13px;color:#888'>Email được gửi từ hệ thống Coaching Platform. Vui lòng không phản hồi.</p>
    //</div>";

    //		await SendEmailAsync(new SendEmailRequest
    //		{
    //			ToEmail = coach.EmailAddress,
    //			Subject = "✅ Tài khoản huấn luyện viên của bạn đã được phê duyệt",
    //			UserName = coach.FullName,
    //			Content = html
    //		});
    //	}

    //	public async Task SendOTPChangePasswordAsync(string email)
    //	{
    //		string otp = CreateOTP();

    //		var coaches = await _coachProfileService.GetAllAsync();
    //		var coach = coaches.FirstOrDefault(x =>
    //			x.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));

    //		if (coach == null)
    //		{
    //			throw new Exception("Email không tồn tại trong hệ thống.");
    //		}

    //		string htmlContent = $@"
    //	<p>Xin chào <strong>{coach.FullName}</strong>,</p>
    //	<p>Bạn đã yêu cầu <strong>đặt lại mật khẩu</strong> cho tài khoản Coaching.</p>
    //	<p>Mã OTP của bạn là:</p>
    //	<div style='font-size: 28px; font-weight: bold; color: #2d6cdf; padding: 12px 0;'>{otp}</div>
    //	<p>Mã OTP này sẽ hết hạn trong <strong>5 phút</strong>.</p>
    //	<hr style='margin: 20px 0;' />
    //	<p style='font-size: 13px; color: #888;'>Nếu bạn không yêu cầu thay đổi mật khẩu, hãy bỏ qua email này.</p>
    //	<p style='font-size: 13px; color: #888;'>Trân trọng,<br />Đội ngũ Coaching</p>";

    //		await SendEmailAsync(new SendEmailRequest
    //		{
    //			ToEmail = email,
    //			Subject = "🔐 Mã OTP khôi phục mật khẩu",
    //			UserName = coach.FullName,
    //			Content = htmlContent
    //		});

    //		_cache.Set($"reset-otp:{email}", otp, TimeSpan.FromMinutes(5));
    //	}

    public bool ValidateResetPasswordOTP(string email, string otp)
	{
		if (_cache.TryGetValue($"reset-otp:{email}", out string storedOtp))
		{
			if (storedOtp == otp)
			{
				_cache.Remove($"reset-otp:{email}");
				return true;
			}
		}
		return false;
	}


}
