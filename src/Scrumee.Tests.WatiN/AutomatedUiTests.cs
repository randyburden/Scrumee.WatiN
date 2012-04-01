using NUnit.Framework;
using Scrumee.Tests.WatiN.Helpers;
using Scrumee.Tests.WatiN.Views.Projects;
using WatiN.Core;

namespace Scrumee.Tests.WatiN
{
    [TestFixture]
    [RequiresSTA]
    public class AutomatedUiTests
    {
        #region Private Fields

        private static IEStaticInstanceHelper _helper;
        private static IE _browser;

        #endregion Private Fields

        #region Setup and Helper Methods

        [SetUp]
        public void Setup()
        {
            if ( _helper == null )
            {
                Settings.Instance.AutoMoveMousePointerToTopLeft = false;
                Settings.Instance.MakeNewIeInstanceVisible = true;
                Settings.Instance.WaitUntilExistsTimeOut = 6;
                Settings.Instance.WaitForCompleteTimeOut = 6;

                _helper = new IEStaticInstanceHelper { IE = new IE( Constants.BaseUrl ) };

                _browser = _helper.IE;
            }
        }

        /// <summary>
        /// Helper method to go to the Project Details page for Project number 1
        /// </summary>
        public void GoToProjectDetailsPage()
        {
            _browser.GoTo( _browser.Page<AllPage>().Url );
            _browser.Page<AllPage>().ProjectNameSpan.Click();
        }

        /// <summary>
        /// Helper method to go to the User Story Details page for Project number 1 and
        /// User Story number 1
        /// </summary>
        public void GoToUserStoryDetailsPage()
        {
            GoToProjectDetailsPage();
            _browser.Page<ProjectDetailsPage>().UserStoryName.Click();
        }

        #endregion Setup and Helper Methods

        // ReSharper disable InconsistentNaming

        #region All Page

        [Test]
        public void All_Page_Should_Redirect_To_The_ProjectDetails_Page_When_A_Project_Name_Is_Clicked()

        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Act
            _browser.Page<AllPage>().ProjectNameSpan.Click();
            
            // Assert
            Assert.That( _browser.Url.Contains( _browser.Page<ProjectDetailsPage>().Url ) );
        }

