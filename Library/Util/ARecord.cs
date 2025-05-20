using System.Diagnostics;

namespace Library.Util
{
  public class ARecord : IDisposable
  {
    public int BufferSize { get; set; }
    public byte[] Buffer { get; set; }
    public Process Process { get; set; }
    public Stream Stream { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public ARecord()
    {
      Process = new()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "arecord",
          Arguments = "-D plughw:1,0 -f U8 -c 1 -r 11025 -t raw",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          //RedirectStandardError = true, // pour debug si problème
          CreateNoWindow = true
        }
      };

      //process.ErrorDataReceived += (sender, e) =>
      //{
      //  if (!string.IsNullOrEmpty(e.Data))
      //    Console.WriteLine("Erreur arecord: " + e.Data);
      //};

      Process.Start();
      //process.BeginErrorReadLine();

      Stream = Process.StandardOutput.BaseStream;

      BufferSize = 128; // 1024 échantillons ≈ 46ms
      Buffer = new byte[BufferSize];
    }

    /// <summary>
    /// Read
    /// </summary>
    /// <returns></returns>
    public double[] Read()
    {
      int bytesRead = Stream.Read(Buffer, 0, Buffer.Length);

      double[] samples = new double[BufferSize];

      for (int i = 0; i < bytesRead; i++)
        samples[i] = Buffer[i] - 128;

      return samples;
    }

    /// <summary>
    /// GetBuffer
    /// </summary>
    /// <returns></returns>
    public byte[] GetBuffer()
    {
      Stream.ReadExactly(Buffer);

      return Buffer;
    }


    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        Process.Dispose();
        Stream.Dispose();
      }
    }
  }
}