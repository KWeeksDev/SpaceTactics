using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string itemName;
    public int index;
    public Sprite icon;
    public Ship ship;

    public Item(string name, int idx, Ship owner)
    {
        itemName = name;
        index = idx;
        ship = owner;
    }
}

// Scrollable list of all the selected ships movements
public class MovementScrollList : MonoBehaviour
{
    public List<Item> ItemList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;

    public void RefreshDisplay()
    {
        RemoveButtons();
        AddButtons();
    }

    public void AddItems(Ship focused)
    {
        for (int i = 0; i < focused.mShipMoves.Count; i++)
        {
            ItemList.Add(new Item(focused.mShipMoves[i].turnName, i, focused));
        }
    }

    public void AddItem(string name, int idx, Ship owner)
    {
        ItemList.Add(new Item(name, idx, owner));
    }

    public void RemoveItems()
    {
        ItemList.Clear();
    }
	
    public void AddButtons()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            Item item = ItemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            MovementButton button = newButton.GetComponent<MovementButton>();
            button.Setup(item, this);
        }
    }

    public void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = contentPanel.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }
}
