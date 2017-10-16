using UnityEngine;
using System.Collections;
using Zenject;

namespace GameStates {
    public class WaitingForNextTurnState : IGameObjectState {
		private GameObject cue;
		private GameObject cueBall;
		private GameObject redBalls;
		private GameObject mainCamera;

		private Vector3 cameraOffset;
		private Vector3 cueOffset;
		private Quaternion cameraRotation;
		private Quaternion cueRotation;

        private IPoolGameController gameController;

        public WaitingForNextTurnState(IPoolGameController gameController) {

            this.gameController = gameController;
            cue = gameController.GetCue();
            cueBall = gameController.GetCueBall();
            redBalls = gameController.GetBalls();
            mainCamera = gameController.GetCamera();
			
			cameraOffset = cueBall.transform.position - mainCamera.transform.position;
			cameraRotation = mainCamera.transform.rotation;
			cueOffset = cueBall.transform.position - cue.transform.position;
			cueRotation = cue.transform.rotation;
		}

		public void FixedUpdate() {
			Debug.Log(redBalls.GetComponentsInChildren<Transform>().Length);
			if (redBalls.GetComponentsInChildren<Transform>().Length == 1) {
                gameController.EndMatch();
			} else {
				var cueBallBody = cueBall.GetComponent<Rigidbody>();
				if (!(cueBallBody.IsSleeping() || cueBallBody.velocity == Vector3.zero))
					return;
				
				foreach (var rigidbody in redBalls.GetComponentsInChildren<Rigidbody>()) {
					if (!(rigidbody.IsSleeping() || rigidbody.velocity == Vector3.zero))
						return;
				}

                // If all balls are sleeping, time for the next turn
                // This is kinda hacky but gets the job done
                gameController.SetCurrentState(new WaitingForStrikeState(gameController));
			}
		}

		public void LateUpdate() {
			mainCamera.transform.position = cueBall.transform.position - cameraOffset;
			mainCamera.transform.rotation = cameraRotation;
			
			cue.transform.position = cueBall.transform.position - cueOffset;
			cue.transform.rotation = cueRotation;
		}

        public void Update()
        {
        }
    }
}