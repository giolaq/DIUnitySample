using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Zenject;

public class PoolGameController : MonoBehaviour, IPoolGameController {
	public GameObject cue;
	public GameObject cueBall;
	public GameObject redBalls;
	public GameObject mainCamera;
	public GameObject scoreBar;
	public GameObject winnerMessage;

	public float maxForce;
	public float minForce;
	public Vector3 strikeDirection;

	public const float MIN_DISTANCE = 27.5f;
	public const float MAX_DISTANCE = 32f;
	
    [Inject]
	public IGameObjectState currentState;

	public Player CurrentPlayer;

    public int TimeToRestart = 5;

	void Start() {
		strikeDirection = Vector3.forward;
		CurrentPlayer = new Player("Giovanni");

		winnerMessage.GetComponent<Canvas>().enabled = false;

	}
	
	void Update() {
		currentState.Update();
	}

	void FixedUpdate() {
		currentState.FixedUpdate();
	}

	void LateUpdate() {
		currentState.LateUpdate();
	}

	public void BallPocketed(int ballNumber) {
		CurrentPlayer.Collect(ballNumber);
	}

	
	public void EndMatch() {
		
		var msg = "Game Over\n";

        msg += String.Format("You realized {0} Points", CurrentPlayer.Points - CurrentPlayer.cueInPocketTimes);
        msg += String.Format("Game will restart in {0} seconds", TimeToRestart);

        var text = winnerMessage.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = msg;
		winnerMessage.GetComponent<Canvas>().enabled = true;

        Invoke("RestartScene", TimeToRestart);
	}

    private void RestartScene() {
		Scene loadedLevel = SceneManager.GetActiveScene();
		SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    public GameObject GetCue()
    {
        return cue;
    }

    public GameObject GetCueBall()
    {
        return cueBall;
    }

    public GameObject GetCamera()
    {
        return mainCamera;
    }

    public GameObject GetBalls()
    {
        return redBalls;
    }

    public float GetMaxForce()
    {
        return maxForce;
    }

    public float GetMinForce()
    {
        return minForce;
    }

    public IGameObjectState GetCurrentState()
    {
        return currentState;
    }

    public Vector3 GetStrikeDirection()
    {
        return strikeDirection;
    }

    public void SetCurrentState(IGameObjectState state)
    {
        currentState = state;
    }

    public void SetStrikeDirection(Vector3 direction)
    {
        strikeDirection = direction;
    }

    public Player GetCurrentPlayer()
    {
        return CurrentPlayer;
    }

}
