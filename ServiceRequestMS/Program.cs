using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FluentValidation.AspNetCore;
using ServiceRequestMS.Api.Controllers;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.Services;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.Application.Validators;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Data;
using ServiceRequestMS.Data.Repositories;
using ServiceRequestMS.Data.Repositories.Interfaces;
using ServiceRequestMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(
                entry => entry.Key,
                entry => entry.Value!.Errors.Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                    ? "The field is invalid."
                    : error.ErrorMessage).ToArray());

        return new BadRequestObjectResult(new
        {
            success = false,
            message = "Validation failed",
            data = (object?)null,
            errors = errors
        });
    };
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<UserRegistraionDto>, UserRegistrationDtoValidator>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();
builder.Services.AddScoped<IValidator<ItemDto>, ItemDtoValidator>();
builder.Services.AddScoped<IValidator<CreateRequestDto>, CreateRequestDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateEmployeeRequestDto>, UpdateEmployeeRequestDtoValidator>();
builder.Services.AddScoped<IValidator<CreateCommentDto>, CreateCommentDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection(DatabaseOptions.SectionName));

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
        ValidateIssuerSigningKey = true
    });
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy => {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAngular");

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