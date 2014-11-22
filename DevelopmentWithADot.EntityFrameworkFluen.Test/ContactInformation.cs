using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevelopmentWithADot.EntityFrameworkFluen.Test
{
	[ComplexType]
	public class ContactInformation
	{
		[Required]
		[MaxLength(50)]
		public String Email
		{
			get;
			set;
		}

		[MaxLength(20)]
		public String Phone
		{
			get;
			set;
		}

		public override String ToString()
		{
			return (String.Format("Email={0}, Phone={1}", this.Email, this.Phone));
		}
	}
}
