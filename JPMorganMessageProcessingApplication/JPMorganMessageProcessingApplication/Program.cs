using System;
using System.Collections.Generic;
using System.IO;
using JPMorganMessageProcessingApplication.Core;
using JPMorganMessageProcessingApplication.Core.Interfaces;

namespace JPMorganMessageProcessingApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			int msgCounter = 0;
			Sale sale = new Sale(new SalesOperation());

			//Replace the path to the path where you have stored the file.
			string path = @"C:\Practise\JPMorganMessageProcessingApplication\JPMorganMessageProcessingApplication\jpTestFile.txt";
			foreach (string line in System.IO.File.ReadLines(path))
			{
				msgCounter++;
				sale.Process(msgCounter, line);
			}
		}
	}
}
