//using LedLibrary.Collection;
//using LedLibrary.Entities;
//using System;
//using System.Linq.Expressions;

//namespace LedLibrary.Classes
//{
//  public class CharToPixel
//  {
//    /// <summary>
//    /// PoliceList
//    /// </summary>
//    /// <param name="lettre"></param>
//    /// <returns></returns>
//    public static PoliceList GetPolice(char lettre)
//    {
//      switch (lettre)
//      {
//        case 'A':
//          return A(GetLargeur(lettre));
//      }

//      return null;
//    }

//    /// <summary>
//    /// GetLargeur
//    /// </summary>
//    /// <param name="lettre"></param>
//    /// <returns></returns>
//    public static int GetLargeur(char lettre)
//    {
//      switch (lettre)
//      {
//        case 'A':
//          return 3;
//      }

//      return 0;
//    }

//    /// <summary>
//    /// A
//    /// </summary>
//    /// <param name="coord"></param>
//    public static PoliceList A(int largeur)
//    {
//      PoliceList polices = new PoliceList(largeur);

//      polices.Add(new Police { });


//      return polices;
//    }



//    private void PrintLettre(Coordonnee coord, char lettre, Couleur couleur)
//    {
//      Coordonnee coo = new Coordonnee(coord);

//      //switch (lettre)
//      //{
//      //  case '0':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '1':
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Droite(1).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '2':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '3':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;


//      //  case '4':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    break;

//      //  case '5':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '6':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '7':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    break;

//      //  case '8':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '9':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);

//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);

//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    break;

//      //  case 'C':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case '°':
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
//      //    break;

//      //  case ':':
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(2)).SetColor(couleur);
//      //    break;

//      //  case '.':
//      //    GetCoordonnee(coo.Bas(4)).SetColor(couleur);
//      //    break;

//      //  case '%':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(1).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    break;

//      //  case ' ':
//      //    break;

//      //  case '-':
//      //    GetCoordonnee(coo.Bas(2)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    break;

//      //  case 'H':
//      //    GetCoordonnee(coo).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    GetCoordonnee(coo.Gauche(2).Bas(1)).SetColor(couleur);
//      //    GetCoordonnee(coo.Droite(2)).SetColor(couleur);
//      //    break;
//      //}
//    }

//  }
//}
