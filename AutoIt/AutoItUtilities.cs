using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using P05B03.AutoIT;
using RobotBase.Utilidades;

namespace P05B03.Utilidades
{
    public class AutoItUtilities
    {
        public ConstantsAutoIt ca { get; private set; }
        public CoordenadasImg Coordenadas;
        public AutoItUtilities(ConstantsAutoIt ca) 
        { 
            this.ca = ca;
        
        }
        public void MoverRaton(int x, int y)
        {
            AutoIt.AutoItX.MouseMove(x, y);
        }
        public void EsperarVentana(string claseVentana = "", string tituloVentana = "", int timeOut = 60)
        {
            try
            {
                if(claseVentana == "")
                {
                    AutoIt.AutoItX.WinWait(tituloVentana, "", timeOut);
                }
                else if(tituloVentana == "")
                {
                    AutoIt.AutoItX.WinWait(claseVentana, "", timeOut);
                }
            }
            catch 
            {
                throw new Exception("No se ha podido encontrar la ventana: " + claseVentana + " - " + tituloVentana);
            }
        }
        public CoordenadasImg BuscaImagen(string imagen, int precision, int reintentos, int numClicks )
        {
            string [] arguments = new string[4] { Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.CarpetaImagenesAutoit, imagen), 
                                                  precision.ToString(), reintentos.ToString(), numClicks.ToString()};
            int x = 0;
            int y = 0;
            if (!LaunchProgramImg(Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.NombreAPPbuscarImagen), arguments, ref x, ref y))
            {
                
                throw new Exception("No se ha encontrado la imagen: "+ imagen);
            }
            else
            {
                LogUtilities.LogFin("Imagen: "+ imagen + " encontrada -> " + x.ToString() + " - " + y.ToString());
                return new CoordenadasImg(x,y,0);
            }
        }
        public void ClickRaton(ConstantsAutoIt.Click click, int x, int y, int numClicks)
        {
            switch (click)
            {
                case ConstantsAutoIt.Click.LEFT:
                    AutoIt.AutoItX.MouseClick("LEFT", x, y, numClicks);
                    break;
                case ConstantsAutoIt.Click.RIGHT:
                    AutoIt.AutoItX.MouseClick("RIGHT", x, y, numClicks);
                    break;
            }
            LogUtilities.Log("Click -> " + x.ToString()+" / "+ y.ToString());
        }
        public void PressKey(string key)
        {
            AutoIt.AutoItX.Send(key);
        }
        public void Type(string type)
        {
            AutoIt.AutoItX.Send(type);
        }
        public void Wait(int seconds)
        {
            AutoIt.AutoItX.Sleep(seconds * 1000);
        }
        public CoordenadasImg ImageExists(string imagen, int precision, int seconds, int click)
        {
            string[] arguments = new string[4] { Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.CarpetaImagenesAutoit, imagen),
                                                  precision.ToString(), "5", click.ToString()};
            int x = 0;
            int y = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (watch.ElapsedMilliseconds < seconds * 1000)
            {
                if (LaunchProgramImg(Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.NombreAPPbuscarImagen), arguments, ref x, ref y))
                {
                    LogUtilities.LogFin("Imagen: " + imagen + " encontrada -> " + x.ToString() + " - " + y.ToString());
                    return new CoordenadasImg(x, y, 0);
                }
            }
            LogUtilities.LogError("Imagen: " + imagen + "NO encontrada -> " + x.ToString() + " - " + y.ToString());
            return new CoordenadasImg(x, y, -1);
        }
    
        public CoordenadasImg WaitImage(string imagen, int precision, int seconds, int click)
        {
            string[] arguments = new string[4] { Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.CarpetaImagenesAutoit, imagen),
                                                  precision.ToString(), "5", click.ToString()};
            int x = 0;
            int y = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (watch.ElapsedMilliseconds < seconds * 1000)
            {
                if (LaunchProgramImg(Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.NombreAPPbuscarImagen), arguments, ref x, ref y))
                {
                    LogUtilities.LogFin("Imagen: " + imagen + " encontrada -> " + x.ToString() + " - " + y.ToString());
                    return new CoordenadasImg(x, y,0);
                }
            }
            throw new Exception("No se ha podido encontrar la imagen: " + imagen);
        }
        public List<CoordenadasImg> WaitAllImages(string imagen, int precision, int seconds, int click, int py0, int py1, int px0, int px1)
        {
            List<CoordenadasImg> coordenadasList = new List<CoordenadasImg>();
            
            int x = 0;
            int y = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (watch.ElapsedMilliseconds < seconds * 1000)
            {
                int py0Temp = py0;
                for (int i = py0; i < py1; i += 20)
                {
                    
                    string[] arguments = new string[8] { Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.CarpetaImagenesAutoit, imagen),
                                          precision.ToString(), "5", click.ToString(), px0.ToString(), py0Temp.ToString(), px1.ToString(), i.ToString()};
                    if (LaunchProgramImg(Path.Combine(ca.CarpetaAppsAuxiliares, ca.CarpetaAppsAutoit, ca.NombreAPPArea), arguments, ref x, ref y))
                    {
                        LogUtilities.LogFin("Imagen: " + imagen + " encontrada -> " + x.ToString() + " - " + y.ToString());
               
                        if (!coordenadasList.Any(coord => coord.x == x && coord.y == y)) 
                        {
                            coordenadasList.Add(new CoordenadasImg(x, y, 0));
                            MoverRaton(x, y);
                        }
                        i += 100;
                        py0Temp += 100;
                        // Opcional: Desplazarse a la siguiente posición para encontrar más instancias
                        // Puedes ajustar esto según tus necesidades

                    }
                    py0Temp += 10;
                }
                if (coordenadasList.Count > 0)
                {
                    return coordenadasList;
                }

            }

            throw new Exception("No se ha podido encontrar la imagen: " + imagen);
        }

        public string Paste()
        {
            return AutoIt.AutoItX.ClipGet().Trim();
        }
        public void SeleccionarTodo()
        {
            PressKey("{HOME}{LSHIFT}+{END}");
        }
        public void AbrirPestaña(string imageIcons, string image)
        {
            Coordenadas = ImageExists("minimizar.png", 100, 20, 0);
            if (Coordenadas.status == 0)
            {
                ClickRaton(ConstantsAutoIt.Click.LEFT, Coordenadas.x, Coordenadas.y, 1);

            }
            Coordenadas = ImageExists(imageIcons, 100, 20, 0);
            if (Coordenadas.status == 0)
            {
                ClickRaton(ConstantsAutoIt.Click.LEFT, Coordenadas.x, Coordenadas.y, 2);
            }
            else if (Coordenadas.status == -1)
            {
                Coordenadas = WaitImage(image, 100, 30, 0);
                ClickRaton(ConstantsAutoIt.Click.LEFT, Coordenadas.x, Coordenadas.y, 2);
                Wait(3);
                Coordenadas = WaitImage(image, 100, 30, 0);
                ClickRaton(ConstantsAutoIt.Click.LEFT, Coordenadas.x, Coordenadas.y, 2);
            }
        }
        public void Copy()
        {
            AutoIt.AutoItX.ClipPut("");
            AutoIt.AutoItX.Sleep(500);
            AutoIt.AutoItX.Send("{CTRLDOWN}");
            AutoIt.AutoItX.Sleep(500);
            AutoIt.AutoItX.Send("c");

            AutoIt.AutoItX.Send("{CTRLUP}");
            AutoIt.AutoItX.Sleep(500);

            if ("".Equals(AutoIt.AutoItX.ClipGet()))
            {
                AutoIt.AutoItX.ClipPut("");
                AutoIt.AutoItX.Sleep(500);
                AutoIt.AutoItX.Send("{CTRLDOWN}");
                AutoIt.AutoItX.Sleep(500);
                AutoIt.AutoItX.Send("c");

                AutoIt.AutoItX.Send("{CTRLUP}");
                AutoIt.AutoItX.Sleep(500);
            }
        }
        private static bool LaunchProgramImg(string program, string[] args, ref int px, ref int py)
        {
            try
            {
                string arguments = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    arguments += "\"" + args[i] + "\" ";
                }
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = program;
                pProcess.StartInfo.Arguments = arguments;
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.Start();
                string output = pProcess.StandardOutput.ReadToEnd();
                pProcess.WaitForExit();
                if (output.Trim().StartsWith("0 "))
                {
                    string[] coords = output.Trim().Split(' ');
                    px = Int32.Parse(coords[1]);
                    py = Int32.Parse(coords[2]);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


        }
    }
}
