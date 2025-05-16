using Library.Entity;
using System.Device.Spi;
using System.Drawing;

namespace Library.Util
{
  public class PixelStrip
  {
    private readonly Color[] _pixels;
    private readonly SpiDevice _spiDevice;
    private readonly int _length;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="length"></param>
    public PixelStrip(int length)
    {
      _length = length;


      //Apa102Writer.Test();

      SpiConnectionSettings spiSettings = new(0, 0)
      {
        ClockFrequency = 8_000_000,
        //ClockFrequency = 4000000,
        //ClockFrequency = 2000000,
        //ClockFrequency = 1000000,
        //ClockFrequency = 500000,
        Mode = SpiMode.Mode0,
        DataBitLength = 8
      };

      _spiDevice = SpiDevice.Create(spiSettings);
      _pixels = new Color[_length];
    }

    /// <summary>
    /// Send
    /// </summary>
    /// <param name="pixels"></param>
    public void Send(IEnumerable<Pixel> pixels)
    {
      Apa102Writer strip = new(_spiDevice, _length);
      int i = 0;

      foreach (Pixel pixel in pixels)
        _pixels[i++] = Color.FromArgb(pixel.Couleur.R, pixel.Couleur.G, pixel.Couleur.B);

      strip.Write(_pixels);
    }
  }
}
