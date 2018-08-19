using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WolfScript : NetworkBehaviour {

	/*
	public GameObject prefabs_footDust;
	public GameObject prefabs_iceBurst;

	public float moveSpeed;
	public float aimFilter;
	public float attackRange = 1.5f;

	GameObject attackTarget;

	Animator ani;
	Rigidbody rigid;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody> ();

		InvokeRepeating("LaunchDust", 0.0f, 0.25f);
	}
	
	// Update is called once per frame
	[ServerCallback]
	void Update () {
		attackTarget = GlobalInfo.current.nowBallOwner;
		if (attackTarget == null){
			rigid.velocity = Vector3.zero;
			ani.SetBool ("Walk", false);
			return;
		}

		gameObject.transform.LookAt (attackTarget.transform);
		ani.SetBool ("Walk", true);

		Vector3 dir = attackTarget.transform.position - transform.position;

		rigid.velocity *= aimFilter;
		rigid.velocity += dir.normalized * Time.deltaTime * moveSpeed;

		//transform.position = transform.position + dir.normalized * Time.deltaTime * moveSpeed;

		if (dir.magnitude < attackRange) {
			Destroy (Instantiate (prefabs_iceBurst, attackTarget.transform.position, Quaternion.identity), 2f);
			Destroy (Instantiate (prefabs_iceBurst, attackTarget.transform.position, Quaternion.identity), 2f);
			ani.SetBool ("Walk", false);
			ani.SetTrigger ("Attack");

			attackTarget.GetComponent<UnitCtrl> ().Kill ();
			GlobalInfo.current.KillPlayer (attackTarget);
			GlobalInfo.current.PrepareNext ();

			GlobalInfo.current.spellPad.SetActive (false);

			GlobalInfo.current.nowBallOwner = null;
		}
	}

	void LaunchDust()
	{
		if (attackTarget == null)
			return;

		Destroy (Instantiate (prefabs_footDust, transform.position,Quaternion.identity), 3f);
	}
	*/

}
