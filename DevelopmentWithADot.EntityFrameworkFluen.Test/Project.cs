using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevelopmentWithADot.EntityFrameworkFluen.Test
{
	public partial class Project
	{
		public Project()
		{
			this.ProjectResources = new HashSet<ProjectResource>();
		}

		public Int32 ProjectId
		{
			get;
			set;
		}

		[Required]
		[MaxLength(50)]
		[ConcurrencyCheck]
		public String Name
		{
			get;
			set;
		}

		public DateTime Start
		{
			get;
			set;
		}

		public DateTime? End
		{
			get;
			set;
		}

		public virtual ProjectDetail Detail
		{
			get;
			set;
		}

		[Required]
		public virtual Customer Customer
		{
			get;
			set;
		}

		public ProjectStatus Status
		{
			get;
			set;
		}

		public void AddResource(Resource resource, Role role)
		{
			this.ProjectResources.Add(new ProjectResource() { Project = this, Resource = resource, Role = role });
			resource.ProjectResources.Add(new ProjectResource() { Project = this, Resource = resource, Role = role });
		}

		public Resource ProjectManager
		{
			get
			{
				return (this.ProjectResources.ToList().Where(x => x.Role == Role.ProjectManager).Select(x => x.Resource).SingleOrDefault());
			}
		}

		public IEnumerable<Resource> Developers
		{
			get
			{
				return (this.ProjectResources.Where(x => x.Role == Role.Developer).Select(x => x.Resource).ToList());
			}
		}

		public IEnumerable<Resource> Testers
		{
			get
			{
				return (this.ProjectResources.Where(x => x.Role == Role.Tester).Select(x => x.Resource)).ToList();
			}
		}

		public virtual ICollection<ProjectResource> ProjectResources
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
