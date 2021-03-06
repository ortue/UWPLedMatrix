﻿using Library.Classes;
using System.ComponentModel;

namespace Library.Entities
{
	public class Pixel : INotifyPropertyChanged
	{
		public int Numero { get; set; }
		public int Position { get; set; }
		public Coordonnee Coord { get; set; }

		//private byte _alpha;
		//public byte Alpha
		//{
		//	get { return _alpha; }
		//	set
		//	{
		//		_alpha = value;
		//		OnPropertyChanged("Alpha");
		//		OnPropertyChanged("Couleur");
		//	}
		//}

		private byte _red;
		public byte Red
		{
			get { return _red; }
			set
			{
				_red = value;
				OnPropertyChanged("Red");
				OnPropertyChanged("Couleur");
			}
		}

		private byte _green;
		public byte Green
		{
			get { return _green; }
			set
			{
				_green = value;
				OnPropertyChanged("Green");
				OnPropertyChanged("Couleur");
			}
		}

		private byte _blue;
		public byte Blue
		{
			get { return _blue; }
			set
			{
				_blue = value;
				OnPropertyChanged("Blue");
				OnPropertyChanged("Couleur");
			}
		}

		public Color Couleur
		{
			get { return Color.FromArgb(Red, Green, Blue); }
			set
			{
				//Alpha = value.A;
				Red = value.R;
				Green = value.G;
				Blue = value.B;

				OnPropertyChanged("Couleur");

				//OnPropertyChanged("Alpha");
				OnPropertyChanged("Red");
				OnPropertyChanged("Green");
				OnPropertyChanged("Blue");
			}
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="position"></param>
		/// <param name="numero"></param>
		/// <param name="coord"></param>
		public Pixel(int position, int numero, Coordonnee coord)
		{
			Mapping(position, numero, coord);
		}

		/// <summary>
		/// Mapping
		/// </summary>
		/// <param name="position"></param>
		private void Mapping(int position, int numero, Coordonnee coord)
		{
			Position = position;
			Numero = numero;
			Coord = coord;
		}

		/// <summary>
		/// SetColor rien
		/// </summary>
		public void SetColor()
		{
			Couleur = new Color();
		}

		/// <summary>
		/// SetColor
		/// </summary>
		/// <param name="couleur"></param>
		public void SetColor(Color couleur)
		{
			Couleur = couleur;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public void Set(int r, int g, int b)
		{
			Couleur = new Color { R = (byte)r, G = (byte)g, B = (byte)b };
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}