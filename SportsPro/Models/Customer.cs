using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	public class Customer
	{
		public Customer() => Products = new HashSet<Product>();

		public int CustomerID { get; set; }

		[Required(ErrorMessage = "First Name Is Required")]
		[StringLength(50)]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Last Name Is Required")]
		[StringLength(50)]
		public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Required")]
		[StringLength(50)]
		public string Address { get; set; } = string.Empty;
		[Required(ErrorMessage = "Required")]
		[StringLength(50)]
		public string City { get; set; } = string.Empty;
		[Required(ErrorMessage = "Required")]
		[StringLength(50)]
		public string State { get; set; } = string.Empty;
		[Required(ErrorMessage = "Required")]
		[StringLength(20)]
		public string PostalCode { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please select a country")]
		public string CountryID { get; set; }
		[ValidateNever]
		public Country? Country { get; set; }

		public string Phone { get; set; } = string.Empty;

		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		[Remote("CheckEmail","Validation", AdditionalFields = "CustomerID")]
		public string Email { get; set; } = string.Empty;

		public ICollection<Product> Products { get; set; }

		public string FullName => FirstName + " " + LastName;

		public string Slug => FirstName?.Replace(' ','-').ToLower() + "-" + LastName?.Replace(' ', '-').ToLower();
	}
}
