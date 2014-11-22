using System;
using System.Collections.Generic;

namespace DevelopmentWithADot.EntityFrameworkFluen.Test
{

	public class Customer
	{
		public Customer()
		{
			this.Projects = new HashSet<Project>();
			this.Contact = new ContactInformation();
		}

		public Int32 CustomerId
		{
			get;
			set;
		}

		public virtual ContactInformation Contact
		{
			get;
			protected set;
		}

		//[Required]
		//[MaxLength(50)]
		public String Name
		{
			get;
			set;
		}

		public virtual ICollection<Project> Projects
		{
			get;
			protected set;
		}

		public override String ToString()
		{
			return (this.Name);
		}
	}
}
