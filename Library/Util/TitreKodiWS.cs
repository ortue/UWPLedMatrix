using Library.Entity;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Library.Util
{
  public class TitreKodiWS
  {
    public string Musique { get; set; }

    /// <summary>
    /// Consructeur
    /// </summary>
    public TitreKodiWS()
    {
      //Musique = GetMusique().Result;
      Musique = GetMusiqueVieux();
    }

    /// <summary>
    /// Refrest
    /// </summary>
    public void Refresh()
    {
      //Task.Run(() => Musique = GetMusique().Result);
      Task.Run(() => Musique = GetMusiqueVieux());
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
        //StringContent data = new(json, Encoding.UTF8, "application/json");

        //HttpClient client = new();
        //using HttpResponseMessage response = await client.PostAsync("http://pc-musique:8080/jsonrpc", data);
        //response.EnsureSuccessStatusCode();
        //string responseBody = await response.Content.ReadAsStringAsync();

        //MusiqueJSONRoot? root = JsonSerializer.Deserialize<MusiqueJSONRoot>(responseBody);

        //string artist = string.Empty;

        //if (root?.result.item.artist != null && root.result.item.artist[0] != null)
        //  artist = root.result.item.artist[0] + " - ";

        //return Diacritic.Remove(artist + root?.result.item.title).ToUpper();

        return "TEST";
      }
      catch (Exception ex)
      {
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