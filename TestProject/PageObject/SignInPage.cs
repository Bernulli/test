using OpenQA.Selenium;

namespace TestProject.PageObject
{
    class SignInPage : BaseApp
    {
        public IWebElement Email => Driver.FindElement(By.Id("email"));
        public IWebElement Password => Driver.FindElement(By.Id("password"));
        public IWebElement SignInBtn => Driver.FindElement(By.XPath("//input[@value='Sign In']"));
        public bool IsVisible => Driver.Title.Contains("Sign In");

        public SignInPage(IWebDriver driver) : base(driver) { }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/signin");
        }

        public UsersPage ClickSignInBtn(string email, string password)
        {
            FillEMail(email);
            FillPassword(password);
            SignInBtn.Click();
            return new UsersPage(Driver);
        }

        private void FillEMail(string email)
        {
            Email.Click();
            Email.Clear();
            Email.SendKeys(email);
        }

        private void FillPassword(string password)
        {
            Password.Click();
            Password.Clear();
            Password.SendKeys(password);
        }
    }
}
