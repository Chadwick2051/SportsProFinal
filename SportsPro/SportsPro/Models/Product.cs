using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
	public class Product
	{
		public Product() => Customers = new HashSet<Customer>();

		public int ProductID { get; set; }

		[Required]
		public string ProductCode { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;

		[Range(0, 100000)]
		[Column(TypeName = "decimal(8,2)")]
		public decimal YearlyPrice { get; set; } = 0.00M;

		public DateTime ReleaseDate { get; set; } = DateTime.Now;

		public ICollection<Customer> Customers { get; set; }

		public string Slug()
		{
			return Name.Replace(' ','-').ToLower();
		}
	}
}
