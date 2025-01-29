using ExecuteAutomation.Reqnroll.Dynamics;
using Microsoft.Playwright;
using PlaywrightTestDemo.Pages;
using Reqnroll;

namespace PlaywrightTestDemo.Steps
{
    [Binding]
    public class CreateUserSteps
    {
        private HomePage _homePage;
        private EmployeeList _employeeList;
        private CreateUser _createUser;
        private readonly ScenarioContext _scenarioContext;

        public CreateUserSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _homePage = new HomePage((IPage)_scenarioContext["Page"]);
        }

        [When("I click the Employee list link")]
        public async Task WhenIClickTheEmployeeListLink()
        {
            _employeeList = await _homePage.ClickEmployeeListLinkAsync();
        }

        [When("I click the Create New Button")]
        public async Task WhenIClickTheCreateNewButton()
        {
            _createUser = await _employeeList.ClickCreateNewAsync();
        }

        [When("I start creating user with following details")]
        public async Task WhenIStartCreatingUserWithFollowingDetails(DataTable dataTable)
        {
            ////Tables
            ///
            //Way 1
            //foreach (var row in dataTable.Rows)
            //{
            //    var name = row["Name"];
            //    var salary = row["Salary"];
            //    int durationWorked = int.Parse(row["Duration Worked"]);
            //    var grade = row["Grade"];
            //    var email = row["Email"];

            //    var userData = new UserData(name, salary, durationWorked, grade, email);

            //    await _createUser.CreateUserAysnc(userData);
            //}

            //Way 2: With Type of UserData
            //var userData = dataTable.CreateInstance<UserData>();
            //await _createUser.CreateUserAysnc(userData);



            //Way 3 - With Dynamic types
            dynamic data = dataTable.CreateDynamicInstance();
            var userData = new UserData((string)data.Name, "10000", (int)data.DurationWorked, (string)data.Grade, (string)data.Email);
            await _createUser.CreateUserAysnc(userData);


            //With Iteration for multiple Rows
            //dynamic datas = dataTable.CreateDynamicSet();

            //foreach (dynamic data in datas)
            //{
            //    var userData = new UserData((string)data.Name, "10000", (int)data.DurationWorked, (string)data.Grade, (string)data.Email);
            //    await _createUser.CreateUserAysnc(userData);
            //}

        }

        [When("I verify the user {string} is created")]
        public void WhenIVerifyTheUserIsCreated(string demoUser)
        {
            Console.WriteLine(_scenarioContext["UserName"]);
        }

    }
}
