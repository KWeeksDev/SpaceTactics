using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    public Button button;
    public GameManager manager;
    public Player localPlayer;

    // Use this for initialization
    void Start ()
    {
        manager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(HandleClick);
	}
	
    public void HandleClick()
    {
        manager.PlayerReady(localPlayer.Id);
    }
}
