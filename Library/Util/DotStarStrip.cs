//using Library.Entity;

//namespace Library.Util
//{
//  public class DotStarStrip
//  {
//    private readonly byte[] StartFrame = { 0, 0, 0, 0 };
//    private readonly byte[] EndFrame;

//    public int PixelCount { get; private set; }

//    public ISpiChannel? Channel { get; set; }

//    /// <summary>
//    /// Constructor
//    /// </summary>
//    /// <param name="numLeds">Number of LEDs in the strip</param>
//    public DotStarStrip(int numLeds, bool work)
//    {
//      PixelCount = numLeds;

//      // The actual logic here is that we need to send a zero to be sent for every 16 pixels in the strip, to signify the end of the color data and reset the addressing.
//      int endFrameSize = (PixelCount + 14) / 16;

//      // By initializing an int array of that specific length, it gets initialized with ints of default value (0).  :)
//      EndFrame = new byte[endFrameSize];

//      if (work)
//        Begin(10000000);
//    }

//    /// <summary>
//    /// Initializes the SPI connection to the strip
//    /// </summary>
//    /// <returns>Task representing the async action</returns>
//    public void Begin(int spiFrequency)
//    {
//      //ClockFrequency = 10 000 000,
//      //Pi.Spi.DefaultFrequency 8 000 000 int
//      Pi.Init<BootstrapWiringPi>();

//      if (spiFrequency == 0)
//        spiFrequency = Pi.Spi.DefaultFrequency;

//      Pi.Spi.Channel0Frequency = spiFrequency;
//      Channel = Pi.Spi.Channel0;

//      //SpiController controller = await SpiController.GetDefaultAsync();
//      //spiDevice = controller.GetDevice(settings);
//    }

//    /// <summary>
//    /// Sends a bunch of colors out to the LED strip
//    /// </summary>
//    /// <param name="pixels">List of <see cref="Color"/>s to send to the strip.</param>
//    public void SendPixels(IEnumerable<Pixel> pixels)
//    {
//      List<byte> spiDataBytes = new();
//      spiDataBytes.AddRange(StartFrame);

//      foreach (var pixel in pixels)
//      {
//        // Global brightness.  Not implemented currently.  0xE0 (binary 11100000) specifies the beginning of the pixel's
//        // color data.  0x1F (binary 00011111) specifies the global brightness.  If you want to actually use this functionality
//        // comment out this line and uncomment the next one.  Then the pixel's RGB value will get scaled based on the alpha
//        // channel value from the Color.
//        spiDataBytes.Add(0xE0 | 0x1F);
//        //spiDataBytes.Add((byte)(0xE0 | (byte)(pixel.A >> 3)));

//        // APA102/DotStar leds take the color data in Blue, Green, Red order.  Weirdly, according to the spec these are supposed
//        // to take a 0-255 value for R/G/B.  However, on the ones I have they only seem to take 0-126.  Specifying 127-255 doesn't
//        // break anything, but seems to show the same exact value 0-126 would have (i.e. 127 is 0 brightness, 255 is full brightness).
//        // Discarding the lowest bit from each to make the value fit in 0-126.
//        spiDataBytes.Add((byte)(pixel.Couleur.B >> 1));
//        spiDataBytes.Add((byte)(pixel.Couleur.G >> 1));
//        spiDataBytes.Add((byte)(pixel.Couleur.R >> 1));
//      }

//      spiDataBytes.AddRange(EndFrame);

//      Channel?.Write(spiDataBytes.ToArray());
//      //spiDevice.Write(spiDataBytes.ToArray());
//    }
//  }
//}