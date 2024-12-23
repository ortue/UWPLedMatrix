using Library.Entity;
using System.Device.Spi;
using System.Drawing;

namespace Library.Util
{
  public class Apa102 : IDisposable
  {
    //public Span<Color> Pixels => _pixels;
    private SpiDevice _spiDevice;
    private Color[] _pixels;
    private byte[] _buffer;

    /// <summary>
    /// Initializes a new instance of the APA102 device.
    /// </summary>
    /// <param name="spiDevice">The SPI device used for communication.</param>
    /// <param name="length">Number of LEDs</param>
    public Apa102(SpiDevice spiDevice, int length)
    {
      _spiDevice = spiDevice ?? throw new ArgumentNullException(nameof(spiDevice));
      _pixels = new Color[length];
      _buffer = new byte[(length + 2) * 4];

      _buffer.AsSpan(0, 4).Fill(0x00); // start frame
      _buffer.AsSpan((length + 1) * 4, 4).Fill(0xFF); // end frame
    }

    /// <summary>
    /// SendPixels
    /// </summary>
    /// <param name="pixels"></param>
    public void SendPixels(IEnumerable<Pixel> pixels)
    {
      int i = 0;

      foreach (Pixel pixel in pixels)
        _pixels[i++] = Color.FromArgb(pixel.Couleur.R, pixel.Couleur.G, pixel.Couleur.B);

      Flush();
    }

    /// <summary>
    /// Update color data to LEDs
    /// </summary>
    public void Flush()
    {
      for (int i = 0; i < _pixels.Length; i++)
      {
        Span<byte> pixel = _buffer.AsSpan((i + 1) * 4);
        pixel[0] = (byte)((_pixels[i].A >> 3) | 0b11100000); // global brightness (alpha)
        pixel[1] = _pixels[i].B; // blue
        pixel[2] = _pixels[i].G; // green
        pixel[3] = _pixels[i].R; // red
      }

      _spiDevice.Write(_buffer);
    }

    public void Dispose()
    {
      _spiDevice?.Dispose();
      _spiDevice = null!;
      _pixels = null!;
      _buffer = null!;
    }
  }
}
