using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public Vector3 destination;
	public int Q;
	public int R;
	public HexMap map;
	public List<Node> currentPath = null;

	// private List<Vector3> currentPathV3 = null;

	float speed = 6;

	// Use this for initialization
	void Start () {
		// LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		destination = transform.position;
	}

	// Update is called once per frame
	void Update() {
		if(currentPath != null) {
			int currNode = 0;


			while( currNode < currentPath.Count-1 ) {

				Vector3 start = map.HexCoordToWorldCoord( currentPath[currNode].Q, currentPath[currNode].R ) +
					new Vector3(0, 0.5f,0) ;

				Vector3 end   = map.HexCoordToWorldCoord( currentPath[currNode+1].Q, currentPath[currNode+1].R )  +
					new Vector3(0, 0.5f,0) ;


				Debug.DrawLine(start, end, Color.red);

				currNode++;
			}

		}

	}
	void pdate () {
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

		// Define a target position above and behind the target transform
	    // Vector3 targetPosition = new Target.TransformPoint(dir);

		transform.Translate(velocity);
		// transform.Rotate(0, 1, 0);

	}

	void NextTurn() {
		// Set "destination" to be the position of the next tile
		// in our pathfinding queue.
	}
}
