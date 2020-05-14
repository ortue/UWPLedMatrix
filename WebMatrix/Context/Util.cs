using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebMatrix.Classes;

namespace WebMatrix.Context
{
  public static class Util
  {
    public static HttpClient Client = new HttpClient() { BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Sainte-Marthe-sur-le-Lac&mode=xml&units=metric&appid=52534a6f666e45fb30ace3343cea4a47") };

    public static int TaskNbr { get; set; }
    public static TaskGoList TaskGo { get; set; }
    public static LedMatrixContext Context { get; set; }

    /// <summary>
    /// Start Task
    /// </summary>
    /// <returns></returns>
    public static int StartTask()
    {
      TaskGo.Add(new TaskGo(TaskNbr));
      TaskGo.Where(t => t.ID < TaskNbr).ToList().ForEach(t => t.Work = false);

      return TaskNbr++;
    }

    /// <summary>
    /// Task Work
    /// </summary>
    /// <returns></returns>
    public static bool TaskWork(int id)
    {
      return TaskGo.SingleOrDefault(t => t.ID == id).Work;
    }

    /// <summary>
    /// Stop Task
    /// </summary>
    public static void StopTask()
    {
      TaskGo.ForEach(t => t.Work = false);
    }

    /// <summary>
    /// Setup
    /// </summary>
    public static void Setup()
    {

      Context = new LedMatrixContext();
      TaskGo = new TaskGoList();
    }

    /// <summary>
    /// SetLeds
    /// </summary>
    public static void SetLeds()
    {
      Context.PixelStrip.SendPixels(Context.Pixels.PixelColors);
    }

    /// <summary>
    /// Get Byte From Pixel
    /// </summary>
    /// <param name="fileNameOfImage"></param>
    /// <returns></returns>
    public static async Task<byte[]> GetByteFromPixel(string fileNameOfImage)
    {
      //Windows.Storage.StorageFile imageFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileNameOfImage); // Bild laden
      //Stream imagestream = await imageFile.OpenStreamForReadAsync(); // Bild in Stream umwandeln
      //BitmapDecoder imageDecoder = await BitmapDecoder.CreateAsync(imagestream.AsRandomAccessStream()); // Stream dekodieren
      //PixelDataProvider imagePixelData = await imageDecoder.GetPixelDataAsync(); // Informationen über Pixel erhalten
      //return imagePixelData.DetachPixelData(); // Pixel Daten bekommen

      return null;
    }

    /// <summary>
    /// UpdateMeteo
    /// </summary>
    public static async void UpdateMeteo()
    {
      current meteo = await GetMeteo();
      Context.Meteo = meteo;
    }

    /// <summary>
    /// GetMeteo
    /// </summary>
    /// <returns></returns>
    private static async Task<current> GetMeteo()
    {
      return await Task.Run(() =>
      {
        Task<HttpResponseMessage> response = Client.GetAsync(Client.BaseAddress);

        if (response.Result.IsSuccessStatusCode)
        {
          string xml = response.Result.Content.ReadAsStringAsync().Result;

          using (TextReader reader = new StringReader(xml))
          {
            XmlSerializer serializer = new XmlSerializer(typeof(current));
            return (current)serializer.Deserialize(reader);
          }
        }
        else
          return null;
      });
    }

    //public static async void SetAnimation()
    //{
    //	AnimationList animations = await SetAnimationAsync("Images");
    //	Context.Animations = animations;
    //}

    //public static async void SetMeteoImg()
    //{
    //	AnimationList animations = await SetAnimationAsync("MeteoImg");
    //	Context.MeteoImgs = animations;
    //}

    //private static async Task<AnimationList> SetAnimationAsync(string path)
    //{
    //	return await Task.Run(() => new AnimationList(path));
    //}
  }
}