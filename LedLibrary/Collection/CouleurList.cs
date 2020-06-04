using LedLibrary.Entities;
using System;
using System.Collections.Generic;

namespace LedLibrary.Collection
{
	public class CouleurList : List<Couleur>
	{
		/// <summary>
		/// Constructeur
		/// </summary>
		public CouleurList()
		{ 
		
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pixel"></param>
		public CouleurList(byte[] pixels)
		{
			int position = 0;

			for (int i = 0; i < pixels.Length; i += 3)
				if (IsTransparent(pixels, i))
					Add(new Couleur(position++, Couleur.FromArgb(0, 0, 0)));
				else
					Add(new Couleur(position++, Couleur.FromArgb((byte)(pixels[i] / 2), (byte)(pixels[i + 1] / 2), (byte)(pixels[i + 2] / 2))));
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="result"></param>
		public CouleurList(List<byte[]> frames)
		{
			int frameCompteur = 0;

			foreach (byte[] frame in frames)
			{
				int position = 0;

				for (int i = 0; i < frame.Length; i += 3)
					if (IsTransparent(frame, i))
						Add(new Couleur(frameCompteur, position++, Couleur.FromArgb(0, 0, 0)));
					else
						Add(new Couleur(frameCompteur, position++, Couleur.FromArgb((byte)(frame[i] / 2), (byte)(frame[i + 1] / 2), (byte)(frame[i + 2] / 2))));

				frameCompteur++;
			}
		}

		/// <summary>
		/// IsTransparent
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		private bool IsTransparent(byte[] frame, int i)
		{
			bool isTransparent = frame[i] == 255 && frame[i + 1] == 255 && frame[i + 2] == 255;
			isTransparent |= frame[i] == 255 && frame[i + 1] == 0 && frame[i + 2] == 128;
			isTransparent |= frame[i] == 0 && frame[i + 1] == 136 && frame[i + 2] == 255;

			return isTransparent;
		}
	}
}