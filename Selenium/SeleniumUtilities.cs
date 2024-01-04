using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RobotBase.Utilidades;
using OpenQA.Selenium.Interactions;
using static RobotBase.Utilidades.ConstantsSelenium;

namespace RobotBase.Utilidades
{
    public class SeleniumUtilities
    {
        public IWebDriver Driver { get; set; }
        private List<string> listaVentanas;
        private ConstantsSelenium.NAVEGADOR navegador;
        private string ultimaVentana;
        public SeleniumUtilities(ConstantsSelenium.NAVEGADOR navegador) 
        {
            this.navegador = navegador;
        }
        public void StartDriver()
        {
            Driver = DriverFactory.SetDriver(navegador);
            listaVentanas = new List<string>(Driver.WindowHandles);
        }
        public void CleanAndType(By locator, string text)
        {
            WebDriverWait wait = NewWait(60);
            wait.Until(d => d.FindElement(locator).Displayed);

            Driver.FindElement(locator).Clear();
          //  CleanJS(locator);
            Driver.FindElement(locator).SendKeys(text);
        }
        public void CleanAndTypeJS(By locator, string text)
        {
            WebDriverWait wait = NewWait(60);
            wait.Until(d => d.FindElement(locator).Displayed);
            CleanJS(locator);
            Driver.FindElement(locator).SendKeys(text);
        }
        public void Type(By locator, string text)
        {
            Driver.FindElement(locator).SendKeys(text);
        }
        public void WaitAndType( By locator, string text, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
            wait.Until(d=>d.FindElement(locator).Displayed);
            Driver.FindElement(locator).SendKeys(text);
        }
        public void CleanJS(By locator)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
            var element = Driver.FindElement(locator);
            jsExecutor.ExecuteScript("arguments[0].value = '';", element);
        }
        public WebDriverWait NewWait(double seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
            wait.IgnoreExceptionTypes(typeof(ElementNotSelectableException));
            wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            return wait;
        }
        public void WaitAndClick(By locator, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
           
            wait.Until(d=>d.FindElement(locator));
            ClickJs(locator);
        }
        public void WaitAndClickNoJS(By locator, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);

