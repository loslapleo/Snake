using System;

namespace Snake
{
    class Program
	{
		public static void Main(string[] args)
		{
			const int X_SIZE = 80;
			const int Y_SIZE = 20;

			bool quit = false;
			Console.Title = "Game";
			Console.CursorVisible = false;

			int[,] grid = new int[X_SIZE, Y_SIZE];

			int[,] snake = new int[X_SIZE * Y_SIZE, 2];
			snake[0, 0] = Convert.ToInt32(X_SIZE * .5);
			snake[0, 1] = Convert.ToInt32(Y_SIZE * .5);
			int appleX = Convert.ToInt32(X_SIZE * .25);
			int appleY = Convert.ToInt32(Y_SIZE * .25);
			int score = 0;

			while (quit == false) {
				Console.Clear();
				for (int i = 0; i < score + 1; i++) {
					Console.SetCursorPosition(snake[i, 0], snake[i, 1]);
					Console.Write("o");
				}
				Console.SetCursorPosition(appleX, appleY);
				Console.Write("@");
				Console.SetCursorPosition(0, Y_SIZE);
				Console.Write("Score: " + score);

				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				switch (keyInfo.Key) {
					case ConsoleKey.Escape:
						quit = true;
						break;
					case ConsoleKey.UpArrow:
						if (snake[0, 1] > 0) snake[0, 1]--;
						break;
					case ConsoleKey.DownArrow:
						if (snake[0, 1] < Y_SIZE - 1) snake[0, 1]++;
						break;
					case ConsoleKey.LeftArrow:
						if (snake[0, 0] > 0) snake[0, 0]--;
						break;
					case ConsoleKey.RightArrow:
						if (snake[0, 0] < X_SIZE - 1) snake[0, 0]++;
						break;
					default:
						// Unrecognized key was used.
						break;
				}

				if (snake[0, 0] == appleX && snake[0, 1] == appleY) {
					Random random = new Random();
					appleX = random.Next(0, X_SIZE);
					appleY = random.Next(0, Y_SIZE);
					score++;
				}
			}

			Console.Clear();
			Console.CursorVisible = true;
		}
	}
}
