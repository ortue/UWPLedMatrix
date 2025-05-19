using Library.Entity;
using System.Runtime.InteropServices;

namespace Library.Util
{
  public class Apa102NativeWriter
  {
    private const string SpiDevice = "/dev/spidev0.0";
    private const int SPI_IOC_MESSAGE_1 = 0x40206B00;
    private const int MAX_TRANSFER_SIZE = 4096;

    [StructLayout(LayoutKind.Sequential)]
    private struct SpiIocTransfer
    {
      public ulong tx_buf;
      public ulong rx_buf;
      public uint len;
      public uint speed_hz;
      public ushort delay_usecs;
      public byte bits_per_word;
      public byte cs_change;
      public uint pad;
    }

    [DllImport("libc", SetLastError = true)]
    private static extern int ioctl(int fd, uint request, ref SpiIocTransfer transfer);

    [DllImport("libc", SetLastError = true)]
    private static extern int open(string pathname, int flags);

    [DllImport("libc")]
    private static extern int close(int fd);

    private readonly int _fd;
    private readonly int _numLeds;

    public Apa102NativeWriter(int numLeds)
    {
      _fd = open(SpiDevice, 2); // O_RDWR = 2
      if (_fd < 0)
        throw new IOException("Cannot open SPI device");

      _numLeds = numLeds;
    }

    public void Send(IEnumerable<Pixel> pixels)
    {
      byte[] startFrame = new byte[4]; // 0x00
      byte[] ledFrame = new byte[_numLeds * 4];
      byte[] endFrame = new byte[(_numLeds + 15) / 16];

      //for (int i = 0; i < _numLeds; i++)
      //{
      //  Color c = colors[i];
      //  ledFrame[i * 4 + 0] = 0xE0 | 0x1F;
      //  ledFrame[i * 4 + 1] = (byte)(c.B);
      //  ledFrame[i * 4 + 2] = (byte)(c.G);
      //  ledFrame[i * 4 + 3] = (byte)(c.R);
      //}


      int j = 0;

      foreach (Pixel pixel in pixels)
      {
        ledFrame[j * 4 + 0] = (byte)(0xE0 | 0x1F);
        ledFrame[j * 4 + 1] = pixel.Couleur.B;
        ledFrame[j * 4 + 2] = pixel.Couleur.G;
        ledFrame[j++ * 4 + 3] = pixel.Couleur.R;
      }



      byte[] all = new byte[startFrame.Length + ledFrame.Length + endFrame.Length];
      Buffer.BlockCopy(startFrame, 0, all, 0, startFrame.Length);
      Buffer.BlockCopy(ledFrame, 0, all, startFrame.Length, ledFrame.Length);
      Buffer.BlockCopy(endFrame, 0, all, startFrame.Length + ledFrame.Length, endFrame.Length);

      for (int offset = 0; offset < all.Length; offset += MAX_TRANSFER_SIZE)
      {
        int len = Math.Min(MAX_TRANSFER_SIZE, all.Length - offset);
        var segment = new byte[len];
        Array.Copy(all, offset, segment, 0, len);

        GCHandle handle = GCHandle.Alloc(segment, GCHandleType.Pinned);
        try
        {
          SpiIocTransfer transfer = new()
          {
            tx_buf = (ulong)handle.AddrOfPinnedObject(),
            rx_buf = 0,
            len = (uint)len,
            speed_hz = 10000000,
            delay_usecs = 0,
            bits_per_word = 8,
            cs_change = 0
          };

          int result = ioctl(_fd, SPI_IOC_MESSAGE_1, ref transfer);
          if (result < 1)
            throw new IOException($"SPI transfer failed: errno {Marshal.GetLastWin32Error()}");
        }
        finally
        {
          handle.Free();
        }
      }
    }

    ~Apa102NativeWriter()
    {
      if (_fd >= 0)
        close(_fd);
    }
  }

  //using System;
  //using System.IO;
  //using System.Runtime.InteropServices;

