using UnityEngine;
using System.Collections.Generic;

public class Node
{
	/**
	* Author: Samuel Overington
	* Class for storing graph data (nodes and edges), and all the different
	* connections between each neighbouring tile.
	*
	* Each tile: a node
	* Each neighbouring (connected) tile: an edge.
	**/

	public List<Node> edges;
	public int Q;
	public int R;
	public HexMap map;

	public Node(int q,int r) {
		// Default Constructor
		edges = new List<Node>();
		Q = q;
		R = r;
	}
	public float DistanceTo(Node n) {
		Vector3 source = map.GetHexAt(Q, R).Position();
		Vector3 target = map.GetHexAt(n.Q, n.R).Position();
		// Debug.LogFormat("src: {0}, target: {1}",source.ToString(), target.ToString());
		return Vector3.Distance( source, target );

	}
}
