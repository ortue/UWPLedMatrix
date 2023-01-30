using Library.Entity;
using System.Xml.Serialization;

namespace Library.Collection
{
  public class CouleurList : List<Couleur>
  {
    public int SelectedPosition { get; set; }

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
    public Couleur? Get(string module, string titre)
    {
      if (Find(c => c.Module == module && c.Titre == titre && c.Position == SelectedPosition) is Couleur couleur)
        return couleur;

      return null;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="module"></param>
    /// <param name="titre"></param>
    /// <returns></returns>
    public Couleur Get(string module, string titre, Couleur defaut)
    {
      if (Find(c => c.Module == module && c.Titre == titre && c.Position == SelectedPosition) is Couleur couleur)
        return couleur;

      defaut.Module = module;
      defaut.Titre = titre;
      defaut.Position = SelectedPosition;

      Add(defaut);

      return defaut;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="module"></param>
    /// <param name="titre"></param>
    /// <returns></returns>
    public Couleur Get(string module, string titre, Couleur defaut, int position)
    {
      if (Find(c => c.Module == module && c.Titre == titre && c.Position == SelectedPosition) is Couleur couleur)
        return couleur;

      defaut.Module = module;
      defaut.Titre = titre;
      defaut.Position = SelectedPosition;

      Add(defaut);

      return defaut;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    public IEnumerable<Couleur>? Get(string module)
    {
      if (this.Where(c => c.Module == module && c.Position == SelectedPosition) is IEnumerable<Couleur> couleurs)
        return couleurs;

      return null;
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <returns></returns>
    public static CouleurList Load()
    {
      if (File.Exists(Filename))
      {
        XmlSerializer serializer = new(typeof(CouleurList));
        using StreamReader reader = new(Filename);

        if ((CouleurList?)serializer.Deserialize(reader) is CouleurList couleurs)
          return couleurs;
      }

      return new CouleurList();
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
