using Library.Entity;
using System.Xml.Serialization;

namespace Library.Collection
{
  public class CouleurList : List<Couleur>
  {
    public static string Filename
    {
      get { return "Couleur.xml"; }
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    public CouleurList()
    {

    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="couleurs"></param>
    public CouleurList(IEnumerable<Couleur> couleurs)
    {
      foreach (Couleur couleur in couleurs)
        Add(couleur);
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="module"></param>
    /// <param name="titre"></param>
    /// <returns></returns>
    public Couleur Get(string module, string titre, Couleur defaut)
    {
      if (Find(c => c.Module == module && c.Titre == titre) is Couleur couleur)
        return couleur;

      Add(defaut);

      return defaut;

    }

    /// <summary>
    /// Load
    /// </summary>
    /// <returns></returns>
    public static CouleurList? Load()
    {
      XmlSerializer serializer = new(typeof(CouleurList));
      FileStream fs = new(Filename, FileMode.Open);

      return (CouleurList?)serializer.Deserialize(fs);
    }

    /// <summary>
    /// Save
    /// </summary>
    public void Save()
    {
      CouleurList couleurs = new(this.Where(c => c.Module != null));

      XmlSerializer serializer = new(typeof(CouleurList));
      using TextWriter writer = new StreamWriter(Filename);
      serializer.Serialize(writer, couleurs);







      //using StreamWriter file = new("Couleur.Sav");
      //await file.WriteLineAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + log + Environment.NewLine + Environment.NewLine);

    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="result"></param>
    public CouleurList(List<byte[]> frames)
    {
      int frameCompteur = 0;

      foreach (byte[] frame in frames)
      {
        int position = 0;

        for (int i = 0; i < frame.Length; i += 3)
          if (IsTransparent(frame, i))
            Add(new Couleur(frameCompteur, position++, Couleur.FromArgb(0, 0, 0)));
          else
            Add(new Couleur(frameCompteur, position++, Couleur.FromArgb((byte)(frame[i] / 2), (byte)(frame[i + 1] / 2), (byte)(frame[i + 2] / 2))));

        frameCompteur++;
      }
    }

    /// <summary>
    /// IsTransparent
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    private static bool IsTransparent(byte[] frame, int i)
    {
      bool isTransparent = frame[i] == 255 && frame[i + 1] == 255 && frame[i + 2] == 255;
      isTransparent |= frame[i] == 255 && frame[i + 1] == 0 && frame[i + 2] == 128;
      isTransparent |= frame[i] == 0 && frame[i + 1] == 136 && frame[i + 2] == 255;

      return isTransparent;
    }
  }
}
