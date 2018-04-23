using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempGoal : MonoBehaviour {

    [SerializeField]
    private Transform ballSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.GetComponent<Transform>().position = ballSpawnPoint.position;
        }
    }
}
