using LedLibrary.Collection;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace LedLibrary.Entities
{
  public class ImageClass
  {
    public string FileName { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public CouleurList Couleurs { get; set; }

    public string FileNameID
    {
      get { return Path.GetFileNameWithoutExtension(FileName); }
    }

    public int NbrByte
    {
      get { return Width * Height * 3; }
    }

    public bool Animation
    {
      get { return FrameCount > 1; }
    }

    private int _frameCount;
    public int FrameCount
    {
      get
      {
        if (_frameCount == 0)
          return 1;

        return _frameCount;
      }
      set { _frameCount = value; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="path"></param>
    public ImageClass(string fileNameOfImage)
    {
      FileName = fileNameOfImage;

      using (Image image = Image.FromFile(fileNameOfImage))
      {
        //Size size = new Size(image.Width, image.Height);
        Height = image.Height;
        Width = image.Width;

        if (ImageAnimator.CanAnimate(image))
        {
          FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
          FrameCount = image.GetFrameCount(dimension);

          List<byte[]> frames = new List<byte[]>();

          for (int i = 0; i < FrameCount; i++)
            frames.Add(BitmapToByte(image));

          Couleurs = new CouleurList(frames);
        }
        else
          Couleurs = new CouleurList(BitmapToByte(image));
      }
    }

    /// <summary>
    /// BitmapToByte
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public byte[] BitmapToByte(Image image)
    {
      int i = 0;
      byte[] frame = new byte[NbrByte];
      Bitmap bitmap = new Bitmap((Image)image.Clone());

      for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
        {
          Color color = bitmap.GetPixel(x, y);

          if (frame.Count() > i)
          {
            //int argb = 0;

            //frame[i++] = color.A;
            frame[i++] = color.R;
            frame[i++] = color.G;
            frame[i++] = color.B;
          }
        }

      return frame;
    }

    /// <summary>
    /// SetÞixelFrame
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="pixels"></param>
    public void SetÞixelFrame(int frame, PixelList pixels, int slide, bool fadeOut = false)
    {
      int heightOffset = (pixels.Hauteur - Height) / 2;
      int widthOffset = (pixels.Largeur - Width) / 2;
      int newLine = pixels.Largeur - Width;
      int pixelOffset = heightOffset * pixels.Largeur + widthOffset + 1;
      int frameCourant = frame % FrameCount;

      foreach (Couleur couleur in Couleurs.Where(c => c.FrameCompteur == frameCourant))
      {
        if (fadeOut)
        {
          if (couleur.Position % Width >= slide) // Pour faire l'effet du slide vers la gauche
            if (pixels.SingleOrDefault(p => p.Position == couleur.Position + pixelOffset - slide) is Pixel pixel)
              pixel.Couleur = couleur;
        }
        else
        {
          if (couleur.Position % Width < Width - slide) // Pour faire l'effet du slide de la droite
            if (pixels.SingleOrDefault(p => p.Position == couleur.Position + pixelOffset + slide) is Pixel pixel)
              pixel.Couleur = couleur;
        }

        //Changement de ligne
        if (couleur.Position % Width == Width - 1)
          pixelOffset += newLine;
      }
    }
  }
}