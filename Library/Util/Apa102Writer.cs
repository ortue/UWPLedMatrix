using System.Device.Spi;
using System.Drawing;

namespace Library.Util
{
  public class Apa102Writer
  {

    private readonly SpiDevice _spi;
    private readonly int _numLeds;

    public Apa102Writer(SpiDevice spi, int numLeds)
    {
      _spi = spi;
      _numLeds = numLeds;
    }

    public void Write(Color[] colors)
    {
      if (colors.Length != _numLeds)
        throw new ArgumentException("Length mismatch with LED count");

      byte[] startFrame = new byte[4];

      //Span<byte> startFrame = stackalloc byte[4];


      byte[] ledFrames = new byte[_numLeds * 4];

      for (int i = 0; i < _numLeds; i++)
      {
        Color color = colors[i];

        //ledFrames[i * 4 + 0] = 0xE0 | 0x10; // demi-luminosité
        ledFrames[i * 4 + 0] = 0xE0 | 0x1F;           // Brightness: 0xE0 | (0x00–0x1F)

        ledFrames[i * 4 + 1] = color.B;
        ledFrames[i * 4 + 2] = color.G;
        ledFrames[i * 4 + 3] = color.R;

      }


      //int endFrameLength = (_numLeds + 15) / 16 + 4; // ici = (400 + 15) / 16 = 25
      //byte[] endFrame = Enumerable.Repeat((byte)0xFF, endFrameLength).ToArray();
      //Span<byte> endFrame = stackalloc byte[endFrameLength];
      //endFrame.Fill(0xFF);
      //_spi.Write(startFrame);
      //_spi.Write(ledFrames);



      int endFrameLength = 64; // plus généreux
      byte[] endFrame = Enumerable.Repeat((byte)0xFF, endFrameLength).ToArray();

      //_spi.Write(endFrame);




      byte[] full = new byte[startFrame.Length + ledFrames.Length + endFrame.Length];
      Buffer.BlockCopy(startFrame, 0, full, 0, startFrame.Length);
      Buffer.BlockCopy(ledFrames, 0, full, startFrame.Length, ledFrames.Length);
      Buffer.BlockCopy(endFrame, 0, full, startFrame.Length + ledFrames.Length, endFrame.Length);

      //_spi.Write(full);



      _spi.TransferFullDuplex(full, new byte[full.Length]);


      //Thread.Sleep(2);

      //_spi.Dispose();
    }

    public static void Test()
    {
      int numLeds = 400;

      int[] frequencies = new int[]
      {
            1_000_000, 2_000_000, 4_000_000, 6_000_000, 8_000_000
      };

      Color[] testColors = new Color[]
      {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.White,
            Color.Yellow
      };

      for (int i = 0; i < frequencies.Length; i++)
      {
        int freq = frequencies[i];
        Color color = testColors[i % testColors.Length];

        Console.WriteLine($"[TEST] SPI à {freq / 1_000_000} MHz — couleur : {color.Name}");

        var spiSettings = new SpiConnectionSettings(0, 0)
        {
          ClockFrequency = freq,
          Mode = SpiMode.Mode0,
          DataBitLength = 8
        };

        using var spi = SpiDevice.Create(spiSettings);
        var strip = new Apa102Writer(spi, numLeds);

        var colors = new Color[numLeds];
        for (int j = 0; j < numLeds; j++)
          colors[j] = color;

        strip.Write(colors);

        Console.WriteLine("→ Observe les LEDs pendant 5 secondes...\n");
        Thread.Sleep(5000);
      }
    }
  }
}