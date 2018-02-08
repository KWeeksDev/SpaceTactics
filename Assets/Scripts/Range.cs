using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public int range;
    public Ship mOwner;

    void OnTriggerEnter(Collider other)
    {
        mOwner.AddShipInRange(other.GetComponent<Ship>(), range);
    }

    void OnTriggerExit(Collider other)
    {
        mOwner.RemoveShipInRange(other.GetComponent<Ship>(), range);
    }
}
