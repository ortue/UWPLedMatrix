using Windows.UI;
using Windows.UI.Xaml.Input;

namespace Library.Entities
{
	public class Couleur
	{
		public int FrameCompteur { get; set; }
		public int Position { get; set; }
		public Color Color { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="position"></param>
		/// <param name="color"></param>
		public Couleur(int position, Color color)
		{
			Position = position;
			Color = color;
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="frameCompteur"></param>
		/// <param name="color"></param>
		public Couleur(int frameCompteur, int position, Color color)
		{
			FrameCompteur = frameCompteur;
			Position = position;
			Color = color;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Color Get(int r, int g, int b)
		{
			return new Color { R = (byte)r, G = (byte)g, B = (byte)b };
		}
	}
}