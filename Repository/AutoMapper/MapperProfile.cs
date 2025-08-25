using AutoMapper;
using Repository.DTO;
using Repository.Entities;

namespace Repository.AutoMapper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
		CreateMap<LoyaltyPoint, LoyaltyPointDTO>().ReverseMap();
		CreateMap<LoyaltyPoint, CreateLoyaltyPointDTO>().ReverseMap();
		CreateMap<LoyaltyPoint, UpdateLoyaltyPointDTO>().ReverseMap();

		CreateMap<Notification, NotificationDTO>().ReverseMap();
		CreateMap<Notification, CreateNotificationDTO>().ReverseMap();
		CreateMap<Notification, UpdateNotificationDTO>().ReverseMap();

		CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
		CreateMap<BankAccount, CreateBankAccountDTO>().ReverseMap();
		CreateMap<BankAccount, UpdateBankAccountDTO>().ReverseMap();

		CreateMap<Booking, BookingDTO>().ReverseMap();
		CreateMap<Booking, CreateBookingDTO>().ReverseMap();
		CreateMap<Booking, UpdateBookingDTO>().ReverseMap();

		CreateMap<Chat, ChatDTO>().ReverseMap();
		CreateMap<Chat, CreateChatDTO>().ReverseMap();
		CreateMap<Chat, UpdateChatDTO>().ReverseMap();

		CreateMap<Payment, PaymentDTO>().ReverseMap();
		CreateMap<Payment, CreatePaymentDTO>().ReverseMap();
		CreateMap<Payment, UpdatePaymentDTO>().ReverseMap();

		CreateMap<ProviderProfile, ProviderProfileDTO>().ReverseMap();
		CreateMap<ProviderProfile, CreateProviderProfileDTO>().ReverseMap();
		CreateMap<ProviderProfile, UpdateProviderProfileDTO>().ReverseMap();

		CreateMap<Review, ReviewDTO>().ReverseMap();
		CreateMap<Review, CreateReviewDTO>().ReverseMap();
		CreateMap<Review, UpdateReviewDTO>().ReverseMap();

		CreateMap<ServiceCategory, ServiceCategoryDTO>().ReverseMap();
		CreateMap<ServiceCategory, CreateServiceCategoryDTO>().ReverseMap();
		CreateMap<ServiceCategory, UpdateServiceCategoryDTO>().ReverseMap();

		CreateMap<Services, ServiceDTO>().ReverseMap();
		CreateMap<Services, CreateServiceDTO>().ReverseMap();
		CreateMap<Services, UpdateServiceDTO>().ReverseMap();

		CreateMap<Transaction, TransactionDTO>().ReverseMap();
		CreateMap<Transaction, CreateTransactionDTO>().ReverseMap();
		CreateMap<Transaction, UpdateTransactionDTO>().ReverseMap();

		CreateMap<User, UserDTO>().ReverseMap();
		CreateMap<User, CreateUserDTO>().ReverseMap();
		CreateMap<User, UpdateUserDTO>().ReverseMap();
		CreateMap<User, RegisterDTO>().ReverseMap();

		CreateMap<Wallet, WalletDTO>().ReverseMap();
		CreateMap<Wallet, CreateWalletDTO>().ReverseMap();
		CreateMap<Wallet, UpdateWalletDTO>().ReverseMap();

		////CreateMap<Plan, PlanDTO>().ReverseMap();
		//CreateMap<Plan, PlanDTO>()
  //    .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src =>
  //          src.ExercisePlans != null ? src.ExercisePlans.Select(ep => ep.Exercise) : null ))
  //    .ReverseMap()
  //    .ForMember(dest => dest.ExercisePlans, opt => opt.MapFrom(src =>
  //          new List<ExercisePlan>() ));
		//CreateMap<Plan, CreatePlanDTO>().ReverseMap();
		//CreateMap<Plan, UpdatePlanDTO>().ReverseMap();

  //  CreateMap<Report, ReviewDTO>().ReverseMap();
		//CreateMap<Report, CreateReportDTO>().ReverseMap();
		//CreateMap<Report, UpdateReportDTO>().ReverseMap();


	}
}
