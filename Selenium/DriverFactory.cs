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
        public static IWebDriver SetDriver(ConstantsSelenium.NAVEGADOR navegador, string pathGecko)
        {
            try
            {
                IWebDriver? driver = null;
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
                        FirefoxOptions? options = new FirefoxOptions();

                        options.AcceptInsecureCertificates = true;
                        options.SetPreference("browser.download.folderList", 2);
                        options.SetPreference("browser.download.manager.showWhenStarting", false);
                        options.SetPreference("browser.download.panel.shown", false);
                        options.SetPreference("browser.download.manager.useWindow", false);
                        options.SetPreference("browser.download.dir", @"C:\Temporal");
                        options.SetPreference("browser.download.useDownloadDir", true);
                        options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf;application/zip;application/x-compressed;application/x-zip-compressed;multipart/x-zip");
                        options.SetPreference("pdfjs.disabled", true); // disable the built-in PDF viewer
                        driver = new FirefoxDriver(pathGecko, options);
                        break;
                    default:
                        throw new Exception("Navegador no contemplado");
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
