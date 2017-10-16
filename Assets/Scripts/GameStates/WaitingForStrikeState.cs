using UnityEngine;
using System.Collections;
using Zenject;

namespace GameStates {
    public class WaitingForStrikeState  : IGameObjectState {
		private GameObject cue;
		private GameObject cueBall;
		private GameObject mainCamera;

        private IPoolGameController gameController;

        public WaitingForStrikeState(IPoolGameController gameController) {

            this.gameController = gameController;
            cue = gameController.GetCue();
            cueBall = gameController.GetCueBall();
            mainCamera = gameController.GetCamera();

            cue.SetActive(true);
		}

		public void Update() {
			var x = Input.GetAxis("Horizontal");

            if (!Mathf.Approximately(x, 0f)) {
				var angle = x * 75 * Time.deltaTime;
                gameController.SetStrikeDirection(Quaternion.AngleAxis(angle, Vector3.up) * gameController.GetStrikeDirection());
				mainCamera.transform.RotateAround(cueBall.transform.position, Vector3.up, angle);
				cue.transform.RotateAround(cueBall.transform.position, Vector3.up, angle);
			}
            Debug.DrawLine(cueBall.transform.position, cueBall.transform.position + gameController.GetStrikeDirection() * 10);

			if (Input.GetButtonDown("Fire1")) {
                gameController.SetCurrentState(new GameStates.StrikingState(gameController));
            }
		}

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }
    }
}