using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Models.DataAccess.Configuration
{
	internal class TechnicianConfig : IEntityTypeConfiguration<Technician>
	{
		public void Configure(EntityTypeBuilder<Technician> entity)
		{
			// seed initial data
			entity.HasData(
				new Technician { TechnicianID = 1, Name = "Alison Diaz", Email = "alison@sportsprosoftware.com", Phone = "800-555-0443" },
				new Technician { TechnicianID = 2, Name = "Andrew Wilson", Email = "awilson@sportsprosoftware.com", Phone = "800-555-0449" },
				new Technician { TechnicianID = 3, Name = "Gina Fiori", Email = "gfiori@sportsprosoftware.com", Phone = "800-555-0459" },
				new Technician { TechnicianID = 4, Name = "Gunter Wendt", Email = "gunter@sportsprosoftware.com", Phone = "800-555-0400" },
				new Technician { TechnicianID = 5, Name = "Jason Lee", Email = "jason@sportsprosoftware.com", Phone = "800-555-0444" },
				new Technician { TechnicianID = -1, Name = "", Email = "", Phone = "" }
			);
		}
	}
}
