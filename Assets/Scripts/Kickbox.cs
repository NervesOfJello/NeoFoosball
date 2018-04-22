﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickbox : MonoBehaviour {


    private Kicker parentKicker;
    private Rigidbody ballRigidbody;

    private string KickInput
    {
        get { return "Fire" + parentKicker.PlayerNumber + parentKicker.KickerNumber; }
    }
	// Use this for initialization
	void Start ()
    {
        parentKicker = GetComponentInParent<Kicker>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball" && Input.GetButtonDown(KickInput))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward.x, parentKicker.KickForce, transform.forward.z, ForceMode.VelocityChange);
        }
    }
}
