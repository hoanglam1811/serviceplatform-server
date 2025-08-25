using API;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Data;
using Repository.Repository.Implement;
using Repository.Repository.Interface;
using System.Text;
using Service.Service.Implement;
using Service.Service.Interface;
using Repository.AutoMapper;
using Service;
using API.Middleware;
using API.Hubs;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service Solution API", Version = "V1" });

	// Add JWT Bearer scheme
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter only your JWT token. Example: `eyJhbGciOi...`"
	});

	// Add global security requirement
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "Bearer",
				Name = "Authorization",
				In = ParameterLocation.Header
			},
			Array.Empty<string>()
		}
	});
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<ServiceSolutionDbContext>(options =>
	options.UseMySQL(connectionString));

builder.Services.Configure<CloudinarySettings>(
	builder.Configuration.GetSection("CloudinarySettings"));

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtIssuer,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""))
	};

	options.Events = new JwtBearerEvents
	{
		OnMessageReceived = context =>
		{
			var accessToken = context.Request.Query["access_token"];
			var path = context.HttpContext.Request.Path;
			if (!string.IsNullOrEmpty(accessToken) &&
				path.StartsWithSegments("/chathub"))
			{
				context.Token = accessToken;
			}
      var token = context.HttpContext.Request.Cookies["auth_token"];
      if (!string.IsNullOrEmpty(token))
      {
        context.Token = token;
      }
			return Task.CompletedTask;
		},
		OnTokenValidated = context =>
		{
			var userId = context.Principal?.FindFirst("id")?.Value;
			if (!string.IsNullOrEmpty(userId))
			{
				context.Principal.AddIdentity(new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, userId)
				}));
			}
			return Task.CompletedTask;
		}
	};
});


builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		//policy.AllowAnyOrigin()
		policy.WithOrigins("http://localhost:3000")
			  //.AllowCredentials()
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials();
	});
});

builder.Services.AddSignalR();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IProviderProfileService, ProviderProfileService>();
builder.Services.AddScoped<ILoyaltyService, LoyaltyService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpClient<IPayOSService, PayOSService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<CloudinaryService>();

builder.Services.AddSingleton<IMapper>(sp =>
{
	var config = new MapperConfiguration(cfg =>
	{
		cfg.AddProfile<MapperProfile>();
	});

	return config.CreateMapper();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Solution API V1");
	//options.RoutePrefix = "swagger"; 
	options.ConfigObject.DeepLinking = true;
});

app.UseCors("AllowFrontend");
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtCookieMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
