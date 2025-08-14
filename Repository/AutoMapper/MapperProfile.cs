using AutoMapper;
using Repository.DTO;
using Repository.Entities;

namespace Repository.AutoMapper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CoachingProgram, CoachingProgramDTO>().ReverseMap();
        CreateMap<CoachingProgram, CreateCoachingProgramDTO>().ReverseMap();
        CreateMap<CoachingProgram, UpdateCoachingProgramDTO>().ReverseMap();
        CreateMap<CoachingProgram, UpdateCoachingProgramPageDTO>().ReverseMap();

		CreateMap<CoachingPackage, CoachingPackageDTO>().ReverseMap();
		CreateMap<CoachingPackage, CreateCoachingPackageDTO>().ReverseMap();
		CreateMap<CoachingPackage, UpdateCoachingPackageDTO>().ReverseMap();

		CreateMap<Page, PageDTO>().ReverseMap();
		CreateMap<Page, CreatePageDTO>().ReverseMap();
		CreateMap<Page, UpdatePageDTO>().ReverseMap();

		CreateMap<PageContent, PageContentDTO>().ReverseMap();
		CreateMap<PageContent, CreatePageContentDTO>().ReverseMap();
		CreateMap<PageContent, UpdatePageContentDTO>().ReverseMap();

		CreateMap<TiktokLink, TiktokLinkDTO>().ReverseMap();
		CreateMap<TiktokLink, CreateTiktokLinkDTO>().ReverseMap();
		CreateMap<TiktokLink, UpdateTiktokLinkDTO>().ReverseMap();

		CreateMap<CustomerRegistration, CustomerRegistrationDTO>().ReverseMap();
		CreateMap<CustomerRegistration, CreateCustomerRegistrationDTO>().ReverseMap();
		CreateMap<CustomerRegistration, UpdateCustomerRegistrationDTO>().ReverseMap();

		CreateMap<Setting, SettingDTO>().ReverseMap();
		CreateMap<Setting, CreateSettingDTO>().ReverseMap();
		CreateMap<Setting, UpdateSettingDTO>().ReverseMap();

		CreateMap<CoachProfile, CoachProfileDTO>().ReverseMap();
        CreateMap<CoachProfile, CreateCoachProfileDTO>().ReverseMap();
        CreateMap<CoachProfile, UpdateCoachProfileDTO>().ReverseMap();
        CreateMap<RegisterDTO, CoachProfile>().ReverseMap();
        CreateMap<CreateCoachProfileRequestDTO, CoachProfile>().ReverseMap();
        CreateMap<UpdateCoachProfileRequestDTO, CoachProfile>().ReverseMap();

        CreateMap<Feedback, FeedbackDTO>().ReverseMap();
        CreateMap<Feedback, CreateFeedbackDTO>().ReverseMap();
        CreateMap<Feedback, UpdateFeedbackDTO>().ReverseMap();

		CreateMap<Client, ClientDTO>().ReverseMap();
		CreateMap<Client, CreateClientDTO>().ReverseMap();
		CreateMap<Client, UpdateClientDTO>().ReverseMap();

		CreateMap<ClientCalendar, ClientCalendarDTO>().ReverseMap();
		CreateMap<ClientCalendar, CreateClientCalendarDTO>().ReverseMap();
		CreateMap<ClientCalendar, UpdateClientCalendarDTO>().ReverseMap();

		CreateMap<ClientProgress, ClientProgressDTO>().ReverseMap();
		CreateMap<ClientProgress, CreateClientProgressDTO>().ReverseMap();
		CreateMap<ClientProgress, UpdateClientProgressDTO>().ReverseMap();

		CreateMap<Note, NoteDTO>().ReverseMap();
		CreateMap<Note, CreateNoteDTO>().ReverseMap();
		CreateMap<Note, UpdateNoteDTO>().ReverseMap();

		CreateMap<WhoAmIImage, WhoAmIImageDTO>().ReverseMap();
		CreateMap<WhoAmIImage, CreateWhoAmIImageDTO>().ReverseMap();
		CreateMap<WhoAmIImage, UpdateWhoAmIImageDTO>().ReverseMap();

        CreateMap<CoachTime, CoachTimeDTO>().ReverseMap();
		CreateMap<CoachTime, CreateCoachTimeDTO>().ReverseMap();
		CreateMap<CoachTime, UpdateCoachTimeDTO>().ReverseMap();

		CreateMap<Admin, AdminDTO>();

    CreateMap<Exercise, ExerciseDTO>().ReverseMap();
		CreateMap<Exercise, CreateExerciseDTO>().ReverseMap();
		CreateMap<Exercise, UpdateExerciseDTO>().ReverseMap();

    CreateMap<ExerciseHistory, ExerciseHistoryDTO>().ReverseMap();
		CreateMap<ExerciseHistory, CreateExerciseHistoryDTO>().ReverseMap();
		CreateMap<ExerciseHistory, UpdateExerciseHistoryDTO>().ReverseMap();

    //CreateMap<Plan, PlanDTO>().ReverseMap();
    CreateMap<Plan, PlanDTO>()
      .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src =>
            src.ExercisePlans != null ? src.ExercisePlans.Select(ep => ep.Exercise) : null ))
      .ReverseMap()
      .ForMember(dest => dest.ExercisePlans, opt => opt.MapFrom(src =>
            new List<ExercisePlan>() ));
		CreateMap<Plan, CreatePlanDTO>().ReverseMap();
		CreateMap<Plan, UpdatePlanDTO>().ReverseMap();

    CreateMap<Report, ReviewDTO>().ReverseMap();
		CreateMap<Report, CreateReportDTO>().ReverseMap();
		CreateMap<Report, UpdateReportDTO>().ReverseMap();


	}
}
