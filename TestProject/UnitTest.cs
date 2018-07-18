using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TestProject.PageObject;
using TestProject.PageObject.TestData;

namespace TestProject
{
    [TestFixture]
    public class UnitTest
    {
        private IWebDriver Driver { get; set; }

        [SetUp]
        public void InitializeBrowser()
        {
            Driver = new FirefoxDriver();
        }

        [Test]
        public void Test()
        {
            var signInPage = new SignInPage(Driver);
            signInPage.GoTo();
            Assert.IsTrue(signInPage.IsVisible);

            var usersListPage = signInPage.ClickSignInBtn("admin@mail.com", "abc123");
            Assert.IsTrue(usersListPage.title.Displayed);
            usersListPage.ClickNewBtn();
            Assert.IsTrue(usersListPage.modalWindow.Displayed);

            var user = new TestUser
            {
                FirstName = "Joe",
                LastName = "Satriani",
                Email = "Joe@gmail.com",
                OfficeLocation = Office.New_York,
                GenderType = Gender.Male
            };
            usersListPage.ClickModalWindowBtn(user);

            Assert.IsTrue(usersListPage.title.Displayed);
            usersListPage.IfNewUserExist();
            var viewUserPage = usersListPage.ClickViewBtn(user);
            viewUserPage.VerifyData(user);
        }

        [TearDown]
        public void Stop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}
