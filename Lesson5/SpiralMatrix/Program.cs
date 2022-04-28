using System;

namespace SpiralMatrix
{
	class Program
	{
		static int[,] MakeSpiralMatrix(int size)
		{
			var a = new int[size, size];
			int n = 0, i, j;
			for (i=0; i < size/2; i++)  // Цикл по сужающимся к середине квадратам
			{
				for (j=i; j<size-1-i; j++) a[i, j] = ++n;          // Верхняя сторона
				for (j=i; j<size-1-i; j++) a[j, size-1-i] = ++n;   // Правая сторона
				for (j=size-1-i; j>i; j--) a[size-1-i, j] = ++n;   // Нижняя сторона
				for (j=size-1-i; j>i; j--) a[j, i] = ++n;          // Левая сторона
			}
			if ((size % 2) == 1) a[size/2, size/2] = ++n;     // Если размер был нечетным - добавляем элемент в середину
			System.Diagnostics.Debug.Assert(n == size*size);  // Проверим, что правильно записано size*size чисел
			return a;
		}

		static void Main(string[] args)
		{
			int size;
			while (true)
			{
				Console.Write("Введите размер матрицы: ");
				try
				{
					size = Int32.Parse(Console.ReadLine());
					if (size > 0) break;
				} catch (Exception) { }
				Console.WriteLine("Размер матрицы должен быть положительным числом.");
			}
			var a = MakeSpiralMatrix(size);
			for (int i = 0; i<size; i++)
			{
				for (int j = 0; j<size; j++) Console.Write($"{a[i, j],6}");
				Console.WriteLine();
			}
			Console.Write("Нажмите любую клавишу для выхода..."); Console.ReadKey(true);
		}
	}
}
