using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	private List<Player> players = new List<Player>();
	private bool inCypher = false;

	void Start(){
		players.Add (new Player (0));
		players.Add (new Player (1));
		GetPlayer (0).SetName ("MC BASHA");
		GetPlayer (1).SetName ("GNOBODY");
		GetPlayer (0).SetEntranceKey (new KeyValue ("leftcommand", KeyCode.LeftCommand));
		GetPlayer (1).SetEntranceKey (new KeyValue ("rightcommand", KeyCode.RightCommand));
	}

	void Update(){
		Player p;
		for (int k = 0; k<players.Count; k++) {
			p = players[k];
			if(p.EntranceKeyHit()){
				PlayerEntranceRequested(p);
			}
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			Metrognome.Instance.StartBeat();
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			Metrognome.Instance.StopBeat();
		}
	}

	public void KeyReceived(string s){
		if (inCypher) {
//			 get score.
			ScoreKeeper.Instance.PlayerInput(s);
		}
	}

	private void StartCypher(){
		inCypher = true;
//		KeyValue anchorKey = 
//		ScoreKeeper.Instance.StartSequence(
		// do something. Scorekeeper starts.
	}


	private void EndCypher(){
		inCypher = false;
		// Scorekeeper ends. Beat keeps going, wait for player to tap in.
	}

	public Player GetPlayer(int i){
		return players [i];
	}

	public void PlayerEntranceRequested(Player p){
		StartCoroutine ("RunPlayerEntrance");
	}

	private IEnumerator RunPlayerEntrance(){
		while (!Metrognome.Instance.isLoopStart) {
			yield return null;
		}
		StartCypher ();
	}

	private void CancelPlayerEntrace(){
		StopCoroutine ("RunPlayerEntrance");
	}

}
