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

		public bool Autorun { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		public GifAnime()
		{
			InitializeComponent();
		}

		private void GridLed_ItemClick(object sender, ItemClickEventArgs e)
		{
			Autorun = false;

			ShowAnimation(((Animation)e.ClickedItem).FileName);
		}

		/// <summary>
		/// ShowAnimation
		/// </summary>
		/// <param name="file"></param>
		private void ShowAnimation(string file)
		{
			ImageClass imageClass = new ImageClass(file);

			Task.Run(() =>
			{
				int task = Util.StartTask();
				int frame = 0;

				for (int slide = imageClass.Width; slide >= 0; slide--)
					SetAnimation(imageClass, frame++, slide);

				while (imageClass.Animation && Util.TaskWork(task))
					SetAnimation(imageClass, frame++);
			});
		}

		/// <summary>
		/// SetAnimation
		/// </summary>
		/// <param name="imageClass"></param>
		/// <param name="frame"></param>
		/// <param name="slide"></param>
		private void SetAnimation(ImageClass imageClass, int frame, int slide = 0)
		{
			if (imageClass.Animation)
				imageClass.SetÞixelFrame(frame++, Util.Context.Pixels, slide);
			else
				imageClass.SetÞixel(Util.Context.Pixels, slide);

			Util.SetLeds();
			Util.Context.Pixels.Reset();

			if (slide == 0)
				using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
					waitHandle.Wait(TimeSpan.FromMilliseconds(80));
		}

		private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			Task.Run(() =>
			{
				Autorun = true;

				int i = 0;
				while (Autorun)
				{
					ShowAnimation(Animations[i++].FileName);

					using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
						waitHandle.Wait(TimeSpan.FromSeconds(10));

					if (Animations.Count <= i)
						i = 0;
				}
			});
		}
	}
}