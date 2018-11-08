using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public GameObject explosion;
	private Rigidbody2D bomb_rigidbody;
	public float bombXvelocity, bombYvelocity;
	public float bombScale = 1f;
	private int temp;
	Anger anger;

	void Start () {
		bomb_rigidbody = GetComponent<Rigidbody2D> ();
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Anger") {
			anger = col.gameObject.GetComponent<Anger> ();
			temp = anger.health;
			anger.health = temp - 1;
			if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0f)
				anger.dir = 1;
			else 
				anger.dir = -1;
		}
		if (col.gameObject.layer != 11) {
			Destroy (gameObject);
			GameObject temp = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
			temp.SendMessage ("getDirection", transform.localScale.x);
		}
	}

	public void initialize(float dir)
	{
		bomb_rigidbody = GetComponent<Rigidbody2D> ();
		if (dir != 0f) {
			transform.localScale = new Vector3 (dir * bombScale, bombScale, 1);
			bomb_rigidbody.velocity = new Vector2 (dir * bombXvelocity, bombYvelocity);
		}
	}
}
