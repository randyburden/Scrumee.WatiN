using WatiN.Core;

namespace Scrumee.Tests.WatiN.Views.Projects
{
    public class UserStoryDetailsPage : BasePage
    {
        public string Title = "User Story Details";

        public Span TaskName
        {
            get { return Document.Span( Find.By( "data-id", "1" ) ); }
        }

        [FindBy( Class = "reader edit", Text = "Edit" )]
        public Link EditLink;

        [FindBy( Class = "editor cancel", Text = "Cancel" )]
        public Link CancelLink;

        [FindBy( Class = "editor update", Text = "Save" )]
        public Link SaveLink;

        [FindBy( Class = "editor delete", Text = "Delete" )]
        public Link DeleteLink;

        [FindBy( Class = "editor name" )]
        public TextField TaskNameTextBox;

        [FindBy( Class = "editor user" )]
        public SelectList AssignedToSelectList;

        [FindBy( Id = "addTask" )]
        public TableRow AddNewTaskTableRow;

        [FindBy( Class = "popup" )]
        public Div CreateNewTaskForm;

        [FindBy( Id = "txtName" )]
        public TextField NewTaskNameTextBox;

        [FindBy( Id = "btnSubmit" )]
        public Button CreateNewTaskButton;
    }
}
