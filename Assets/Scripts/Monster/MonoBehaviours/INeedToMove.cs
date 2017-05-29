using UnityEngine;

public interface INeedToMoveUntilIDie
{
    event System.Action<INeedToMoveUntilIDie> OnKilled;

    Transform target { get; }
}