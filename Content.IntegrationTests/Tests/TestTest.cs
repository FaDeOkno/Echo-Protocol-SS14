#nullable enable
using Content.IntegrationTests.Fixtures;
using Content.Shared.Medical;

namespace Content.IntegrationTests.Tests.Medical;

[TestOf(typeof(DefibrillatorComponent))]
public sealed class TestTest : GameTest
{
    [Test]
    public async Task TestIntegrationTests()
    {
        Assert.That(false, "Test results catched successfully");
        Assert.Fail("Test results was not catched, but fail was");
    }
}
