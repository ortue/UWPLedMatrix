namespace LedMatrix.Class
{
  public class LogToFile
  {
    /// <summary>
    /// Save
    /// </summary>
    /// <param name=""></param>
    /// <param name="log"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static async Task Save(string log, string filename = "Error.log")
    {
      using StreamWriter file = new(filename, append: true);
      await file.WriteLineAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + log + Environment.NewLine + Environment.NewLine);
    }
  }
}
