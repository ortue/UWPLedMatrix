using LedLibrary.Entities;
using System.Collections.Generic;

namespace LedLibrary.Collection
{
  public class BriqueList : List<Brique>
  {
    /// <summary>
    /// Constructeur
    /// </summary>
    public BriqueList()
    {
      for (int ranger = 0; ranger < 6; ranger++)
        for (int i = 0; i < 17; i++)
          Add(new Brique(i, ranger));
    }
  }
}