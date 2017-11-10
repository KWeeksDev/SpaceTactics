using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Base Ship Class that defines the ships movement, attack, and abilities
*/ 
public class Ship : MonoBehaviour
{
    public int health = 3;
    public int shield = 3;
	public bool isSelected = false; // In future we'll have different behaviour depending which player is selecting the ship
	private Renderer rend;
	private Shader shaderStandard;
	private Shader shaderOutline; 

	// Use this for initialization
	void Start ()
    {
		shaderStandard  = Shader.Find("Standard");
		shaderOutline = Shader.Find("Custom/Silhouette-Outline");
		rend = GetComponent<Renderer>();
		rend.material.shader = shaderStandard;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	// Modifies this object when selected by a player
	public void Select()
	{
		if (isSelected == true)
		{
			Debug.Log("Ship is already selected");
			return;
		}

		isSelected = true;

		rend.material.shader = shaderOutline;


	}

	public void Deselect()
	{
		if(isSelected == false)
		{
			Debug.Log("Ship is already unselected");
			return;
		}

		isSelected = false;

		rend.material.shader = shaderStandard;

	}
}
