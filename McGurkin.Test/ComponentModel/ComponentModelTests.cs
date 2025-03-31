using McGurkin.ComponentModel;
using System.Reflection;

namespace McGurkin.Test.ComponentModel;

[TestClass]
public class ComponentModelTests
{
    [TestMethod]
    public void Item_ShouldInitializeWithNewGuid()
    {
        // Arrange & Act
        var item = new Item();

        // Assert
        Assert.AreNotEqual(Guid.Empty, item.Id);
    }

    [TestMethod]
    public void Item_ShouldInitializeWithGivenGuid()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var item = new Item(guid);

        // Assert
        Assert.AreEqual(guid, item.Id);
    }

    [TestMethod]
    public void Id_Setter_ShouldRaisePropertyChanged()
    {
        // Arrange
        var item = new Item();
        var newGuid = Guid.NewGuid();
        var propertyChangedRaised = false;

        item.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(Item.Id))
            {
                propertyChangedRaised = true;
            }
        };

        // Act
        item.Id = newGuid;

        // Assert
        Assert.IsTrue(propertyChangedRaised);
        Assert.AreEqual(newGuid, item.Id);
    }

    [TestMethod]
    public void BeginEdit_ShouldCreateBackup()
    {
        // Arrange
        var item = new Item();

        // Act
        item.BeginEdit();

        // Assert
        Assert.IsNotNull(item.Backup);
    }

    [TestMethod]
    public void EndEdit_ShouldClearBackup()
    {
        // Arrange
        var item = new Item();
        item.BeginEdit();

        // Act
        item.EndEdit();

        // Assert
        Assert.IsNull(item.Backup);
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void CancelEdit_ShouldThrowNotImplementedException()
    {
        // Arrange
        var item = new Item();

        // Act
        item.CancelEdit();
    }


    private class TestClass : NotifyPropertyChanged
    {
        private string _testProperty;
        public string TestProperty
        {
            get => _testProperty;
            set
            {
                _testProperty = value;
                RaisePropertyChanged(nameof(TestProperty));
            }
        }
    }

    [TestMethod]
    public void RaisePropertyChanged_ShouldRaiseEventForAllProperties()
    {
        // Arrange
        var testClass = new TestClass();
        var properties = typeof(TestClass).GetTypeInfo().DeclaredProperties.Select(p => p.Name).ToList();
        var raisedProperties = new List<string>();

        testClass.PropertyChanged += (sender, e) =>
        {
            raisedProperties.Add(e.PropertyName);
        };

        // Act
        testClass.RaisePropertyChanged();

        // Assert
        CollectionAssert.AreEquivalent(properties, raisedProperties);
    }

    [TestMethod]
    public void RaisePropertyChanged_ShouldRaiseEventForSpecificProperty()
    {
        // Arrange
        var testClass = new TestClass();
        var propertyName = nameof(TestClass.TestProperty);
        var eventRaised = false;

        testClass.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == propertyName)
            {
                eventRaised = true;
            }
        };

        // Act
        testClass.TestProperty = "New Value";

        // Assert
        Assert.IsTrue(eventRaised);
    }

}
