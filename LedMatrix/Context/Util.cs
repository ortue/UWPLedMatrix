using Library.Collection;
using Library.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;

namespace LedMatrix.Context
{
  public static class Util
  {
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
      Windows.Storage.StorageFile imageFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileNameOfImage); // Bild laden
      Stream imagestream = await imageFile.OpenStreamForReadAsync(); // Bild in Stream umwandeln
      BitmapDecoder imageDecoder = await BitmapDecoder.CreateAsync(imagestream.AsRandomAccessStream()); // Stream dekodieren
      PixelDataProvider imagePixelData = await imageDecoder.GetPixelDataAsync(); // Informationen über Pixel erhalten
      return imagePixelData.DetachPixelData(); // Pixel Daten bekommen
    }
  }
}