public interface ITarget
{
    event System.Action<INeedToMoveUntilIDie> OnKilled;

    UnityEngine.Transform target { get; }

    void StartBeingHit();
    void StopBeingHit();
}