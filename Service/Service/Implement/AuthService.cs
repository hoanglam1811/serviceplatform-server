using Repository.Entities;
using Repository.Repository.Interface;
using Repository.DTO;
using AutoMapper;
using MySqlX.XDevAPI;

namespace Service.Service.Implement;

public class AuthService
{
  private readonly IGenericRepository<User> _userRepository;
  private readonly CloudinaryService _cloudinaryService;
  private readonly IMapper _mapper;
    private readonly EmailService _emailService;
    
    public AuthService(
        IGenericRepository<User> userRepository,
        IMapper mapper,
        CloudinaryService cloudinaryService,
        EmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailService = emailService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<User?> LoginCustomerAsync(LoginDTO dto)
    {
        var customers = await _userRepository.GetAllAsync();
        var customer = customers.FirstOrDefault(x => x.Username == dto.Username );
            //&& x.Role == "Customer");
        if(customer == null || PasswordManager.VerifyPassword(dto.Password, customer.PasswordHashed) == false) 
            throw new Exception("Invalid email or password");

        return customer;
    }

	public async Task<User?> LoginServiceProviderAsync(LoginDTO dto)
	{
		var providers = await _userRepository.GetAllAsync();
		var provider = providers.FirstOrDefault(x => x.Username == dto.Username && x.Role == "Provider");

		if (provider == null || PasswordManager.VerifyPassword(dto.Password, provider.PasswordHashed) == false)
			throw new Exception("Invalid email or password");

		return provider;
	}

	public async Task<User?> LoginAdminAsync(LoginDTO dto)
	{
		var admins = await _userRepository.GetAllAsync();
		var admin = admins.FirstOrDefault(x => x.Username == dto.Username && x.Role == "Admin");

		if (admin == null || PasswordManager.VerifyPassword(dto.Password, admin.PasswordHashed) == false)
			throw new Exception("Invalid email or password");

		return admin;
	}

	public async Task<User?> RegisterCustomerAsync(RegisterDTO dto)
	{
		var emailExists = await _userRepository.GetFirstOrDefaultAsync(
			x => x.Email == dto.Email && x.Role == "Customer");
		if (emailExists != null)
			throw new Exception("Email already exists");
		var usernameExists = await _userRepository.GetFirstOrDefaultAsync(
			x => x.Username == dto.Username && x.Role == "Customer");
		if (usernameExists != null)
			throw new Exception("Username already exists");
		var newCustomer = _mapper.Map<User>(dto);
		newCustomer.PasswordHashed = PasswordManager.HashPassword(dto.Password);
		newCustomer.Status = "Pending";
		newCustomer.Role = "Customer";
		newCustomer.CreatedAt = DateTime.Now;
		newCustomer.UpdatedAt = DateTime.Now;

    var images = await _cloudinaryService.UploadImagesAsync(dto.NationalId);
    newCustomer.NationalId = string.Join(", ", images);

		return await _userRepository.AddAsync(newCustomer);
	}

	public async Task<User?> RegisterServiceProviderAsync(RegisterDTO dto)
	{
		var emailExists = await _userRepository.GetFirstOrDefaultAsync(
			x => x.Email == dto.Email && x.Role == "Provider");
		if (emailExists != null)
			throw new Exception("Email already exists");
		var usernameExists = await _userRepository.GetFirstOrDefaultAsync(
			x => x.Username == dto.Username && x.Role == "Provider");
		if (usernameExists != null)
			throw new Exception("Username already exists");
		var newProvider = _mapper.Map<User>(dto);
		newProvider.PasswordHashed = PasswordManager.HashPassword(dto.Password);
		newProvider.Status = "Pending";
		newProvider.Role = "Provider";
		newProvider.CreatedAt = DateTime.Now;
		newProvider.UpdatedAt = DateTime.Now;	

    var images = await _cloudinaryService.UploadImagesAsync(dto.NationalId);
    newProvider.NationalId = string.Join(", ", images);

		return await _userRepository.AddAsync(newProvider);
	}

	public async Task<List<UserDTO>> GetAllCustomersAsync()
	{
		var customers = await _userRepository.GetAllAsync();
		var customerEntities = customers
			.Where(u => u.Role == "Customer")
			.ToList();

		// map sang DTO
		return _mapper.Map<List<UserDTO>>(customerEntities);
	}

	public async Task<List<UserDTO>> GetAllServiceProvidersAsync()
	{
		var providers = await _userRepository.GetAllAsync();
		var providerEntities = providers
			.Where(u => u.Role == "Provider")
			.ToList();

		return _mapper.Map<List<UserDTO>>(providerEntities);
	}

	public async Task<UserDTO> UpdateUserStatusAsync(Guid userId, string newStatus)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user == null)
			throw new Exception("User not found");

