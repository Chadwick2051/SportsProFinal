using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.Models.DataAccess.Configuration
{
	internal class ProductConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> entity)
		{
			// seed initial data
			entity.HasData(
					new Product { ProductID = 1, ProductCode = "TRNY10", Name = "Tournament Master 1.0", YearlyPrice = 4.99M, ReleaseDate = DateTime.Today },
					new Product { ProductID = 2, ProductCode = "LEAG10", Name = "League Scheduler 1.0", YearlyPrice = 4.99M, ReleaseDate = DateTime.Today },
					new Product { ProductID = 3, ProductCode = "LEAGD10", Name = "League Scheduler Deluxe 1.0", YearlyPrice = 7.99M, ReleaseDate = DateTime.Today },
					new Product { ProductID = 4, ProductCode = "DRAFT10", Name = "Draft Manager 1.0", YearlyPrice = 4.99M, ReleaseDate = DateTime.Today },
					new Product { ProductID = 5, ProductCode = "TEAM10", Name = "Team Manager 1.0", YearlyPrice = 4.99M, ReleaseDate = DateTime.Today },
					new Product { ProductID = 6, ProductCode = "TRNY20", Name = "Tournament Master 2.0", YearlyPrice = 5.99M, ReleaseDate = DateTime.Today },
					new Product { ProductID = 7, ProductCode = "DRAFT20", Name = "Draft Manager 2.0", YearlyPrice = 5.99M, ReleaseDate = DateTime.Today }
				);
		}
	}
}
