using System.Linq;
using JPMorganMessageProcessingApplication.Core.Interfaces;
using JPMorganMessageProcessingApplication.Data;
using Pluralize.NET.Core;

namespace JPMorganMessageProcessingApplication.Core
{
	public class SalesOperation : ISaleOperations
	{
		
		public void AddSales(string item, int ad)
		{
			var salesList = SalesList.Instance.GetSalesList();
			salesList?.Where(x=>x.Description == item).ToList().ForEach(x=>x.Adjustment += ad);
		}

		public void SubtractSales(string item, int ad)
		{
			var salesList = SalesList.Instance.GetSalesList();
			salesList?.Where(x => x.Description == item).ToList().ForEach(x =>x.Adjustment -= x.Price + x.Adjustment -ad <= 0 ? 0 : ad );
		}

		public void MultiplySales(string item, int adjAmt)
		{
			var salesList = SalesList.Instance.GetSalesList();
			salesList?.Where(x => x.Description == item).ToList().ForEach(x => x.Adjustment = adjAmt * x.Price);
		}

		public SaleInfo SalesInfo(string[] msgParts, Pluralizer pluralizer)
		{
			string itemName = pluralizer.Pluralize(msgParts[0]);
			SaleInfo salesInfo = new SaleInfo { Description = itemName, Qty = 1 };
			if (decimal.TryParse(msgParts[2].Replace("p", ""), out decimal price))
				salesInfo.Price = price;
			return salesInfo;
		}

		public SaleInfo SalesInfo(string[] msgParts)
		{
			SaleInfo salesInfo = new SaleInfo { Description = msgParts[3] };
			if (int.TryParse(msgParts.FirstOrDefault(), out int qty))
				salesInfo.Qty = qty;

			if (decimal.TryParse(msgParts[5].Replace("p", ""), out decimal price))
				salesInfo.Price = price;
			return salesInfo;
		}
	}
}
