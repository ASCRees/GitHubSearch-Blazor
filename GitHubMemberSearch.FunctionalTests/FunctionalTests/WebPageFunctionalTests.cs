using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace GitMemberSearch.FunctionalTests
{
    [ExcludeFromCodeCoverage]
    public class WebPageFunctionalTests:BaseFunctionalTests
    {

        public WebPageFunctionalTests(string webDriverName)
        {
            this.webDriverName = webDriverName;
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Field_Error_Is_Shown()
        {
            //Arrange
            SearchField.SendKeys(".");

            SearchField.SendKeys(Keys.Tab);

            //Act
            SearchButton.Click();

            //Assert
            Assert.IsNotNull(ErrorLabel);
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Field_Error_Is_NotShown()
        {
            //Arrange
            SearchField.SendKeys("ascrees");
            SearchField.SendKeys(Keys.Tab);

            //Act
            SearchButton.Click();

            //Assert
            Assert.IsTrue(ErrorLabel == null);
        }

        [Test]
        public void WebPage_EmptyUserName_Check_Search_Error_Is_Shown()
        {
            //Arrange
            SearchField.SendKeys("...");
            SearchField.SendKeys(Keys.Tab);

            //Act
            SearchButton.Click();

            //Assert
            Assert.IsTrue(ErrorLabel.Text.Contains("No records found for user"));
        }


        [Test]
        public void WebPage_EmptyUserName_Check_Field_Name_Is_Shown_for_Valid_User()
        {
            //Arrange
            SearchField.SendKeys("ascrees");
            SearchField.SendKeys(Keys.Tab);

            //Act
            SearchButton.Click();

            //Assert
            Assert.IsTrue(NameField.Text.Contains("Name"));
        }

        [Test]
        public void WebPage_EmptyUserName_Check_No_Repository_Items_Valid_User()
        {
            //Arrange
            SearchField.SendKeys("speacock1970");
            SearchField.SendKeys(Keys.Tab);

            //Act
            SearchButton.Click();

            //Assert
            Assert.IsTrue(NoRepositoryItems.Text.Contains("The user does not have any repository items"));
        }

        [Test]
        public void WebPage_EmptyUserName_Check_No_Repository_Items_Not_Shown_For_Valid_User()
        {
            //Arrange
            SearchField.SendKeys("ascrees");
            SearchField.SendKeys(Keys.Tab);

            //Act
            SearchButton.Click();

            //Assert
            Assert.IsTrue(NoRepositoryItems==null);
        }


        [Test]
        public void WebPage_Display_Error_For_Long_UserName()
        {
            //Arrange
            SearchField.SendKeys("abcdefghjkabcdefghjkabcdefghjkabcdeslajfsldkjfsldkjfsdasdfghjk");
            SearchField.SendKeys(Keys.Tab);
            //Act


            //Assert
            Assert.IsTrue(BadUserNameInputMessage.Text.Contains("Search too long"));
        }

    }
}