  //public class Apa102NativeWriter
  //{
  //  private const string SpiDevicePath = "/dev/spidev0.0";
  //  private const int SPI_IOC_MESSAGE_1 = 0x40206b00;
  //  private const int MAX_FRAGMENT_SIZE = 4096; // Ou 16384 si le buffer kernel le permet
  //  private readonly int _fd;
  //  private readonly int _numLeds;

  //  [StructLayout(LayoutKind.Sequential)]
  //  private struct SpiIocTransfer
  //  {
  //    public IntPtr tx_buf;
  //    public IntPtr rx_buf;
  //    public uint len;
  //    public uint speed_hz;
  //    public ushort delay_usecs;
  //    public byte bits_per_word;
  //    public byte cs_change;
  //    public uint pad;
  //  }

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int ioctl(int fd, uint request, ref SpiIocTransfer xfer);

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int open(string pathname, int flags);

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int close(int fd);

  //  private const int O_RDWR = 0x0002;

  //  public Apa102NativeWriter(int numLeds)
  //  {
  //    _numLeds = numLeds;
  //    _fd = open(SpiDevicePath, O_RDWR);
  //    if (_fd < 0)
  //      throw new IOException($"Failed to open SPI device: {SpiDevicePath}");
  //  }

  //  public void Write(IEnumerable<Pixel> pixels)
  //  {
  //    byte[] startFrame = new byte[4]; // zeros
  //    byte[] ledFrames = new byte[_numLeds * 4];

  //    //for (int i = 0; i < _numLeds; i++)
  //    //{
  //    //  ledFrames[i * 4 + 0] = (byte)(0xE0 | 0x1F);
  //    //  ledFrames[i * 4 + 1] = colors[i].B;
  //    //  ledFrames[i * 4 + 2] = colors[i].G;
  //    //  ledFrames[i * 4 + 3] = colors[i].R;
  //    //}

  //    int j = 0;

  //    foreach (Pixel pixel in pixels)
  //    {
  //      //int offset = 4 + i++ * 4;
  //      //buffer[offset + 0] = 0xE0 | 0x1F;      // Brightness max
  //      //buffer[offset + 1] = (byte)(c.Couleur.B);      // Blue
  //      //buffer[offset + 2] = (byte)(c.Couleur.G);      // Green
  //      //buffer[offset + 3] = (byte)(c.Couleur.R);      // Red


  //      ledFrames[j * 4 + 0] = (byte)(0xE0 | 0x1F);
  //      ledFrames[j * 4 + 1] = pixel.Couleur.B;
  //      ledFrames[j * 4 + 2] = pixel.Couleur.G;
  //      ledFrames[j++ * 4 + 3] = pixel.Couleur.R;
  //    }


  //    int endFrameLength = (_numLeds + 15) / 16;
  //    byte[] endFrame = new byte[endFrameLength];

  //    using MemoryStream ms = new();
  //    ms.Write(startFrame, 0, startFrame.Length);
  //    ms.Write(ledFrames, 0, ledFrames.Length);
  //    ms.Write(endFrame, 0, endFrame.Length);

  //    byte[] fullData = ms.ToArray();
  //    SendInFragments(fullData);
  //  }

  //  private void SendInFragments(byte[] data)
  //  {
  //    int offset = 0;
  //    while (offset < data.Length)
  //    {
  //      int len = Math.Min(MAX_FRAGMENT_SIZE, data.Length - offset);
  //      SendFragment(data, offset, len);
  //      offset += len;
  //    }
  //  }











  //  private void SendFragment(byte[] buffer, int offset, int length)
  //  {
  //    GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
  //    try
  //    {
  //      IntPtr ptr = handle.AddrOfPinnedObject() + offset;

  //      SpiIocTransfer transfer = new()
  //      {
  //        tx_buf = ptr,
  //        rx_buf = IntPtr.Zero,
  //        len = (uint)length,
  //        speed_hz = 10_000_000,
  //        delay_usecs = 0,
  //        bits_per_word = 8,
  //        cs_change = 0,
  //        pad = 0
  //      };

