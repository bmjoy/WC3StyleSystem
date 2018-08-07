using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MoveController : NetworkBehaviour {

    Animator ani;
    UnityEngine.AI.NavMeshAgent nav;

    float h, v;
    Vector3 moveVec;
	Vector3 faceVec;

	public float speedMove = 20;
	public float speedRotate = 0.2f;

	bool isDead= false;
	bool isBusy = false;

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

		if (isLocalPlayer) {
			h = ETCInput.GetAxis ("Horizontal");
			v = ETCInput.GetAxis ("Vertical");
			moveVec = new Vector3 (h, 0, v);
			moveVec.Normalize ();
		}

		if (isDead) {
			return;
		}

		if (isBusy) {
			//Quaternion targetRotation = Quaternion.LookRotation(faceVec);
			//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotate);

			transform.LookAt (faceVec);
			return;
		}

        if (h != 0 || v != 0)
        {
            ani.SetBool("Run", true);
            // 根据摄像机方向 进行移动
            moveVec = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * moveVec;
			nav.Move(moveVec * Time.deltaTime * speedMove);
            RotatePlayer();
        }
        else
        {
            ani.SetBool("Run", false);
        }
	}

    private void RotatePlayer()
    {
        //向量v围绕y轴旋转cameraAngle.y度
        Vector3 vec = Quaternion.Euler(0, 0, 0) * moveVec;
        if (vec == Vector3.zero)
            return;
        Quaternion qua = Quaternion.LookRotation(vec);
		transform.rotation = Quaternion.Lerp(transform.rotation, qua, speedRotate);
    }

	public void SetUnitControlRevive(){
		isDead = false;
	}

	public void SetUnitControlDeath(){
		isDead = true;
	}

	public void SetUnitBusy(float time){
		StartCoroutine(SetUnitBusyIE(time));
	}

	IEnumerator SetUnitBusyIE(float time){
		isBusy = true;
		yield return new WaitForSeconds (time);
		isBusy = false;
	}

	public void SetFaceDirect(Vector3 src){
		faceVec = src;
	}
}
