using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	private List<Player> players = new List<Player>();
	private Player playerOne, playerTwo;
	private bool inCypher = false;

	private Player cPlayer;

	private int barCount = 40;

	void Start(){
		playerOne = new Player (0);
		playerTwo =  new Player (1);
		playerOne.SetEntranceKey (new KeyValue ("leftcommand", KeyCode.LeftCommand));
		playerTwo.SetEntranceKey (new KeyValue ("rightcommand", KeyCode.RightCommand));
		players.Add (playerOne);
		players.Add (playerTwo);
	}

	void Update(){
		Player p;
		for (int k = 0; k<players.Count; k++) {
			p = players[k];
			if(p.EntranceKeyHit()){
				PlayerEntranceRequested(p);
			}
		}
	}

	public bool PlayerOneUp(){
		return cPlayer == playerOne;
	}

	public void KeyReceived(string s){
		if (inCypher) {
//			 get score.
			ScoreKeeper.Instance.PlayerInput(s);
		}
	}


	public void StartCypher(Player p){
		inCypher = true;
		cPlayer = p;
//		KeyValue anchorKey = 
		ScoreKeeper.Instance.NewPlayerStarted (players [0]);
		ScoreKeeper.Instance.StartSequence (KeyData.Instance.GetRandomKey (), barCount);
		// do something. Scorekeeper starts.
	}


	public void EndCypher(){
		inCypher = false;
		cPlayer = null;
		// Scorekeeper ends. Beat keeps going, wait for player to tap in.
	}

	public Player GetPlayer(int i){
		return players [i];
	}

	public void PlayerEntranceRequested(Player p){
		StartCoroutine ("RunPlayerEntrance", p);
		Debug.Log ("Player requested entrance oooh shit! "+(p==playerOne));
	}

	private bool loopStarted = false;

	public void LoopStarted(){
		loopStarted = true;
	}

	private IEnumerator RunPlayerEntrance(Player p){
		Debug.Log ("Request received, waiting for entrance...");
		while (!loopStarted) {
			yield return null;
		}

		loopStarted = false;

		float countDown = Metrognome.Instance.GetLoopRemainingTime();
		while (countDown>=0) {
			countDown -= Time.deltaTime;
			Displays.Instance.UpdatePlayerCountdown(p==playerOne, countDown);
			yield return null;
		}
		Displays.Instance.PlayerCountdownComplete ();
//		Displays.
		if (p==playerOne) {
			Displays.Instance.BeginPlayerOne ();
		} else {
			Displays.Instance.BeginPlayerTwo();
		}
		StartCypher (p);
	}

	private void CancelPlayerEntrace(){
		StopCoroutine ("RunPlayerEntrance");
	}

}
