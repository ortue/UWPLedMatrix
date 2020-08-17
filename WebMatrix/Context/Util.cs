using LedLibrary.Collection;
using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebMatrix.Classes;

namespace WebMatrix.Context
{
  public static class Util
  {
    public static int TaskNbr { get; set; }
    public static bool Autorun { get; set; }
    public static current Meteo { get; set; }
    public static TaskGoList TaskGo { get; set; }
    public static string LastAutoRun { get; set; }
    public static List<string> Nouvelles { get; set; }

    public static string NouvelleStr
    {
      get
      {
        if (Nouvelles != null)
          return RemoveDiacritics(Regex.Replace(string.Join(string.Empty, Nouvelles), @"http[^\s]+", ""));

        return string.Empty;
      }
    }

    public static LedMatrixContext Context { get; set; }

    //c812ff96b7594775ae0b19df9309aae9

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
    public static current GetMeteo()
    {
      try
      {
        HttpClient Client = new HttpClient() { BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Sainte-Marthe-sur-le-Lac&mode=xml&units=metric&appid=52534a6f666e45fb30ace3343cea4a47") };
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
      }
      catch
      {
        return null;
      }

    }

    /// <summary>
    /// GetMeteoAsync
    /// </summary>
    public static async void GetMeteoAsync()
    {
      Task<current> task = new Task<current>(GetMeteo);
      task.Start();
      Meteo = await task;
    }

    /// <summary>
    /// GetNouvelle
    /// </summary>
    /// <returns></returns>
    public static List<string> GetNouvelle()
    {
      List<string> nouvelles = new List<string>();

      try
      {
        XmlReader reader = XmlReader.Create("https://ici.radio-canada.ca/rss/4159");
        SyndicationFeed feed = SyndicationFeed.Load(reader);
        reader.Close();

        foreach (SyndicationItem item in feed.Items)
        {
          nouvelles.Add(StriperText(item.Title.Text));
          nouvelles.Add(StriperText(item.Summary.Text));
        }
      }
      catch (Exception ex)
      {
        return new List<string> { ex.Message.ToUpper() };
      }

      return nouvelles;
    }

    /// <summary>
    /// StriperText
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string StriperText(string text)
    {
      text = text.Replace("<p>", "");
      text = text.Replace("</p>", "");
      text = text.Replace("<em>", "");
      text = text.Replace("</em>", "");

      if (text.TrimEnd().LastOrDefault() is char lettre)
        if (lettre != '.' && lettre != '!' && lettre != '?')
          text += ".";

      return text.ToUpper() + " ";
    }

    /// <summary>
    /// GetNouvelleAsync
    /// </summary>
    public static async void GetNouvelleAsync()
    {
      using Task<List<string>> task = new Task<List<string>>(GetNouvelle);
      task.Start();
      Nouvelles = await task;
    }

    static string RemoveDiacritics(string text)
    {
      string normalizedString = text.Normalize(NormalizationForm.FormD);
      StringBuilder stringBuilder = new StringBuilder();

      foreach (char c in normalizedString)
      {
        UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
          stringBuilder.Append(c);
      }

      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
  }
}