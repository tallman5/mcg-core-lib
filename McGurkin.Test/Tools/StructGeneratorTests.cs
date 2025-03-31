using McGurkin.Tools.StructGenerator;

namespace McGurkin.Test.Tools.StructGenerator;

[TestClass]
public class StructGeneratorTests
{
    [TestMethod]
    public void Generate_ShouldReturnGeneratedStruct()
    {
        // Arrange
        var settings = new StructGeneratorSettings
        {
            Namespace = "McGurkin.ServiceProviders",
            StructSingular = "ResponseType",
            StructPlural = "ResponseTypes",
            StructValues = new Dictionary<string, string>
            {
                { "Successfully completed", "Success" },
                { "Completed with warnings", "Warning" },
                { "Errors occurred", "Error" }
            }
        };

        // Act
        var result = McGurkin.Tools.StructGenerator.StructGenerator.Generate(settings);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Contains("namespace McGurkin.ServiceProviders"));
        Assert.IsTrue(result.Contains("internal enum ResponseTypeKnown"));
        Assert.IsTrue(result.Contains("public struct ResponseType"));
    }
}