  //      int result = ioctl(_fd, SPI_IOC_MESSAGE_1, ref transfer);
  //      if (result < 1)
  //      {
  //        int err = Marshal.GetLastWin32Error();
  //        Console.WriteLine($"ioctl failed, errno: {err} ({new System.ComponentModel.Win32Exception(err).Message})");



  //        throw new IOException("SPI transfer failed");
  //      }
  //    }
  //    finally
  //    {
  //      handle.Free();
  //    }
  //  }

  //  public void Close()
  //  {
  //    close(_fd);
  //  }
  //}
















  //public class Apa102NativeWriter : IDisposable
  //{
  //  private const int SPI_MODE_0 = 0x00;
  //  private const int BITS_PER_WORD = 8;

  //  private const uint SPI_IOC_WR_MODE = 0x40016B01;
  //  private const uint SPI_IOC_WR_BITS_PER_WORD = 0x40016B03;
  //  private const uint SPI_IOC_WR_MAX_SPEED_HZ = 0x40046B04;

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int open(string pathname, int flags);

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int close(int fd);

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int ioctl(int fd, uint request, ref int value);

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int ioctl(int fd, uint request, ref uint value);

  //  [DllImport("libc", SetLastError = true)]
  //  private static extern int write(int fd, byte[] buffer, int count);

  //  private readonly int _fd;
  //  private readonly int _numLeds;

  //  public Apa102NativeWriter(string devicePath, int numLeds, int speedHz = 10_000_000)
  //  {
  //    _numLeds = numLeds;

  //    _fd = open(devicePath, 2); // O_RDWR
  //    if (_fd < 0)
  //      throw new IOException($"Impossible d'ouvrir {devicePath}");

  //    int mode = SPI_MODE_0;
  //    int bits = BITS_PER_WORD;
  //    uint speed = (uint)speedHz;

  //    if (ioctl(_fd, SPI_IOC_WR_MODE, ref mode) < 0)
  //      throw new IOException("Erreur SPI_IOC_WR_MODE");

  //    if (ioctl(_fd, SPI_IOC_WR_BITS_PER_WORD, ref bits) < 0)
  //      throw new IOException("Erreur SPI_IOC_WR_BITS_PER_WORD");

  //    if (ioctl(_fd, SPI_IOC_WR_MAX_SPEED_HZ, ref speed) < 0)
  //      throw new IOException("Erreur SPI_IOC_WR_MAX_SPEED_HZ");
  //  }

  //  public void Write(IEnumerable<Pixel> pixels)
  //  {
  //    if (pixels.Count() != _numLeds)
  //      throw new ArgumentException($"pixels.Length != {_numLeds}");

  //    int endFrameSize = (_numLeds + 14) / 16;

  //    byte[] buffer = new byte[4 + _numLeds * 4 + endFrameSize];

  //    // Start frame
  //    for (int j = 0; j < 4; j++)
  //      buffer[j] = 0x00;

  //    // LED frames
  //    //for (int i = 0; i < _numLeds; i++)

  //    int i = 0;

  //    foreach (Pixel pixel in pixels)
  //    {
  //      Pixel c = pixel;
  //      int offset = 4 + i++ * 4;
  //      buffer[offset + 0] = 0xE0 | 0x1F;      // Brightness max
  //      buffer[offset + 1] = (byte)(c.Couleur.B);      // Blue
  //      buffer[offset + 2] = (byte)(c.Couleur.G);      // Green
  //      buffer[offset + 3] = (byte)(c.Couleur.R);      // Red
  //    }

  //    // End frame
  //    for (int j = 0; j < endFrameSize; j++)
  //      buffer[4 + _numLeds * 4 + j] = 0xFF;

  //    int written = write(_fd, buffer, buffer.Length);
  //    if (written != buffer.Length)
  //      throw new IOException($"Écriture SPI incomplète ({written}/{buffer.Length})");
  //  }

  //  public void Dispose()
  //  {
  //    close(_fd);
  //  }
  //}
}