using System;

namespace Library.Classes
{
	public class Coordonnee
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int MaxX { get; set; }
		public int MaxY { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="largeur"></param>
		/// <param name="hauteur"></param>
		/// <param name="i"></param>
		public Coordonnee(int largeur, int hauteur, int i)
		{
			MaxX = largeur;
			MaxY = hauteur;

			X = i % largeur;
			Y = Convert.ToInt32(Math.Floor((decimal)(i / MaxY)));
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		public Coordonnee()
		{

		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="largeur"></param>
		/// <param name="hauteur"></param>
		public Coordonnee(int largeur, int hauteur)
		{
			MaxX = largeur;
			MaxY = hauteur;
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="coord"></param>
		public Coordonnee(Coordonnee coord)
		{
			X = coord.X;
			Y = coord.Y;
			MaxX = coord.MaxX;
			MaxY = coord.MaxY;
		}

		/// <summary>
		/// Get
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static Coordonnee Get(int x, int y, int largeur, int hauteur)
		{
			return new Coordonnee { X = x, Y = y, MaxX = largeur, MaxY = hauteur };
		}

		/// <summary>
		/// Droite
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public Coordonnee Droite(int x)
		{
			X += x;

			return CheckCoord();
		}

		/// <summary>
		/// Gauche
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public Coordonnee Gauche(int x)
		{
			X -= x;

			return CheckCoord();
		}

		/// <summary>
		/// Bas
		/// </summary>
		/// <param name="y"></param>
		/// <returns></returns>
		public Coordonnee Bas(int y)
		{
			Y += y;

			return CheckCoord();
		}

		/// <summary>
		/// CheckCoord
		/// </summary>
		/// <param name="coord"></param>
		/// <returns></returns>
		public Coordonnee CheckCoord()
		{
			if (X < 0)
				X = 0;

			if (Y < 0)
				Y = 0;

			if (X > MaxX - 1)
				X = MaxX - 1;

			if (Y > MaxY - 1)
				Y = MaxY - 1;

			return this;
		}
	}
}
