using McGurkin.Cli;

namespace McGurkin.Test.Cli
{
    [TestClass]
    public class UiTests
    {
        [TestMethod]
        public void DrawProgressBar_ShouldDisplayCorrectProgress()
        {
            // Arrange
            var progress = 50;
            var total = 100;
            var startTime = DateTimeOffset.Now;

            // Act
            Ui.DrawProgressBar(progress, total, startTime);

            // Assert
            // Verify the output manually or use a console output capturing library
        }

        [TestMethod]
        public void DrawSelectOptions_ShouldReturnSelectedIndex()
        {
            // Arrange
            var options = new List<string> { "Option1", "Option2", "Option3" };

            // Act
            var selectedIndex = Ui.DrawSelectOptions(options);

            // Assert
            // Verify the selected index manually or use a console input simulation library
        }
    }
}

