using OpenQA.Selenium;
using RobotBase.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotBase.Pages;
using Newtonsoft.Json.Linq;

namespace RobotBase.Utilidades
{
    public class BasePage : IPage
    {
        private IWebDriver driver;
        private SeleniumUtilities su;
        private AloeUtilities aloe;
        private JObject jo;
        public BasePage(SeleniumUtilities su, AloeUtilities aloe, JObject jo) {

            Set(su, aloe, jo);
        }
        public IWebDriver Driver => driver;
        public SeleniumUtilities Su => su;
        public AloeUtilities Aloe => aloe;
        public JObject Jo => jo;

        public void Set(SeleniumUtilities su, AloeUtilities aloe, JObject jo)
        {
            this.driver = su.Driver;
            this.su = su;
            this.aloe = aloe;
            this.jo = jo;
        }
    }
}
