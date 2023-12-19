using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateExtensions
{
    public static bool CanMove(List<Status> statuses)
    {
        if (statuses == null) return true;
        return !statuses.Contains(Status.Freeze) &&
               !statuses.Contains(Status.Stun) &&
               !statuses.Contains(Status.Sleep);
    }

    public static bool CanAttack(List<Status> statuses)
    {
        if (statuses == null) return true;
        return !statuses.Contains(Status.Freeze) &&
               !statuses.Contains(Status.Stun) &&
               !statuses.Contains(Status.Sleep);
    }

    public static bool CanCast( List<Status> statuses)
    {
        if (statuses == null) return true;
        return !statuses.Contains(Status.Silence);
    }
}