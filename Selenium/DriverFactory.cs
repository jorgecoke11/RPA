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
                IWebDriver driver = null;
                switch (navegador)
                {
                    case ConstantsSelenium.NAVEGADOR.EDGE:
                       
                        break;
                      
                    case ConstantsSelenium.NAVEGADOR.FIREFOX:
                        string downloadDir = @"C:\Temporal\";
                        var options = new FirefoxOptions();
                        options.AcceptInsecureCertificates = true;
                        options.SetPreference("browser.download.folderList", 2);
                        options.SetPreference("browser.download.manager.showWhenStarting", false);
                        options.SetPreference("browser.download.panel.shown", false);
                        options.SetPreference("browser.download.manager.useWindow", false);
                        options.SetPreference("browser.download.dir", downloadDir);
                        options.SetPreference("browser.download.useDownloadDir", true);
                        options.SetPreference("browser.helperApps.neverAsk.saveToDisk", Constant.APP);
                        options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                        options.SetPreference("pdfjs.disabled", true); // disable the built-in PDF viewer
                        driver = new FirefoxDriver(pathGecko, options);
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
