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


	public List<Node> destinationList;
	public List<Vector3> currentPathV3List;
	public float MP = 2f;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;

	float speed = 6;

	// Use this for initialization
	void Start () {
		currentPathV3List = new List<Vector3>();
		destinationList = new List<Node>();

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

	void Update() {

		// user has clicked the move button which triggers enqueueNextTurn
		// which enqueues next (MP) position vectors into destinationList
		if(destination != transform.position){
			// if there is a destination vector, move there.

			// Move to destination
			Vector3 dir = destination - transform.position;
			Vector3 velocity = dir.normalized * speed * Time.deltaTime;

			// Make sure the velocity doesn't actually exceed the distance we want.
			velocity = Vector3.ClampMagnitude( velocity, dir.magnitude );

			// Define a target position above and behind the target transform
		    // Vector3 targetPosition = new Target.TransformPoint(dir);

			transform.Translate(velocity);
		} else if (destinationList.Count > 0){

			// check if there is anymore nodes on our destinationList, and we are not at our current destination
			// pop them off and set the next destination.

			Node n = destinationList[0]; // The next node
			destinationList.RemoveAt(0);

			Q = n.Q;
			R = n.R;

			// currentNodePath.RemoveAt(0);
			RegenerateCurrentPath();
			Debug.LogFormat("{0} -> {1}",destination, transform.position);
			destination = node2vec3(n);
			Debug.LogFormat("{0} -> {1}",destination, transform.position);
			Debug.Log("Loading a new Node onto destination as a vector");
		} else {
			// Nothing to do
			// Debug.Log("There is nothing to do here");
		}
	} // Update

	Vector3 node2vec3(Node n){
		return map.HexCoordToWorldCoord( n.Q, n.R );
		// return map.GetHexAt(n.Q, n.R).postion;
	}

	public void RenderPath(){
			LineRenderer lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.positionCount = currentPathV3List.Count;
			Vector3[] currentPathV3Array = currentPathV3List.ToArray();
			// for (int i = 0; i < currentPathV3List.Count; i++) {
			// 	currentPathV3Array[i] += currentPathOffset;
			// }
			lineRenderer.SetPositions( currentPathV3Array );
	}

	public void RegenerateCurrentPath() {
		// Converts Nodes to Vector3, and then call RenderPath. This enables us
		// to turn off the Path Render, wile still keeping our path.
		GenerateNewQueue(currentNodePath);
	}

	public void GenerateNewQueue(List<Node> nodes) {
			// Nodes and vectors.
			// Are we creating Vectors from node list here?
			currentPathV3List = new List<Vector3>();
			currentNodePath = nodes;
			foreach(Node n in currentNodePath ) {
				Vector3 pos = map.HexCoordToWorldCoord( n.Q, n.R );
				currentPathV3List.Add(pos + currentPathOffset);
			}
			RenderPath();
	}

	public void enqueueNextTurn() {
		// move the next node onto the destination queue and recalculate the new line
		if (currentNodePath.Count > 1){ // we are left with our current node
			// you can only enqueue the next turn if there is a current path
			Node n = currentNodePath[1];
			currentNodePath.RemoveAt(0);
			destinationList.Add(n);
			Debug.LogFormat("We have just added a new node to the destinationList, the new lenth of the list is {0}", destinationList.Count);
			RegenerateCurrentPath();
		}
	}
}
