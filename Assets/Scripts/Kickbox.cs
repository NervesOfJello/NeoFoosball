using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickbox : MonoBehaviour {

    //component variables
    private Kicker parentKicker;
    private Rigidbody ballRigidbody;
    private AudioSource source;

    //variables for treating the triggers like buttons
    private bool triggerDown = false;
    private bool canKick = false;
    private float triggerkWindowTime = 0.1f;

    //input string property
    private string KickInput
    {
        get { return "Fire" + parentKicker.PlayerNumber + parentKicker.KickerNumber; }
    }

	// Use this for initialization
	void Start ()
    {
        parentKicker = GetComponentInParent<Kicker>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetTriggerDown();
	}

    //Code to treat the trigger pull like Input.GetButtonDown
    private void GetTriggerDown()
    {
        if (Input.GetAxis(KickInput) != 0)
        {
            if (triggerDown == false)
            {
                StartCoroutine(TriggerDownWindowCoroutine());
                triggerDown = true;
            }
            
        }
        else
        {
            triggerDown = false;
        }
    }

    //provides a very short window after the trigger pull in which the kick will function
    private IEnumerator TriggerDownWindowCoroutine()
    {
        canKick = true;
        yield return new WaitForSeconds(triggerkWindowTime);
        canKick = false;
    }

    //while the ball is in the trigger, and the trigger has just been pulled, kick the ball
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball" && canKick)
        {
            Kick(other.GetComponent<Rigidbody>());
        }
    }

    //adds force to the ball with a vector with rotation equivalent to the kicker's rotation and multiplied magnitude
    private void Kick(Rigidbody rigidbody)
    {
        canKick = false;
        source.Play();
        Vector3 forceVector = new Vector3(transform.forward.x, 0, transform.forward.z) * parentKicker.KickForce;
        rigidbody.AddForce(forceVector, ForceMode.VelocityChange);
    }
}
