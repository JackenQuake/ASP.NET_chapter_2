using System;
using System.Text.RegularExpressions;

namespace TaskAdvanced
{
	class Program
	{
		static void Main(string[] args)
		{
			string str = " Предложение один  Теперь предложение два     Предложение три    ";
			RegexOptions options = RegexOptions.None;
			Regex regex = new Regex(@"\s{2,}", options);
			string res = regex.Replace(str, ".\n").Trim();
			
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Исходный вариант:\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(str);
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Преобразованный вариант:\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(res);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine(); Console.Write("Press any key to exit..."); Console.ReadKey(true);
			
		}
	}
}
