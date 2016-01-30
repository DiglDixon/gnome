using UnityEngine;
using System.Collections;

public class Player {

	private int playerIndex;
	private string playerName;
	private KeyValue entranceKey;
	private PlayerHistory _history;
	public PlayerHistory history{
		set{
			_history = value;
		}
		get{
			return _history;
		}
	}

	public Player(int n){
		playerIndex = n;
		history = new PlayerHistory ();
	}

	public void SetName(string n){
		playerName = n;
	}

	public void SetEntranceKey(KeyValue k){
		entranceKey = k;
	}

	public bool EntranceKeyHit(){
		return Input.GetKeyDown (entranceKey.GetKeyCode());
	}

}