            wait.Until(d => d.FindElement(locator));
            Click(locator);
        }
        public void ScrollElement(By locator)
        {
            IWebElement element = Driver.FindElement(locator);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element).Click().Build().Perform();
        }
        public void MoveToElementAndClick(By locator)
        {
            IWebElement element = Driver.FindElement(locator);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element).Click().Build().Perform();

        }
        public void MoveToElementByIdJs(string id)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("document.getElementById('"+id+"').scrollIntoView();");
        }
        public void Click(string id, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
            wait.Until(d => d.FindElement(By.Id(id)).Displayed);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("document.getElementById('"+id+"').click()");

        }
        public void ClickJs(By locator)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            var element = Driver.FindElement(locator);
            jse.ExecuteScript("arguments[0].click();", element);
        }
        public void ClickJs(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            jse.ExecuteScript("arguments[0].click();", element);
        }
        public void Click(By locator)
        {
            WebDriverWait wait = NewWait(60);
            wait.Until(d => d.FindElement(locator).Displayed);
            Driver.FindElement(locator).Click();
        }
        public string GetTextElement(By locator)
        {
            WebDriverWait wait = NewWait(60);
            wait.Until(d=>d.FindElement(locator).Displayed);
            return Driver.FindElement(locator).Text.Trim();
        }
        public void Click(IWebElement element)
        {
            WebDriverWait wait = NewWait(60);
            wait.Until(d => element.Displayed);
            element.Click();
        }
        public void WaitShadeDisapear()
        {
            var wait = NewWait(60);
            wait.Until(driver => !driver.FindElement(By.Id("shade")).Displayed);
        }
        public void ClickSelectOption(SelectElement select, IWebElement option)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript($"arguments[0].value='{option.GetAttribute("value")}';", select);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(option).Build().Perform();
            actions.Click().Build().Perform();
        }
        public bool ElementExists(By locator, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
            try
            {
                wait.Until(d=>d.FindElement(locator).Displayed);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public void ClickIfExists(By locator, int seconds)
        {
            if(ElementExists(locator, seconds))
            {
                Click(locator);
            }
        }
        public void EsperarElementoVisible(TipoBusqueda tipo, string cadenaBusqueda, double segundos)
        {
            try
            {
                WebDriverWait wait = NewWait(segundos);
                By tipoBusqueda;

                switch (tipo)
                {
                    case TipoBusqueda.ID:
                        tipoBusqueda = By.Id(cadenaBusqueda);
                        break;
                    case TipoBusqueda.XPATH:
                        tipoBusqueda = By.XPath(cadenaBusqueda);
                        break;
                    case TipoBusqueda.CSS:
                        tipoBusqueda = By.CssSelector(cadenaBusqueda);
                        break;
                    default:
                        throw new Exception("Debe indicar el tipo de búsqueda");
                }
                wait.Until(dvr => dvr.FindElement(tipoBusqueda).Displayed);

            }
            catch (Exception ex)
            {

                throw new Exception("No se pudo encontrar el elemnto : " + cadenaBusqueda + Environment.NewLine + ex.Message);
            }
        }
        public void GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
        public void SwithToIframe(string idFrame, double segundos)
        {
            LogUtilities.LogInicio("Cambiando al frame: " + idFrame);

            WebDriverWait wait = NewWait(segundos);
            // Esperar a que se inicie la sesión // 
            EsperarElementoVisible(TipoBusqueda.ID, idFrame, segundos);
            // Cambiamos al frame que contiene el div de referencia
            IWebElement iframe = Driver.FindElement(By.Id(idFrame));
            Driver.SwitchTo().Frame(iframe);
            LogUtilities.LogFin("Cambio de frame");
        }
        public void EsperarElementoNoVisible(TipoBusqueda tipo, string cadenaBusqueda, double segundos)
        {
            try
            {
                WebDriverWait wait = NewWait(segundos);
                By tipoBusqueda;

                switch (tipo)
                {
                    case TipoBusqueda.ID:
                        tipoBusqueda = By.Id(cadenaBusqueda);
                        break;
                    case TipoBusqueda.XPATH:
                        tipoBusqueda = By.XPath(cadenaBusqueda);
                        break;
                    case TipoBusqueda.CSS:
                        tipoBusqueda = By.CssSelector(cadenaBusqueda);
                        break;
                    default:
                        throw new Exception("Debe indicar el tipo de búsqueda");
                }
                wait.Until(dvr => !dvr.FindElement(tipoBusqueda).Displayed);

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo encontrar el elemnto : " + cadenaBusqueda + Environment.NewLine + ex.Message);
            }
        }
        public string SeleccionarNuevaVentana(double segundos)
        {
            try
            {
                List<string> windows = new List<string>(Driver.WindowHandles);
                WebDriverWait wait = NewWait(segundos);

                // Esperar a que aparezca la ventana de pólizas
                Stopwatch watch = new Stopwatch();
                watch.Start();
                while (windows.Count < listaVentanas.Count() + 1 && watch.ElapsedMilliseconds < segundos * 1000)
                {
                    Thread.Sleep(1000);
                    windows = new List<string>(Driver.WindowHandles);
                }
                watch.Stop();
                if (windows.Count < listaVentanas.Count() + 1)
                {
                    throw new Exception("No se ha abierto una nueva ventana");
                }
                // Cambio el contenido a la nueva ventana
                string nuevaVentana = windows.Except(listaVentanas).FirstOrDefault();
                ultimaVentana = Driver.CurrentWindowHandle;
                Driver.SwitchTo().Window(nuevaVentana);
                Driver.Manage().Window.Maximize();
                // Actualizamos el listado de ventanas abiertas
                listaVentanas.Add(nuevaVentana);
                return nuevaVentana;
            }
            catch (Exception exc)
            {

                throw new Exception("No se ha encontrado ninguna ventana nueva " + Environment.NewLine + exc.Message);
            }
        }
        public void SeleccionarVentanaAnterior()
        {
            try
            {
                listaVentanas.RemoveAt(listaVentanas.Count - 1);
                Driver.SwitchTo().Window(listaVentanas.Last());
            }catch(Exception exc)
            {
                throw new Exception("No se pudo seleccionar la ventana anterior: " + listaVentanas.Last() 
                                    + Environment.NewLine + exc.Message); 
            }
        }
        public void AceptarAlerta(bool cierraVentana =false)
        {
            Driver.SwitchTo().Alert().Accept();
            if (cierraVentana)
            {
                SeleccionarVentanaAnterior();
            }
        }
        public IWebElement GetOpcionElemento(By locator, string opcion, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
            wait.Until(d=>d.FindElement(locator).Displayed);
            return Driver.FindElements(locator).FirstOrDefault(o => o.Text.Contains(opcion));
        }
        public void SelectOptionFromSelect(By locator, By locatorOption, string option, int seconds)
        {
            Click(locator);
            IWebElement optionElement = GetOpcionElemento(locatorOption, option, seconds);
            SelectElement select = new SelectElement(Driver.FindElement(locator));
            ClickSelectOption(select, optionElement);
        }
        public bool EsperarAlerta(int seconds)
        {
            try
            {
                WebDriverWait wait = NewWait(seconds);
                wait.Until(d => d.SwitchTo().Alert());
            }
            catch
            {
                return false;
            }
            return true;
        }
        internal void StopDriver()
        {
            try
            {
                Driver.Close();
                Driver.Quit();

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cerrar el driver " + Environment.NewLine + ex.Message);

            }
            if (navegador.Equals(NAVEGADOR.EDGE)){
                Process[] procesos = Process.GetProcessesByName("msedgewebview2");
                foreach (Process pro in procesos) { pro.Kill(); }

                procesos = Process.GetProcessesByName("msedge");
                foreach (Process pro in procesos) { pro.Kill(); }
            }
        }

        internal void MaximizarVentana()
        {
            Driver.Manage().Window.Maximize();
        }
    }

}
