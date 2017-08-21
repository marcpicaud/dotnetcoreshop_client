﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace netshop_client.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		
        [Required]
		public string Name { get; set; }
	}
}
