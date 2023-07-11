﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Models
{
	public class RolesModel
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string Nom { get; set; }
	}
}
