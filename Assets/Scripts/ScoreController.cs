using UnityEngine;
using System;
using System.Collections;
using Zenject;

public class ScoreController : MonoBehaviour {

    private IPoolGameController gameController;

    [Inject]
    public void Construct(IPoolGameController gameController) {
        this.gameController = gameController;
    }

	// Update is called once per frame
	void Update () {
		var text = GetComponent<UnityEngine.UI.Text>();
        var currentPlayer = gameController.GetCurrentPlayer();
        text.text = String.Format("{0} : {1}", currentPlayer.Name, currentPlayer.Points - currentPlayer.cueInPocketTimes);
	}
}
