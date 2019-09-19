using System;

namespace Library.Entities
{
  public class Animation
  {
    public string FileName { get; set; }
    public string FileNameXaml { get; set; }
    public Uri FileUri { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="file"></param>
    public Animation(string file)
    {
      FileName = @"\Images\" + file;
      FileNameXaml = "/Images/" + file;
      FileUri = new Uri("ms-appx:///Images/" + file);
    }
  }
}