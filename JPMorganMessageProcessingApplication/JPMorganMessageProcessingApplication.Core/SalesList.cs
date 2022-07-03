using System;
using System.Collections.Generic;
using System.Text;
using JPMorganMessageProcessingApplication.Data;

namespace JPMorganMessageProcessingApplication.Core
{
	public class SalesList
	{
		private static List<SaleInfo> _salesList;
		private SalesList() { _salesList = new List<SaleInfo>();}
		private static SalesList _instance;
		public static SalesList Instance
		{
			get { return _instance ??= new SalesList(); }
		}

		public List<SaleInfo> GetSalesList()
		{
			return _salesList;
		}

		public void AddToSalesList(SaleInfo saleInfo)
		{
			_salesList.Add(saleInfo);
		}
	}
}
