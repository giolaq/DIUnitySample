using UnityEngine;
using System.Collections;

public class SnookerBallController : MonoBehaviour {
	void Start() {
		GetComponent<Rigidbody>().sleepThreshold = 0.15f;
	}

	void FixedUpdate () {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        if (rigidBody.velocity.y > 0) {
            var velocity = rigidBody.velocity;
			velocity.y *= 0.3f;
            rigidBody.velocity = velocity;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Balls") ) {
            GetComponent<AudioSource>().Play();
            Debug.Log("Collision");
        }
    }
}
