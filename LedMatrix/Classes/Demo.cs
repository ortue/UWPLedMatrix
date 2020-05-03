using LedMatrix.Context;
using System;
using System.Linq;
using System.Threading;
using Windows.UI;

namespace LedMatrix.Classes
{
	public class Demo
	{
		public static void Go()
		{
			Random random = new Random();
			int demo = random.Next(0, 2);

			switch (demo)
			{
				case 0:
					Demo1();
					break;

				case 1:
					Demo2();
					break;
			}
		}

		/// <summary>
		/// Demo1
		/// </summary>
		public static void Demo1()
		{
			int task = Util.StartTask();
			int[] bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			while (Util.TaskWork(task))
			{
				if (bot.All(bo => bo > 20))
				{
					Util.Context.Pixels.Reset();
					bot = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
				}

				Random random = new Random();
				int x = random.Next(0, Util.Context.Pixels.Largeur);
				int r = random.Next(0, 127);
				int g = random.Next(0, 127);
				int b = random.Next(0, 127);

				int y = 0;

				while (y < Util.Context.Pixels.Hauteur - bot[x])
				{
					EffacerDernier(x, y);
					Util.Context.Pixels.GetCoordonnee(x, y).Set(r, g, b);
					Util.SetLeds();

					int temp = 100;

					if (y > 2)
						temp = 50;

					if (y > 4)
						temp = 25;

					if (y > 6)
						temp = 1;

					using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
						waitHandle.Wait(TimeSpan.FromMilliseconds(temp));

					y++;

					if (y >= Util.Context.Pixels.Hauteur - bot[x])
					{
						if (x > 0 && y < Util.Context.Pixels.Hauteur - bot[x - 1])
							EffacerDernier(x--, y);

						if (x < Util.Context.Pixels.Largeur - 1 && y < Util.Context.Pixels.Hauteur - bot[x + 1])
							EffacerDernier(x++, y);

						using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
							waitHandle.Wait(TimeSpan.FromMilliseconds(100));
					}
				}

				bot[x]++;
			}
		}

		/// <summary>
		/// EffacerDernier
		/// </summary>
		private static void EffacerDernier(int x, int y)
		{
			if (y > 0)
				Util.Context.Pixels.GetCoordonnee(x, y - 1).SetColor(new Color());
		}

		/// <summary>
		/// 
		/// </summary>
		private static void Demo2()
		{
			int task = Util.StartTask();

			Random random = new Random();
			int x = random.Next(0, Util.Context.Pixels.Largeur);
			int y = random.Next(0, Util.Context.Pixels.Hauteur);
			decimal xx = random.Next(5, 50) / 10;
			decimal yy = random.Next(5, 50) / 10;

			while (Util.TaskWork(task))
			{
				Util.Context.Pixels.GetCoordonnee(x, y).Set(0, 0, 0);

				if (GetBall(x + xx) >= Util.Context.Pixels.Largeur || GetBall(x + xx) < 0)
				{
					xx -= (xx * 2);
				}

				x = GetBall(x + xx);

				if (GetBall(y + yy) >= Util.Context.Pixels.Largeur || GetBall(y + yy) < 0)
					yy -= (yy * 2);

				y = GetBall(y + yy);

				Util.Context.Pixels.GetCoordonnee(x, y).Set(0, 0, 127);
				Util.SetLeds();

				using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
					waitHandle.Wait(TimeSpan.FromMilliseconds(1));
			}
		}

		private static int GetBall(decimal point)
		{
			return (int)(Math.Round(point, 0));
		}
	}
}