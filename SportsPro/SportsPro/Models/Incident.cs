using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	public class Incident
	{
		public int IncidentID { get; set; }

		[Required]
		public int CustomerID { get; set; }
		[ValidateNever]
		public Customer Customer { get; set; }

		[Required]
		public int ProductID { get; set; }

		[ValidateNever]
		public Product Product { get; set; }

		public int? TechnicianID { get; set; }

		[ValidateNever]
		public Technician Technician { get; set; }

		[Required]
		public string Title { get; set; } = string.Empty;
		[Required]
		public string Description { get; set; } = string.Empty;


		[Required(ErrorMessage = "Please enter a date opened")]
		[DataType(DataType.Date)]
		public DateTime DateOpened { get; set; } = DateTime.Now;
		public DateTime? DateClosed { get; set; } = null;
	}
}
