using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DeckManager : MonoBehaviour {
	
	private int currentPlayer = 0;
	
	public static DeckManager dm;
	public List<GameObject> allCards = new List<GameObject>();
	public List<GameObject> playerCards = new List<GameObject>();
	
	public GameObject defaultCard;
	
	public Transform availableCardsPanel, playerDeckPanel;
	
	public UnityEngine.UI.Text availabelCardsText;
	public UnityEngine.UI.Text playerCardsText;
	public UnityEngine.UI.Text playerText;
	
	public GameObject prompt;
	
	public int minimumCardsInDeck;
	
	void Awake ()
	{
		SetPlayer( PlayerPrefs.GetInt ("player") );
		
		if (dm == null) {
		
		dm = this;
		
		//DontDestroyOnLoad ( this.gameObject );
		}
	}
	
	// Use this for initialization
	void Start () 
	{	
		LoadPlayerDeck ();
		LoadDefaultDeck();
		playerText.text = "PLAYER " + currentPlayer.ToString(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (allCards != null) availabelCardsText.text = "Available Cards (" + allCards.Count  + ")";
		if (playerCards != null) playerCardsText.text = "Player Cards (" + playerCards.Count  + " / " + minimumCardsInDeck.ToString() + " )";
	}
	
	public void MoveCard (GameObject _card)
	{
		if (_card.transform.parent == availableCardsPanel)
		{ 
			allCards.Remove (_card);
			playerCards.Add (_card);
			_card.transform.SetParent (playerDeckPanel);
		}
		else if (_card.transform.parent == playerDeckPanel)
		{ 
			allCards.Add (_card);
			playerCards.Remove (_card);
			_card.transform.SetParent (availableCardsPanel);
		}
	}
	
	void LoadDefaultDeck ()
	{
		foreach (BaseCard bc in baseDeck)
		{
			if (!IsInPlayerDeck (bc.id, currentPlayer))
			{ 
				GameObject newCard = Instantiate (defaultCard);
				newCard.transform.SetParent ( availableCardsPanel.transform );
				newCard.GetComponent<Card>().id = bc.id;
				newCard.GetComponent<Card>().cardName = bc.name;
				newCard.GetComponent<Card>().cardType = bc.type;
				newCard.GetComponent<Card>().cardPower = bc.power;
				allCards.Add ( newCard );
			}
		}
	}
	
	bool IsInPlayerDeck (int _id, int _player)
	{
		if ( _player == 1) 
		{
			foreach (BaseCard bc in player1Deck)
			{
				if (bc.id == _id) return true;
			}
		}
		else if ( _player == 2) 
		{
			foreach (BaseCard bc in player2Deck)
			{
				if (bc.id == _id) return true;
			}
		}
		
		return false;
	}
	
	public void SavePlayerDeck ()
	{
		if (playerCards.Count < minimumCardsInDeck)
		{
			ShowWarning();
			return;
		}
		
		player1Deck = new List<BaseCard>();
		player2Deck = new List<BaseCard>();
		//GameObject currentPlayer = (player == 1) ? player1 : player2;
		foreach (GameObject card in playerCards)
		{
			Card c = card.GetComponent<Card>();
			
			if (currentPlayer == 1) player1Deck.Add ( new BaseCard (c.id, c.cardType, c.cardPower, c.cardName) );
			else if (currentPlayer == 2) player2Deck.Add ( new BaseCard (c.id, c.cardType, c.cardPower, c.cardName) );
		}
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/player" + currentPlayer + ".deck");
		
		if (currentPlayer == 1) bf.Serialize(file, player1Deck);
		else if (currentPlayer == 2) bf.Serialize(file, player2Deck);

		
		file.Close();
	}
	
	public void LoadPlayerDeck() 
	{
		if(File.Exists(Application.persistentDataPath + "/player" + currentPlayer + ".deck")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/player" + currentPlayer + ".deck", FileMode.Open);
			if (currentPlayer == 1) player1Deck = (List<BaseCard>)bf.Deserialize(file);
			else if (currentPlayer == 2) player2Deck = (List<BaseCard>)bf.Deserialize(file);
			file.Close();
		}
		
		List<BaseCard> tmp = (currentPlayer == 1) ? player1Deck : player2Deck;
		
		foreach (BaseCard bc in tmp)
		{
			GameObject newCard = Instantiate (defaultCard);
			newCard.transform.SetParent ( playerDeckPanel.transform );
			newCard.GetComponent<Card>().id = bc.id;
			newCard.GetComponent<Card>().cardName = bc.name;
			newCard.GetComponent<Card>().cardType = bc.type;
			newCard.GetComponent<Card>().cardPower = bc.power;
			playerCards.Add ( newCard );
		}
	}	
	
	public void SetPlayer (int _player)
	{
		currentPlayer = _player;	
	}
	
	public void ShowWarning ()
	{
		prompt.SetActive ( true );
	}	
	
	public void HideWarning ()
	{
		prompt.SetActive ( false );
	}
	
	public void Back () { Application.LoadLevelAsync ("Menu"); }
	
	public List<BaseCard> player1Deck = new List<BaseCard>();
	public List<BaseCard> player2Deck = new List<BaseCard>();	
	
	public List<BaseCard> baseDeck = new List<BaseCard> 
	{
		new BaseCard(0,0,10,"card1"),
		new BaseCard(1,0,10,"card2"),
		new BaseCard(2,0,10,"card3"),
		new BaseCard(3,2,10,"card4"),
		new BaseCard(4,2,8,"card5"),
		new BaseCard(5,2,8,"card6"),
		new BaseCard(6,1,6,"card7"),
		new BaseCard(7,2,6,"card8"),
		new BaseCard(8,2,6,"card9"),
		new BaseCard(9,2,6,"card10"),
		new BaseCard(10,2,6,"card11"),
		new BaseCard(11,2,6,"card12"),
		new BaseCard(12,0,5,"card13"),
		new BaseCard(13,0,5,"card14"),
		new BaseCard(14,0,5,"card15"),
		new BaseCard(15,1,5,"card16"),
		new BaseCard(16,1,5,"card17"),
		new BaseCard(17,1,5,"card18"),
		new BaseCard(18,1,5,"card19"),
		new BaseCard(19,1,5,"card20"),
		new BaseCard(20,2,5,"card21"),
		new BaseCard(21,0,4,"card22"),
		new BaseCard(22,0,4,"card23"),
		new BaseCard(23,0,4,"card24"),
		new BaseCard(24,0,4,"card25"),
		new BaseCard(25,1,4,"card26"),
		new BaseCard(26,1,4,"card27"),
		new BaseCard(27,0,2,"card28"),
		new BaseCard(28,0,1,"card29"),
		new BaseCard(29,0,1,"card30"),
		new BaseCard(30,0,1,"card31"),
		new BaseCard(31,0,1,"card32"),
		new BaseCard(32,0,1,"card33"),
		new BaseCard(33,2,1,"card34"),
		new BaseCard(34,2,1,"card35"),
		new BaseCard(35,2,1,"card35"),
		new BaseCard(36,2,1,"card36")
	};
	
}
