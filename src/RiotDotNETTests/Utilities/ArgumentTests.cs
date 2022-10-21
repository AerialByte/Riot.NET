namespace RiotDotNETTests.Utilities;
using RiotDotNET.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ArgumentTests
{
    [TestMethod]
    public void NotNullTest()
    {
        string? arg1 = "Test Arg 1", arg2 = null;
        Argument.NotNull(arg1, nameof(arg1));

        bool wasThrown = false;
        try
        {
            Argument.NotNull(arg2, nameof(arg2), "Argument cannot be null.");
        }
        catch (ArgumentNullException ex)
        {
            wasThrown = true;
            Assert.AreEqual(nameof(arg2), ex.ParamName);
            Assert.AreEqual(new ArgumentNullException(nameof(arg2), "Argument cannot be null.").Message, ex.Message);
        }

        Assert.IsTrue(wasThrown);
    }
}