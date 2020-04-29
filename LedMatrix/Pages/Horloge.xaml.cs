using LedMatrix.Context;
using Library.Classes;
using Library.Collection;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.ApplicationModel.Background;
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
		static HttpClient Client = new HttpClient();


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

		private void BtnMeteo_Click(object sender, RoutedEventArgs e)
		{
			Client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Sainte-Marthe-sur-le-Lac&mode=xml&units=metric&appid=52534a6f666e45fb30ace3343cea4a47");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

			Task.Run(() =>
		 {







			 Task<HttpResponseMessage> response = Client.GetAsync(Client.BaseAddress);

			 if (response.Result.IsSuccessStatusCode)
			 {

				 XmlSerializer serializer = new XmlSerializer(typeof(current));
				 string xml = response.Result.Content.ReadAsStringAsync().Result;

				 current meteo = new current();

				 using (TextReader reader = new StringReader(xml))
				 {
					 meteo = (current)serializer.Deserialize(reader);




					 AnimationList Animations = new AnimationList("MeteoImg");


						 ImageClass imageClass = new ImageClass(Animations[0].FileName);
						 imageClass.SetÞixelFrame(0, Util.Context.Pixels, 0, false);







					 Util.Context.Pixels.SetMeteo(meteo.temperature.value.ToString());
					 Util.SetLeds();
					 Util.Context.Pixels.Reset();
				 }
			 }
		 });
		}

	}
}
