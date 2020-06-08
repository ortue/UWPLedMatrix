using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebMatrix.Context
{
  public static class Util
  {
    public static int TaskNbr { get; set; }
    public static bool Autorun { get; set; }
    public static current Meteo { get; set; }
    //public static int Background { get; set; }
    public static TaskGoList TaskGo { get; set; }
    public static string LastAutoRun { get; set; }
    public static LedMatrixContext Context { get; set; }

    public static HttpClient Client = new HttpClient() { BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Sainte-Marthe-sur-le-Lac&mode=xml&units=metric&appid=52534a6f666e45fb30ace3343cea4a47") };

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
      return TaskGo.SingleOrDefault(t => t.ID == id)?.Work ?? false;
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

      //Background = 0;

      GetMeteo();
    }

    /// <summary>
    /// SetLeds
    /// </summary>
    public static void SetLeds()
    {
      if (Environment.MachineName != "PC-BENOIT")
        Context.PixelStrip.SendPixels(Context.Pixels.PixelColors);
    }

    /// <summary>
    /// GetMeteo
    /// </summary>
    /// <returns></returns>
    public static void GetMeteo()
    {
      Meteo = Task.Run(() =>
      {
        Task<HttpResponseMessage> response = Client.GetAsync(Client.BaseAddress);

        if (response.Result.IsSuccessStatusCode)
        {
          string xml = response.Result.Content.ReadAsStringAsync().Result;

          using TextReader reader = new StringReader(xml);
          XmlSerializer serializer = new XmlSerializer(typeof(current));
          return (current)serializer.Deserialize(reader);
        }
        else
          return null;

      }).Result;
    }
  }
}