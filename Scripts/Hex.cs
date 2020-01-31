using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
	// Our coordinates in the map array
	public int x;
	public int y;

	public Hex[] GetNeighbours()
	{

		// So if we are at x, y -- the neighbour to our left is at x-1, y
		GameObject leftNeighbour = GameObject.Find("Hex_" + (x - 1) + "_" + y);

		// Right neighbour is also easy to find.
		GameObject right = GameObject.Find("Hex_" + (x + 1) + "_" + y);

		// The problem is that our neighbours to our upper-left and upper-right
		// might be at x-1 and x, OR they might be at x and x+1, depending
		// on whether we our on an even or odd row (i.e. if y % 2 is 0 or 1)

		return null;
	}

}

