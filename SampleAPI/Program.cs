using DAL;
using DB;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SampleContext>(
		options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddControllers();

//CORS
string _policyName = "AllPolicy";
builder.Services.AddCors(opt => 
{
	opt.AddPolicy(name: _policyName, b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//JWT
var ConfJwt =builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = ConfJwt["Issuer"],
			ValidAudience = ConfJwt["Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfJwt["Key"]))
		};
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//CORS
app.UseCors(_policyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
