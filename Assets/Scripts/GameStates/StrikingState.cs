using UnityEngine;
using System.Collections;
using Zenject;

namespace GameStates {
    public class StrikingState : IGameObjectState {

		private GameObject cue;
		private GameObject cueBall;

		private float cueDirection = -1;
		private float speed = 7;

        private IPoolGameController gameController;

        public StrikingState(IPoolGameController gameController) {
            this.gameController = gameController;

            cue = gameController.GetCue();
            cueBall = gameController.GetCueBall();
		}

		public void Update() {
			if (Input.GetButtonUp("Fire1")) {
                gameController.SetCurrentState(new GameStates.StrikeState(gameController));
			}
		}

		public void FixedUpdate () {
			var distance = Vector3.Distance(cue.transform.position, cueBall.transform.position);
			if (distance < PoolGameController.MIN_DISTANCE || distance > PoolGameController.MAX_DISTANCE)
				cueDirection *= -1;
            cue.transform.Translate(Vector3.forward * speed * cueDirection * Time.fixedDeltaTime);
		}

        public void LateUpdate()
        {
        }
    }
}