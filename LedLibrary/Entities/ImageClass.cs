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

      using (Image image = Image.FromFile(FileName))
      {
        Height = image.Height;
        Width = image.Width;

        FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
        FrameCount = image.GetFrameCount(dimension);
      }
    }

    /// <summary>
    /// Parses individual Bitmap frames from a multi-frame Bitmap into an array of Bitmaps
    /// </summary>
    /// <param name="Animation"></param>
    /// <returns></returns>
    private Bitmap[] ParseFrames(Bitmap Animation)
    {
      // Allocate a Bitmap array to hold individual frames from the animation
      Bitmap[] Frames = new Bitmap[FrameCount];

      // Copy the animation Bitmap frames into the Bitmap array
      for (int Index = 0; Index < FrameCount; Index++)
      {
        // Set the current frame within the animation to be copied into the Bitmap array element
        Animation.SelectActiveFrame(FrameDimension.Time, Index);

        // Create a new Bitmap element within the Bitmap array in which to copy the next frame
        Frames[Index] = new Bitmap(Animation.Size.Width, Animation.Size.Height);

        // Copy the current animation frame into the new Bitmap array element
        Graphics.FromImage(Frames[Index]).DrawImage(Animation, new Point(0, 0));
      }

      return Frames;
    }

    /// <summary>
    /// BitmapToByte
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public byte[] BitmapToByte(Bitmap bitmap)
    {
      int i = 0;
      byte[] octets = new byte[NbrByte];
      //Bitmap bitmap = new Bitmap((Image)image.Clone());

      for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
        {
          Color color = bitmap.GetPixel(x, y);

          if (octets.Count() > i)
          {
            //int argb = 0;

            //frame[i++] = color.A;
            octets[i++] = color.R;
            octets[i++] = color.G;
            octets[i++] = color.B;
          }
        }

      return octets;
    }

    /// <summary>
    /// SetÞixelFrame
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="pixels"></param>
    public void SetÞixelFrame(int frame, PixelList pixels, int slide, bool fadeOut)
    {
      using (Image image = Image.FromFile(FileName))
      {
        List<byte[]> frames = new List<byte[]>();

        if (FrameCount > 1)
        {
          foreach (Bitmap bitmap in ParseFrames((Bitmap)image))
            frames.Add(BitmapToByte(bitmap));
        }
        else
          frames.Add(BitmapToByte((Bitmap)image));

        Couleurs = new CouleurList(frames);
      }

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