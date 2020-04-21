using LedMatrix.Context;
using Library.Classes;
using Library.Collection;
using Library.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LedMatrix.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class GifAnime : Page
	{
		public AnimationList Animations
		{
			get { return Util.Context.Animations; }
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		public GifAnime()
		{
			InitializeComponent();
		}

		private void GridLed_ItemClick(object sender, ItemClickEventArgs e)
		{
			ShowAnimation(((Animation)e.ClickedItem).FileName);
		}

		/// <summary>
		/// ShowAnimation
		/// </summary>
		/// <param name="file"></param>
		private void ShowAnimation(string file)
		{
			ImageClass imageClass = new ImageClass(file);

			if (imageClass.Animation)
			{
				Task.Run(() =>
				{
					int task = Util.StartTask();
					int frame = 0;

					while (Util.TaskWork(task))
					{
						imageClass.SetÞixelFrame(frame++, Util.Context.Pixels);
						Util.SetLeds();
						Util.Context.Pixels.Reset();

						using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
							waitHandle.Wait(TimeSpan.FromMilliseconds(80));
					}
				});
			}
			else
			{
				Util.StopTask();

				for (int slide = 19; slide > 0; slide--)
				{
					imageClass.SetÞixel(Util.Context.Pixels, slide);
					Util.SetLeds();
					Util.Context.Pixels.Reset();

					using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
					  waitHandle.Wait(TimeSpan.FromMilliseconds(1));
				}
			}
		}
	}
}