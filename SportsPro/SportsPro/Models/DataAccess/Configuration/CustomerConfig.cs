using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Models.DataAccess.Configuration
{
	internal class CustomerConfig : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> entity)
		{
			// seed initial data
			entity.HasData(
				new Customer { CustomerID = 1, FirstName = "Kaitlyn", LastName = "Anthoni", Address = "123 Spooner Street", City = "San Francisco", State = "California", PostalCode = "63141", CountryID = "A", Email = "kanthoni@pge.com", Phone = "970-331-1691" },
				new Customer { CustomerID = 2, FirstName = "Ania", LastName = "Irvin", Address = "123 Spooner Street", City = "Washington", State = "California", PostalCode = "63141", CountryID = "A", Email = "ania@mma.nidc.com", Phone = "970-331-1691" },
				new Customer { CustomerID = 3, FirstName = "Gonzalo", LastName = "Keeton", Address = "123 Spooner Street", City = "Mission Viejo", State = "California", PostalCode = "63141", CountryID = "B", Phone = "970-331-1691" },
				new Customer { CustomerID = 4, FirstName = "Anton", LastName = "Mauro", Address = "123 Spooner Street", City = "Sacramento", State = "California", PostalCode = "63141", CountryID = "A", Email = "amauro@yahoo.org", Phone = "970-331-1691" },
				new Customer { CustomerID = 5, FirstName = "Kendall", LastName = "Mayte", Address = "123 Spooner Street", City = "Fresno", State = "California", PostalCode = "63141", CountryID = "A", Email = "kmayte@fresno.ca.gov", Phone = "970-331-1691" },
				new Customer { CustomerID = 6, FirstName = "Kenzie", LastName = "Quinn", Address = "123 Spooner Street", City = "Los Angeles", State = "California", PostalCode = "63141", CountryID = "A", Email = "kenzie@mma.jobtrak.com", Phone = "970-331-1691" },
				new Customer { CustomerID = 7, FirstName = "Marvin", LastName = "Quintin", Address = "123 Spooner Street", City = "Fresno", State = "California", PostalCode = "63141", CountryID = "A", Email = "marvin@expedata.com" }
			);
		}
	}
}
