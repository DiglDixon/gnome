using UnityEngine;
using System.Collections;

public class ScoreKeeper : Singleton<ScoreKeeper> {

	private Player cPlayer;
	public KeyboardAudioMap keyboardAudio;
	private GnoteSequence cSequence, pSequence;
	private GnoteBar cBar, pBar;

	public void PlayerInput(string s){
		// calculate everything.
		keyboardAudio.HandleKeyPress (s);
		AudioClip c = keyboardAudio.GetKeySound (s);
		cBar.AddGnote(new Gnote(s, Metrognome.Instance.GetDivision(), c));
	}

	public void StartSequence(string s){
		cSequence = new GnoteSequence (s);
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

}