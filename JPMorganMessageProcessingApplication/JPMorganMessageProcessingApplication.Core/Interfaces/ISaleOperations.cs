using System;
using System.Collections.Generic;
using System.Text;
using JPMorganMessageProcessingApplication.Data;
using Pluralize.NET.Core;

namespace JPMorganMessageProcessingApplication.Core.Interfaces
{
	 public interface ISaleOperations
	 {
		 void AddSales(string item,int ad);
		 void SubtractSales(string item, int ad);
		 void MultiplySales(string item, int ad);
		 SaleInfo SalesInfo(string[] msgParts, Pluralizer pluralizer);
		 SaleInfo SalesInfo(string[] msgParts);
	 }
}
