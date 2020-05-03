using LedMatrix.Context;
using Library.Classes;
using Library.Collection;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LedMatrix.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Horloge : Page
	{
		public AnimationList MeteoImgs
		{
			get { return Util.Context.MeteoImgs; }
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		public Horloge()
		{
			InitializeComponent();
		}

		private void BtnHorloge_Click(object sender, RoutedEventArgs e)
		{
			Util.StopTask();

			Task.Run(() =>
			{
				int task = Util.StartTask();

				while (Util.TaskWork(task))
				{
					Util.Context.Pixels.SetHorloge();
					Util.SetLeds();
					Util.Context.Pixels.Reset();
				}
			});
		}

		private void BtnMeteo_Click(object sender, RoutedEventArgs e)
		{
			Util.StopTask();

			Task.Run(() =>
			{
				DateTime update = DateTime.Now.AddMinutes(-10);
				int task = Util.StartTask();

				while (Util.TaskWork(task))
				{
					if (Util.Context.Meteo != null)
					{
						ImageClass imageClass = new ImageClass(MeteoImgs.GetName(Util.Context.Meteo.weather.icon).FileName);
						imageClass.SetÞixelFrame(0, Util.Context.Pixels, 0, false);
					}

					Util.Context.Pixels.SetMeteo(Util.Context.Meteo);
					Util.SetLeds();
					Util.Context.Pixels.Reset();

					if (update.AddMinutes(5) < DateTime.Now)
					{
						update = DateTime.Now;
						Util.UpdateMeteo();
					}
				}
			});
		}
	}
}