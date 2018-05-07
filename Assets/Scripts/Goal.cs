using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private int teamNumber;
    [SerializeField]
    private float ballDestroyDelay;

    private AudioSource source;

	// Use this for initialization
	void Start () 
	{
        source = GetComponent<AudioSource>();
	}

    //when a ball enters the goal, destroy it after a very short delay, play the scoring sound then change the value (which will trigger the ui theatrics in the GameManager
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            source.Play();

            Destroy(other);

            switch (teamNumber)
            {
                case 1:
                    gameManager.Team1Score++;
                    break;
                case 2:
                    gameManager.Team2Score++;
                    break;
                default:
                    break;
            }
        }
    }
}
