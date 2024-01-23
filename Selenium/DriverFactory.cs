using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotBase.Utilidades
{
    public static class DriverFactory
    {
        public static IWebDriver SetDriver(ConstantsSelenium.NAVEGADOR navegador)
        {
            try
            {
                IWebDriver driver = null;
                switch (navegador)
                {
                    case ConstantsSelenium.NAVEGADOR.EDGE:
                        InternetExplorerDriverService service = InternetExplorerDriverService.CreateDefaultService();
                        
                        InternetExplorerOptions optionsEdge = new InternetExplorerOptions();
                        optionsEdge.AttachToEdgeChrome = true;
                        optionsEdge.IgnoreZoomLevel = true;
                        optionsEdge.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;// Necesario para que no cierre las alertas de forma automática 
                        optionsEdge.EdgeExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                        int intentos = 0;
                        while (intentos < 10)
                        {
                            try
                            {
                                driver = new InternetExplorerDriver(service, optionsEdge);
                                break; // Si tiene éxito, sal del bucle
                            }
                            catch (Exception ex)
                            {
                                intentos++;
                                // Puedes registrar la excepción si lo deseas
                                Console.WriteLine($"Intento {intentos} fallido. Excepción: {ex.Message}");
                            }
                        }
                        
                        break;
                      
                    case ConstantsSelenium.NAVEGADOR.FIREFOX:
                        FirefoxOptions options = new FirefoxOptions();
                        options.AcceptInsecureCertificates = true; //Permite aceptar certificados no válidos 
                        options.SetPreference("browser.download.folderList", 2); // Indica que las descargas se harán en una ubicación personalizada
                        options.SetPreference("browser.download.manager.showWhenStarting", false); // No muestra la ventana emergente del adm de descargas
                        options.SetPreference("browser.download.dir", ""); // Configura la ubicación de la carpeta de descarga
                        //options.SetPreference("browser.helperApps.neverAsk.saveToDisk", Constant.APP); // Indica los archivos o el tipo MIME que se guardan automáticamente
                        options.SetPreference("pdfjs.disabled", true); // Deshabilita el visor PDF por defecto incorporado por el navegador(PDF.js)
                        driver = new FirefoxDriver( options);
                        break;
                }
                return driver;
            }catch (Exception ex)
            {
                Console.WriteLine("No se ha podido inicializar el driver " + Environment.NewLine + ex.Message);
                throw new Exception("No se ha podido inicializar el driver " + Environment.NewLine + ex.Message);
            }
        }
    }
}
