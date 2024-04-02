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
using OpenQA.Selenium.Support.Extensions;
using System.Web.ModelBinding;

namespace RobotBase.Utilidades
{
    public class SeleniumUtilities
    {
        public IWebDriver Driver { get; set; }
        private List<string> listaVentanas;
        private ConstantsSelenium.NAVEGADOR navegador;
        private string ultimaVentana;
        private int numVentanasAbiertas = 0;
        private const int TIEMPO_ESPERA = 60;
        public SeleniumUtilities(ConstantsSelenium.NAVEGADOR navegador) 
        {
            this.navegador = navegador;
        }
        public void StartDriver()
        {
            Driver = DriverFactory.SetDriver(navegador);
            listaVentanas = new List<string>(Driver.WindowHandles);
            ultimaVentana = listaVentanas.Last();
            numVentanasAbiertas = listaVentanas.Count;
        }
        public void PressKey(ConstantsSelenium.KeysSelenium key)
        {
            Actions actions = new Actions(Driver);
            switch (key)
            {
                case ConstantsSelenium.KeysSelenium.ESCAPE:
                    actions.SendKeys(Keys.Escape).Build().Perform();
                    break;
            }
        }
        public void UpdateListaVentanasYSwitch()
        {
            listaVentanas = new List<string>(Driver.WindowHandles);
            Stopwatch wath = new Stopwatch();
            wath.Start();
            while (listaVentanas.Last() == ultimaVentana && wath.ElapsedMilliseconds < TIEMPO_ESPERA * 1000)
            {
                listaVentanas = new List<string>(Driver.WindowHandles);
                Thread.Sleep(1000);
            }
            if (listaVentanas.Last () == ultimaVentana || wath.ElapsedMilliseconds > (TIEMPO_ESPERA * 1000))
            {
                throw new Exception("No se ha abierto correctamente la ventana de GRECO");
            }
            wath.Stop();
            ultimaVentana = listaVentanas.Last();
            numVentanasAbiertas = listaVentanas.Count;
            Driver.SwitchTo().Window(listaVentanas.Last());
            Thread.Sleep(2000);
            listaVentanas = new List<string>(Driver.WindowHandles);
        }
        public void CleanAndType(By locator, string text)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            wait.Until(d => d.FindElement(locator).Displayed);
            Driver.FindElement(locator).Clear();
            Driver.FindElement(locator).SendKeys(text);
        }
        public void CleanAndTypeJS(By locator, string text)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            wait.Until(d => d.FindElement(locator).Displayed);
            CleanJS(locator);
            Driver.FindElement(locator).SendKeys(text);
        }
        public void Type(By locator, string text, TipoWait tipoWait = TipoWait.DISPLAYED)
        {
            Wait(locator, tipoWait);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            Driver.FindElement(locator).SendKeys(text);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public void TypeJs(By locator, string text, TipoWait tipoWait = TipoWait.DISPLAYED)
        {
            Wait(locator, tipoWait);
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
            Thread.Sleep(1000);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            jsExecutor.ExecuteScript("arguments[0].value = '" + text + "';", Driver.FindElement(locator));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public void TypeTextContent(IWebElement element, string text, TipoWait tipoWait = TipoWait.DISPLAYED)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
            Thread.Sleep(1000);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            jsExecutor.ExecuteScript("arguments[0].innerText = arguments[1];", element, text);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public void AbrirNuevaPestaña()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");
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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            jsExecutor.ExecuteScript("arguments[0].value = '';", element);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public WebDriverWait NewWait(double seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
            wait.IgnoreExceptionTypes(typeof(ElementNotSelectableException));
            wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            wait.IgnoreExceptionTypes(typeof(InvalidSelectorException));
            return wait;
        }
        public void WaitAndClick(By locator, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
           
            wait.Until(d=>d.FindElement(locator));
            ClickJs(locator);
        }
        public void ScrollElementActions(By locator)
        {
            IWebElement element = Driver.FindElement(locator);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element).Click().Build().Perform();
        }
        public void MoveToElementJs(By locator)
        {
            IWebElement element = Driver.FindElement(locator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView()", element);
        }
        public void MoveToElementAndClickActions(By locator)
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
        public void ClickByIdJs(string id, int seconds)
        {
            WebDriverWait wait = NewWait(seconds);
            wait.Until(d => d.FindElement(By.Id(id)).Displayed);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("document.getElementById('"+id+"').click()");
        }
        public void Click(IWebElement element)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            wait.Until(d => element.Displayed);
            Thread.Sleep(1000);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            element.Click();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public void DoubleClickJs(IWebElement element)
        {
            Driver.ExecuteJavaScript("arguments[0].dispatchEvent(new MouseEvent('dblclick', { bubbles: true }));", element);
        }
        public void ClickJs(By locator, TipoWait tipoWait = TipoWait.DISPLAYED)
        {
            Wait(locator, tipoWait);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            var element = Driver.FindElement(locator);
            Thread.Sleep(1000);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            jse.ExecuteScript("arguments[0].click();", element);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public void Wait(By locator, TipoWait tipoWait)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            switch (tipoWait)
            {
                case TipoWait.DISPLAYED:
                    wait.Until(d => d.FindElement(locator).Displayed);
                    break;
                case TipoWait.ENABLED:
                    wait.Until(d => d.FindElement(locator).Enabled);
                    break;
            }
        }
        public IWebElement GetWebElement(By locator, TipoWait tipoWait = TipoWait.DISPLAYED)
        {
            Wait(locator, tipoWait);
            return Driver.FindElement(locator);
        }
        public void ClickActions(By locator)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            wait.Until(d => d.FindElement(locator).Displayed);
            Actions actions = new Actions(Driver);
            IWebElement element = Driver.FindElement(locator);
            actions.Click(element).Build().Perform();
        }
        public void ClickJs(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            Thread.Sleep(1000);
            jse.ExecuteScript("arguments[0].click();", element);
        }
        public void Click(By locator)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            wait.Until(d => d.FindElement(locator).Displayed);
            Thread.Sleep(1000);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIEMPO_ESPERA);
            Driver.FindElement(locator).Click();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }
        public string GetTextElement(By locator)
        {
            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            wait.Until(d=>d.FindElement(locator).Displayed);
            return Driver.FindElement(locator).Text.Trim();
        }

        public void WaitShadeDisapear()
        {
            var wait = NewWait(TIEMPO_ESPERA);
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
        public void SwithToIframe(By locator)
        {


            WebDriverWait wait = NewWait(TIEMPO_ESPERA);
            // Esperar a que se inicie la sesión // 
            wait.Until(d => d.FindElement(locator).Displayed);
            // Cambiamos al frame que contiene el div de referencia
            IWebElement iframe = Driver.FindElement(locator);
            Driver.SwitchTo().Frame(iframe);
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
                    Thread.Sleep(1500);
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
        public void SelectOptionFromSelect(By locatorSelect, By locatorOption, string option, int seconds)
        {
            Click(locatorSelect);
            IWebElement optionElement = GetOpcionElemento(locatorOption, option, seconds);
            SelectElement select = new SelectElement(Driver.FindElement(locatorSelect));
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
            else if (navegador.Equals(NAVEGADOR.FIREFOX))
            {
                Process[] procesos = Process.GetProcessesByName("firefox");
                foreach (Process pro in procesos) { pro.Kill(); }
                procesos = Process.GetProcessesByName("geckodriver");
                foreach (Process pro in procesos) { pro.Kill(); }
            }else if (navegador.Equals(NAVEGADOR.EXPLORER))
            {
                Process[] procesos = Process.GetProcessesByName("msedgewebview2");
                foreach (Process pro in procesos) { pro.Kill(); }

                procesos = Process.GetProcessesByName("msedge");
                foreach (Process pro in procesos) { pro.Kill(); }

                procesos = Process.GetProcessesByName("iexplorer");
                foreach (Process pro in procesos) { pro.Kill(); }
            }
        }

        internal void MaximizarVentana()
        {
            Driver.Manage().Window.Maximize();
        }
    }

}
