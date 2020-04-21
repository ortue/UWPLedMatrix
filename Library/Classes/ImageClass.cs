using Library.Collection;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace Library.Classes
{
	public class ImageClass
	{
		public int Pixels
		{
			get { return Height * Width; }
		}

		public int Height { get; set; }
		public int Width { get; set; }
		public int FrameCount { get; set; }
		public CouleurList Couleurs { get; set; }

		public bool Animation
		{
			get { return FrameCount > 1; }
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="path"></param>
		public ImageClass(string fileNameOfImage)
		{
			Task<BitmapDecoder> bitmap = Task.Run(async () => await BitmapDecoderGet(fileNameOfImage));

			FrameCount = (int)bitmap.Result.FrameCount;

			if (Animation)
			{
				Task<List<byte[]>> pixel = Task.Run(async () => await GetPixelFrame(bitmap.Result));
				Couleurs = new CouleurList(pixel.Result);
			}
			else
			{
				Task<byte[]> pixel = Task.Run(async () => await GetPixel(bitmap.Result));
				Couleurs = new CouleurList(pixel.Result);
			}

			Height = (int)bitmap.Result.PixelHeight;
			Width = (int)bitmap.Result.PixelWidth;
		}

		/// <summary>
		/// Get Bitmap
		/// </summary>
		/// <param name="fileNameOfImage"></param>
		/// <returns></returns>
		public async Task<BitmapDecoder> BitmapDecoderGet(string fileNameOfImage)
		{
			StorageFile imageFile = await Package.Current.InstalledLocation.GetFileAsync(fileNameOfImage);
			Stream imagestream = await imageFile.OpenStreamForReadAsync();

			return await BitmapDecoder.CreateAsync(imagestream.AsRandomAccessStream()); // Stream dekodieren
		}

		/// <summary>
		/// GetPixel
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		public async Task<byte[]> GetPixel(BitmapDecoder bitmap)
		{
			PixelDataProvider imagePixelData = await bitmap.GetPixelDataAsync();
			return imagePixelData.DetachPixelData();
		}

		/// <summary>
		/// GetPixelFrame
		/// </summary>
		/// <param name="bitmapDecoder"></param>
		/// <returns></returns>
		public async Task<List<byte[]>> GetPixelFrame(BitmapDecoder bitmap)
		{
			List<byte[]> frames = new List<byte[]>();

			for (uint f = 0; f < FrameCount; f++)
			{
				BitmapFrame frame = await bitmap.GetFrameAsync(f).AsTask().ConfigureAwait(false);
				PixelDataProvider pixelData = await frame.GetPixelDataAsync().AsTask().ConfigureAwait(false);
				frames.Add(pixelData.DetachPixelData());
			}

			return frames;
		}

		/// <summary>
		/// Mapping de pixel
		/// </summary>
		/// <param name="pixels"></param>
		/// <param name="slide">Slider l'image de la droite a la gauche</param>
		public void SetÞixel(PixelList pixels, int slide = 0)
		{
			int heightOffset = (pixels.Hauteur - Height) / 2;
			int widthOffset = (pixels.Largeur - Width) / 2;


			//int test = 0;
			//if (slide > 0)
			//	test = 1;

			int newLine = pixels.Largeur - Width;


			int pixelOffset = heightOffset * pixels.Largeur + widthOffset + 1;

			foreach (Couleur couleur in Couleurs)
			{
				if (pixels.SingleOrDefault(p => p.Position == couleur.Position + pixelOffset + slide) is Pixel pixel)
					pixel.Couleur = couleur.Color;

				//Changement de ligne
				if (couleur.Position % Width == Width - 1)
					pixelOffset += newLine;
			}
		}

		/// <summary>
		/// SetÞixelFrame
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="pixels"></param>
		public void SetÞixelFrame(int frame, PixelList pixels)
		{
			int heightOffset = (pixels.Hauteur - Height) / 2;
			int widthOffset = (pixels.Largeur - Width) / 2;
			int newLine = pixels.Largeur - Width;
			int pixelOffset = heightOffset * pixels.Largeur + widthOffset + 1;
			int frameCourant = frame % FrameCount;

			foreach (Couleur couleur in Couleurs.Where(c => c.FrameCompteur == frameCourant))
			{
				if (pixels.SingleOrDefault(p => p.Position == couleur.Position + pixelOffset) is Pixel pixel)
					pixel.Couleur = couleur.Color;

				//Changement de ligne
				if (couleur.Position % Width == Width - 1)
					pixelOffset += newLine;
			}
		}
	}
}