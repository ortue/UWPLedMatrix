﻿using LedLibrary.Collection;
using LedLibrary.Entities;
using WebMatrix.Classes;

namespace WebMatrix.Context
{
	public class LedMatrixContext
	{
		public PixelList Pixels { get; set; }
		public LedStripAPA102C PixelStrip { get; set; }
		public AnimationList Animations { get; set; }
		public AnimationList MeteoImgs { get; set; }
		public current Meteo { get; set; }
		public bool Autorun { get; set; }

		/// <summary>
		/// Nombre de Led de Largeur par defaut
		/// </summary>
		public int Largeur
		{
			get { return 20; }
		}

		/// <summary>
		/// Nombre de Led de Longeur par defaut
		/// </summary>
		public int Hauteur
		{
			get { return 20; }
		}

		/// <summary>
		/// Nombre de Led par defaut
		/// </summary>
		public int NbrLed
		{
			get { return Largeur * Hauteur; }
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		public LedMatrixContext()
		{
			PixelStrip = new LedStripAPA102C(NbrLed);
			Pixels = new PixelList(Largeur, Hauteur);

			//Util.SetAnimation();
			//Util.SetMeteoImg();
			//Util.UpdateMeteo();

			//Animations = new AnimationList("Images");
			//MeteoImgs = new AnimationList("MeteoImg");
		}
	}
}