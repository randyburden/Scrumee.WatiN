using WatiN.Core;

namespace Scrumee.Tests.WatiN.Views.Projects
{
    public class AllPage : BasePage
    {
        public string Title = "Add Projects";

        public Span ProjectNameSpan
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
        public TextField ProjectNameTextBox;

        [FindBy( Id = "addProject" )]
        public TableRow AddNewProjectTableRow;

        [FindBy( Class = "popup" )]
        public Div CreateNewProjectForm;

        [FindBy( Id = "txtName")]
        public TextField NewProjectNameTextBox;

        [FindBy( Id = "btnSubmit")]
        public Button CreateNewProjectButton;
    }
}
