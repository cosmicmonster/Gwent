using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Card : MonoBehaviour{
	
	public int id = 0;
	public string cardName = "Card";
	public int cardPower = 1;
	public int cardType = 0;
	
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Text powerText;
	public UnityEngine.UI.Text typeText;

	// Use this for initialization
	void Start () 
	{	
		GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClick(); });

		nameText.text = cardName.ToString();
		powerText.text = cardPower.ToString();
		typeText.text = cardType.ToString();
	}
	
	public void OnClick ()
	{
		if (GameManager.gm != null) GameManager.gm.PlayCard ( this.gameObject );
		else if (DeckManager.dm != null) DeckManager.dm.MoveCard( this.gameObject );
		else Debug.LogWarning ("Can't find Deck or Game managers!");
	}
}