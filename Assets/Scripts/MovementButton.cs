using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour
{
    public Button button;
    public Text moveName;
    public Image icon;
    public int index;

    private Item mItem;
    private MovementScrollList mScrollList;
    private Ship mShip;

	// Use this for initialization
	void Start ()
    {
        button.onClick.AddListener(HandleClick);
	}

    public void Setup(Item currentItem, MovementScrollList currentList)
    {
        mItem = currentItem;
        moveName.text = mItem.itemName;
        icon.sprite = mItem.icon;
        index = mItem.index;

        mShip = mItem.ship;
        mScrollList = currentList;
    }

    public void HandleClick()
    {
        Debug.Log("You Clicked " + moveName.text + " button");
        mShip.AssignMovement(index);
    }
	
}
