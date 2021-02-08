namespace LedLibrary.Entities
{
  public class TetrisHorizontal
  {
    public int X { get; set; }
    public int Score { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="x"></param>
    /// <param name="score"></param>
    public TetrisHorizontal(int x, int score)
    {
      X = x;
      Score = score;
    }
  }
}