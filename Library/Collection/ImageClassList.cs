using Library.Entity;

namespace Library.Collection
{
  public class ImageClassList : List<ImageClass>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="path"></param>
    public ImageClassList(string path)
    {
      foreach (string file in Directory.GetFiles(path))
        if (file.ToUpper().Contains("GIF"))
          Add(new ImageClass(file));
    }

    /// <summary>
    /// SetPixel
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="pixels"></param>
    public void SetPixel(string icon, PixelList pixels)
    {
      if (Find(a => a.FileName.Contains(icon)) is ImageClass imageClass)
        imageClass.SetÞixelFrame(0, pixels, 0, false);
    }
  }
}