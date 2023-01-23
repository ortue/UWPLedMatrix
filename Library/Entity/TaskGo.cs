namespace Library.Entity
{
  public class TaskGo
  {
    public int ID { get; set; }
    public bool Work { get; set; }
    public string Module { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TaskGo(int id, string module = "")
    {
      ID = id;
      Work = true;
      Module = module;
    }
  }
}