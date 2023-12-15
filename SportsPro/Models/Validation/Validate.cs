using SportsPro.Models.DataAccess;

namespace SportsPro.Models.Validation
{
	public static class Validate
	{
		public static string CheckEmail(Repository<Customer> customerData, Customer customer)
		{
			var dbCust = customerData.Get(new QueryOptions<Customer>
			{
				Where = c => c.Email == customer.Email && c.CustomerID != customer.CustomerID
			});

			if (dbCust == null)
			{
				return "";
			}
			else
			{
				return $"{customer.Email} is already in the database";
			}
		}
	}
}
