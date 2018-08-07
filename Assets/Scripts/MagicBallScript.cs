using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallScript : MonoBehaviour {

	GameObject source;
	GameObject target;

	float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	float speed = 0.5f;
	float hitRange = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (source == null || target == null)
			return;
		//transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothTime);

		Vector3 dir = target.transform.position - transform.position;
		if (dir.magnitude > hitRange) {
			dir = dir.normalized * speed;
			transform.position = transform.position + dir;
		} else {
			GetComponent<MagicProjectileScript> ().Explosion ();
			//Destroy (gameObject);
		}
	}

	public void SetSourceAndTarget(GameObject src, GameObject tar){
		source = src;
		target = tar;
	}

}
