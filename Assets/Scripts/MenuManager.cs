using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	public static MenuManager mm;
	
	public Transform menuPanel;
	
	public GameObject playerPrompt;
	
	// Use this for initialization
	void Awake () {
		if (mm == null) mm = this;
	}
	
	public void Play ()
	{
		Application.LoadLevelAsync ("Game");
	}
	
	public void Credits ()
	{
		
	}
	
	public void DeckBuilder ()
	{
		foreach ( Transform t in menuPanel )
		{
			t.gameObject.SetActive ( false );
		}
		
		playerPrompt.SetActive ( true );
	}
	
	public void Quit ()
	{
		Application.Quit ();
	}
	
	public void Player1 ()
	{
		PlayerPrefs.SetInt ("player", 1);
		Application.LoadLevelAsync ("DeckBuilder");		
	}
	
	public void Player2 ()
	{
		PlayerPrefs.SetInt ("player", 2);
		Application.LoadLevelAsync ("DeckBuilder");
	}
}
