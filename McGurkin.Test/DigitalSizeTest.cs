namespace McGurkin.Test;

[TestClass]
public class DigitalSizeTests
{
    [TestMethod]
    public void FromBits_ShouldReturnCorrectDigitalSize()
    {
        var size = DigitalSize.FromBits(1024);
        Assert.AreEqual(1024, size.Bits);
    }

    [TestMethod]
    public void FromBytes_ShouldReturnCorrectDigitalSize()
    {
        var size = DigitalSize.FromBytes(128);
        Assert.AreEqual(1024, size.Bits);
    }

    [TestMethod]
    public void FromKilobytes_ShouldReturnCorrectDigitalSize()
    {
        var size = DigitalSize.FromKilobytes(1);
        Assert.AreEqual(8192, size.Bits);
    }

    [TestMethod]
    public void FromMegabytes_ShouldReturnCorrectDigitalSize()
    {
        var size = DigitalSize.FromMegabytes(1);
        Assert.AreEqual(8388608, size.Bits);
    }

    [TestMethod]
    public void FromGigabytes_ShouldReturnCorrectDigitalSize()
    {
        var size = DigitalSize.FromGigabytes(1);
        Assert.AreEqual(8589934592, size.Bits);
    }

    [TestMethod]
    public void Bits_ShouldReturnCorrectValue()
    {
        var size = DigitalSize.FromBits(1024);
        Assert.AreEqual(1024, size.Bits);
    }

    [TestMethod]
    public void Bytes_ShouldReturnCorrectValue()
    {
        var size = DigitalSize.FromBits(1024);
        Assert.AreEqual(128, size.Bytes);
    }

    [TestMethod]
    public void Kilobytes_ShouldReturnCorrectValue()
    {
        var size = DigitalSize.FromBits(8192);
        Assert.AreEqual(1, size.Kilobytes);
    }

    [TestMethod]
    public void Megabytes_ShouldReturnCorrectValue()
    {
        var size = DigitalSize.FromBits(8388608);
        Assert.AreEqual(1, size.Megabytes);
    }

    [TestMethod]
    public void Gigabytes_ShouldReturnCorrectValue()
    {
        var size = DigitalSize.FromBits(8589934592);
        Assert.AreEqual(1, size.Gigabytes);
    }

    [TestMethod]
    public void AdditionOperator_ShouldReturnCorrectDigitalSize()
    {
        var size1 = DigitalSize.FromBits(1024);
        var size2 = DigitalSize.FromBits(2048);
        var result = size1 + size2;
        Assert.AreEqual(3072, result.Bits);
    }

    [TestMethod]
    public void SubtractionOperator_ShouldReturnCorrectDigitalSize()
    {
        var size1 = DigitalSize.FromBits(2048);
        var size2 = DigitalSize.FromBits(1024);
        var result = size1 - size2;
        Assert.AreEqual(1024, result.Bits);
    }

    [TestMethod]
    public void ToString_ShouldReturnCorrectString()
    {
        var size = DigitalSize.FromBits(8589934592);
        Assert.AreEqual("1 GB", size.ToString());
    }

    [TestMethod]
    public void Equals_ShouldReturnTrueForEqualSizes()
    {
        var size1 = DigitalSize.FromBits(1024);
        var size2 = DigitalSize.FromBits(1024);
        Assert.IsTrue(size1.Equals(size2));
    }

    [TestMethod]
    public void Equals_ShouldReturnFalseForDifferentSizes()
    {
        var size1 = DigitalSize.FromBits(1024);
        var size2 = DigitalSize.FromBits(2048);
        Assert.IsFalse(size1.Equals(size2));
    }

    [TestMethod]
    public void EqualityOperator_ShouldReturnTrueForEqualSizes()
    {
        var size1 = DigitalSize.FromBits(1024);
        var size2 = DigitalSize.FromBits(1024);
        Assert.IsTrue(size1 == size2);
    }

    [TestMethod]
    public void InequalityOperator_ShouldReturnTrueForDifferentSizes()
    {
        var size1 = DigitalSize.FromBits(1024);
        var size2 = DigitalSize.FromBits(2048);
        Assert.IsTrue(size1 != size2);
    }

    [TestMethod]
    public void GetHashCode_ShouldReturnSameHashCodeForEqualSizes()
    {
        var size1 = DigitalSize.FromBits(1024);
        var size2 = DigitalSize.FromBits(1024);
        Assert.AreEqual(size1.GetHashCode(), size2.GetHashCode());
    }
}
