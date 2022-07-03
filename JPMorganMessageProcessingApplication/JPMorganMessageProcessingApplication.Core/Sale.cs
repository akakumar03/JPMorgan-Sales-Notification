using System;
using System.Collections.Generic;
using System.Linq;
using JPMorganMessageProcessingApplication.Core.Interfaces;
using JPMorganMessageProcessingApplication.Data;
using Pluralize.NET.Core;

namespace JPMorganMessageProcessingApplication.Core
{
	public class Sale
	{
		private readonly ISaleOperations _saleOperations;
		public Sale(ISaleOperations saleOperations)
		{
			_saleOperations = saleOperations;
		}

		public void Process(int msgCounter, string msg)
		{
			Pluralizer pluralizer = new Pluralizer();
			string[] msgParts = msg.Split(' ');
			if (msg != "")
			{
				SalesMessages(msgCounter, msg, msgParts, pluralizer);
			}
		}

		private void SalesMessages(int msgCounter, string msg, string[] msgParts, Pluralizer pluralizer)
		{
			if (msg.ToLower().StartsWith("add") || msg.ToLower().StartsWith("subtract") || msg.ToLower().StartsWith("multiply"))
			{
				string operation = msgParts[0]?.ToLower();
				string item = pluralizer.Pluralize(msgParts[2]);
				if (decimal.TryParse(msgParts[1].Replace("p", ""), out _))
				{
					string adjAt = msgParts[1].Replace("p", "");
					int adjustedAmount = int.Parse(adjAt);
					switch (operation)
					{
						case "add":
							_saleOperations.AddSales(item,adjustedAmount);
							break;
						case "subtract":
							_saleOperations.SubtractSales(item, adjustedAmount);
							break;
						case "multiply":
							_saleOperations.MultiplySales(item, adjustedAmount);
							break;
					}
				}
			}
			else if (msgParts.Length == 3)
			{
				SaleInfo salesInfo = _saleOperations.SalesInfo(msgParts, pluralizer);
				SalesList.Instance.AddToSalesList(salesInfo);
			}
			else if (msgParts.Length > 4)
			{
				SaleInfo salesInfo = _saleOperations.SalesInfo(msgParts);
				SalesList.Instance.AddToSalesList(salesInfo);
			}

			if (msgCounter % 10 == 0)
			{
				Console.WriteLine("******Summary******");
				GetOverAllSize();
			}
			if (msgCounter % 50 == 0)
			{
				Console.WriteLine("Pausing the application as the message count has reached 50");
				GetOVerAllAdjustPrice();
				Environment.Exit(0);
			}
			
		}

		private void GetOverAllSize()
		{
			foreach (var item in SalesList.Instance.GetSalesList().GroupBy(x => x.Description))
			{
				Console.WriteLine($"{item.Sum(x => x.Qty)} {item.Key} at {item.Sum(x => x.Qty * (x.Price + x.Adjustment) )}p");
			}
		}

		private void GetOVerAllAdjustPrice()
		{
			foreach (var item in SalesList.Instance.GetSalesList())
			{
				Console.WriteLine($"The adjustment price for {item.Description} is : {item.Adjustment}p and the total final price is {item.Price + item.Adjustment}p");
			}
		}
	}
}
