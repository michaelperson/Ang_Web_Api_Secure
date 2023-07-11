using DAL;
using Entities;
using MappersTool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleApi.Models;
using Security;
using System.Text;

namespace SampleAPI.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private IConfiguration _configuration;
		private IGenericRepository<User> _repo;
		public AccountController(IGenericRepository<User> repo, IConfiguration configuration)
		{
			_configuration = configuration;
			_repo= repo;
		}

		/// <summary>
		/// Point de terminaisaon pour le login
		/// </summary>
		/// <param name="login">un <see cref="LoginModel"/> contenant les credentials</param>
		/// <returns>un <see cref="OkObjectResult"/> contenant le JWT Token en cas de succès et un <see cref="BadRequestObjectResult"/> dans le cas contraire</returns>
		[HttpPost]
		[AllowAnonymous]
		[Route("Login")]
		public IActionResult Login(LoginModel login)
		{
			if (!ModelState.IsValid)
				return BadRequest("Unable to login");

			//Hash password
			UserModel user = (_repo as UserRepository).GetByEmail(login.Email).ToApi();
			if(user == null) return new BadRequestObjectResult("Unable to login");
			string pass = user.Password.Split("|")[0];
			byte[] salt = Encoding.UTF8.GetBytes(user.Password.Split("|")[1]);
			PasswordTool pt = new PasswordTool();
			if (pt.VerifyPassword(login.Password, pass, salt))
			{
				string Token = TokenTool.GenerateToken(user, _configuration, new List<string> { ((RolesEnum)user.IdRole).ToString() });

				return new OkObjectResult(Token);
			}
			else
			{
				return new BadRequestObjectResult("Unable to login");
			}

		} 
	}
}
