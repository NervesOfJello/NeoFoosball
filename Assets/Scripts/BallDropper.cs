using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDropper : MonoBehaviour {

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private float dropInterval;
    [SerializeField]
    private float initialWait;
	// Use this for initialization
	void Start () 
	{
        StartCoroutine(initialWaitCoroutine());
	}
	
	private IEnumerator dropPeriodicallyCoroutine()
    {
        Destroy(Instantiate(ball,this.transform), 8);
        yield return new WaitForSeconds(dropInterval);
        
        StartCoroutine(dropPeriodicallyCoroutine());
    }
    private IEnumerator initialWaitCoroutine()
    {
        yield return new WaitForSeconds(initialWait);
        StartCoroutine(dropPeriodicallyCoroutine());
    }
}
