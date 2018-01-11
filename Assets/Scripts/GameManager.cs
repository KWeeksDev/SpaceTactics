using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Ship FocusedShip;
    [SerializeField]
    private Canvas mCanvas;

    [SerializeField]
    private Player mPlayer1; // The player that has initiative and goes first when ship scores are tied
    [SerializeField]
    private Player mPlayer2; // The player going second, not necessarily the second player to join

    public static float Initiative = -0.5f; // Player 1 has initiative which will modify their ships in the action/combat orders

    [SerializeField]
    private List<Ship> mPlayer1Ships;
    [SerializeField]
    private List<Ship> mPlayer2Ships;
    [SerializeField]
    private List<Ship> mShips;

    [SerializeField]
    private Stack<Ship> mActivationStack;

    public int NumberOfShips()
    {
        return mShips.Count;
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
	void Start ()
    {
        mCanvas = FindObjectOfType<Canvas>();
        currentPhase = TurnPhase.Planning;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
			Debug.Log("Mouse id Down");

			RaycastHit hitInfo = new RaycastHit();

			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit)
			{
				Debug.Log("Hit the " + hitInfo.transform.gameObject.name);
				Ship hitShip = hitInfo.transform.gameObject.GetComponent<Ship>();
				if (hitShip)
				{
					if (FocusedShip)
					{
						FocusedShip.Deselect();
					}
					hitShip.Select();
					FocusedShip = hitShip;
				}
			}
		}

        // Game Loop keep repeating the steps until one player has no ships remaining
        while(mPlayer1Ships.Count > 0 && mPlayer2Ships.Count > 0)
        {
            switch(currentPhase)
            {
                case TurnPhase.Planning:
                    AssignShipMomvement();
                    break;
                case TurnPhase.Activate:
                    break;
                case TurnPhase.Combat:
                    break;
                case TurnPhase.Cleanup:
                    break;
            }
        }
	}

    // Update the selected ship for movment options
    void AssignShipMomvement()
    {
    }

    // 
}
