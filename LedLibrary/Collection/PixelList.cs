using LedLibrary.Classes;
using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class PixelList : List<Pixel>
  {
    public Emplacement Emplacement { get; set; }
    public int NbrLed { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }

    public List<Couleur> PixelColors
    {
      get
      {
        List<Couleur> pixelColors = new List<Couleur>();

        foreach (Pixel pixel in this.OrderBy(p => p.Numero))
          pixelColors.Add(pixel.Couleur);

        return pixelColors;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="largeur"></param>
    /// <param name="hauteur"></param>
    public PixelList(int largeur, int hauteur)
    {
      Largeur = largeur;
      Hauteur = hauteur;
      NbrLed = Largeur * Hauteur;

      Emplacement = new Emplacement(Largeur, Hauteur);

      for (int i = 0; i < Largeur * Hauteur; i++)
        Add(new Pixel(i + 1, Emplacement.Convertion(i), new Coordonnee(Largeur, Hauteur, i)));
    }

    /// <summary>
    /// BackGround
    /// </summary>
    public void BackGround(int bg = 2)
    {
      switch (bg)
      {
        case 1:
          Random r = new Random();

          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir)
              pixel.SetColor(new Couleur { B = (byte)r.Next(5, 20) });
          break;

        case 2:
          int sec = DateTime.Now.Millisecond / 50;

          List<int> b = new List<int>
          {
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
            11, 10, 9, 8, 7, 6, 5, 4, 3, 2,
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
            11, 10, 9, 8, 7, 6, 5, 4, 3, 2
          };

          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir)
              pixel.SetColor(new Couleur { B = (byte)b[sec + pixel.Coord.Y] });
          break;

        default:
          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir)
              pixel.SetColor(new Couleur { B = (byte)(5 + pixel.Coord.Y * 2) });
          break;
      }
    }

    /// <summary>
    /// SetHorloge
    /// </summary>
    /// <returns></returns>
    public void SetHorloge()
    {
      Couleur minuteCouleur = new Couleur { R = 39 / 2, G = 144 / 2, B = 176 / 2 };//39,144,176
      Couleur heureCouleur = new Couleur { R = 148 / 2, G = 200 / 2, B = 80 / 2 };//148,186,101
      Couleur pointCouleur = new Couleur { R = 148 / 5, G = 200 / 5, B = 80 / 5 };//148,186,101

      //5 minutes
      for (int i = 0; i < 60; i += 5)
        GetCoordonnee(GetTempsCoord(i, 10)).SetColor(pointCouleur);

      GetCoordonnee(Coordonnee.Get(9, 0, Largeur, Hauteur)).SetColor(pointCouleur);
      GetCoordonnee(Coordonnee.Get(9, 19, Largeur, Hauteur)).SetColor(pointCouleur);

      BackGround();

      //Fond Centre 
      //for (int j = 1; j < 10; j++)
      //	for (int i = 1; i < 359; i += 4)
      //		GetCoordonnee(GetCercleCoord(Coordonnee.Get(10, 10, Largeur, Hauteur), i, j)).SetColor(new Color { R = (byte)(7 * j), G = (byte)(7 * j), B = (byte)(7 * j) });

      //Cadran
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
        leading = " ";

      Print(Coordonnee.Get(2, 13, Largeur, Hauteur), leading + hh + deuxPoint + DateTime.Now.ToString("mm"), new Couleur());

      //Aiguille
      int minute = DateTime.Now.Minute;
      int heure = DateTime.Now.Hour;

      for (int i = 0; i < 9; i++)
        GetCoordonnee(GetTempsCoord(DateTime.Now.Second + DateTime.Now.Millisecond / (double)1000, i)).SetColor(new Couleur { R = 80 / 2, G = 78 / 2, B = 114 / 2 });//43,78,114

      for (int i = 0; i < 8; i++)
        GetCoordonnee(GetTempsCoord(minute + DateTime.Now.Second * 1.6 / 100, i)).SetColor(minuteCouleur);

      for (int i = 0; i < 6; i++)
        GetCoordonnee(GetHeureCoord(heure, minute, i)).SetColor(heureCouleur);

      //for (int i = 0; i < 5; i++)
      //  GetCoordonnee(GetTempsCoord(DateTime.Now.Millisecond / (double)100 * 6, i)).SetColor(new Couleur { R = 255 / 2, G = 0 / 2, B = 0 / 2 });//43,78,114

      for (int i = 0; i < 9; i++)
        GetCoordonnee(GetTempsCoord((DateTime.Now.Millisecond / (double)100 * 6) - i, 9)).SetColor(new Couleur { R = new List<byte> { 128, 64, 32, 16, 8, 8, 8, 4, 4 }[i], B = 5 });

      //Print(Coordonnee.Get(1, 1, Largeur, Hauteur), DateTime.Now.Second.ToString(), new Color());
    }

    /// <summary>
    /// Reset
    /// </summary>
    public void Reset()
    {
      foreach (Pixel pixel in this)
        pixel.Couleur = Couleur.FromArgb(0, 0, 0);
    }

    /// <summary>
    /// Carre
    /// </summary>
    //public void Carre(int position, int size, Color couleur)
    //{
    //	for (int j = 0; j < size; j++)
    //		for (int i = position + (Largeur * j); i < size + position + (Largeur * j); i++)
    //			GetPosition(i).SetColor(couleur);
    //}

    /// <summary>
    /// SetCouleur
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    public void SetCouleur(Pixel pixel)
    {
      if (this.SingleOrDefault(p => p.Numero == pixel.Numero && p.Position == pixel.Position) is Pixel pix)
        pix.Couleur = pixel.Couleur;
    }

    /// <summary>
    /// GetPosition
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Pixel GetPosition(int position)
    {
      return this.SingleOrDefault(p => p.Position == position);
    }

    /// <summary>
    /// GetNumero
    /// </summary>
    /// <param name="numero"></param>
    /// <returns></returns>
    public Pixel GetNumero(int numero)
    {
      return this.SingleOrDefault(p => p.Numero == numero);
    }

    /// <summary>
    /// GetCoordonnee
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Pixel GetCoordonnee(int x, int y)
    {
      return this.SingleOrDefault(p => p.Coord.X == x && p.Coord.Y == y);
    }

    /// <summary>
    /// GetCoordonnee
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Pixel GetCoordonnee(decimal x, decimal y)
    {
      int xx = (int)Math.Round(x, 0);

      if (xx >= Largeur - 1)
        xx = Largeur - 1;

      if (xx < 0)
        xx = 0;

      int yy = (int)Math.Round(y, 0);

      if (yy >= Hauteur - 1)
        yy = Hauteur - 1;

      if (yy < 0)
        yy = 0;

      return this.SingleOrDefault(p => p.Coord.X == xx && p.Coord.Y == yy);
    }

    /// <summary>
    /// GetCoordonnee
    /// </summary>
    /// <param name="coordonnee"></param>
    /// <returns></returns>
    public Pixel GetCoordonnee(Coordonnee coordonnee)
    {
      return this.SingleOrDefault(p => p.Coord.X == coordonnee.X && p.Coord.Y == coordonnee.Y);
    }

    /// <summary>
    /// GetTempCoord
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    private Coordonnee GetTempsCoord(double temp, int rayon)
    {
      Coordonnee coord = new Coordonnee(Largeur, Hauteur);

      temp *= 6;   //each minute and second make 6 degree

      if (temp >= 0 && temp <= 180)
        coord.X = (Largeur / 2) + (int)(rayon * Math.Sin(Math.PI * temp / 180));
      else
        coord.X = (Largeur / 2) - (int)(rayon * -Math.Sin(Math.PI * temp / 180)) - 1;

      coord.Y = (Hauteur / 2) - (int)(rayon * Math.Cos(Math.PI * temp / 180) + 0.5);

      return coord.CheckCoord();
    }

    /// <summary>
    /// GetHeureCoord
    /// </summary>
    /// <param name="heure"></param>
    /// <param name="minute"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    private Coordonnee GetHeureCoord(int heure, int minute, int rayon)
    {
      Coordonnee coord = new Coordonnee(Largeur, Hauteur);

      //each hour makes 30 degree
      //each min makes 0.5 degree
      double val = (heure * 30) + (minute * 0.5);

      if (val >= 0 && val <= 180)
        coord.X = (Largeur / 2) + (int)(rayon * Math.Sin(Math.PI * val / 180));
      else
        coord.X = (Largeur / 2) - (int)(rayon * -Math.Sin(Math.PI * val / 180)) - 1;

      coord.Y = (Hauteur / 2) - (int)(rayon * Math.Cos(Math.PI * val / 180) + 0.5);

      return coord.CheckCoord();
    }

    /// <summary>
    /// GetCerlcleCoord
    /// </summary>
    /// <param name="degree"></param>
    /// <param name="rayon"></param>
    /// <returns></returns>
    public Coordonnee GetCercleCoord(Coordonnee centre, int degree, int rayon)
    {
      Coordonnee coord = new Coordonnee(Largeur, Hauteur);

      if (degree >= 0 && degree <= 180)
        coord.X = centre.X + (int)(rayon * Math.Sin(Math.PI * degree / 180));
      else
        coord.X = centre.X - (int)(rayon * -Math.Sin(Math.PI * degree / 180)) - 1;

      coord.Y = centre.Y - (int)(rayon * Math.Cos(Math.PI * degree / 180) + 0.5);

      return coord.CheckCoord();
    }

    /// <summary>
    /// Print
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="chaine"></param>
    /// <param name="couleur"></param>
    public void Print(Coordonnee coord, string chaine, Couleur couleur)
    {
      int y = coord.Y;

      for (int i = 0; i < chaine.Length; i++)
      {
        coord.Y = y;

        PrintLettre(coord, chaine[i], couleur);

        if (chaine[i] == ':' || chaine[i] == ' ' || chaine[i] == '.')
          coord.Droite(2);
        else if (chaine[i] == '-')
          coord.Droite(3);
        else
          coord.Droite(4);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="chaine"></param>
    /// <param name="couleur"></param>
    public void PrintText(Coordonnee coord, string chaine, Couleur couleur)
    {
      int y = coord.Y;

      for (int i = 0; i < chaine.Length; i++)
      {
        coord.Y = y;

        PrintLettre(coord, chaine[i], couleur);

        if (chaine[i] == ':' || chaine[i] == ' ' || chaine[i] == '.')
          coord.Droite(2);
        else if (chaine[i] == '-')
          coord.Droite(3);
        else
          coord.Droite(4);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="chaine"></param>
    /// <param name="couleur"></param>
    public void Print(int x, int y, string chaine, Couleur couleur)
    {
      Print(Coordonnee.Get(x, y, Largeur, Hauteur), chaine, couleur);
    }

    /// <summary>
    /// SetNouvelle
    /// </summary>
    public void SetNouvelle(PoliceList caracteres, int debut)
    {
      Couleur couleur = new Couleur { R = 64, G = 0, B = 0 };

      //Cadran
      string leading = "";
      string deuxPoint = " ";
      int heure = DateTime.Now.Hour;

      if (heure == 0)
        heure = 12;

      if (heure > 12)
        heure -= 12;

      if (DateTime.Now.Millisecond < 500)
        deuxPoint = ":";

      if (heure < 10)
        leading = " ";

      if (caracteres != null)
      {
        Couleur nouvelleCouleur = new Couleur { R = 127, G = 32, B = 32 };

        foreach (Police lettre in caracteres.Where(c => c.Point))
          if (GetCoordonnee(lettre.X, lettre.Y + 1) is Pixel pixel)
            pixel.SetColor(nouvelleCouleur);
      }

      Print(Coordonnee.Get(1, 8, Largeur, Hauteur), DateTime.Now.ToString("MM-dd"), couleur);
      Print(Coordonnee.Get(2, 14, Largeur, Hauteur), leading + heure + deuxPoint + DateTime.Now.ToString("mm"), couleur);

      BackGround();
    }

    /// <summary>
    /// SetMeteo
    /// </summary>
    /// <param name="temp"></param>
    public void SetMeteo(current meteo)
    {
      Couleur couleur = new Couleur { R = 64, G = 0, B = 0 };

      //Cadran
      string leading = "";
      string deuxPoint = " ";
      int heure = DateTime.Now.Hour;

      if (heure == 0)
        heure = 12;

      if (heure > 12)
        heure -= 12;

      if (DateTime.Now.Millisecond < 500)
        deuxPoint = ":";

      if (heure < 10)
        leading = " ";

      if (meteo != null)
        Print(Coordonnee.Get(4, 2, Largeur, Hauteur), meteo.temperature.value.ToString("0") + "°C", couleur);

      if (meteo != null)
        Print(Coordonnee.Get(2, 8, Largeur, Hauteur), "H " + meteo.humidity.value.ToString() + "%", couleur);

      Print(Coordonnee.Get(2, 14, Largeur, Hauteur), leading + heure + deuxPoint + DateTime.Now.ToString("mm"), couleur);

      BackGround();
    }

    /// <summary>
    /// PrintLettre
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="lettre"></param>
    private void PrintLettre(Coordonnee coord, char lettre, Couleur couleur)
    {
      Coordonnee coo = new Coordonnee(coord);

      switch (lettre)
      {
        case '0':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '1':
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(1)).SetColor(couleur);

          GetCoordonnee(coo.Droite(1).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '2':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '3':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;


        case '4':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          break;

        case '5':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '6':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '7':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          break;

        case '8':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '9':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);

          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);

          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          break;

        case 'C':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case '°':
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
          break;

        case ':':
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(2)).SetColor(couleur);
          break;

        case '.':
          GetCoordonnee(coo.Bas(4)).SetColor(couleur);
          break;

        case '%':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          break;

        case ' ':
          break;

        case '-':
          GetCoordonnee(coo.Bas(2)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          break;

        case 'H':
          GetCoordonnee(coo).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(1)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
          GetCoordonnee(coo.Droite(2)).SetColor(couleur);
          break;
      }
    }
  }
}