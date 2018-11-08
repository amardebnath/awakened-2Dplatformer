using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public Transform start, end;
	public float dir;
	private bool near;
	private Animator animator;
	
	void Start()
	{
		dir = -1;
		animator = gameObject.GetComponent<Animator> ();
	}

	void Update () {
		near = Physics2D.Linecast(start.position, end.position, 1 << LayerMask.NameToLayer("Player"));
		if (near)
			animator.SetBool ("attack", true);
		else
			animator.SetBool ("attack", false);
		gameObject.transform.localScale = new Vector3 (dir * -2, 2, 1);
	}
}
