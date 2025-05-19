using System.Runtime.InteropServices;

namespace Library.Util
{
  public class SpiNative : IDisposable
  {
    private const int SPI_MODE_0 = 0x00;
    private const int BITS_PER_WORD = 8;

    private const uint SPI_IOC_WR_MODE = 0x40016B01;
    private const uint SPI_IOC_WR_BITS_PER_WORD = 0x40016B03;
    private const uint SPI_IOC_WR_MAX_SPEED_HZ = 0x40046B04;

    [DllImport("libc", SetLastError = true)]
    private static extern int open(string pathname, int flags);

    [DllImport("libc", SetLastError = true)]
    private static extern int close(int fd);

    [DllImport("libc", SetLastError = true)]
    private static extern int ioctl(int fd, uint request, ref int value);

    [DllImport("libc", SetLastError = true)]
    private static extern int ioctl(int fd, uint request, ref uint value);

    [DllImport("libc", SetLastError = true)]
    private static extern int write(int fd, byte[] buffer, int count);

    private readonly int _fd;

    public SpiNative(string devicePath = "/dev/spidev0.0", int speedHz = 10_000_000)
    {
      _fd = open(devicePath, 2); // O_RDWR = 2
      if (_fd < 0)
        throw new IOException("Impossible d'ouvrir le périphérique SPI");

      int mode = SPI_MODE_0;
      int bits = BITS_PER_WORD;
      uint speed = (uint)speedHz;

      if (ioctl(_fd, SPI_IOC_WR_MODE, ref mode) < 0)
        throw new IOException("Erreur SPI_IOC_WR_MODE");

      if (ioctl(_fd, SPI_IOC_WR_BITS_PER_WORD, ref bits) < 0)
        throw new IOException("Erreur SPI_IOC_WR_BITS_PER_WORD");

      if (ioctl(_fd, SPI_IOC_WR_MAX_SPEED_HZ, ref speed) < 0)
        throw new IOException("Erreur SPI_IOC_WR_MAX_SPEED_HZ");
    }

    public void Write(byte[] data)
    {
      int result = write(_fd, data, data.Length);
      if (result != data.Length)
        throw new IOException("Erreur lors de l'écriture SPI");
    }

    public void Dispose()
    {
      close(_fd);
    }
  }
}
