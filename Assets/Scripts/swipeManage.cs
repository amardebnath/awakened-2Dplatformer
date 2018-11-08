using UnityEngine;
using System.Collections;

public class swipeManage : MonoBehaviour {

	private Vector3 touchPosition;
	private float swipeResistanceY = 100f;

	void Update () {
		if (Input.GetMouseButtonDown (0))
			touchPosition = Input.mousePosition;
		if (Input.GetMouseButtonUp (0)) {
			Vector2 deltaSwipe = touchPosition - Input.mousePosition;
			if (Mathf.Abs (deltaSwipe.y) > swipeResistanceY) {
				if (deltaSwipe.y > 0)
					StartCoroutine(jumpDown(0.5f));
			}
		}
	}

	IEnumerator jumpDown(float bla)
	{
		Physics2D.IgnoreLayerCollision (11, 15, true);
		print ("start");
		yield return new WaitForSeconds (bla);
		print ("end");
		Physics2D.IgnoreLayerCollision (11, 15, false);
	}
}
