using System;
using System.ComponentModel.DataAnnotations;

namespace DevelopmentWithADot.EntityFrameworkFluen.Test
{
	public class ProjectResource
	{
		public Int32 ProjectResourceId
		{
			get;
			set;
		}

		[Required]
		public virtual Project Project
		{
			get;
			set;
		}

		[Required]
		public virtual Resource Resource
		{
			get;
			set;
		}

		public Role Role
		{
			get;
			set;
		}
	}
}
