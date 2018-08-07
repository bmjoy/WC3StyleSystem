using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private float mSpeed = 5;
    private CharacterController mController; 

	void Start ()
    {
        mController = gameObject.GetComponent<CharacterController>();

    }
	
	void Update ()
    {
		if (mController.isGrounded)
        {

        }
        else
        {

        }
	}
}
