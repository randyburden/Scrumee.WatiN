using WatiN.Core;

namespace Scrumee.Tests.WatiN.Views.Projects
{
    public class ProjectDetailsPage : BasePage
    {
        public string Title = "Project Details";

        public Span UserStoryName
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
        public TextField UserStoryTextBox;

        [FindBy( Id = "addUserStory")]
        public TableRow AddNewUserStoryTableRow;

        [FindBy( Class = "popup" )]
        public Div CreateNewUserStoryForm;

        [FindBy( Id = "txtName" )]
        public TextField NewUserStoryNameTextBox;

        [FindBy( Id = "btnSubmit" )]
        public Button CreateNewUserStoryButton;
    }
}
