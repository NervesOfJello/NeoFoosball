using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour {

    private AudioSource source;
	// Use this for initialization
	void Start () 
	{
        source = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Bumper"))
        {
            source.Play();
        }
    }
}
