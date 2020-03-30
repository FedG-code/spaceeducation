using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public Vector3 destination;
	public int Q;
	public int R;
	public HexMap map;
	public Vector3 currentPathOffset = new Vector3(0, 0.5f, 0);

	public List<Node> currentNodePath = null;


	public List<Vector3> currentPathV3List;
	// public Vector3[] currentPathV3Array;
	public List<Vector3> destinationList;
	public float MP = 2f;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;

	float speed = 6;

	// Use this for initialization
	void Start () {
		currentPathV3List = new List<Vector3>();
		destinationList = new List<Vector3>();

		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

		destination = transform.position;

		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.widthMultiplier = 0.2f;
		lineRenderer.positionCount = 0;

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;
	}

	// Update is called once per frame
	void Update() {
		if(currentNodePath != null) {
			// Tile has been clicked, which sets nodes for current path
			// convert these nodes to list of vectors (all), and reset our nodepath to be null
			GenerateCurrentPathV3List();
		}
		if(currentPathV3List.Count > 0) {
			// Render the path as a line if there is one

			// TODO set initial point in renderpath to be the ships current
			RenderPath();
		}

		// user has clicked the move button which triggers enqueueNextTurn
		// which enqueues next (MP) position vectors into destinationList
		if (destinationList.Count > 0){
			// gotoDestination();
			if(destination != transform.position){
				Vector3 dir = destination - transform.position;
				Vector3 velocity = dir.normalized * speed * Time.deltaTime;

				// Make sure the velocity doesn't actually exceed the distance we want.
				velocity = Vector3.ClampMagnitude( velocity, dir.magnitude );

				// Define a target position above and behind the target transform
			    // Vector3 targetPosition = new Target.TransformPoint(dir);

				transform.Translate(velocity);

			} else {

				destination = destinationList[0];
				destinationList.RemoveAt(0);
			} //
		}

		// need to regenerate


	}

	public void gotoDestination() {
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
	public void RenderPath(){

			LineRenderer lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.positionCount = currentPathV3List.Count;
			Vector3[] currentPathV3Array = currentPathV3List.ToArray();
			for (int i = 0; i < currentPathV3List.Count; i++) {
				currentPathV3Array[i] += currentPathOffset;
			}

			lineRenderer.SetPositions( currentPathV3Array );


	}
	public void GenerateCurrentPathV3List() {
			currentPathV3List = new List<Vector3>();
			int i = 0;
			// currentPathV3Array = new Vector3[currentNodePath.Count];
			foreach(Node n in currentNodePath ) {

				Vector3 pos = map.HexCoordToWorldCoord( n.Q, n.R );

				currentPathV3List.Add( pos);
				// currentPathV3Array[i] = pos + currentPathOffset;
				i++;
			}
			currentNodePath = null;
	}

	public void enqueueNextTurn() {

		// only allow new destination to be enqueued if enough MP and there isn't currently a destination

		// currentPathV3List.RemoveAt(0);
		if (((int)MP >= 1) & (destinationList.Count == 0) & (currentPathV3List.Count > 0)){
			// convert MP to an int, so that we can see if we have reset the
			// user HPs for a new turn
			destinationList = new List<Vector3>();
			Debug.LogFormat("current position: {0}", transform.position.ToString());
			Debug.LogFormat("	about to remove position: {0}", currentPathV3List[0].ToString());
			currentPathV3List.RemoveAt(0); // Remove your current position

			// destination = currentPathV3List[0];


			for (int i = 0; i < (int)MP; i++){
				// Push the ammount of allowable moves into destination list
				Vector3 dest = currentPathV3List[0];

				Debug.LogFormat("	enquing position: {0}", dest.ToString());

				destinationList.Add(dest );
				currentPathV3List.RemoveAt(0);

			}
			
		}
	}

/**
	public void enqueueNextTurn2() {
		Debug.Log("Clicked on that button for sure!");
		// check to see if the first item on the queue is our current position?
		if (currentPathV3List.Count < 1)
			return;

		// Remove current / old node from Pan
		currentNodePath.RemoveAt(0);

		// Now grab the new first node and move us to that Position
		// Vector3 destvec = map.HexCoordToWorldCoord(currentNodePath[0].Q, currentNodePath[0].R);
		float ttlj = MP;
		while (MP > 0){
			// Debug.LogFormat("taking move {0} out of {1}",MP,ttlj);
			int i = (int)(ttlj - MP);
			Vector3 nextdest = currentPathV3List[i];
			// updateDestination(nextdest);
			MP--;
		}


		if (currentNodePath.Count == 1) {
			// We only have one tile left in the path, and that tile must be
			// our ultimate destination: This is the current position of the unit.
			// Therefore we only need to clear our pathfinding array:
			currentNodePath = null;
		}
	}
**/
}
