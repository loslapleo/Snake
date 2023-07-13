namespace Snake
{
	using System;
	using System.Timers;

    class Program
	{
		const int X_SIZE = 80;
		const int Y_SIZE = 20;
		const int SIZE = X_SIZE * Y_SIZE;
		static Timer timer = new Timer();
		enum Direction {
			Up,
			Down,
			Left,
			Right
		}
		static Direction direction = Direction.Right;
		static int[,] grid = new int[X_SIZE, Y_SIZE];
		static int[,] snake = new int[SIZE, 2];
		static int score = 0;
		static int appleX = Convert.ToInt32(X_SIZE * .25);
		static int appleY = Convert.ToInt32(Y_SIZE * .25);

		public static void Main(string[] args)
		{
			bool quit = false;
			Console.Title = "Game";
			Console.CursorVisible = false;

			snake[0, 0] = Convert.ToInt32(X_SIZE * .5);
			snake[0, 1] = Convert.ToInt32(Y_SIZE * .5);

			timer.Interval = 128;
			timer.Elapsed += Tick;
			timer.AutoReset = true;
			timer.Enabled = true;

			while (!quit) {
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				switch (keyInfo.Key) {
					case ConsoleKey.Escape:
						quit = true;
						break;
					case ConsoleKey.UpArrow:
						direction = Direction.Up;
						break;
					case ConsoleKey.DownArrow:
						direction = Direction.Down;
						break;
					case ConsoleKey.LeftArrow:
						direction = Direction.Left;
						break;
					case ConsoleKey.RightArrow:
						direction = Direction.Right;
						break;
					default:
						// Unrecognized key was used.
						break;
				}
			}

			Console.Clear();
			Console.CursorVisible = true;
		}

		private static void Tick(Object source, ElapsedEventArgs e)
		{
			Console.Clear();
			for (int i = 0; i < score + 1; i++) {
				Console.SetCursorPosition(snake[i, 0], snake[i, 1]);
				Console.Write("o");
			}
			Console.SetCursorPosition(appleX, appleY);
			Console.Write("@");
			Console.SetCursorPosition(0, Y_SIZE);
			Console.Write("Score: " + score);

			switch (direction) {
				case Direction.Up:
					if (snake[0, 1] > 0) {
						snake = Slither(snake);
						snake[0, 1]--;
					}
					break;
				case Direction.Down:
					if (snake[0, 1] < Y_SIZE - 1) {
						snake = Slither(snake);
						snake[0, 1]++;
					}
					break;
				case Direction.Left:
					if (snake[0, 0] > 0) {
						snake = Slither(snake);
						snake[0, 0]--;
					}
					break;
				case Direction.Right:
					if (snake[0, 0] < X_SIZE - 1) {
						snake = Slither(snake);
						snake[0, 0]++;
					}
					break;
			}

			if (snake[0, 0] == appleX && snake[0, 1] == appleY) {
				Random random = new Random();
				appleX = random.Next(0, X_SIZE);
				appleY = random.Next(0, Y_SIZE);
				score++;
			}

			Console.SetCursorPosition(0, Y_SIZE + 1);
			Console.Write(e.SignalTime);
		}

		private static int[,] Slither(int[,] snake)
		{
			for (int i = SIZE - 1; i > 0; i--) {
				snake[i, 0] = snake[i - 1, 0];
				snake[i, 1] = snake[i - 1, 1];
			}

			return snake;
		}
	}
}
