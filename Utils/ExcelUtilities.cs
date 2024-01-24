using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotBase.Utilidades;
using RobotBase.Excepciones;
using SpreadsheetLight;
namespace P01_B03.Utils
{
    public static class ExcelUtilities
    {
        public static SLDocument ReadXLSX(string ruta)
        {
            if (RobotBase.Utilidades.Utils.FileExists(ruta))
            {
                SLDocument doc = new SLDocument(ruta);
                return doc;
            }
            else
            {
                return null;
            }
        }
        public static bool EncontrarCoindidenciaColumna(SLDocument sl, int numCol, string value)
        {
            int rowCount = sl.GetWorksheetStatistics().EndRowIndex;
            for(int i = 1; i <= rowCount; i++)
            {
                string cellValue = sl.GetCellValueAsString(i, numCol);
                if (cellValue.Equals(value))
                {
                    return true;
                } 
            }
            return false;
        }
    }
}
