﻿using LedLibrary.Classes;
using LedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedLibrary.Collection
{
  public class PixelList : List<Pixel>
  {
    public Emplacement Emplacement { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public double[,] PlasmaArray { get; set; }
    public double HueShift { get; set; }

    public int NbrBackground
    {
      get { return 5; }
    }

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
      Emplacement = new Emplacement(Largeur, Hauteur);

      for (int i = 0; i < Largeur * Hauteur; i++)
        Add(new Pixel(i + 1, Emplacement.Convertion(i), new Coordonnee(Largeur, Hauteur, i)));
    }

    /// <summary>
    /// BackGround
    /// </summary>
    public decimal BackGround(int bg = 2, decimal cycle = 0, bool reverse = false)
    {
      switch (bg)
      {
        case 1:
          int max = 20;

          if (reverse)
            max = 127;

          Random r = new Random();

          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir != reverse)
              pixel.SetColor(new Couleur { B = (byte)r.Next(5, max) });
          break;

        case 2:
          List<int> b = new List<int>
          {
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
            11, 10, 9, 8, 7, 6, 5, 4, 3, 2,
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
            11, 10, 9, 8, 7, 6, 5, 4, 3, 2
          };

          int sec = DateTime.Now.Millisecond / 50;
          int multi = 1;

          if (reverse)
            multi = 3;

          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir != reverse)
              //pixel.SetColor(new Couleur { B = (byte)(b[sec + pixel.Coord.Y] * multi) });
              pixel.SetColor(Couleur.Get(0, 0, b[sec + pixel.Coord.Y] * multi));

          break;

        case 3:
          List<int> g = new List<int>
          {
            3, 0, 0, 0, 0, 1, 0, 0, 0, 6,
            0, 0, 0, 3, 0, 0, 0, 0, 0, 1,
            0, 0, 0, 1, 0, 0, 0, 0, 0, 6,
            0, 3, 0, 0, 0, 0, 0, 6, 0, 0
          };

          int milli = DateTime.Now.Millisecond % 20;

          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir != reverse)
              pixel.SetColor(new Couleur { G = (byte)g[milli + pixel.Coord.Y] });
          break;

        case 4:
          int alpha = 32;

          if (reverse)
            alpha = 127;

          cycle = Plasma(alpha, cycle, reverse);
          break;

        default:
          foreach (Pixel pixel in this)
            if (pixel.Couleur.IsNoir != reverse)
              pixel.SetColor(new Couleur { B = (byte)(5 + pixel.Coord.Y * 2) });
          break;
      }

      return cycle;
    }

    /// <summary>
    /// SetHorloge
    /// </summary>
    /// <returns></returns>
    public decimal SetHorloge(PoliceList caracteres, decimal cycle, int bg)
    {
      Couleur minuteCouleur = new Couleur { R = 39 / 2, G = 144 / 2, B = 176 / 2 };//39,144,176
      Couleur heureCouleur = new Couleur { R = 148 / 2, G = 200 / 2, B = 80 / 2 };//148,186,101
      Couleur pointCouleur = new Couleur { R = 148 / 5, G = 200 / 5, B = 80 / 5 };//148,186,101

      //5 minutes
      for (int i = 0; i < 60; i += 5)
        GetCoordonnee(GetTempsCoord(i, 10)).SetColor(pointCouleur);

      GetCoordonnee(Coordonnee.Get(9, 0, Largeur, Hauteur)).SetColor(pointCouleur);
      GetCoordonnee(Coordonnee.Get(9, 19, Largeur, Hauteur)).SetColor(pointCouleur);

      cycle = BackGround(bg, cycle);

      Print(caracteres, 1, 13, Couleur.Noir);

      //Aiguille
      int minute = DateTime.Now.Minute;
      int heure = DateTime.Now.Hour;

      for (int i = 0; i < 9; i++)
        GetCoordonnee(GetTempsCoord(DateTime.Now.Second + DateTime.Now.Millisecond / (double)1000, i)).SetColor(new Couleur { R = 80 / 2, G = 78 / 2, B = 114 / 2 });//43,78,114

      for (int i = 0; i < 8; i++)
        GetCoordonnee(GetTempsCoord(minute + DateTime.Now.Second * 1.6 / 100, i)).SetColor(minuteCouleur);

      for (int i = 0; i < 6; i++)
        GetCoordonnee(GetHeureCoord(heure, minute, i)).SetColor(heureCouleur);

      for (int i = 0; i < 9; i++)
        GetCoordonnee(GetTempsCoord((DateTime.Now.Millisecond / (double)100 * 6) - i, 9)).SetColor(new Couleur { R = new List<byte> { 128, 64, 32, 16, 8, 8, 8, 4, 4 }[i], B = 5 });

      return cycle;
    }

    /// <summary>
    /// SetDate
    /// </summary>
    /// <param name="policeLists"></param>
    /// <param name="heure"></param>
    public void SetDate(PoliceList caracteres)
    {
      Couleur couleur = Couleur.Get(0, 0, 127);
      Print(DateTime.Now.ToString("yyyy"), 4, 1, couleur);
      Print(DateTime.Now.ToString("MM-dd"), 1, 7, couleur);
      Print(caracteres, 2, 13, couleur);
    }

    /// <summary>
    /// SetBinaire
    /// </summary>
    /// <param name="caracteres"></param>
    public void SetBinaire(PoliceList caracteres)
    {
      Couleur couleur = Couleur.Get(31, 64, 127);
      Couleur noir = Couleur.Get(1, 1, 1);

      string heure = Convert.ToString(DateTime.Now.Hour, 2).PadLeft(6, '0');
      string minute = Convert.ToString(DateTime.Now.Minute, 2).PadLeft(6, '0');
      string seconde = Convert.ToString(DateTime.Now.Second, 2).PadLeft(6, '0');

      for (int h = 0; h < 6; h++)
        if (heure[h] == '1')
        {
          GetCoordonnee(Coordonnee.Get(2, h * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
          GetCoordonnee(Coordonnee.Get(3, h * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
          GetCoordonnee(Coordonnee.Get(4, h * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
        }
        else
        {
          GetCoordonnee(Coordonnee.Get(2, h * 2 + 1, Largeur, Hauteur)).SetColor(noir);
          GetCoordonnee(Coordonnee.Get(3, h * 2 + 1, Largeur, Hauteur)).SetColor(noir);
          GetCoordonnee(Coordonnee.Get(4, h * 2 + 1, Largeur, Hauteur)).SetColor(noir);
        }

      for (int m = 0; m < 6; m++)
        if (minute[m] == '1')
        {
          GetCoordonnee(Coordonnee.Get(9, m * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
          GetCoordonnee(Coordonnee.Get(10, m * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
          GetCoordonnee(Coordonnee.Get(11, m * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
        }
        else
        {
          GetCoordonnee(Coordonnee.Get(9, m * 2 + 1, Largeur, Hauteur)).SetColor(noir);
          GetCoordonnee(Coordonnee.Get(10, m * 2 + 1, Largeur, Hauteur)).SetColor(noir);
          GetCoordonnee(Coordonnee.Get(11, m * 2 + 1, Largeur, Hauteur)).SetColor(noir);
        }

      for (int s = 0; s < 6; s++)
        if (seconde[s] == '1')
        {
          GetCoordonnee(Coordonnee.Get(16, s * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
          GetCoordonnee(Coordonnee.Get(17, s * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
          GetCoordonnee(Coordonnee.Get(18, s * 2 + 1, Largeur, Hauteur)).SetColor(couleur);
        }
        else
        {
          GetCoordonnee(Coordonnee.Get(16, s * 2 + 1, Largeur, Hauteur)).SetColor(noir);
          GetCoordonnee(Coordonnee.Get(17, s * 2 + 1, Largeur, Hauteur)).SetColor(noir);
          GetCoordonnee(Coordonnee.Get(18, s * 2 + 1, Largeur, Hauteur)).SetColor(noir);
        }

      Print(caracteres, 2, 14, couleur);
    }

    /// <summary>
    /// Reset
    /// </summary>
    public void Reset()
    {
      foreach (Pixel pixel in this)
        pixel.Couleur = new Couleur();
    }

    /// <summary>
    /// GetCoordonnee
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Pixel GetCoordonnee(int x, int y)
    {
      return Find(p => p.Coord.X == x && p.Coord.Y == y);
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

      return Find(p => p.Coord.X == xx && p.Coord.Y == yy);
    }

    /// <summary>
    /// GetCoordonnee
    /// </summary>
    /// <param name="coordonnee"></param>
    /// <returns></returns>
    public Pixel GetCoordonnee(Coordonnee coordonnee)
    {
      return Find(p => p.Coord.X == coordonnee.X && p.Coord.Y == coordonnee.Y);
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
    public Coordonnee GetCercleCoord(Coordonnee centre, int degree, double rayon)
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
    /// SetNouvelle
    /// </summary>
    public void SetNouvelle(PoliceList caracteres, string heure)
    {
      Couleur couleur = Couleur.Get(64, 0, 0);
      Print(caracteres, 0, 1, Couleur.Get(32, 32, 127));
      Print(DateTime.Now.ToString("MM-dd"), 1, 7, couleur);
      Print(heure, 2, 13, couleur);
      BackGround();
    }

    /// <summary>
    /// SetMeteo
    /// </summary>
    /// <param name="temp"></param>
    public void SetMeteo(current meteo, string heure)
    {
      Couleur couleur = new Couleur { R = 64, G = 0, B = 0 };

      if (meteo != null)
      {
        string leading = "";

        if (meteo.temperature.value.ToString("0").Length < 2)
          leading = "  ";

        Print(leading + meteo.temperature.value.ToString("0") + "°C", 1, 1, couleur);
        Print("H " + meteo.humidity.value.ToString() + "%", 2, 7, couleur);
      }

      Print(heure, 2, 13, couleur);
      BackGround();
    }

    /// <summary>
    /// Print
    /// </summary>
    /// <param name="caracteres"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="couleur"></param>
    public void Print(PoliceList caracteres, int x, int y, Couleur couleur)
    {
      if (caracteres != null)
        foreach (Police lettre in caracteres.Where(c => c.Point))
          if (GetCoordonnee(lettre.X + x, lettre.Y + y) is Pixel pixel)
            pixel.SetColor(couleur);
    }

    /// <summary>
    /// Print
    /// </summary>
    /// <param name="texte"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="couleur"></param>
    public void Print(string texte, int x, int y, Couleur couleur)
    {
      CaractereList textes = new CaractereList(Largeur);
      textes.SetText(texte);
      Print(textes.GetCaracteres(), x, y, couleur);
    }

    /// <summary>
    /// Plasma
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="cycle"></param>
    public decimal Plasma(int alpha, decimal cycle = 0, bool reverse = false)
    {
      //int multi = 49;
      int multi = 24;
      HueShift = (HueShift + 0.02) % 1;

      if (PlasmaArray == null)
      {
        PlasmaArray = new double[Largeur * (multi + 1), Hauteur];

        for (int y = 0; y < Hauteur; y++)
          for (int x = 0; x < Largeur * (multi + 1); x++)
          {
            double value = Math.Sin(x / 8.0);
            value += Math.Sin(y / 4.0);
            value += Math.Sin((x + y) / 8.0);
            value += Math.Sin(Math.Sqrt(x * x + y * y) / 4.0);
            value += 4; // shift range from -4 .. 4 to 0 .. 8
            value /= 8; // bring range down to 0 .. 1

            PlasmaArray[x, y] = value;
          }
      }

      cycle += (decimal)0.5;
      //cycle += 1;

      int offset = (int)(cycle % (Largeur * multi));

      if ((int)(cycle / (Largeur * multi)) % 2 == 1)
        offset = (int)(Largeur * multi - cycle % (Largeur * multi));

      for (int y = 0; y < Hauteur; y++)
        for (int x = 0; x < Largeur; x++)
          if (GetCoordonnee(x, y) is Pixel pixel)
            if (pixel.Couleur.IsNoir != reverse)
            {
              double hue = HueShift % 1 + PlasmaArray[x + offset, y] % 1;
              Couleur couleur = HSVtoRGB(hue, 1, 1, alpha);
              pixel.SetColor(couleur);
            }

      return cycle;
    }

    /// <summary>
    /// HSVtoRGB
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    private Couleur HSVtoRGB(double h, int s, int v, int alpha)
    {
      int i = (int)Math.Floor(h * 6);
      double f = h * 6 - i;
      int p = v * (1 - s);
      double q = v * (1 - f * s);
      double t = v * (1 - (1 - f) * s);

      return (i % 6) switch
      {
        0 => Couleur.Get(v * alpha, (int)Math.Round(t * alpha), p * alpha),
        1 => Couleur.Get((int)Math.Round(q * alpha), v * alpha, p * alpha),
        2 => Couleur.Get(p * alpha, v * alpha, (int)Math.Round(t * alpha)),
        3 => Couleur.Get(p * alpha, (int)Math.Round(q * alpha), v * alpha),
        4 => Couleur.Get((int)Math.Round(t * alpha), p * alpha, v * alpha),
        _ => Couleur.Get(v * alpha, p * alpha, (int)Math.Round(q * alpha))
      };
    }
  }
}