using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Models.DataAccess.Configuration
{
	internal class CountryConfig : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> entity)
		{
			// seed initial data
			entity.HasData(
				new Country { CountryID = "A", Name = "United States" },
				new Country { CountryID = "B", Name = "Mexico" },
				new Country { CountryID = "C", Name = "Canada" },
				new Country { CountryID = "D", Name = "United Kingdom" },
				new Country { CountryID = "E", Name = "Australia" }
			);
		}
	}
}
