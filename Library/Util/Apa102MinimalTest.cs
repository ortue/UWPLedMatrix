using System.Device.Spi;

public class Apa102MinimalTest
{

  //public static void Main()
  //{
  //  const int numLeds = 400;

  //  Color[] pixels = new Color[numLeds];

  //  // Exemple : dégradé rouge
  //  for (int i = 0; i < numLeds; i++)
  //    pixels[i] = Color.FromArgb(i * 255 / numLeds, 0, 0);

  //  using var writer = new Apa102NativeWriter("/dev/spidev0.0", numLeds);
  //  writer.Write(pixels);

  //  Console.WriteLine("400 LEDs mises à jour sans fragmentation !");
  //}


  public static void Main2()
  {
    const int ledCount = 400;
    const int fragmentSize = 512; // taille à adapter selon les limites du buffer

    var settings = new SpiConnectionSettings(0, 0)
    {
      ClockFrequency = 500_000,
      Mode = SpiMode.Mode0,
      DataBitLength = 8
    };

    using var spi = SpiDevice.Create(settings);

    // Préparer les données complètes
    byte[] startFrame = new byte[4]; // tous à 0
    byte[] ledFrames = new byte[ledCount * 4];
    for (int i = 0; i < ledCount; i++)
    {
      ledFrames[i * 4 + 0] = 0xE0 | 0x1F; // Brightness
      ledFrames[i * 4 + 1] = 255;         // Blue
      ledFrames[i * 4 + 2] = 255;         // Green
      ledFrames[i * 4 + 3] = 255;         // Red
    }

    int endFrameLength = (ledCount + 15) / 16;
    byte[] endFrame = new byte[endFrameLength];

    // Combiner toutes les données
    byte[] fullData = new byte[startFrame.Length + ledFrames.Length + endFrame.Length];
    Buffer.BlockCopy(startFrame, 0, fullData, 0, startFrame.Length);
    Buffer.BlockCopy(ledFrames, 0, fullData, startFrame.Length, ledFrames.Length);
    Buffer.BlockCopy(endFrame, 0, fullData, startFrame.Length + ledFrames.Length, endFrame.Length);

    Console.WriteLine($"Envoi des données en fragments de {fragmentSize} octets...");

    // Envoyer en fragments
    for (int i = 0; i < fullData.Length; i += fragmentSize)
    {
      int currentLength = Math.Min(fragmentSize, fullData.Length - i);
      byte[] writeFragment = new byte[currentLength];
      byte[] readFragment = new byte[currentLength];

      Array.Copy(fullData, i, writeFragment, 0, currentLength);
      spi.TransferFullDuplex(writeFragment, readFragment);
    }

    Console.WriteLine("Transmission terminée.");
  }
}
