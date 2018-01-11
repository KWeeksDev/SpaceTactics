using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container Class that handle the information to move ships around
public class Movement
{
    public string turnName;
    public Ship mShip;
    public Vector3 endDirection;   // In relation to the ships current direction ie left, right, straight
    public Vector3 rotation;       //
    public float turnSoftness;     //
    public float rotationRate;     // How quickly the ship turns
    public float moveSpeed;        // How much throttle is applied to the ship

    private Vector3 startPoint;
    private Vector3 midPoint;
    private Vector3 endPoint;

    private Quaternion endRotation = Quaternion.identity;
    private float lerpTime = 1f;
    private float currentTime = 0f;
    private bool isShipMoving = false;
    

    public Movement(Ship myShip, string name, Vector3 end, Vector3 rot, float soft, float rate, float speed)
    {
        mShip = myShip;
        turnName = name;
        endDirection = end;
        rotation = rot;
        turnSoftness = soft;
        rotationRate = rate;
        moveSpeed = speed;
    }

    public void Move()
    {
        if (mShip.isShipMoving)
        {
            // update time
            currentTime += Time.deltaTime;
            if (currentTime >= lerpTime)
            {
                currentTime = lerpTime;
                mShip.isShipMoving = false;
            }

            float percent = currentTime / lerpTime;
            // lerp position
            mShip.transform.position = Mathf.Pow(1 - percent, 2) * startPoint + 2 * (1 - percent) * percent * midPoint + Mathf.Pow(percent, 2) * endPoint;
            // lerp rotation
            mShip.transform.rotation = Quaternion.RotateTowards(mShip.transform.rotation, endRotation, rotationRate * Time.deltaTime);
        }
    }

    public void InitializeMove()
    {
        startPoint = mShip.transform.position;
        midPoint = startPoint + (mShip.transform.forward * (int)mShip.mSize * moveSpeed);
        endPoint = midPoint + (endDirection * (int)mShip.mSize * moveSpeed * turnSoftness);
        endRotation = Quaternion.Euler(mShip.transform.rotation.eulerAngles + rotation);
        currentTime = 0f;
        mShip.isShipMoving = true;
    }
}
