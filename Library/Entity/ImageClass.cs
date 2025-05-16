using Library.Collection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Library.Entity
{
  public class ImageClass
  {
    public string FileName { get; set; }
    public string PathWeb { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public CouleurList? Couleurs { get; set; }

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
    public ImageClass(string fileNameOfImage, string pathWeb)
    {
      FileName = fileNameOfImage;
      PathWeb = pathWeb + "/" + Path.GetFileName(FileName);

      using Image<Rgba32> image = Image.Load<Rgba32>(FileName);
      Height = image.Height;
      Width = image.Width;

      //FrameDimension dimension = new(image.FrameDimensionsList[0]);
      FrameCount = image.Frames.Count;
    }

    //private Bitmap[] ParseFrames(Bitmap Animation)
    //{
    //  // Allocate a Bitmap array to hold individual frames from the animation
    //  Bitmap[] Frames = new Bitmap[FrameCount];

    //  // Copy the animation Bitmap frames into the Bitmap array
    //  for (int Index = 0; Index < FrameCount; Index++)
    //  {
    //    // Set the current frame within the animation to be copied into the Bitmap array element
    //    Animation.SelectActiveFrame(FrameDimension.Time, Index);

    //    // Create a new Bitmap element within the Bitmap array in which to copy the next frame
    //    Frames[Index] = new Bitmap(Animation.Size.Width, Animation.Size.Height);

    //    // Copy the current animation frame into the new Bitmap array element
    //    Graphics.FromImage(Frames[Index]).DrawImage(Animation, new Point(0, 0));
    //  }

    //  return Frames;
    //}


    //public byte[] BitmapToByte(ImageFrame bitmap)
    //{
    //  int i = 0;
    //  byte[] octets = new byte[NbrByte];

    //  for (int y = 0; y < Height; y++)
    //    for (int x = 0; x < Width; x++)
    //    {
    //      SixLabors.ImageSharp.Color color = bitmap[x, y];

    //      if (octets.Length > i)
    //      {
    //        octets[i++] = color.R;
    //        octets[i++] = color.G;
    //        octets[i++] = color.B;
    //      }
    //    }

    //  return octets;
    //}

    /// <summary>
    /// BitmapToByte
    /// </summary>
    /// <param name="imageFrame"></param>
    /// <returns></returns>
    private byte[] BitmapToByte(Image<Rgba32> imageFrame)
    {
      int i = 0;
      byte[] octets = new byte[NbrByte];

      for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
        {
          if (octets.Length > i)
          {
            Rgba32 pixel = imageFrame[x, y];

            octets[i++] = pixel.R;
            octets[i++] = pixel.G;
            octets[i++] = pixel.B;
          }
        }

      return octets;
    }

    /// <summary>
    /// SetÞixelFrame
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="pixels"></param>
    public void SetPixelFrame(int frame, PixelList pixels, int slide, bool fadeOut)
    {
      using Image<Rgba32> image = Image.Load<Rgba32>(FileName);
      List<byte[]> frames = [];

      //if (FrameCount > 1)
      //{
      for (int i = 0; i < image.Frames.Count; i++)
      {
        Image<Rgba32> imageFrame = image.Frames.CloneFrame(i);

        frames.Add(BitmapToByte(imageFrame));
      }


      //foreach (Bitmap bitmap in ParseFrames((Bitmap)image))
      //foreach (ImageFrame imageFrame in image.Frames)
      //frames.Add(BitmapToByte(imageFrame));
      //}
      //else
      //{
      //  Image<Rgba32> imageFrame = image.Frames.CloneFrame(0);

      //  frames.Add(BitmapToByte(imageFrame));
      //}

      Couleurs = new CouleurList(frames);


      int heightOffset = (PixelList.Hauteur - Height) / 2;
      int widthOffset = (PixelList.Largeur - Width) / 2;
      int newLine = PixelList.Largeur - Width;
      int pixelOffset = heightOffset * PixelList.Largeur + widthOffset + 1;
      int frameCourant = frame % FrameCount;

      foreach (Couleur couleur in Couleurs.Where(c => c.FrameCompteur == frameCourant))
      {
        if (fadeOut)
        {
          if (couleur.Position % Width >= slide) // Pour faire l'effet du slide vers la gauche
            if (pixels.Find(p => p.Position == couleur.Position + pixelOffset - slide) is Pixel pixel)
              pixel.Couleur = couleur;
        }
        else
        {
          if (couleur.Position % Width < Width - slide) // Pour faire l'effet du slide de la droite
            if (pixels.Find(p => p.Position == couleur.Position + pixelOffset + slide) is Pixel pixel)
              pixel.Couleur = couleur;
        }

        //Changement de ligne
        if (couleur.Position % Width == Width - 1)
          pixelOffset += newLine;
      }
    }
  }
}