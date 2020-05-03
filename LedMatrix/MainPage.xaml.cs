using LedMatrix.Classes;
using LedMatrix.Context;
using LedMatrix.Pages;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
		/// <summary>
		/// Constructeur
		/// </summary>
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
				await Task.Run(() => Demo.Go());
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
				Util.Context.Autorun = false;

				NavigationViewItem selectedItem = (NavigationViewItem)args.SelectedItem;
				string pageName = "LedMatrix.Pages." + selectedItem.Tag.ToString();
				Type pageType = Type.GetType(pageName);
				MainFrame.Navigate(pageType);
			}
		}
	}
}