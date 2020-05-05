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
		public string LastAutorun { get; set; }

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
			Util.Context.Autorun = false;
			Util.StopTask();

			ShowAnimation(((Animation)e.ClickedItem).FileName);
		}

		/// <summary>
		/// ShowAnimation
		/// </summary>
		/// <param name="file"></param>
		private void ShowAnimation(string file)
		{
			Task.Run(() =>
			{
				int task = Util.StartTask();
				int frame = 0;

				//Fade Out
				if (!string.IsNullOrWhiteSpace(LastAutorun))
				{
					ImageClass imageClassLastAutorun = new ImageClass(LastAutorun);

					for (int slide = 0; slide < imageClassLastAutorun.Width; slide++)
						SetAnimation(imageClassLastAutorun, frame++, slide, true);
				}

				ImageClass imageClass = new ImageClass(file);

				//Fade In
				for (int slide = imageClass.Width; slide >= 0; slide--)
					SetAnimation(imageClass, frame++, slide);

				LastAutorun = file;

				//Animation
				while (imageClass.Animation && Util.TaskWork(task))
					SetAnimation(imageClass, frame++, 0);
			});
		}

		/// <summary>
		/// SetAnimation
		/// </summary>
		/// <param name="imageClass"></param>
		/// <param name="frame"></param>
		/// <param name="slide"></param>
		/// <param name="fadeOut"></param>
		private void SetAnimation(ImageClass imageClass, int frame, int slide, bool fadeOut = false)
		{
			imageClass.SetÞixelFrame(frame++, Util.Context.Pixels, slide, fadeOut);
			Util.SetLeds();
			Util.Context.Pixels.Reset();

			if (slide == 0)
				using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
					waitHandle.Wait(TimeSpan.FromMilliseconds(60));
		}

		private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			Task.Run(() =>
			{
				Util.Context.Autorun = true;

				int i = 0;

				while (Util.Context.Autorun)
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