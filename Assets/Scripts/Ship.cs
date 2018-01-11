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
    public Player   shipOwner;
    public bool     movementSelected;
    public List<Movement> mShipMoves;
    public int mMovementIndex = 0;
    
    public bool isSelected = false; // In future we'll have different behaviour depending which player is selecting the ship
    private Renderer rend;
    private Shader shaderStandard;
    private Shader shaderOutline; 

    public ShipSize mSize;
    private float mRotationRate;

    // Ship Movement values
    public bool isShipMoving = false;
    public TurnType currentTurn;
    public int shipSpeed = 1;

    // Use this for initialization
    void Start ()
    {
        mSize = ShipSize.Small;

        // Temp block to initialize the test moves this ship will have
        mShipMoves = new List<Movement>()
        {
        //hard left
        new Movement(this, "Hard Left 2 Speed", -transform.right, new Vector3(0f, -90f, 0f), 1f, 90f, 2f),
        //hard right
        new Movement(this, "Hard Right 2 Speed", transform.right, new Vector3(0f, 90f, 0f), 1f, 90f, 2f),
        // soft left
        new Movement(this, "Soft Left 2 Speed", -transform.right, new Vector3(0f, 45f, 0f), .5f, 45f, 2f),
        // soft right
        new Movement(this, "Soft Right 2 Speed", transform.right, new Vector3(0f, 45f, 0f), .5f, 45f, 2f),
        // straight
        new Movement(this, "Straight 2 Speed", transform.forward, new Vector3(0f, 0f, 0f), 0f, 0f, 2f)
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

    public void MoveHardLeft()
    {
        mMovementIndex = 0;
        mShipMoves[mMovementIndex].InitializeMove();
    }

    public void MoveHardRight()
    {
        mMovementIndex = 1;
        mShipMoves[mMovementIndex].InitializeMove();
    }

    public void MoveSoftLeft()
    {
        mMovementIndex = 2;
        mShipMoves[mMovementIndex].InitializeMove();
    }

    public void MoveSoftRight()
    {
        mMovementIndex = 3;
        mShipMoves[mMovementIndex].InitializeMove();
    }

    public void MoveStraight()
    {
        mMovementIndex = 4;
        mShipMoves[mMovementIndex].InitializeMove();
    }

    public void SetRotationRate(float rate)
    {
        mRotationRate = rate;
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
		if(isSelected == false)
		{
			Debug.Log("Ship is already unselected");
			return;
		}

		isSelected = false;

	}
}
