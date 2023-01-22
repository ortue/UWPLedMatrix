using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace Library.Util
{
  public partial class RadioCanada
  {
    public List<string>? Nouvelles { get; set; }

    public string NouvelleStr
    {
      get
      {
        if (Nouvelles != null)
          return Diacritic.Remove(MyRegex().Replace(string.Join(string.Empty, Nouvelles), ""));

        return string.Empty;
      }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public RadioCanada()
    {
      Refresh();
    }

    /// <summary>
    /// Refrest
    /// </summary>
    public void Refresh()
    {
      Task.Run(() => Nouvelles = GetNouvelle());
    }

    /// <summary>
    /// GetNouvelle
    /// </summary>
    /// <returns></returns>
    public static List<string> GetNouvelle()
    {
      List<string> nouvelles = new();

      try
      {
        using XmlReader reader = XmlReader.Create("https://ici.radio-canada.ca/rss/4159");
        SyndicationFeed feed = SyndicationFeed.Load(reader);

        foreach (SyndicationItem item in feed.Items)
        {
          nouvelles.Add(StriperText(item.Title.Text));

          if (item.Summary != null)
            nouvelles.Add(StriperText(item.Summary.Text));
        }
      }
      catch (Exception ex)
      {
        return new List<string> { ex.Message.ToUpper() };
      }

      return nouvelles;
    }

    /// <summary>
    /// StriperText
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string StriperText(string text)
    {
      text = text.Replace("<p>", "");
      text = text.Replace("</p>", "");
      text = text.Replace("<em>", "");
      text = text.Replace("</em>", "");

      if (text.TrimEnd().LastOrDefault() is char lettre)
        if (lettre != '.' && lettre != '!' && lettre != '?')
          text += ".";

      return text.ToUpper() + " ";
    }

    [GeneratedRegex("http[^\\s]+")]
    private static partial Regex MyRegex();
  }
}