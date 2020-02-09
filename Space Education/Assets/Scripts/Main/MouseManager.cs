using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

	Unit selectedUnit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Is the mouse over a Unity UI Element?
		if(EventSystem.current.IsPointerOverGameObject()) {
			// It is, so let's not do any of our own custom
			// mouse stuff, because that would be weird.

			// NOTE!  We might want to ask the system WHAT KIND
			// of object we're over -- so for things that aren't
			// buttons, we might not actually want to bail out early.

			return;
		}
		// could also check if game is paused?
		// if main menu is open?
	
		//Debug.Log( "Mouse Position: " + Input.mousePosition );

		// This only works in orthographic, and only gives us the
		// world position on the same plane as the camera's
		// near clipping play.  (i.e. It's not helpful for our application.)
		//Vector3 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		//Debug.Log( "World Point: " + worldPoint );

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		RaycastHit hitInfo;

		if( Physics.Raycast(ray, out hitInfo) ) {
			GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;

			//Debug.Log("Clicked On: " + ourHitObject.name);

			// So...what kind of object are we over?
			if(ourHitObject.GetComponent<HexNew>() != null) {
				// Ah! We are over a hex!
				MouseOver_Hex(ourHitObject);
			}
			else if (ourHitObject.GetComponent<Unit>() != null) {
				// We are over a unit!
				MouseOver_Unit(ourHitObject);

			}


		}

		// If this were an FPS, what you'd do is probably something like this:
		// (This fires a ray out from the center of the camera's view.)
		//Ray fpsRay = new Ray( Camera.main.transform.position, Camera.main.transform.forward );


	}

	void MouseOver_Hex(GameObject ourHitObject) {
		Debug.Log("Raycast hit: " + ourHitObject.name );

		// We know what we're mousing over. 
		// Maybe we want to show a tooltip?

		// Do we have a unit selected?  Because that might change
		// what we do on click.

		// We could also check to see if we're clicking on the thing.

		if(Input.GetMouseButtonDown(0)) {

			// We have clicked on a hex.  Do something about it!
			// This might involve calling a bunch of other functions
			// depending on what mode you happen to be in, in your game.

			// We're just gonna colorize the hex, as an example.
			MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

			if(mr.material.color == Color.red) {
				mr.material.color = Color.white;
			}
			else {
				mr.material.color = Color.red; 	
			}

			// If we have a unit selected, let's move it to this tile!

			if(selectedUnit != null) {
				selectedUnit.destination = ourHitObject.transform.position;
			}


		}

	}

	void MouseOver_Unit(GameObject ourHitObject) {
		Debug.Log("Raycast hit: " + ourHitObject.name );

		if(Input.GetMouseButtonDown(0)) {
			// We have click on the unit
			selectedUnit = ourHitObject.GetComponent<Unit>();
		}

	}
}
