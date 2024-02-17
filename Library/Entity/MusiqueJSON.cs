using System.Runtime.Serialization;

namespace Library.Entity
{
  [DataContract(Name = "Item")]
  public class MusiqueJSON
  {
    public string? album { get; set; }
    public List<string>? artist { get; set; }
    public int duration { get; set; }
    public string? label { get; set; }
    public string? title { get; set; }
    public string? type { get; set; }
  }

  [DataContract(Name = "Result")]
  public class MusiqueJSONResult
  {
    public MusiqueJSON? item { get; set; }
  }

  [DataContract(Name = "Root")]
  public class MusiqueJSONRoot
  {
    public string? id { get; set; }
    public string? jsonrpc { get; set; }

    public MusiqueJSONResult? result { get; set; }
  }
}