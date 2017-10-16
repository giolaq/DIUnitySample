using UnityEngine;

public interface IPoolGameController
{
    GameObject GetCue();
    GameObject GetCueBall();
    GameObject GetCamera();
    GameObject GetBalls();

    float GetMaxForce();
    float GetMinForce();

    IGameObjectState GetCurrentState();
    void SetCurrentState(IGameObjectState state);

    Vector3 GetStrikeDirection();
    void SetStrikeDirection(Vector3 direction);

    Player GetCurrentPlayer();

    void BallPocketed(int ballNumber);
    void EndMatch();
}