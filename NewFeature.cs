namespace SonarNet;

/// <summary>Нова фіча: обчислення відстані за часом проходження ехо-сигналу.</summary>
public static class NewFeature
{
    /// <summary>Швидкість звуку у воді, м/с.</summary>
    public const double SpeedOfSoundInWater = 1500.0;

    public static double DistanceFromEcho(double roundTripSeconds, double speed = SpeedOfSoundInWater)
    {
        if (roundTripSeconds < 0) throw new ArgumentOutOfRangeException(nameof(roundTripSeconds));
        if (speed <= 0) throw new ArgumentOutOfRangeException(nameof(speed));
        return speed * roundTripSeconds / 2.0;
    }
}
