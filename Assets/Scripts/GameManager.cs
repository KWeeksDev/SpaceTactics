using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Ship FocusedShip;
    [SerializeField]
    private Canvas mCanvas;
    public MovementScrollList mScrollList;

    [SerializeField]
    private Player mPlayer1; // The player that has initiative and goes first when ship scores are tied
    [SerializeField]
    private Player mPlayer2; // The player going second, not necessarily the second player to join


    public ShipManager mShipManager;

    public int NumberOfShips()
    {
        return mShipManager.mShips.Count;
    }

    public enum TurnPhase
    {
        Planning = 0,
        Activate,
        Combat,
        Cleanup,
        Total
    };

    public TurnPhase currentPhase;

    // Use this for initialization
    void Start()
    {
        mCanvas = FindObjectOfType<Canvas>();
        currentPhase = TurnPhase.Planning;
    }

    // Update is called once per frame
    void Update()
    {
        // Game Loop keep repeating the steps until one player has no ships remaining
        if (mShipManager.mShips.Count > 0)
        {
            // Track if a different ship is selected
            bool hit = false;
            RaycastHit hitInfo = new RaycastHit();

            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    Debug.Log("Hit the " + hitInfo.transform.gameObject.name);
                    Ship hitShip = hitInfo.transform.gameObject.GetComponent<Ship>();
                    if (hitShip)
                    {
                        // Focus the ship if it wasn't already selected
                        if (hitShip != FocusedShip)
                        {
                            if (FocusedShip)
                            {
                                FocusedShip.Deselect();
                            }
                            FocusedShip = hitShip;
                            FocusedShip.Select();

                            // Actions that happen when a new ship is focused
                            switch (currentPhase)
                            {
                                case TurnPhase.Planning:
                                    mScrollList.RemoveItems();
                                    mScrollList.AddItems(FocusedShip);
                                    mScrollList.RefreshDisplay();
                                    break;
                                case TurnPhase.Activate:
                                    break;
                                case TurnPhase.Combat:

                                    break;
                                case TurnPhase.Cleanup:
                                    break;
                            }
                            mScrollList.RefreshDisplay();
                        }
                    }
                }
            }

            // Actions that need to be checked every frame
            switch (currentPhase)
            {
                case TurnPhase.Planning:
                    // All ships have movement, so we go through and move all the ships
                    if (/*mShipManager.AllShipsAssignedMovement() &&*/ AllPlayersReady())
                    {
                        mShipManager.MoveShips();
                        currentPhase = TurnPhase.Combat;
                        UnreadyAllPlayers();
                    }
                    break;
                case TurnPhase.Activate:
                    break;
                case TurnPhase.Combat:
                    if (mShipManager.AllShipsAttacked())
                    {
                        currentPhase = TurnPhase.Cleanup;
                    }
                    break;
                case TurnPhase.Cleanup:
                    break;
            }
        }
    }

    public void PlayerReady(Player.PlayerId id)
    {
        if (id == Player.PlayerId.Player1)
        {
            mPlayer1.isReady = true;
        }
        else if (id == Player.PlayerId.Player2)
        {
            mPlayer2.isReady = true;
        }
        else
        {
            Debug.Log("Error: Unknown player is trying to ready");
        }
    }

    public bool AllPlayersReady()
    {
        return mPlayer1.isReady && mPlayer2.isReady;
    }

    private void UnreadyAllPlayers()
    {
        mPlayer1.isReady = false;
        mPlayer2.isReady = false;
    }
}
