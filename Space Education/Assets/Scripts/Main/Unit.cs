using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public Vector3 destination;
	public int Q;
	public int R;
	public HexMap map;
	public Vector3 currentPathOffset = new Vector3(0, 0.5f, 0);
	public List<Node> currentPath = null;

	List<Vector3> currentPathV3List;
	private float jump = 2f;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;

	float speed = 6;

	// Use this for initialization
	void Start () {
		currentPathV3List = new List<Vector3>();

		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

		destination = transform.position;
		currentPathV3List = null;

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
		if(currentPath != null) {
			GenerateCurrentPathVectorList();
		}

	}

	private void RenderPath(){
			LineRenderer lineRenderer = GetComponent<LineRenderer>();

			lineRenderer.positionCount = currentPathV3List.Count;
			lineRenderer.SetPositions(currentPathV3List.ToArray());

			currentPath = null;

	}
	private void GenerateCurrentPathVectorList() {
			foreach(Node n in currentPath ) {

				Vector3 pos = map.HexCoordToWorldCoord( n.Q, n.R );
				Debug.LogFormat("pos: {0}",pos.ToString());
				currentPathV3List.Add( pos );
				Debug.LogFormat("currentPathV3List size: {0}",currentPathV3List.Count);
			}
			currentPath = null;
			RenderPath();

	}
	public void updateDestination (Vector3 v) {
		// Move towards our destination

		// NOTE!  This just moves directly there, but really you'd want to feed
		// this into a pathfinding system to get a list of sub-moves or something
		// to walk a reasonable route.
		// To see how to do this, look up my TILEMAP tutorial.  It does A* pathfinding
		// and throughout the video I explain how you can apply that pathfinding
		// to hexes.


		destination += v;
		Vector3 dir = destination - transform.position;
		Vector3 velocity = dir.normalized * speed * Time.deltaTime;

		// Make sure the velocity doesn't actually exceed the distance we want.
		velocity = Vector3.ClampMagnitude( velocity, dir.magnitude );

		// Define a target position above and behind the target transform
	    // Vector3 targetPosition = new Target.TransformPoint(dir);

		transform.Translate(velocity);
		// transform.Rotate(0, 1, 0);

	}

	public void MoveNextTurn() {
		Debug.Log("Clicked on that button for sure!");
		if (currentPathV3List.Count < 1)
			return;

		// Remove current / old nod from Pan
		currentPath.RemoveAt(0);

		// Now grab the new first node and move us to that Position
		// Vector3 destvec = map.HexCoordToWorldCoord(currentPath[0].Q, currentPath[0].R);
		float ttlj = jump;
		while (jump > 0){
			int i = (int)(ttlj - jump);
			Vector3 nextdest = currentPathV3List[i];
			updateDestination(nextdest);
			jump--;
		}


		if (currentPath.Count == 1) {
			// We only have one tile left in the path, and that tile must be
			// our ultimate destination: This is the current position of the unit.
			// Therefore we only need to clear our pathfinding array:
			currentPath = null;
		}
	}
}
