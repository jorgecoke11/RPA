using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        public static DateTime StringToDateTime(string cadenaFecha, string format)
        {
            if(DateTime.TryParseExact(cadenaFecha, format, null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
            {
                return fecha;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        public static string DateTimeToString(DateTime fecha, string format)
        {
            return fecha.ToString(format);
        }
        public static void MatarProcesos(string proceso)
        {
            Process[] procesos = Process.GetProcessesByName(proceso);
            foreach (var item in procesos)
            {
                item.Kill();
            }
        }
        public static void IniciarProceso(string nombreProceso)
        {
            Process.Start(nombreProceso);
        }
        public static void EscribirFichero(string rutaFichero, string data)
        {
            
            using (StreamWriter writer = new StreamWriter(rutaFichero)) 
            { 
                writer.WriteLine(data);
            }
        }
        public static void EliminarFichero(string rutaArchivo)
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar eliminar el archivo: {ex.Message}");
            }
        }
    }
}
