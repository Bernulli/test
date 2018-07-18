using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using TestProject.PageObject.TestData;

namespace TestProject.PageObject
{
    /// <summary>
    /// Назви міст зберігаю в enum.
    /// Щоб позбутися '_' в назві міста New_York
    /// використовую делегат
    /// </summary>
    delegate string ReplaceNY(string s);

    public class UsersPage : BaseApp
    {
        public IWebElement CreateBtn => Driver.FindElement(By.XPath("//button[contains(., 'Create')]"));

        public IWebElement NewBtn => Driver.FindElement(By.XPath("//button[contains(., 'New')]"));

        private IWebElement FirstName => Driver.FindElement(By.Id("first-name"));

        private IWebElement LastName => Driver.FindElement(By.Id("last-name"));

        private IWebElement Email => Driver.FindElement(By.Id("email"));

        private IWebElement OfficeDDL => Driver.FindElement(By.Id("office"));

        private IWebElement GenderRadioButton => Driver.FindElement(By.XPath($"//input[@name='gender']"));

        private IWebElement table => Driver.FindElement(By.XPath("//div/table"));

        public IWebElement title => Driver.FindElement(By.XPath("//div[@class='page-header']/h1"));

        private IWebElement viewBtn;

        private WebDriverWait wait { get; set; }

        public IWebElement modalWindow;

        public string str;

        public bool IsVisible => Driver.Title.Contains("Users");

        public UsersPage(IWebDriver Driver) : base(Driver)
        {
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        public void ClickNewBtn()
        {
            NewBtn.Click();
            modalWindow = wait.Until(Driver => Driver.FindElement(By.XPath("//*[@class='modal-dialog']")));
        }

        public void ClickModalWindowBtn(TestUser user)
        {
            FillModalWindow(user);
            CreateBtn.Click();
        }

        private void FillModalWindow(TestUser user)
        {
            FirstName.Click();
            FirstName.Clear();
            FirstName.SendKeys(user.FirstName);

            LastName.Click();
            LastName.Clear();
            LastName.SendKeys(user.LastName);

            Email.Clear();
            Email.Click();
            Email.SendKeys(user.Email);

            SetOffice(user);
            SetGender(user);
        }

        private void SetGender(TestUser user)
        {
            switch (user.GenderType)
            {
                case Gender.Male:
                    GenderRadioButton.Click();
                    break;
                case Gender.Female:
                    GenderRadioButton.Click();
                    break;
                default:
                    break;
            }
        }

        public string Replace(string s)
        {
            return s.Replace('_', ' ');
        }

        private void SetOffice(TestUser user)
        {
            var select_country = new SelectElement(OfficeDDL);
            switch (user.OfficeLocation)
            {
                case Office.Berlin:
                    select_country.SelectByValue(Office.Berlin.ToString());
                    break;
                case Office.London:
                    select_country.SelectByValue(Office.London.ToString());
                    break;
                case Office.New_York:
                    ReplaceNY replace = Replace;
                    str = replace(Office.New_York.ToString());
                    select_country.SelectByValue(str);
                    break;
                case Office.Paris:
                    select_country.SelectByValue(Office.Paris.ToString());
                    break;
                case Office.Rome:
                    select_country.SelectByValue(Office.Rome.ToString());
                    break;
                case Office.Tokio:
                    select_country.SelectByValue(Office.Tokio.ToString());
                    break;
                case Office.Washington:
                    select_country.SelectByValue(Office.Washington.ToString());
                    break;
                default:
                    break;
            }
        }

        public void IfNewUserExist()
        {
            IList<IWebElement> list = table.FindElements(By.XPath("//div/table/tbody/tr"));

            if (list.Count == 2)
            {
                throw new NoSuchElementException();
            }
        }

        public ViewUserPage ClickViewBtn(TestUser user)
        {
            IList<IWebElement> list = table.FindElements(By.XPath("//div//table/tbody/tr"));
            var counter = list.Count;
            viewBtn = Driver.FindElement(By.XPath($"//a[@href='#!/users/{counter}']"));
            viewBtn.Click();
            wait.Until(Driver => Driver.FindElement(By.XPath($"//h1[contains(., '{user.FirstName}')]")));
            return new ViewUserPage(Driver, str);
        }
    }
}