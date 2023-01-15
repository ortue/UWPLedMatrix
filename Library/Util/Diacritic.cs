using System.Globalization;
using System.Text;

namespace Library.Util
{
  public class Diacritic
  {
    /// <summary>
    /// Remove
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Remove(string text)
    {
      string normalizedString = text.Normalize(NormalizationForm.FormD);
      StringBuilder stringBuilder = new();

      foreach (char c in normalizedString)
      {
        UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
          stringBuilder.Append(c);
      }

      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
  }
}