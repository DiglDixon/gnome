using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreKeeper : Singleton<ScoreKeeper> {

	private Player cPlayer;
	public KeyboardAudioMap keyboardAudio;
	private GnoteSequence cSequence, pSequence;
	private GnoteBar cBar, pBar;

	public Text pendingTricks;
	public Text pendingMultiplier;

	private float cMultiplier = 1;
	private float cBaseScore = 0;

	private Trick[] trickList = {
		new SingleKeyTrick()
	};


	public void PlayerInput(string s){
		// calculate everything.
		keyboardAudio.HandleKeyPress (s);
		AudioClip c = keyboardAudio.GetKeySound (s);
		cBar.AddGnote(new Gnote(s, Metrognome.Instance.GetDivision(), c));
		// for each trick, calculate and add to multi and base
		TrickResults res = new TrickResults();
		for(int k = 0; k<trickList.Length;k++){
			Debug.Log (trickList[k].name);
			res = trickList[k].CalculateResults(cBar, cSequence, cPlayer.history);
			if(res.trickCaption!="null"){
				AddPendingTrick(res);
			}
		}

	}

	public void StartSequence(string s){
		cSequence = new GnoteSequence (s);
		cBar = new GnoteBar ();
		cMultiplier = 1;
		cBaseScore = 0;
	}

	public void NewPlayerStarted(Player p){
		cPlayer = p;
	}

	public void BarFinished(){
		cSequence.AddBar (cBar);
		cBar = new GnoteBar ();
	}

	public void SequenceFinished(){
		cPlayer.history.AddSequence (cSequence);
		cSequence = null;
	}

	private void AddPendingTrick(TrickResults tr){
		cMultiplier += tr.multiplier;
		cBaseScore += tr.baseScore;
		pendingTricks.text = pendingTricks.text + "\n" + tr.baseScore+" "+tr.trickCaption;
		pendingMultiplier.text = cMultiplier.ToString ()+"x";
	}

}