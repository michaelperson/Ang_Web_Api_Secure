using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DB.configs
{
	internal class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.UseIdentityColumn(1).ValueGeneratedOnAdd();
			builder.Property(x => x.Firstname).IsRequired();
			builder.Property(x => x.Lastname).IsRequired();
			builder.Property(x => x.Email).IsRequired();
			builder
			.ToTable(b => b.HasCheckConstraint("CK_Email", "[Email] LIKE '__%@__%.__%'")).HasIndex(x=>x.Email).IsUnique();

			builder.Property(x => x.Password).HasConversion
				(v => GenerateHashAndSalt(v),
				v => v.ToString());

		}

		private string GenerateHashAndSalt(string v)
		{
			PasswordTool pt = new PasswordTool();
			string pass = pt.HashPasword(v.ToString(), out byte[] salt);
			return $"{pass}|{Convert.ToBase64String(salt)}";
		}

		 
	}
}
