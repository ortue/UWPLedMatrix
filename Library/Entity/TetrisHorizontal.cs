namespace Library.Entity
{
  public class TetrisHorizontal
  {
    public int X { get; set; }
    public int Rotation { get; set; }
    public double Score { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="rotation"></param>
    /// <param name="x"></param>
    /// <param name="score"></param>
    public TetrisHorizontal(int rotation, int x, double score)
    {
      Rotation = rotation;
      X = x;
      Score = score;
    }
  }
}