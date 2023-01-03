using Library.Entity;

namespace Library.Collection
{
  public class CaractereList : List<Caractere>
  {
    public int Largeur { get; set; }
    public int LargeurTotal { get; set; }
    public string? TextTmp { get; set; }

    public static string Heure
    {
      get
      {
        string leading = "";
        string deuxPoint = " ";
        int hh = DateTime.Now.Hour;

        if (hh == 0)
          hh = 12;

        if (hh > 12)
          hh -= 12;

        if (DateTime.Now.Millisecond < 500)
          deuxPoint = ":";

        if (hh < 10)
          leading = "  ";

        return leading + hh + deuxPoint + DateTime.Now.ToString("mm");
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    public CaractereList(int largeur)
    {
      Largeur = largeur;
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="texte"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="couleur"></param>
    public static PixelList Print(string texte, int x, int y, Couleur couleur)
    {
      PixelList pixels = new(true);
      CaractereList caracteres = new(PixelList.Largeur);

      caracteres.SetText(texte);

      foreach (Police lettre in caracteres.GetCaracteres().Where(c => c.Point))
        pixels.Add(new Pixel(lettre.X + x, lettre.Y + y, couleur));

      return pixels;
    }

    /// <summary>
    /// SetText
    /// </summary>
    /// <param name="nouvelleStr"></param>
    public int SetText(string nouvelleStr)
    {
      if (TextTmp != nouvelleStr)
      {
        TextTmp = nouvelleStr;

        Clear();

        foreach (char lettre in TextTmp)
          Add(new Caractere(lettre));

        LargeurTotal = this.Sum(p => p.Largeur);
      }

      return LargeurTotal;
    }

    /// <summary>
    /// Caracteres
    /// </summary>
    /// <param name="debut"></param>
    /// <param name="fin"></param>
    /// <returns></returns>
    public PoliceList GetCaracteres(int debut = 0)
    {
      int i = 0;
      int position = 0;
      PoliceList polices = new();

      //Définir le offset de la fraction de lettre qui disparait du coté gauche
      while (position < debut && Count > i)
        if (this[i++].Polices(0, position) is PoliceList lettre)
        {
          position += lettre.Largeur;
          polices.Offset = lettre.Largeur - (position - debut);
        }

      position = 0;

      if (polices.Offset != 0)
        i--;

      //Espace du coté gauche au début du défilement
      while (debut++ < 0)
        polices.AddRange(PoliceList.GetPolice(0, position++, ' '));

      while (position <= Largeur && Count > i)
        if (this[i++].Polices(polices.Offset, position) is PoliceList lettre)
        {
          polices.AddRange(lettre);
          position += lettre.Largeur;
        }

      //Fraction de la derniere lettre du coté droit
      if (Count > i && this[i++].Polices(polices.Offset, position) is PoliceList derniereLettre)
        polices.AddRange(derniereLettre);

      return polices;
    }
  }
}