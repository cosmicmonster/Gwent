using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameManager gm;
	
	public GameObject defaultCard;
	public List<GameObject> player1, player1Graveyard;
	public List<GameObject> player2, player2Graveyard;
	
	public Color player1Color, player2Color;
	
	public GameObject player1Panel, player2Panel;
	
	public Transform[] player1Board, player2Board;
	
	public int player1Power = 0;
	public int player2Power = 0;
	
	public UnityEngine.UI.Text player1Score;
	public UnityEngine.UI.Text player2Score;
	public UnityEngine.UI.Text totalScore;
	
	public bool passed = false;
	public int[] score = new int[2];
	
	public enum State { Start, Player1, Player2, Pause, GameOver };
	
	public State gameState = State.Player1;
	
	
	void Awake ()
	{
		if (gm == null) gm = this;
	}
	// Use this for initialization
	void Start () 
	{
		player1 = new List<GameObject>();
		player2 = new List<GameObject>();
		
		//GenerateHand (player1, player1Color, player1Panel);
		//GenerateHand (player2, player2Color, player2Panel);
		
		DisablePlayer ( player2 );
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateScore ();
		
		if (gameState == State.Player1)
		{
			//DisablePlayer (player1);
			//EnablePlayer (player2);
		}
	}
	
	void UpdateScore ()
	{
		player1Score.text = "Player 1: " + player1Power.ToString();
		player2Score.text = "Player 2: " + player2Power.ToString();
		
		totalScore.text = score[0].ToString() + " - " + score[1].ToString();
	}

	void GenerateHand (List<GameObject> _player, Color _c, GameObject _parent)
	{
		/*foreach (BaseCard bc in deck.allCards)
		{
			GameObject newCard = Instantiate (defaultCard);
			newCard.transform.SetParent ( _parent.transform );
			newCard.GetComponent<UnityEngine.UI.Image>().color = _c;
			newCard.GetComponent<Card>().id = bc.id;
			newCard.GetComponent<Card>().cardName = bc.name;
			newCard.GetComponent<Card>().cardType = bc.type;
			newCard.GetComponent<Card>().cardPower = bc.power;
			_player.Add ( newCard );
		}
		/*for(int i = 0; i < 10; i++)
		{
			GameObject newCard = Instantiate (defaultCard);
			newCard.transform.SetParent ( _parent.transform );
			newCard.GetComponent<UnityEngine.UI.Image>().color = _c;
			_player.Add ( newCard );
		}*/
	}
	
	public void Pass ()
	{
		if (passed) EndRound ();
		
		passed = true;
		
		EndTurn ();	
	}
	
	public void PlayCard (GameObject _card)
	{
		Card tempCard = _card.GetComponent <Card>();
		
		if ( gameState == State.Player1 ) 
		{
			player1Power += tempCard.cardPower;
			//player1.Remove ( _card );
		}
		else if ( gameState == State.Player2 ) 
		{
			player2Power += tempCard.cardPower;
			//player2.Remove ( _card );
		}
		
		//Destroy (_card);
		
		MoveCard (_card);
		
		if (!passed) EndTurn ();
	}
	
	void MoveCard (GameObject _card)
	{
		if (gameState == State.Player1) 
		{
			_card.transform.SetParent (player1Board[_card.GetComponent<Card>().cardType]);
		} 
		else if (gameState == State.Player2) 
		{
			_card.transform.SetParent (player2Board[_card.GetComponent<Card>().cardType]);
		}
		else Debug.LogWarning ("Can't move card during non-player phase");
	}
	
	void EndTurn ()
	{
		if ( gameState == State.Player1 ) 
		{
			gameState = State.Player2;
			EnablePlayer ( player2 );
			DisablePlayer( player1 );
		}
		else if ( gameState == State.Player2 ) 
		{
			gameState = State.Player1;
			EnablePlayer ( player1 );
			DisablePlayer( player2 );			
		}
	}
	
	void EndRound ()
	{
		if (player1Power > player2Power) score[0]++;
		else if (player1Power < player2Power) score[1]++;
		else { score[0]++; score[1]++;}
		
		ClearBoard ();
		ResetScore ();
		
		passed = false;
	}
	
	void ClearBoard ()
	{
		for (int i = 0; i < 3; i++)
		{
			foreach (Transform c in player1Board[i])
			{
				player1Graveyard.Add (c.gameObject);
				player1.Remove (c.gameObject);
				Destroy (c.gameObject);	
			}
		}	
		for (int i = 0; i < 3; i++)
		{
			foreach (Transform c in player2Board[i])
			{
				player2Graveyard.Add (c.gameObject);
				player2.Remove (c.gameObject);
				Destroy (c.gameObject);	
			}
		}					
	}
	
	void ResetScore ()
	{
		player1Power = player2Power = 0;
	}
	
	void EnablePlayer (List<GameObject> _player)
	{
		foreach (GameObject g in _player) g.GetComponent<UnityEngine.UI.Button>().interactable = true;
	}
	
	void DisablePlayer (List<GameObject> _player)
	{
		foreach (GameObject g in _player) g.GetComponent<UnityEngine.UI.Button>().interactable = false;
	}
	
	void OnGUI ()
	{
		GUILayout.Label ("Turn: " + gameState.ToString());
	}
}
