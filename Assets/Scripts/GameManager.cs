using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	[SerializeField]
	private Ship FocusedShip;

	// Use this for initialization
	void Start () {
				if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse id Down");

			RaycastHit hitInfo = new RaycastHit();

			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit)
			{
				Debug.Log("Hit" + hitInfo.transform.gameObject.name);
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
	}
	}
	
	// Update is called once per frame
	void Update ()
	{

}
