using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LedLibrary.Collection
{
  public class AnimationList : List<Animation>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="path"></param>
    public AnimationList(string path)
    {
      foreach (string file in Directory.GetFiles(path))
        Add(new Animation(file));
    }

    /// <summary>
    /// GetName
    /// </summary>
    /// <param name="icon"></param>
    /// <returns></returns>
    public Animation GetName(string icon)
    {
      return this.SingleOrDefault(a=>a.FileName.Contains(icon));
    }
  }
}