        [Test]
        public void All_Page_Should_Hide_All_Textboxes_When_The_Page_Is_Initially_Loaded()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );
            
            // Assert
            Assert.False( _browser.Page<AllPage>().ProjectNameTextBox.IsVisible() );
        }

        [Test]
        public void All_Page_Should_Switch_To_Edit_Mode_And_Unhide_All_TextBoxes_When_An_Edit_Link_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );
            
            // Act
            _browser.Page<AllPage>().EditLink.Click();
            
            // Assert
            Assert.True( _browser.Page<AllPage>().ProjectNameTextBox.IsVisible() );
        }

        [Test]
        public void All_Page_Should_Switch_Back_To_Reader_Mode_When_The_Cancel_Link_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Act
            _browser.Page<AllPage>().EditLink.Click();

            _browser.Page<AllPage>().CancelLink.Click();

            // Assert
            Assert.False( _browser.Page<AllPage>().ProjectNameTextBox.IsVisible() );
        }

        [Test]
        public void All_Page_Should_Update_The_Project_Name_When_The_Project_Name_Is_Edited_And_The_Save_Link_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Act
            _browser.Page<AllPage>().EditLink.Click();

            _browser.Page<AllPage>().ProjectNameTextBox.Type( "Instructor Profile Website" );

            _browser.Page<AllPage>().SaveLink.Click();
            
            // Assert
            Assert.That( _browser.Page<AllPage>().ProjectNameSpan.InnerHtml.Contains( "Instructor Profile Website" ) );
        }

        [Test]
        [Explicit]
        public void All_Page_Should_Delete_The_Project_When_The_Delete_Link_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Act
            _browser.Page<AllPage>().EditLink.Click();

            _browser.Page<AllPage>().DeleteLink.Click();

            // Assert
            Assert.False( _browser.Page<AllPage>().ProjectNameSpan.Exists );
        }

        [Test]
        public void All_Page_Should_Hide_The_New_Project_Form_When_The_Page_Is_Initially_Loaded()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Assert
            Assert.False( _browser.Page<AllPage>().CreateNewProjectForm.IsVisible() );
        }

        [Test]
        public void All_Page_Should_Make_The_New_Project_Form_Visible_When_The_Add_New_Project_Table_Row_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Act
            _browser.Page<AllPage>().AddNewProjectTableRow.Click();

            // Assert
            Assert.That( _browser.Page<AllPage>().CreateNewProjectForm.IsVisible() );
        }

        [Test]
        public void All_Page_Should_Make_Create_A_New_Project_When_A_Name_Is_Entered_Into_The_Project_Name_Textbox_And_The_CreateNewProject_Button_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AllPage>().Url );

            // Act
            _browser.Page<AllPage>().AddNewProjectTableRow.Click();

            _browser.Page<AllPage>().NewProjectNameTextBox.Type( "Some Random Project Name" );

            _browser.Page<AllPage>().CreateNewProjectButton.Click();

            // Assert
            Assert.That( _browser.Page<AllPage>().Document.Span( Find.ByText( "Some Random Project Name" ) ).Exists );
        }

        #endregion All Page

        #region Add New User Page

        [Test]
        public void AddNewUser_Page_Should_Create_A_New_User_When_All_Text_Fields_Are_Populated_And_The_AddNewUser_Button_Is_Clicked()
        {
            // Arrange
            _browser.GoTo( _browser.Page<AddNewUserPage>().Url );

            // Act
            _browser.Page<AddNewUserPage>().FirstName.Type( "Ronald" );

            _browser.Page<AddNewUserPage>().LastName.Type( "McDonald" );

            _browser.Page<AddNewUserPage>().AddNewUserButton.Click();

            GoToUserStoryDetailsPage();
            
            // Assert
            Assert.That( _browser.Page<UserStoryDetailsPage>().AssignedToSelectList.AllContents.Contains( "Ronald McDonald" ) );
        }

        #endregion Add New User Page

        #region Project Details Page
        
        [Test]
        public void ProjectDetails_Page_Should_Redirect_To_The_UserStoryDetails_Page_When_A_UserStory_Name_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().UserStoryName.Click();

            // Assert
            Assert.That( _browser.Url.Contains( _browser.Page<UserStoryDetailsPage>().Url ) );
        }

        [Test]
        public void ProjectDetails_Page_Should_Hide_All_Textboxes_When_The_Page_Is_Initially_Loaded()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Assert
            Assert.False( _browser.Page<ProjectDetailsPage>().UserStoryTextBox.IsVisible() );
        }

        [Test]
        public void ProjectDetails_Page_Should_Switch_To_Edit_Mode_And_Unhide_All_TextBoxes_When_An_Edit_Link_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().EditLink.Click();

            // Assert
            Assert.True( _browser.Page<ProjectDetailsPage>().UserStoryTextBox.IsVisible() );
        }

        [Test]
        public void ProjectDetails_Page_Should_Switch_Back_To_Reader_Mode_When_The_Cancel_Link_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().EditLink.Click();

            _browser.Page<ProjectDetailsPage>().CancelLink.Click();

            // Assert
            Assert.False( _browser.Page<ProjectDetailsPage>().UserStoryTextBox.IsVisible() );
        }

        [Test]
        public void ProjectDetails_Page_Should_Update_The_User_Story_Name_When_The_User_Story_Name_Is_Edited_And_The_Save_Link_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().EditLink.Click();

            _browser.Page<ProjectDetailsPage>().UserStoryTextBox.Type( "Build the User Interface" );

            _browser.Page<ProjectDetailsPage>().SaveLink.Click();

            // Assert
            Assert.That( _browser.Page<ProjectDetailsPage>().UserStoryName.InnerHtml.Contains( "Build the User Interface" ) );
        }

        [Test]
        [Explicit]
        public void ProjectDetails_Page_Should_Delete_The_User_Story_When_The_Delete_Link_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().EditLink.Click();

            _browser.Page<ProjectDetailsPage>().DeleteLink.Click();

            // Assert
            Assert.False( _browser.Page<ProjectDetailsPage>().UserStoryName.Exists );
        }

        [Test]
        public void ProjectDetails_Page_Should_Hide_The_New_User_Story_Form_When_The_Page_Is_Initially_Loaded()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Assert
            Assert.False( _browser.Page<ProjectDetailsPage>().CreateNewUserStoryForm.IsVisible() );
        }

        [Test]
        public void ProjectDetails_Page_Should_Make_The_New_User_Story_Form_Visible_When_The_Add_New_User_Story_Table_Row_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().AddNewUserStoryTableRow.Click();

            // Assert
            Assert.That( _browser.Page<ProjectDetailsPage>().CreateNewUserStoryForm.IsVisible() );
        }

        [Test]
        public void ProjectDetails_Page_Should_Create_A_New_User_Story_When_A_Name_Is_Entered_Into_The_User_Story_Name_Textbox_And_The_CreateNewUserStory_Button_Is_Clicked()
        {
            // Arrange
            GoToProjectDetailsPage();

            // Act
            _browser.Page<ProjectDetailsPage>().AddNewUserStoryTableRow.Click();

            _browser.Page<ProjectDetailsPage>().NewUserStoryNameTextBox.Type( "Some Random User Story Name" );

            _browser.Page<ProjectDetailsPage>().CreateNewUserStoryButton.Click();

            // Assert
            Assert.That( _browser.Page<ProjectDetailsPage>().Document.Span( Find.ByText( "Some Random User Story Name" ) ).Exists );
        }

        #endregion Project Details Page

        #region User Story Details Page
        
        [Test]
        public void UserStoryDetails_Page_Should_Hide_All_Textboxes_When_The_Page_Is_Initially_Loaded()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Assert
            Assert.False( _browser.Page<UserStoryDetailsPage>().TaskNameTextBox.IsVisible() );
        }

        [Test]
        public void UserStoryDetails_Page_Should_Switch_To_Edit_Mode_And_Unhide_All_TextBoxes_When_An_Edit_Link_Is_Clicked()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Act
            _browser.Page<UserStoryDetailsPage>().EditLink.Click();

            // Assert
            Assert.True( _browser.Page<UserStoryDetailsPage>().TaskNameTextBox.IsVisible() );
        }

        [Test]
        public void UserStoryDetails_Page_Should_Switch_Back_To_Reader_Mode_When_The_Cancel_Link_Is_Clicked()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Act
            _browser.Page<UserStoryDetailsPage>().EditLink.Click();

            _browser.Page<UserStoryDetailsPage>().CancelLink.Click();

            // Assert
            Assert.False( _browser.Page<UserStoryDetailsPage>().TaskNameTextBox.IsVisible() );
        }

        [Test]
        public void UserStoryDetails_Page_Should_Update_The_Task_Name_When_The_Task_Name_Is_Edited_And_The_Save_Link_Is_Clicked()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Act
            _browser.Page<UserStoryDetailsPage>().EditLink.Click();

            _browser.Page<UserStoryDetailsPage>().TaskNameTextBox.Type( "Build the Administrative Web Pages" );

            _browser.Page<UserStoryDetailsPage>().SaveLink.Click();

            // Assert
            Assert.That( _browser.Page<UserStoryDetailsPage>().TaskName.InnerHtml.Contains( "Build the Administrative Web Pages" ) );
        }

        [Test]
        [Explicit]
        public void UserStoryDetails_Page_Should_Delete_The_Task_When_The_Delete_Link_Is_Clicked()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Act
            _browser.Page<UserStoryDetailsPage>().EditLink.Click();

            _browser.Page<UserStoryDetailsPage>().DeleteLink.Click();

            // Assert
            Assert.False( _browser.Page<UserStoryDetailsPage>().TaskName.Exists );
        }

        [Test]
        public void UserStoryDetails_Page_Should_Hide_The_New_Task_Form_When_The_Page_Is_Initially_Loaded()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Assert
            Assert.False( _browser.Page<UserStoryDetailsPage>().CreateNewTaskForm.IsVisible() );
        }

        [Test]
        public void UserStoryDetails_Page_Should_Make_The_New_Task_Form_Visible_When_The_Add_New_Task_Table_Row_Is_Clicked()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Act
            _browser.Page<UserStoryDetailsPage>().AddNewTaskTableRow.Click();

            // Assert
            Assert.That( _browser.Page<UserStoryDetailsPage>().CreateNewTaskForm.IsVisible() );
        }

        [Test]
        public void UserStoryDetails_Page_Should_Create_A_New_Task_When_A_Name_Is_Entered_Into_The_Task_Name_Textbox_And_The_CreateNewTask_Button_Is_Clicked()
        {
            // Arrange
            GoToUserStoryDetailsPage();

            // Act
            _browser.Page<UserStoryDetailsPage>().AddNewTaskTableRow.Click();

            _browser.Page<UserStoryDetailsPage>().NewTaskNameTextBox.Type( "Some Random Task Name" );

            _browser.Page<UserStoryDetailsPage>().CreateNewTaskButton.Click();

            // Assert
            Assert.That( _browser.Page<UserStoryDetailsPage>().Document.Span( Find.ByText( "Some Random Task Name" ) ).Exists );
        }

        #endregion User Story Details Page

        // ReSharper restore InconsistentNaming
    }
}
