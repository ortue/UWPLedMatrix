using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LedLibrary.Collection
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
    /// GetName
    /// </summary>
    /// <param name="icon"></param>
    /// <returns></returns>
    //public ImageClass GetName(string icon)
    //{
    //  return this.SingleOrDefault(a => a.FileName.Contains(icon));
    //}

    /// <summary>
    /// SetPixel
    /// </summary>
    /// <param name="icon"></param>
    /// <param name="pixels"></param>
    public void SetPixel(string icon, PixelList pixels)
    {
      if (this.SingleOrDefault(a => a.FileName.Contains(icon)) is ImageClass imageClass)
        imageClass.SetÞixelFrame(0, pixels, 0, false);
    }
  }
}