		user.Status = newStatus;
		await _userRepository.UpdateAsync(user);

		// Gửi email notify
		string htmlContent = $@"
		<p>Xin chào <strong>{user.FullName}</strong>,</p>
		<p>Tài khoản ServiceHub của bạn đã được <strong>{newStatus}</strong>.</p>
		<p>Bạn có thể đăng nhập và bắt đầu sử dụng dịch vụ ngay.</p>
		<p style='margin-top: 16px;'>Chúc bạn một ngày làm việc hiệu quả 🌟</p>
		<hr style='margin: 20px 0;' />
		<p style='font-size: 13px; color: #888;'>Nếu bạn không thực hiện việc này, vui lòng liên hệ hỗ trợ ngay.</p>
		<p style='font-size: 13px; color: #888;'>Trân trọng,<br />Đội ngũ ServiceHub</p>";

		await _emailService.SendEmailAsync(new SendEmailRequest
		{
			ToEmail = user.Email,
			Subject = $"🔔 Tài khoản ServiceHub đã được {newStatus}",
			UserName = user.FullName,
			Content = htmlContent
		});

		if (string.IsNullOrWhiteSpace(user.Email))
			throw new Exception("User email is empty, cannot send email");

		if (string.IsNullOrWhiteSpace(htmlContent))
			throw new Exception("Email content is empty, cannot send email");

		return _mapper.Map<UserDTO>(user);
	}

	public async Task<UserDTO> RejectUserAsync(Guid userId, string reason)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		if (user == null)
			throw new Exception("User not found");

		user.Status = "Rejected";
		await _userRepository.UpdateAsync(user);

		// Gửi email notify
		string htmlContent = $@"
<p>Xin chào <strong>{user.FullName}</strong>,</p>
<p>Tài khoản ServiceHub của bạn đã bị <strong>Từ chối</strong> sau quá trình xác minh.</p>
<p><strong>Lý do:</strong> {reason}</p>
<p style='margin-top: 16px;'>Vui lòng kiểm tra lại thông tin và đăng ký lại nếu cần.</p>
<hr style='margin: 20px 0;' />
<p style='font-size: 13px; color: #888;'>Nếu có thắc mắc, vui lòng liên hệ đội ngũ hỗ trợ ServiceHub.</p>
<p style='font-size: 13px; color: #888;'>Trân trọng,<br />Đội ngũ ServiceHub</p>";

		await _emailService.SendEmailAsync(new SendEmailRequest
		{
			ToEmail = user.Email,
			Subject = "⚠️ Tài khoản ServiceHub bị từ chối",
			UserName = user.FullName,
			Content = htmlContent
		});

		return _mapper.Map<UserDTO>(user);
	}



	//public async Task SendResetPasswordOTPAsync(string email)
	//{
	//	await _emailService.SendOTPChangePasswordAsync(email);
	//}

	//public bool VerifyResetPasswordOTP(string email, string otp)
	//{
	//	return _emailService.ValidateResetPasswordOTP(email, otp);
	//}

	//public async Task ResetPasswordAsync(string email, string newPassword)
	//{
	//	var coach = (await _coachRepository.GetAllAsync())
	//		.FirstOrDefault(x => x.EmailAddress == email);

	//	if (coach == null)
	//		throw new Exception("Coach not found with given email");

	//	coach.PasswordHashed = PasswordManager.HashPassword(newPassword);
	//	await _coachRepository.UpdateAsync(coach);

	//	// ✅ Gửi email xác nhận
	//	string htmlContent = $@"
	//<p>Xin chào <strong>{coach.FullName}</strong>,</p>
	//<p>Bạn đã <strong>đổi mật khẩu thành công</strong> cho tài khoản Coaching.</p>
	//<p>Nếu đây là hành động của bạn, không cần làm gì thêm.</p>
	//<p style='margin-top: 16px;'>Chúc bạn một ngày làm việc may mắn và hiệu quả 🌟</p>
	//<hr style='margin: 20px 0;' />
	//<p style='font-size: 13px; color: #888;'>Nếu bạn không thực hiện việc thay đổi này, vui lòng liên hệ hỗ trợ ngay lập tức.</p>
	//<p style='font-size: 13px; color: #888;'>Trân trọng,<br />Đội ngũ Coaching</p>";

	//	await _emailService.SendEmailAsync(new SendEmailRequest
	//	{
	//		ToEmail = email,
	//		Subject = "✅ Đổi mật khẩu thành công",
	//		UserName = coach.FullName,
	//		Content = htmlContent
	//	});
	//}

}
