using UnityEngine;
using System.Collections;

public class HexNew : MonoBehaviour {

	// Our coordinates in the map array
	public int column;
	public int row;

	public HexNew[] GetNeighbours() {

		// So if we are at x, y -- the neighbour to our left is at x-1, y
		GameObject leftNeighbour = GameObject.Find("Hex_" + (column-1) + "_" + row);

		// Right neighbour is also easy to find.
		GameObject right = GameObject.Find("Hex_" + (column+1) + "_" + row);

		// The problem is that our neighbours to our upper-left and upper-right
		// might be at x-1 and x, OR they might be at x and x+1, depending
		// on whether we our on an even or odd row (i.e. if y % 2 is 0 or 1)

		return null;
	}

}
