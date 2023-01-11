using Library.Collection;
using Library.Entity;
using Library.Util;

namespace BLedMatrix.Shared
{
  public partial class LoremBarnak
  {
    public static string EspaceFin
    {
      get { return "                    "; }
    }

    public static List<string> Lorembarnaks
    {
      get
      {
        return new List<string>
        {
          "tabarnak", "tabarnouche", "tabarouette", "taboire", "tabarslaque", "tabarnane",
          "câlisse", "câlique", "câline", "câline de bine", "câliboire", "caltor",
          "crisse", "christie", "crime", "bout d'crisse",
          "ostie", "astie", "estique", "ostifie", "esprit",
          "ciboire", "saint-ciboire",
          "torrieux", "torvisse",
          "cimonaque", "saint-cimonaque",
          "baptême", "batince", "batèche",
          "bâtard",
          "calvaire", "calvince", "calvinouche",
          "mosus",
          "maudit", "mautadit", "maudine", "mautadine",
          "sacrament", "sacréfice", "saint-sacrament",
          "viarge", "sainte-viarge", "bout d'viarge",
          "ciarge", "saint-ciarge", "bout d'ciarge",
          "cibouleau",
          "cibole", "cibolac",
          "enfant d'chienne", "chien sale",
          "verrat",
          "marde", "maudite marde", "mangeux d'marde",
          "boswell",
          "sacristi", "sapristi",
          "Jésus de plâtre", "Jésus Marie Joseph", "p'tit Jésus", "doux Jésus",
          "crucifix",
          "patente à gosse", "cochonnerie", "cossin",
          "viande à chien",
          "cul", "saintes fesses",
          "purée",
          "etole",
          "charogne", "charrue",
          "gériboire", "géritole",
          "colon"
        };
      }
    }

    /// <summary>
    /// Article
    /// </summary>
    /// <param name="loremBarnak"></param>
    /// <returns></returns>
    private static string Article(string loremBarnak)
    {
      List<char> voyelle = new() { 'A', 'E', 'I', 'O', 'U', 'H', 'Y' };

      if (voyelle.Contains(char.ToUpper(loremBarnak[0])))
        return "D'" + loremBarnak;

      return "DE " + loremBarnak;
    }

    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecLoremBarnak);
    }

    /// <summary>
    /// LoremBarnak
    /// </summary>
    private void ExecLoremBarnak()
    {
      int largeur = 0;
      int debut = -20;
      int task = TaskGo.StartTask();
      string loremBarnak = GetLoremBarnak(10);
      CaractereList caracteres = new(20);

      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        //Reset après avoir défiler tout le texte
        if (!string.IsNullOrWhiteSpace(loremBarnak) && largeur < debut++)
        {
          debut = -20;
          loremBarnak = GetLoremBarnak(10);
        }

        largeur = caracteres.SetText(loremBarnak);

        Pixels.Set(CaractereList.Print(caracteres.GetCaracteres(debut), 0, 1, Couleur.Get(64, 0, 0)));
        Pixels.Set(CaractereList.Print(DateTime.Now.ToString("MM-dd"), 1, 7, Couleur.Get(0, 0, 127)));
        Pixels.Set(CaractereList.Print(CaractereList.Heure, 2, 13, Couleur.Get(0, 0, 127)));

        Background.Grichage(Pixels);
        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(50));
      }
    }

    /// <summary>
    /// GetLoremBarnak
    /// </summary>
    /// <param name="taille"></param>
    /// <returns></returns>
    private static string GetLoremBarnak(int taille)
    {
      string loremBarnak = string.Empty;

      List<string> lorembarnaks = Lorembarnaks;

      Random random = new();

      for (int i = 0; i < taille; i++)
      {
        int r = random.Next(0, lorembarnaks.Count);

        if (i == 0)
          loremBarnak += lorembarnaks[r] + " ";
        else if (i == taille - 1)
          loremBarnak += Article(lorembarnaks[r]) + "." + EspaceFin;
        else
          loremBarnak += Article(lorembarnaks[r]) + " ";

        lorembarnaks.RemoveAt(r);
      }

      return CaractereList.RemoveDiacritics(loremBarnak.ToUpper());
    }
  }
}