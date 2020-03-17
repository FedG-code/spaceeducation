using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public Vector3 destination;

	float speed = 6;

	// Use this for initialization
	void Start () {
		destination = transform.position;
	}

	// Update is called once per frame
	void Update () {
		// Move towards our destination

		// NOTE!  This just moves directly there, but really you'd want to feed
		// this into a pathfinding system to get a list of sub-moves or something
		// to walk a reasonable route.
		// To see how to do this, look up my TILEMAP tutorial.  It does A* pathfinding
		// and throughout the video I explain how you can apply that pathfinding
		// to hexes.

		Vector3 dir = destination - transform.position;
		Vector3 velocity = dir.normalized * speed * Time.deltaTime;

		// Make sure the velocity doesn't actually exceed the distance we want.
		velocity = Vector3.ClampMagnitude( velocity, dir.magnitude );

		transform.Translate(velocity);

	}

	void NextTurn() {
		// Set "destination" to be the position of the next tile
		// in our pathfinding queue.
	}
}
