using System;
using System.ComponentModel.DataAnnotations;

namespace JPMorganMessageProcessingApplication.Data
{
	public class SaleInfo
	{
		public string Description { get; set; }
		public int Qty { get; set; }

		[Required]
		public decimal Price { get; set; }

		public decimal Adjustment { get; set; }
	}

}
