using Library.Entity;

namespace Library.Collection
{
  public class TaskGoList : List<TaskGo>
  {
    public int TaskNbr { get; set; }
    public bool HeureMusique { get; set; }
    public bool TitreKodi { get; set; }
    public bool AudioCaptureConcurence { get; set; }
    public bool Autorun { get; set; }

    /// <summary>
    /// Constructeur
    /// </summary>
    public TaskGoList()
    {

    }

    /// <summary>
    /// Start Task
    /// </summary>
    /// <returns></returns>
    public int StartTask(string module = "")
    {
      Add(new TaskGo(TaskNbr, module));
      this.Where(t => t.ID < TaskNbr).ToList().ForEach(t => t.Work = false);

      return TaskNbr++;
    }

    /// <summary>
    /// Task Work
    /// </summary>
    /// <returns></returns>
    public bool TaskWork()
    {
      return Find(t => t.ID == TaskNbr)?.Work ?? false;
    }

    /// <summary>
    /// Task Work
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool TaskWork(int id)
    {
      return Find(t => t.ID == id)?.Work ?? false;
    }

    /// <summary>
    /// Stop Task
    /// </summary>
    public void StopTask()
    {
      ForEach(t => t.Work = false);
    }
  }
}