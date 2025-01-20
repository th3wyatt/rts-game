using System;

public class GameEvents
{
    public static event Action OnUnitDied;
    public static event Action OnResourceExausted;
    public static event Action<StatResource> OnStatCardPressed;

    public static void RaiseUnitDied() => OnUnitDied?.Invoke();

    public static void RaiseResourceExausted() => OnResourceExausted?.Invoke();

    public static void RaiseStatCardPressed(StatResource stat) 
        => OnStatCardPressed?.Invoke(stat);

}