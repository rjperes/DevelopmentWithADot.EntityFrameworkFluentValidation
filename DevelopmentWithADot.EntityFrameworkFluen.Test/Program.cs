using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevelopmentWithADot.EntityFrameworkFluentValidation;

namespace DevelopmentWithADot.EntityFrameworkFluen.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var ctx = new ProjectsContext())
			{
				ctx.AddEntityValidation(x => ctx.Customers, x => x.Name.Length > 3, "Invalid name");



				var customers = ctx.Customers.ToList();
			}
		}
	}
}
