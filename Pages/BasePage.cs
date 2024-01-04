using OpenQA.Selenium;
using RobotBase.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotBase.Pages;

namespace RobotBase.Utilidades
{
    public class BasePage : IPage
    {
        private IWebDriver driver;
        private SeleniumUtilities su;
        private AloeUtilities aloe;
        public BasePage(SeleniumUtilities su, AloeUtilities aloe) {

            Set(su, aloe);
        }
        public IWebDriver Driver => driver;
        public SeleniumUtilities Su => su;
        public AloeUtilities Aloe => aloe;

        public void Set(SeleniumUtilities su, AloeUtilities aloe)
        {
            this.driver = su.Driver;
            this.su = su;
            this.aloe = aloe;
        }
    }
}
