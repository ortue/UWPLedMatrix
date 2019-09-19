using LedMatrix.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

      //    using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
						//waitHandle.Wait(TimeSpan.FromMilliseconds(80));
				}
			});
		}
	}
}
