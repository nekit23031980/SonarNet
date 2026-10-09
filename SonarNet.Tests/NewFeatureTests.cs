using SonarNet;

namespace SonarNet.Tests;

public class NewFeatureTests
{
    [Fact]
    public void DistanceFromEcho_DefaultSpeed_IsCorrect() =>
        Assert.Equal(150.0, NewFeature.DistanceFromEcho(0.2), 6);

    [Fact]
    public void DistanceFromEcho_CustomSpeed_IsCorrect() =>
        Assert.Equal(170.0, NewFeature.DistanceFromEcho(1.0, 340.0), 6);

    [Fact]
    public void DistanceFromEcho_NegativeTime_Throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => NewFeature.DistanceFromEcho(-1));
}
