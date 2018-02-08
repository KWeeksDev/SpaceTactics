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
        Small = 1,
        Medium = 5,
        Large  = 7,
        Total
    };

    // Public Interface for the game manager
    public GameManager      mGameManager;
    public ShipManager      mShipManager;
    public Player           shipOwner;
    public ShipSize         mSize;
    public List<Movement>   mShipMoves;
    public List<Weapon>     mWeapons;
    public Dictionary<int, List<Ship>> shipsInRange;
    public bool             movementSelected;
    public int              mMovementIndex = 0;
    private int maxRange = 3;
    public float              pilotSkill;

    // Interactive values that will change as the game goes on
    public int shield;
    public int hull;
    // In future we'll have different behaviour depending which player is selecting the ship
    public bool             isSelected = false; 

    // Ship Movement values
    public bool isShipMoving = false;

    // Use this for initialization
    void Start ()
    {
        movementSelected = false;
        mSize = ShipSize.Small;
        shipsInRange = new Dictionary<int, List<Ship>>();
        shipsInRange.Add(1, new List<Ship>());
        shipsInRange.Add(2, new List<Ship>());
        shipsInRange.Add(3, new List<Ship>());

        // Temp block to initialize the test moves this ship will have
        mShipMoves = new List<Movement>()
        {
            //hard left
            new Movement(this, "Hard Left 1 Speed", -transform.right, new Vector3(0f, -90f, 0f), 1f, 90f, 1f),
            //hard right
            new Movement(this, "Hard Right 1 Speed", transform.right, new Vector3(0f, 90f, 0f), 1f, 90f, 1f),
            // soft left
            new Movement(this, "Soft Left 1 Speed", -transform.right, new Vector3(0f, -45f, 0f), .5f, 45f, 1f),
            // soft right
            new Movement(this, "Soft Right 1 Speed", transform.right, new Vector3(0f, 45f, 0f), .5f, 45f, 1f),
            // straight
            new Movement(this, "Straight 1 Speed", transform.forward, new Vector3(0f, 0f, 0f), 0f, 0f, 1f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (isShipMoving)
        {
            mShipMoves[mMovementIndex].Move();
        }
    }

    // Will be called by the button when the ship's movement has been selected
    public void AssignMovement(int idx)
    {
        mMovementIndex = idx;
        if (!movementSelected)
        {
            movementSelected = true;
            mShipManager.ShipAssignedMove();
        }
    }

    // Will be called on all ships once everyone has a movement assigned
    public void StartMovement()
    {
        movementSelected = false;
        mShipMoves[mMovementIndex].InitializeMove();
    }

    // Handles when a ship is selected by the player
    // Later will handle ships being selected by opponents
    public void Select()
    {
        if (isSelected == true)
        {
            Debug.Log("Ship is already selected");
            return;
        }

        isSelected = true;
    }

    public void Deselect()
    {
        if (isSelected == false)
        {
            Debug.Log("Ship is already unselected");
            return;
        }

        isSelected = false;
    }

    public void AddShipInRange(Ship other, int range)
    {
        if (range < maxRange)
        {
            Debug.Log("Removing Ship: " + other.name + " at Range: " + (range+1));
            shipsInRange[range + 1].Remove(other);
        }
        
        Debug.Log("Adding Ship: " + other.name + " at Range: " + range);
        shipsInRange[range].Add(other);
    }

    public void RemoveShipInRange(Ship other, int range)
    {
        if (range < maxRange)
        {
            Debug.Log("Adding Ship: " + other.name + " at Range: " + (range+1));
            shipsInRange[range + 1].Add(other);
        }
        Debug.Log("Removing Ship: " + other.name + " at Range: " + range);
        shipsInRange[range].Remove(other);
    }
}
