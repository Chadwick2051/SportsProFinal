using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Models.DataAccess.Configuration
{
	internal class IncidentConfig : IEntityTypeConfiguration<Incident>
	{
		public void Configure(EntityTypeBuilder<Incident> entity)
		{
			// seed initial data
			entity.HasData(
				new Incident { IncidentID = 1, CustomerID = 5, ProductID = 2, Title = "Error launching program", Description = "Program fails with error code 510, unable to open database.", TechnicianID = 4, DateOpened = DateTime.Now },
				new Incident { IncidentID = 2, CustomerID = 2, ProductID = 7, Title = "Could not install", Description = "Program fails with error code 510, unable to open database.", TechnicianID = 1, DateOpened = DateTime.Now },
				new Incident { IncidentID = 3, CustomerID = 3, ProductID = 1, Title = "Error launching program", Description = "Program fails with error code 510, unable to open database.", TechnicianID = 3, DateOpened = DateTime.Now },
				new Incident { IncidentID = 4, CustomerID = 5, ProductID = 4, Title = "Program keeps crashing", Description = "Program fails with error code 510, unable to open database.", TechnicianID = 2, DateOpened = DateTime.Now }
			);
		}
	}
}
