using Library.Collection;
using Library.Entity;
using Library.Util;
using System.Text;
using System.Text.Json;

namespace BLedMatrix.Shared
{
  public partial class TitreMusique
  {
    public string TitreKodi { get; set; } = "TEST";

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      _ = SetTitreAsync();

      Task.Run(ExecTitreMusique);
    }


    private async Task SetTitreAsync()
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

        TitreKodi = Diacritic.Remove(artist + root?.result.item.title).ToUpper();

        //throw new Exception("test2");

      }
      catch (Exception ex)
      {
        //TitreKodi = Diacritic.Remove(ex.ToString()).ToUpper();
        //await LogToFile.Save(ex.ToString());


        using StreamWriter file = new("Error.log", append: true);
        await file.WriteLineAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + ex.ToString() + Environment.NewLine + Environment.NewLine);


      }
    }


    /// <summary>
    /// ExecTitreMusique
    /// </summary>
    private void ExecTitreMusique()
    {
      using ManualResetEventSlim waitHandle = new(false);

      int task = TaskGo.StartTask();
      //byte[] audioBuffer = new byte[256];
      //using AudioCapture audioCapture = new(AudioCapture.AvailableDevices[1], 22000, ALFormat.Mono8, audioBuffer.Length);
      //audioCapture.Start();
      int cycle = 0;
      int debut = -20;

      while (TaskGo.TaskWork(task))
      {
        //double[] fft = Capture(audioCapture, audioBuffer);
        //double amplitude = GetAmplitudeGraph(fft);

        //GetGraph(fft, amplitude);
        //AffHeure();
        debut = AffTitre(cycle++, debut);

        Pixels.SendPixels();
        Pixels.Reset();
        waitHandle.Wait(TimeSpan.FromMilliseconds(30));
      }
    }

    /// <summary>
    /// Afficher titre
    /// </summary>
    private int AffTitre(int cycle, int debut)
    {
      foreach (Pixel pixel in Pixels.Where(p => p.Couleur.IsRouge && p.Y < 8))
        pixel.Couleur = Couleur.Noir;

      CaractereList textes = new(PixelList.Largeur);
      int largeur = textes.SetText(TitreKodi);

      foreach (Police lettre in textes.GetCaracteres(debut).Where(c => c.Point))
        if (Pixels.Get(lettre.X, lettre.Y + 1) is Pixel pixel)
          if (pixel.Couleur.IsNoir)//|| pixel.Couleur.IsRouge
            pixel.SetColor(Couleur.Rouge);
          else
            pixel.SetColor(Couleur.RougePale);

      if (cycle % 4 == 0)
        debut++;

      //Reset après avoir défiler tout le texte
      //if (cycle % 100000 == 0 || largeur < debut)
      //{
      //  debut = -20;
      //  Titre.Refresh();
      //}

      return debut;
    }
  }
}