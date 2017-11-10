using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Base Ship Class that defines the ships movement, attack, and abilities
*/ 
public class Ship : MonoBehaviour
{
    public enum TurnType
    {
        None = 0,
        Soft = 45,
        Hard = 90,
        Count
    };

    public enum ShipSize
    {
        Small = 3,
        Medium = 5,
        Large  = 7,
        Total
    };

    private ShipSize mSize;
    private float mRotationRate;

    // Ship Movement values
    public bool isShipMoving = false;
    public TurnType currentTurn;
    public int shipSpeed = 1;
    public Vector3 startPoint;
    public Vector3 midPoint;
    public Vector3 endPoint;
    public float lerpTime = 1f;
    public float currentTime;
    
    public Quaternion endRotation = Quaternion.identity;
	
    // Use this for initialization
	void Start ()
    {
        mSize = ShipSize.Small;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isShipMoving)
        {
            // update time
            currentTime += Time.deltaTime;
            if (currentTime >= lerpTime)
            {
                currentTime = lerpTime;
                isShipMoving = false;
            }

            float percent = currentTime / lerpTime;
            // lerp position
            transform.position = Mathf.Pow(1 - percent, 2) * startPoint + 2 * (1 - percent) * percent * midPoint + Mathf.Pow(percent, 2) * endPoint;
            // lerp rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, mRotationRate * Time.deltaTime);

        }
    }


    private void InitializeMove(Vector3 endVector, float softness, Vector3 rotationVector)
    {
        startPoint = transform.position;
        midPoint = startPoint + (transform.forward * (int)mSize * shipSpeed);
        endPoint = midPoint + (endVector * (int)mSize * shipSpeed * softness);
        endRotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationVector);
        currentTime = 0f;
        isShipMoving = true;
    }

    public void MoveHardLeft()
    {
        mRotationRate = 90f;
        InitializeMove(-transform.right, 1f, new Vector3(0f, -90f, 0f));
    }

    public void MoveHardRight()
    {
        mRotationRate = 90f;
        InitializeMove(transform.right, 1f, new Vector3(0f, 90f, 0f));
    }

    public void MoveSoftLeft()
    {
        mRotationRate = 45f;
        InitializeMove(-transform.right + transform.forward, .5f, new Vector3(0f, -45f, 0f));
    }

    public void MoveSoftRight()
    {
        mRotationRate = 45f;
        InitializeMove(transform.right + transform.forward, .5f, new Vector3(0f, 45f, 0f));
    }

    public void MoveStraight()
    {
        mRotationRate = 0f;
        InitializeMove(transform.forward, 0f, new Vector3(0f, 0f, 0f));
    }
}
