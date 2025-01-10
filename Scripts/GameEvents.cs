using System;

public class GameEvents
{
    public static event Action OnUnitDied;

    public static void RaiseUnitDied() => OnUnitDied?.Invoke();
}