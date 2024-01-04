using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01B05.F.Utilidades
{
    public static class Utils
    {
        public static string QuitarAcentos(string cadena)
        {
            // Utilizar NormalizationForm.FormD para descomponer caracteres diacríticos
            string normalizedString = cadena.Normalize(NormalizationForm.FormD);
            // Filtrar los caracteres que no son letras básicas ASCII
            return new string(normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
        }
    }
}
