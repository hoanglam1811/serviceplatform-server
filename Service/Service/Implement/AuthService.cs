using Repository.Entities;
using Repository.Repository.Interface;
using Repository.DTO;
using AutoMapper;
using MySqlX.XDevAPI;

namespace Service.Service.Implement;

public class AuthService
{
  private readonly IGenericRepository<User> _userRepository;
  private readonly IMapper _mapper;
    private readonly EmailService _emailService;
    
    public AuthService(
        IGenericRepository<User> userRepository,
        IMapper mapper,
        EmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailService = emailService;
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
		newCustomer.Status = "Unverified";
		newCustomer.Role = "Customer";

		// 4. Lưu DB
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
		newProvider.Status = "Unverified";
		newProvider.Role = "Provider";
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
			.Where(u => u.Role == "ServiceProvider")
			.ToList();

		return _mapper.Map<List<UserDTO>>(providerEntities);
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
