using Entities;
using SampleApi.Models;

namespace MappersTool
{
	public static class Mapper
	{
		public static UserModel ToApi(this User bu)
		{
			if (bu == null) return null;
			return new UserModel
			{
				Id = bu.Id,
				Email = bu.Email,
				LastName = bu.Lastname,
				FirstName = bu.Firstname,
				Password = bu.Password, 

				IdRole = bu.IdRole
			};
		}

		public static User ToBLL(this UserModel u)
		{
			if (u == null) return null;
			return new User
			{
				Id = u.Id,
				Email = u.Email,
				Lastname = u.LastName,
				Firstname = u.FirstName,
				Password = u.Password, 
				IdRole = u.IdRole
			};
		}
	}
}