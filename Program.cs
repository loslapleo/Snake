﻿namespace Snake
{
	using System;
	using System.Timers;

    class Program
	{
		const int X_SIZE = 80;
		const int Y_SIZE = 20;
		const int SIZE = X_SIZE * Y_SIZE;
		static bool quit = false;
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
			// Draw barrier.
			for (int i = 0; i <= X_SIZE + 1; i++) {
				Console.SetCursorPosition(i, 0);
				Console.Write("+");
				Console.SetCursorPosition(i, Y_SIZE + 1);
				Console.Write("+");
			}
			for (int i = 1; i < Y_SIZE + 1; i++) {
				Console.SetCursorPosition(0, i);
				Console.Write("+");
				Console.SetCursorPosition(X_SIZE + 1, i);
				Console.Write("+");
			}
			// Draw snake;
			for (int i = 0; i < score + 1; i++) {
				Console.SetCursorPosition(snake[i, 0], snake[i, 1]);
				Console.Write("o");
			}
			Console.SetCursorPosition(appleX, appleY);
			Console.Write("@");
			Console.SetCursorPosition(X_SIZE / 2 - 7, Y_SIZE + 1);
			Console.Write("| Score: " + score + " |");

			switch (direction) {
				case Direction.Up:
					if (snake[0, 1] > 1) {
						snake = Slither(snake);
						snake[0, 1]--;
					} else Quit();
					break;
				case Direction.Down:
					if (snake[0, 1] < Y_SIZE) {
						snake = Slither(snake);
						snake[0, 1]++;
					} else Quit();
					break;
				case Direction.Left:
					if (snake[0, 0] > 1) {
						snake = Slither(snake);
						snake[0, 0]--;
					} else Quit();
					break;
				case Direction.Right:
					if (snake[0, 0] < X_SIZE) {
						snake = Slither(snake);
						snake[0, 0]++;
					} else Quit();
					break;
			}

			if (snake[0, 0] == appleX && snake[0, 1] == appleY) {
				Random random = new Random();
				appleX = random.Next(0, X_SIZE);
				appleY = random.Next(0, Y_SIZE);
				score++;
			}
		}

		private static int[,] Slither(int[,] snake)
		{
			for (int i = SIZE - 1; i > 0; i--) {
				snake[i, 0] = snake[i - 1, 0];
				snake[i, 1] = snake[i - 1, 1];
			}

			return snake;
		}

		private static void Quit()
		{
			quit = true;
			Console.SetCursorPosition(X_SIZE / 2 - 7, Y_SIZE / 2 - 1);
			Console.Write("H===========H");
			Console.SetCursorPosition(X_SIZE / 2 - 7, Y_SIZE / 2);
			Console.Write("H Game Over H");
			Console.SetCursorPosition(X_SIZE / 2 - 7, Y_SIZE / 2 + 1);
			Console.Write("H===========H");
		}
	}
}
