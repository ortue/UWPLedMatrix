using LedMatrix.Classes;
using LedMatrix.Context;
using LedMatrix.Pages;
using Library.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LedMatrix
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();

			Loaded += MainPage_Loaded;
		}

		private async void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				// Initialize the led strip
				await Util.Context.PixelStrip.Begin();

				await Task.Run(() =>
				{
					Demo.Demo1();

					//while (Util.TaskWork(task))
					//{
					//  i++;

					//  for (int position = 1; position <= Util.Context.NbrLed; position++)
					//  {
					//    if (i % 400 == position)
					//    {
					//      Util.Context.Pixels.GetPosition(position).SetColor(new Color { B = (byte)(i % 128) });
					//      Util.Context.Pixels.GetNumero(position).SetColor(new Color { R = (byte)(i % 128) });
					//    }
					//  }

					//Util.SetLeds();

					//using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
					//  waitHandle.Wait(TimeSpan.FromMilliseconds(1));
					//}
				});
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
			Util.StopTask();

			if (args.IsSettingsSelected)
			{
				MainFrame.Navigate(typeof(GifAnime));
			}
			else
			{
				NavigationViewItem selectedItem = (NavigationViewItem)args.SelectedItem;
				string pageName = "LedMatrix.Pages." + selectedItem.Tag.ToString();
				Type pageType = Type.GetType(pageName);
				MainFrame.Navigate(pageType);
			}
		}
	}
}