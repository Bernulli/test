using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace TestProject.PageObject
{
    public class ViewUserPage : BaseApp
    {
        private IWebElement table => Driver.FindElement(By.XPath("//div[@class='row details-list']"));
        public bool IsVisible => Driver.Title.Contains("Sign In");
        private string str;

        public ViewUserPage(IWebDriver Driver, string str) : base(Driver)
        {
            this.str = str;
        }

        public void VerifyData(TestUser user)
        {
            IList<IWebElement> list = table.FindElements(By.TagName("dl"));

            for (int i = 0; i < list.Count; i++)
            {
                var row = list[i];
                IList<IWebElement> dd = row.FindElements(By.TagName("dd"));

                foreach (var definition in dd)
                {
                    if (definition.Text == user.Email || definition.Text == user.GenderType.ToString()
                        || definition.Text == user.OfficeLocation.ToString() || definition.Text == str)
                    {
                        continue;
                    }
                    else
                    {
                        throw new NoSuchElementException();
                    }
                }
            }
        }
    }
}