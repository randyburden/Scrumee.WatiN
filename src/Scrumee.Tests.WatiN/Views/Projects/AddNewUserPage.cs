using WatiN.Core;

namespace Scrumee.Tests.WatiN.Views.Projects
{
    public class AddNewUserPage : BasePage
    {
        public string Title = "Add Projects";
        
        [FindBy( Id = "firstName")]
        public TextField FirstName;

        [FindBy( Id = "lastName" )]
        public TextField LastName;

        [FindBy( Id = "btnSubmit" )]
        public Button AddNewUserButton;
    }
}
