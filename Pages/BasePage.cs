using OpenQA.Selenium;
using RobotBase.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotBase.Pages;
using Newtonsoft.Json.Linq;
using P01_B03.Models;

namespace RobotBase.Utilidades
{
    public class BasePage : IPage
    {
        private IWebDriver driver;
        private SeleniumUtilities su;
        private AloeUtilities aloe;
        private IBusinessModel business_model;
        public BasePage(SeleniumUtilities su, AloeUtilities aloe, IBusinessModel business_model = null) {
            if (business_model != null)
            {
                Set(su, aloe, business_model);
            }
            else
            {
                Set(su, aloe);
            }
        }
        public IWebDriver Driver => driver;
        public SeleniumUtilities Su => su;
        public AloeUtilities Aloe => aloe;
        public IBusinessModel Business_model => business_model;

        public void Set(SeleniumUtilities su, AloeUtilities aloe, IBusinessModel business_model)
        {
            this.driver = su.Driver;
            this.su = su;
            this.aloe = aloe;
            if (business_model != null)
            {
                this.business_model = business_model;
            }
        }
    }
}
