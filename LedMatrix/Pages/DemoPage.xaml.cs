using LedMatrix.Classes;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace LedMatrix.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class DemoPage : Page
	{
		/// <summary>
		/// Constructeur
		/// </summary>
		public DemoPage()
		{
			InitializeComponent();
		}

		private void BtnChute_Click(object sender, RoutedEventArgs e)
		{
			Task.Run(() => Demo.Demo1());
		}

		private void BtnPong_Click(object sender, RoutedEventArgs e)
		{
			Task.Run(() => Demo.Demo2());
		}

		private void BtnTetris_Click(object sender, RoutedEventArgs e)
		{

		}

		private void BtnSerpent_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}