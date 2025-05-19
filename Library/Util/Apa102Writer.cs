using Library.Entity;
using System.Device.Spi;

namespace Library.Util
{
  public class Apa102Writer
  {
    private readonly SpiDevice _spi;
    private readonly byte[] StartFrame = { 0, 0, 0, 0 };
    private readonly byte[] EndFrame;

    public Apa102Writer(int numLeds)
    {
      //_numLeds = numLeds;

      int endFrameSize = (numLeds + 14) / 16;
      EndFrame = new byte[endFrameSize];

      SpiConnectionSettings spiSettings = new(0, 0)
      {
        //ClockFrequency = 10000000,
        //ClockFrequency = 8_000_000,
        ClockFrequency = 4000000,
        //ClockFrequency = 2000000,
        //ClockFrequency = 1000000,
        //ClockFrequency = 500000,
        Mode = SpiMode.Mode0,
        DataBitLength = 8
      };

      _spi = SpiDevice.Create(spiSettings);
    }

    public void SendPixels(IEnumerable<Pixel> pixels)
    {
      List<byte> spiDataBytes = new();
      spiDataBytes.AddRange(StartFrame);

      foreach (Pixel pixel in pixels)
      {
        // Global brightness.  Not implemented currently.  0xE0 (binary 11100000) specifies the beginning of the pixel's
        // color data.  0x1F (binary 00011111) specifies the global brightness.  If you want to actually use this functionality
        // comment out this line and uncomment the next one.  Then the pixel's RGB value will get scaled based on the alpha
        // channel value from the Color.
        spiDataBytes.Add(0xE0 | 0x1F);
        //spiDataBytes.Add((byte)(0xE0 | (byte)(pixel.A >> 3)));

        // APA102/DotStar leds take the color data in Blue, Green, Red order.  Weirdly, according to the spec these are supposed
        // to take a 0-255 value for R/G/B.  However, on the ones I have they only seem to take 0-126.  Specifying 127-255 doesn't
        // break anything, but seems to show the same exact value 0-126 would have (i.e. 127 is 0 brightness, 255 is full brightness).
        // Discarding the lowest bit from each to make the value fit in 0-126.
        //spiDataBytes.Add((byte)(pixel.Couleur.B >> 1));
        //spiDataBytes.Add((byte)(pixel.Couleur.G >> 1));
        //spiDataBytes.Add((byte)(pixel.Couleur.R >> 1));

        spiDataBytes.Add(pixel.Couleur.B);
        spiDataBytes.Add(pixel.Couleur.G);
        spiDataBytes.Add(pixel.Couleur.R);

      }

      spiDataBytes.AddRange(EndFrame);

      _spi.Write(spiDataBytes.ToArray());
    }
  }
}