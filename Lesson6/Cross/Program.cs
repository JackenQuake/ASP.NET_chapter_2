using System;

namespace Cross
{
	class Program
	{
		static int SIZE_X = 5;
		static int SIZE_Y = 5;
		static int WIN_BLOCK = 4;  // Длина блока, требуемого для победы

		static char[,] field = new char[SIZE_Y, SIZE_X];

		static char PLAYER_DOT = 'X';
		static char AI_DOT = '0';
		static char EMPTY_DOT = '.';

		static Random rand = new Random();

		private static void initField()
		{
			for (int i = 0; i < SIZE_Y; i++)
				for (int j = 0; j<SIZE_X; j++)
					field[i, j] = EMPTY_DOT;
		}

		private static void printField()
		{
			int i, j;
			for (i = 0; i<2*SIZE_X+1; i++)  Console.Write("-");
			Console.WriteLine();
			for (i = 0; i<SIZE_Y; i++)
			{
				Console.Write("|");
				for (j = 0; j<SIZE_X; j++)
					Console.Write(field[i, j]+"|");
				Console.WriteLine();
			}
			for (i = 0; i<2*SIZE_X+1; i++) Console.Write("-");
			Console.WriteLine();
		}

		private static void setSym(int y, int x, char sym)
		{
			field[y, x] = sym;
		}

		private static int inputNumber(string msg)
		{
			while (true)
			{
				Console.Write(msg);
				try
				{
					return Int32.Parse(Console.ReadLine());
				} catch (Exception)
				{
					Console.WriteLine("Нужно ввести число, попробуйте еще раз.");
				}
			}
		}

		private static void playerStep()
		{
			int x, y;
			while (true)
			{

				x = inputNumber($"Введите координату X (1..{SIZE_X}) : ") - 1;
				y = inputNumber($"Введите координату Y (1..{SIZE_X}) : ") - 1;
				if (isCellValid(y, x)) break;
				Console.WriteLine("Это недопустимый ход, попробуйте еще раз.");
			}
			setSym(y, x, PLAYER_DOT);
		}

		// Размер максимального блока для игрока и сколько блоков такого размера существует в таблице
		private static int blockSize, blockCount;

		// Вспомогательная функция для подсчета блоков: ищет блоки на линии из точки (x,y) в направлении (dx,dy)
		private static void countLine(int x, int y, int dx, int dy, char sym)
		{
			int len = 0;  // Длина текущего найденного блока
			while (true)
			{
				if ((x < 0) || (y < 0) || (x >= SIZE_X) || (y >= SIZE_Y)) return;
				if (field[y, x] == sym)
				{
					len++;
					if (len == blockSize) blockCount++;
					if (len > blockSize) { blockSize = len; blockCount = 1; }
				} else len = 0;
				x += dx; y += dy;
			}
		}

		// Вычисляет длину максимального блока, построенного игроком, и количество таких блоков
		private static void countBlocks(char sym)
		{
			int i;
			blockSize = 0; blockCount = 0;
			// Горизонтали
			for (i = 0; i < SIZE_Y; i++) countLine(0, i, 1, 0, sym);
			// Вертикали
			for (i = 0; i < SIZE_X; i++) countLine(i, 0, 0, 1, sym);
			// Диагонали																		
			for (i = 0; i < SIZE_Y; i++)
			{
				countLine(0, i, 1, 1, sym);
				countLine(0, i, 1, -1, sym);
			}
			for (i = 1; i < SIZE_X; i++)
			{
				countLine(i, 0, 1, 1, sym);
				countLine(i, SIZE_Y-1, 1, -1, sym);
			}
		}

		// Условие победы - блок имеет заданный размер
		private static bool checkWin(char sym)
		{
			countBlocks(sym);  return (blockSize >= WIN_BLOCK);
		}

		// Максимальный размер блока, который можно получить следующим ходом
		static int aiSize;
		 // Массив, в котором подсчитывается, сколько блоков максимального размера позволяет создать ход в данную клетку
		static int[,] aiBrain = new int[SIZE_Y, SIZE_X];

		// Очищает "мозг"
		private static void aiClear()
		{
			for (int y = 0; y<SIZE_Y; y++)
				for (int x = 0; x<SIZE_X; x++)
					aiBrain[y, x] = 0;
		}

		// Пробует все возможные ходы игрока sym и определяет результат
		private static void aiAnalyze(char sym)
		{
			aiClear(); aiSize = 0;
			for (int y = 0; y<SIZE_Y; y++)
				for (int x = 0; x<SIZE_X; x++)
				{
					if (field[y, x] != EMPTY_DOT) continue;
					field[y, x] = sym;        // Временно пробуем пойти в данную клетку
					countBlocks(sym);         // Смотрим, что получилось по блокам
					field[y, x] = EMPTY_DOT;  // Восстанавливаем клетку пустой
					if (blockSize > aiSize) { aiClear(); aiSize = blockSize; }
					if (blockSize == aiSize) aiBrain[y, x] = blockCount;
				}
		}

		private static void aiStep()
		{
			// Сначала смотрим, может ли выиграть компьютер
			aiAnalyze(AI_DOT);
			// Если выиграть нельзя - смотрим, что своим ходом может добиться человек
			if (aiSize < WIN_BLOCK) {
				aiAnalyze(PLAYER_DOT);
				// Если блокировать пока не надо - делаем ход, максимально улучшающий нашу ситуацию
				if (aiSize < WIN_BLOCK)
				{
					aiAnalyze(AI_DOT);
				}
			}
			// И ходим в одну из лучших клеток - свою или человека
			// Для этого сначала вычисляем, какая клетка лучшая, и сколько их
			int x, y, size = 0, count = 0;
			for (y = 0; y<SIZE_Y; y++)
				for (x = 0; x<SIZE_X; x++)
				{
					if (aiBrain[y, x] == size) count++;
					if (aiBrain[y, x] > size) { size = aiBrain[y, x]; count = 1; }
				}
			if (count == 0) throw new Exception("Impossible error 1"); // Такого быть не должно
			// выбираем случайно одну из лучших клекток
			count = rand.Next(0, count);
			// и ходим в нее
			int moveX = -1, moveY = -1;
			for (y = 0; y<SIZE_Y; y++)
				for (x = 0; x<SIZE_X; x++)
					if (aiBrain[y, x] == size)
					{
						if (count == 0) { moveX = x; moveY = y; }
						count--;
					}
			if (!isCellValid(moveY, moveX)) throw new Exception("Impossible error 1"); // И такого быть не должно
			setSym(moveY, moveX, AI_DOT);
		}

		private static bool isFieldFull()
		{
			for (int i = 0; i < SIZE_Y; i++)
				for (int j = 0; j<SIZE_X; j++)
					if (field[i, j] == EMPTY_DOT)
						return false;
			return true;
		}

		private static bool isCellValid(int y, int x)
		{
			if ((x < 0) || (y < 0) || (x >= SIZE_X) || (y >= SIZE_Y)) return false;
			return (field[y, x] == EMPTY_DOT);
		}

		static void Main(string[] args)
		{
			initField();
			printField();
			while (true)
			{
				playerStep();
				printField();
				if (checkWin(PLAYER_DOT))
				{
					Console.WriteLine("Player WIN!");
					break;
				}
				if (isFieldFull())
				{
					Console.WriteLine("DRAW");
					break;
				}
				aiStep();
				printField();
				if (checkWin(AI_DOT))
				{
					Console.WriteLine("Win SkyNet!");
					break;
				}
				if (isFieldFull())
				{
					Console.WriteLine("DRAW");
					break;
				}
			}
		}
	}
}
