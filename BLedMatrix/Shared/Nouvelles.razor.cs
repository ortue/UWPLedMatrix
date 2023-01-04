namespace BLedMatrix.Shared
{
  public partial class Nouvelles
  {
    /// <summary>
    /// Set
    /// </summary>
    private void Set()
    {
      Task.Run(ExecNouvelles);
    }

    /// <summary>
    /// Nouvelles
    /// </summary>
    private void ExecNouvelles()
    {
      //int largeur = 0;
      //int debut = ResetNouvelle();
      int task = TaskGo.StartTask();
      //DateTime update = DateTime.Now.AddMinutes(-60);
      //CaractereList caracteres = new(20);

      using ManualResetEventSlim waitHandle = new(false);

      while (TaskGo.TaskWork(task))
      {
        //Reset après avoir défiler tout le texte
        //if (!string.IsNullOrWhiteSpace(Util.NouvelleStr) && largeur < debut++)
        //  debut = ResetNouvelle();

        //largeur = caracteres.SetText(Util.NouvelleStr);
        //Util.Context.Pixels.SetNouvelle(caracteres.GetCaracteres(debut), Heure);
        Pixels.SendPixels();
        Pixels.Reset();

        waitHandle.Wait(TimeSpan.FromMilliseconds(50));

        //Mettre a jour les nouvelle aux heures
        //if (update.AddMinutes(60) < DateTime.Now)
        //{
        //  update = DateTime.Now;
        //  Util.GetNouvelleAsync();
        //}
      }
    }
  }
}