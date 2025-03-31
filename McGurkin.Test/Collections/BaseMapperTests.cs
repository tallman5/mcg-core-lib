using McGurkin.Collections.Generic;

namespace McGurkin.Test.Collections.Generic;

[TestClass]
public class BaseMapperTests
{
    private BaseMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        var mappings = new Dictionary<string, string>
        {
            { "Left1", "Right1" },
            { "Left2", "Right2" }
        };
        _mapper = new BaseMapper(mappings);
    }

    [TestMethod]
    public void GetLeftName_ShouldReturnCorrectLeftName()
    {
        // Act
        var leftName = _mapper.GetLeftName("Right1");

        // Assert
        Assert.AreEqual("Left1", leftName);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void GetLeftName_ShouldThrowExceptionForUnknownRightName()
    {
        // Act
        _mapper.GetLeftName("UnknownRight");
    }

    [TestMethod]
    public void GetRightName_ShouldReturnCorrectRightName()
    {
        // Act
        var rightName = _mapper.GetRightName("Left1");

        // Assert
        Assert.AreEqual("Right1", rightName);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void GetRightName_ShouldThrowExceptionForUnknownLeftName()
    {
        // Act
        _mapper.GetRightName("UnknownLeft");
    }
}
