using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleApi.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[JsonIgnore]
		public string Password { get; set; } 

		[Required]
		public int IdRole { get; set; }

		 
	}
}