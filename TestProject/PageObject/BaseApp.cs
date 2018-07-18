using OpenQA.Selenium;

namespace TestProject.PageObject
{
    public class BaseApp
    {
        protected IWebDriver Driver { get; set; }

        public BaseApp(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
