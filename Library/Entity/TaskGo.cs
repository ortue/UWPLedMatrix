namespace Library.Entity
{
  public class TaskGo
  {
    public int ID { get; set; }
    public bool Work { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TaskGo(int id)
    {
      ID = id;
      Work = true;
    }
  }
}