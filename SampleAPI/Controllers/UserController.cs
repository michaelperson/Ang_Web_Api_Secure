using DAL;
using DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IGenericRepository<User> _repo;

		public UserController(IGenericRepository<User> repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_repo.GetAll().Select(u=> new { u.Lastname, u.Firstname, u.Email, u.Id }));
		}

		[HttpPost]
		public IActionResult Post(User user)
		{
			try
			{
				_repo.Insert(user);
				_repo.Save();
				return Created($"/api/user/{user.Id}", user);
	}
			catch (Exception ex)
			{
				return BadRequest(user);
			}
		}
	}
}
