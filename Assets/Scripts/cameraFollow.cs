using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	private GameObject target;
	private Vector3 firstDis;
    public float smooth;
	public float manageX, manageY;
	// Use this for initialization
    
	void Start () {
		target = GameObject.Find ("Player");
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y + 1.51f, -54.68f);
		firstDis = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = target.transform.position + firstDis;
        if(target.transform.localScale.x > 0f)
        {
			pos.x += manageX;
        } else
        {
			pos.x -= manageX;
        }
        

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, smooth * Time.deltaTime);
    }
}
