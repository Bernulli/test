using TestProject.PageObject.TestData;

namespace TestProject.PageObject
{
    public class TestUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Office OfficeLocation { get; set; }
        public Gender GenderType { get; set; }
    }
}