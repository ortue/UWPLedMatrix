using LedMatrix.Context;
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
		public Horloge()
		{
			InitializeComponent();
		}

		private void BtnHorloge_Click(object sender, RoutedEventArgs e)
		{
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
	}
}
