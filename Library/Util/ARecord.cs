using System.Diagnostics;

namespace Library.Util
{
  public class ARecord : IDisposable
  {

    public static bool IsBusy { get; set; }


    public int BufferSize { get; set; }
    public byte[] RawBuffer { get; set; }
    public Process Process { get; set; }
    public Stream Stream { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public ARecord(int bufferSize = 256)
    {
      Process = new()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "arecord",
          Arguments = "-D hw:1,0 -f S16_LE -c 1 -r 44100 -t raw",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true
        }
      };

      //process.ErrorDataReceived += (sender, e) =>
      //{
      //  if (!string.IsNullOrEmpty(e.Data))
      //    Console.WriteLine("Erreur arecord: " + e.Data);
      //};

      Process.Start();

      IsBusy = true;
      //process.BeginErrorReadLine();

      Stream = Process.StandardOutput.BaseStream;

      BufferSize = bufferSize; // 1024 échantillons ≈ 46ms
      RawBuffer = new byte[BufferSize * 2];
    }

    public double[] Read()
    {
      Stream.ReadExactly(RawBuffer, 0, RawBuffer.Length);
      double[] samples = new double[BufferSize];

      for (int i = 0; i < BufferSize; i++)
      {
        short sample = (short)(RawBuffer[i * 2] | (RawBuffer[i * 2 + 1] << 8));
        samples[i] = sample / 128.0; // Normalisation entre -1 et 1
      }

      return samples;
    }

    public short[] GetBuffer()
    {
      int sampleCount = BufferSize / 2; // car chaque sample = 2 octets

      short[] shortBuffer = new short[BufferSize];

      for (int i = 0; i < sampleCount; i++)
        shortBuffer[i] = (short)(RawBuffer[2 * i] | (RawBuffer[2 * i + 1] << 8));

      return shortBuffer;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
      IsBusy = false;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        Process.Kill(true); // stoppe proprement arecord
        Process.Dispose();
        Stream.Dispose();
      }
    }












    //public int BufferSize { get; set; }
    //public byte[] Buffer { get; set; }
    //public Process Process { get; set; }
    //public Stream Stream { get; set; }

    ///// <summary>
    ///// Constructeur
    ///// </summary>
    //public ARecord(int bufferSize = 128)
    //{
    //  Process = new()
    //  {
    //    StartInfo = new ProcessStartInfo
    //    {
    //      FileName = "arecord",
    //      Arguments = "-D plughw:1,0 -f U8 -c 1 -r 11025 -t raw",
    //      RedirectStandardOutput = true,
    //      UseShellExecute = false,
    //      //RedirectStandardError = true, // pour debug si problème
    //      CreateNoWindow = true
    //    }
    //  };

    //  //process.ErrorDataReceived += (sender, e) =>
    //  //{
    //  //  if (!string.IsNullOrEmpty(e.Data))
    //  //    Console.WriteLine("Erreur arecord: " + e.Data);
    //  //};

    //  Process.Start();
    //  //process.BeginErrorReadLine();

    //  Stream = Process.StandardOutput.BaseStream;

    //  BufferSize = bufferSize; // 1024 échantillons ≈ 46ms
    //  Buffer = new byte[BufferSize];
    //}

    ///// <summary>
    ///// Read
    ///// </summary>
    ///// <returns></returns>
    //public double[] Read()
    //{
    //  int bytesRead = Stream.Read(Buffer, 0, Buffer.Length);

    //  double[] samples = new double[BufferSize];

    //  for (int i = 0; i < bytesRead; i++)
    //    samples[i] = Buffer[i] - 128;

    //  return samples;
    //}

    ///// <summary>
    ///// GetBuffer
    ///// </summary>
    ///// <returns></returns>
    //public byte[] GetBuffer()
    //{
    //  Stream.ReadExactly(Buffer);

    //  return Buffer;
    //}

    //public void Dispose()
    //{
    //  Dispose(true);
    //  GC.SuppressFinalize(this);
    //}

    //protected virtual void Dispose(bool disposing)
    //{
    //  if (disposing)
    //  {
    //    Process.Kill(true); // stoppe proprement arecord
    //    Process.Dispose();
    //    Stream.Dispose();
    //  }
    //}
  }
}
