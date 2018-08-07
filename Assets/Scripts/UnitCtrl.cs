using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitCtrl : NetworkBehaviour {

	public GameObject prefabs_ball;

	public GameObject spellPad;

	Animator ani;
	MoveController movCtrl;

	Vector3 moveVec;

	public bool hasBall;
	public bool isDead;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator>();
		movCtrl = GetComponent<MoveController> ();
		spellPad = GlobalInfo.current.spellPad;

		hasBall = false;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Revive(){
		ani.SetTrigger ("Revive");
		movCtrl.SetUnitControlRevive ();
		isDead = false;
	}

	public void Kill(){
		ani.SetTrigger ("Death");
		movCtrl.SetUnitControlDeath ();
		isDead = true;
	}

	public void SpellSkill(UnitCtrl target){
		StartCoroutine (SpellSkillIE (target));
	}

	[Command]
	void CmdSpellSkill(){
		//StartCoroutine (SpellSkillIE (target));
	}

	IEnumerator SpellSkillIE(UnitCtrl target){
		//停頓單位, 面向施法方向, 命令攻擊動作
		movCtrl.SetUnitBusy (0.7f);
		movCtrl.SetFaceDirect (target.transform.position);
		ani.SetTrigger ("Attack");

		yield return new WaitForSeconds (0.3f);

		//Shoot ok

		GlobalInfo.current.nowBallOwner = target.gameObject;
		target.hasBall = true;
		GlobalInfo.current.spellPad.SetActive (false);
		target.GetComponent<LocalPlayerSetting> ().SetLocalSkillPad ();
		hasBall = false;


		GameObject projectile = Instantiate (prefabs_ball, gameObject.transform.position + new Vector3 (0, 2, 0), Quaternion.identity) as GameObject;
		projectile.transform.LookAt(target.transform.position);
		projectile.GetComponent<MagicProjectileScript> ().impactNormal = new Vector3 (0, 1, 0);
		MagicBallScript _ball = projectile.GetComponent<MagicBallScript> ();
		_ball.SetSourceAndTarget (gameObject, target.gameObject);

		NetworkServer.Spawn (projectile);

	}

}
