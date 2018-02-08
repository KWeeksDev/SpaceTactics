using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrapper to manage all the ships in play and general information for easier retrieval
public class ShipManager : MonoBehaviour
{
    public List<Ship> mShips;
    public List<Ship> player1Ships;
    public List<Ship> player2Ships;
    public int mShipsWithMovement;
    public int mShipsThatAttacked;
    public static float Initiative = -0.5f; // Player 1 has initiative which will modify their ships in the action/combat orders

    // Use this for initialization
    void Start ()
    {
        mShipsWithMovement = 0;
        foreach (Ship s in FindObjectsOfType<Ship>())
        {
            mShips.Add(s);
            if (s.shipOwner.Id == Player.PlayerId.Player1)
            {
                player1Ships.Add(s);
            }
            else
            {
                player2Ships.Add(s);
            }
        }
    }

    public void ShipAssignedMove()
    {
        mShipsWithMovement++;
    }

    public bool AllShipsAssignedMovement()
    {
        return mShipsWithMovement == mShips.Count;
    }

    public void MoveShips()
    {
        foreach (Ship s in mShips)
        {
            if (s.movementSelected)
            {
                s.StartMovement();
            }
        }
        mShipsWithMovement = 0;
    }

    public bool AllShipsAttacked()
    {
        return mShipsThatAttacked == mShips.Count;
    }
}
