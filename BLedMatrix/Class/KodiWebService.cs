using Library.Util;
using System.Net;
using System.Text.Json;
using System.Text;
using Library.Entity;

namespace BLedMatrix.Class
{
  public class KodiWebService
  {
    public string Musique { get; set; } = string.Empty;

    /// <summary>
    /// Consructeur
    /// </summary>
    public KodiWebService()
    {
      Refresh();
    }

    /// <summary>
    /// Refrest
    /// </summary>
    public void Refresh()
    {
      Task.Run(() => Musique = GetMusique().Result);
    }

    /// <summary>
    /// GetMusique
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetMusique()
    {
      try
      {
        string json = "{\"jsonrpc\": \"2.0\",\"method\": \"Player.GetItem\",\"params\": { \"properties\": [\"title\",\"album\",\"artist\",\"duration\"],\"playerid\": 0},\"id\": \"AudioGetItem\"} ";
        StringContent data = new(json, Encoding.UTF8, "application/json");

        HttpClient client = new();
        using HttpResponseMessage response = await client.PostAsync("http://192.168.2.11:8080/jsonrpc", data);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        MusiqueJSONRoot? root = JsonSerializer.Deserialize<MusiqueJSONRoot>(responseBody);

        string artist = string.Empty;

        if (root?.result.item.artist != null && root.result.item.artist[0] != null)
          artist = root.result.item.artist[0] + " - ";

        return Diacritic.Remove(artist + root?.result.item.title).ToUpper();
        //return "";
      }
      catch (Exception ex)
      {
        using StreamWriter file = new("Error.log", append: true);
        await file.WriteLineAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + ex.ToString() + Environment.NewLine + Environment.NewLine);

        return ex.ToString().ToUpper();
      }
    }


    /// <summary>
    /// GetMusique
    /// </summary>
    /// <returns></returns>
    public static string GetMusiqueVieux()
    {
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.2.11:8080/jsonrpc");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (StreamWriter streamWriter = new(httpWebRequest.GetRequestStream()))
        {
          string json = "{\"jsonrpc\": \"2.0\",\"method\": \"Player.GetItem\",\"params\": { \"properties\": [\"title\",\"album\",\"artist\",\"duration\"],\"playerid\": 0},\"id\": \"AudioGetItem\"} ";
          streamWriter.Write(json);
        }

        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using StreamReader streamReader = new(httpResponse.GetResponseStream());
        MusiqueJSONRoot? root = JsonSerializer.Deserialize<MusiqueJSONRoot>(streamReader.ReadToEnd());

        string artist = string.Empty;

        if (root?.result.item.artist != null && root.result.item.artist[0] != null)
          artist = root.result.item.artist[0] + " - ";

        return Diacritic.Remove(artist + root.result.item.title).ToUpper();
      }
      catch (Exception ex)
      {
        return ex.ToString().ToUpper();
      }
    }
  }
}