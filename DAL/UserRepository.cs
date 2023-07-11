using DB;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class UserRepository : GenericRepository<User>
	{
		public UserRepository(SampleContext _context) : base(_context)
		{
		}

		public User? GetByEmail(string email)
		{
			return table.FirstOrDefault(t => t.Email == email);
		}
	}
}
