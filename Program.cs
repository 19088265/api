using Architecture.Factory;
using Architecture.Models;
using Architecture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// 1. DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("db")));

// 2. Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//
//

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ElevatedRights", policy =>
        policy.RequireRole(Role.Admin));
    options.AddPolicy("ProgramRights", policy =>
        policy.RequireRole(Role.Admin, Role.User));
    options.AddPolicy("EmployeeRights", policy =>
        policy.RequireRole(Role.Admin, Role.EmployeeSub));
});

// 3. Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// 4. Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });

//Register repos
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<DonationIRepository, DonationRepository>();
//builder.Services.AddScoped<ProgramIRepository, ProgramRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddScoped<BeneficiaryIRepository, BeneficiaryRepository>();
builder.Services.AddScoped<SponsorIRepository, SponsorRepository>();
builder.Services.AddScoped<CafeteriaIRepository, CafeteriaRepository>();
builder.Services.AddScoped<AttendanceIRepository, AttendanceRepository>();
builder.Services.AddScoped<SponsorTypeIRepository, SponsorTypeRepository>();
builder.Services.AddScoped<CafeteriaTypeIRepository, CafeteriaTypeRepository>();
builder.Services.AddScoped<ProvinceIRepository, ProvinceRepository>();
builder.Services.AddScoped<CityIRepository, CityRepository>();
builder.Services.AddScoped<DonationTypeIRepository, DonationTypeRepository>();
builder.Services.AddScoped<DonationIRepository, DonationRepository>();
builder.Services.AddScoped<AttendanceTypeIRepository, AttendanceTypeRepository>();
builder.Services.AddScoped<BookGenreIRepository, BookGenreRepository>();
builder.Services.AddScoped<BookStatusIRepository, BookStatusRepository>();
builder.Services.AddScoped<BookIRepository, BookRepository>();
builder.Services.AddScoped<ProgramIRepository, ProgramRepository>();
builder.Services.AddScoped<ScheduleIRepository, ScheduleRepository>();
builder.Services.AddScoped<PaymentIRepository, PaymentRepository>();
builder.Services.AddScoped<PaymentTypeIRepository, PaymentTypeRepository>();
builder.Services.AddScoped<InvoiceIRepository, InvoiceRepository>();
builder.Services.AddScoped<ItemIRepository, ItemRepository>();
builder.Services.AddScoped<SuburbIRepository, SuburbRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// 5. Swagger authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rose API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// 6. Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        b =>
        {
            b
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });



});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//7. Use CORS
app.UseCors("AllowAngularDevClient");
app.UseHttpsRedirection();

// 8. Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Add seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedManager.Seed(services);
}

app.Run();