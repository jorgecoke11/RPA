using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05B03.AutoIT
{
    public class ConstantsAutoIt
    {
        public string CarpetaAppsAuxiliares { get; private set; }
        public string CarpetaAppsAutoit { get; private set; }
        public string CarpetaImagenesAutoit { get; private set; }
        public string NombreAPPbuscarImagen { get; private set; }
        public string NombreAPPArea { get; private set; }
        public enum Click
        {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }
        public ConstantsAutoIt(string carpetaAppsAuxiliares, string carpetaAppsAutoit, string carpetaImagenesAutoit, string nombreAPPbuscarImagen,string nombreAPPbuscarImagenArea)
        {
            this.CarpetaAppsAuxiliares = carpetaAppsAuxiliares;
            this.CarpetaAppsAutoit = carpetaAppsAutoit;
            this.CarpetaImagenesAutoit = carpetaImagenesAutoit;
            this.NombreAPPbuscarImagen = nombreAPPbuscarImagen;
            this.NombreAPPArea = nombreAPPbuscarImagenArea;
        }
        public Dictionary<string, string> AreaOrigen = new Dictionary<string, string>
        {
            { "SOPORTE CANALES", "soporte_canales.png" }
        };
        public Dictionary<string, string> ORIGEN_TICKET = new Dictionary<string, string> 
        {
            {"EMAIL", "2"}
        }; 
    }
}
