using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player {
	private IList<Int32> ballsCollected = new List<Int32>();
    public Int32 cueInPocketTimes = 0;

	public Player(string name) {
		Name = name;
	}

	public string Name {
		get;
		private set;
	}

	public int Points {
		get { return ballsCollected.Count; }
	}


	public void Collect(int ballNumber) {
		Debug.Log(Name + " collected ball " + ballNumber);
		if (ballNumber==0) {
            cueInPocketTimes++;
        } else {
			ballsCollected.Add(ballNumber);
		}
	}
}
