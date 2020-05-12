using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace LedMatrix.Classes
{
  public class AnimationList : List<Animation>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="path"></param>
    public AnimationList(string path)
    {
      Task<List<string>> files = Task.Run(async () => await FetchAsync(path));

      foreach (string file in files.Result)
        Add(new Animation(file, path));
    }

    /// <summary>
    /// FetchAsync
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private async Task<List<string>> FetchAsync(string path)
    {
      string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

      StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(root + "\\" + path);
      IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();

      return files.Select(f => f.Name).ToList();
    }

    /// <summary>
    /// GetName
    /// </summary>
    /// <param name="icon"></param>
    /// <returns></returns>
    public Animation GetName(string icon)
    {
      return this.SingleOrDefault(a=>a.FileNameXaml.Contains(icon));
    }
  }
}