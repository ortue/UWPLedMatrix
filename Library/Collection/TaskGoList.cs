using Library.Entity;

namespace Library.Collection
{
  public class TaskGoList : List<TaskGo>
  {
    public int TaskNbr { get; set; }

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
    public int StartTask()
    {
      Add(new TaskGo(TaskNbr));
      this.Where(t => t.ID < TaskNbr).ToList().ForEach(t => t.Work = false);

      return TaskNbr++;
    }

    /// <summary>
    /// Task Work
    /// </summary>
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