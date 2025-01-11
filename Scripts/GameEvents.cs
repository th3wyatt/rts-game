using System;

public class GameEvents
{
    public static event Action OnUnitDied;
    public static event Action OnResourceExausted;

    public static void RaiseUnitDied() => OnUnitDied?.Invoke();

    internal static void RaiseResourceExausted() => OnResourceExausted?.Invoke();
}