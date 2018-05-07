using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{

    #region Kicker ID
    [Header("Kicker ID")]
    //Player Input Axis Variables
    [SerializeField]
    public int PlayerNumber;
    [SerializeField]
    public int KickerNumber;
    #endregion

    #region Rotation/Direction Variables
    private float horizontalInput;
    private float verticalInput;
    [HideInInspector]
    public Vector3 moveDirection;
    private Quaternion kickerRotation;
    #endregion

    #region Gameplay Variables
    [Header("Gameplay Variables")]
    //Player movement variables
    [SerializeField]
    private float maxMovementSpeed;
    [SerializeField]
    private float turnSpeed;
    public bool controlIsEnabled = true;

    //Kick Variables (Placed in Kicker script for ease of editing along with the other player-focused variables)
    [SerializeField]
    public float KickForce;
    #endregion

    //component variables
    private Rigidbody kickerRigidBody;

    private string HorizontalInputAxis //gets Horizontal axis as a matrix of player number + Kicker Number
    {
        get { return "Horizontal" + PlayerNumber + KickerNumber; }
    }
    private string VerticalInputAxis //gets Vertical axis as a matrix of player number + Kicker Number
    {
        get { return "Vertical" + PlayerNumber + KickerNumber; }
    }

    // Use this for initialization
    void Start ()
    {
        kickerRigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
    {
        GetInput();
        RotateAndMoveKicker();
	}

    private void GetInput()
    {
        if (controlIsEnabled)
        {
            horizontalInput = Input.GetAxis(HorizontalInputAxis);
            verticalInput = Input.GetAxis(VerticalInputAxis);
            //turn both input axes into one vector for movement
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        }
    }

    private void RotateAndMoveKicker()
    {
        if (moveDirection.magnitude != 0)
        {
            kickerRotation = Quaternion.LookRotation(moveDirection, transform.up);
            transform.rotation = kickerRotation;
        }

        float speed = maxMovementSpeed * moveDirection.magnitude;
        kickerRigidBody.velocity = moveDirection * speed;
    }
}
