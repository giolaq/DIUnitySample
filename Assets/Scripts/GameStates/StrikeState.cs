using UnityEngine;
using System;
using Zenject;

namespace GameStates {
    public class StrikeState : IGameObjectState {
		
		private GameObject cue;
		private GameObject cueBall;

		private float speed = 30f;
		private float force = 0f;

        private IPoolGameController gameController;
		
        public StrikeState(IPoolGameController gameController) {

            this.gameController = gameController;

            cue = gameController.GetCue();
            cueBall = gameController.GetCueBall();

            var forceAmplitude = gameController.GetMaxForce() - gameController.GetMinForce();
			var relativeDistance = (Vector3.Distance(cue.transform.position, cueBall.transform.position) - PoolGameController.MIN_DISTANCE) / (PoolGameController.MAX_DISTANCE - PoolGameController.MIN_DISTANCE);
            force = forceAmplitude * relativeDistance + gameController.GetMinForce();
		}

		public void FixedUpdate () {
			var distance = Vector3.Distance(cue.transform.position, cueBall.transform.position);
			if (distance < PoolGameController.MIN_DISTANCE) {
                cueBall.GetComponent<Rigidbody>().AddForce(gameController.GetStrikeDirection() * force);
                cue.SetActive(false);
                cue.transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);
                gameController.SetCurrentState(new GameStates.WaitingForNextTurnState(gameController));
			} else {
                cue.transform.Translate(Vector3.back * speed * -1 * Time.fixedDeltaTime);
			}
		}

        public void Update()
        {
        }

        public void LateUpdate()
        {
        }
    }
}