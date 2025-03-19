using System.Reflection;
using System.Text;
using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Dtos.DepartmentDtos;
using Company_Expense_Tracker.Dtos.ExpenseDtos;
using Company_Expense_Tracker.Dtos.UserDtos;
using Company_Expense_Tracker.Dtos.WorkerDtos;
using Company_Expense_Tracker.Repositories.RegisterRepositories;
using Company_Expense_Tracker.Services.RegisterServices;
using Company_Expense_Tracker.Validators.DepartmentValidator;
using Company_Expense_Tracker.Validators.ExpenseValidator;
using Company_Expense_Tracker.Validators.UserValidator;
using Company_Expense_Tracker.Validators.WorkerValidator;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateDepartmentValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateDepartmentValidator>();

        fv.RegisterValidatorsFromAssemblyContaining<CreateExpenseValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateExpenseValidator>();

        fv.RegisterValidatorsFromAssemblyContaining<LoginUserValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<RegisterUserValidator>();

        fv.RegisterValidatorsFromAssemblyContaining<CreateWorkerValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateWorkerValidator>();
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
