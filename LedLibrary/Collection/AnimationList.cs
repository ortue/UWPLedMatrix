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
    /// FetchAsync
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    //private async Task<List<string>> FetchAsync(string path)
    //{
    //  string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

    //  StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(root + "\\" + path);
    //  IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();

    //  return files.Select(f => f.Name).ToList();
    //}

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