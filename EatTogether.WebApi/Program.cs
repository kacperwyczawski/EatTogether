using EatTogether.Application.Common.Interfaces.Authentication;
using EatTogether.Application.Common.Interfaces.Persistence;
using EatTogether.Infrastructure.Authentication;
using EatTogether.Infrastructure.Persistent;
using Mapster;
using MediatR;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();