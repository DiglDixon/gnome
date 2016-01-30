using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreKeeper : Singleton<ScoreKeeper> {

	private Player cPlayer;
	public KeyboardAudioMap keyboardAudio;
	private GnoteSequence cSequence, pSequence;
	private GnoteBar cBar, pBar;

	public RectTransform pendingTricksParent;
//	public RectTransform pendingTricksFinalParent;
	public Text pendingMultiplier;
	public Transform pendingPosition;

	private float cMultiplier = 1;
	private float cBaseScore = 0;
	private bool cCapped = false;
	private int cBarsRemaining = -1;

	public Text barsRemainingText;
	public Text sequencePointsText;


	private int capLeeway = 6;

	private Trick[] trickList = {
		new SingleKeyTrick()
	};

	public GameObject pendingTrickListing;


	public void PlayerInput(string s){
		// calculate everything.
		keyboardAudio.HandleKeyPress (s);
		AudioClip c = keyboardAudio.GetKeySound (s);
		
		if (cCapped) {
			return;
		}
		cBar.AddGnote(new Gnote(s, Metrognome.Instance.GetDivision(), c));
		// for each trick, calculate and add to multi and base
		if (IsCappingInput (s)) {
			SetCapped();
			return;
		}

		TrickResults res = new TrickResults();
		for(int k = 0; k<trickList.Length;k++){
			Debug.Log (trickList[k].name);
			res = trickList[k].CalculateResults(cBar, cSequence, cPlayer.history);
			if(res.trickCaption!="null"){
				AddPendingTrick(res);
			}
		}

	}

	private bool IsCappingInput(string s){
		return (s == cSequence.anchorKey) && (Metrognome.Instance.GetDivisionsLeft()<=capLeeway);
	}

	private void SetCapped(){
		cCapped = true;
	}

	public void StartSequence(string s, int barCount){
		cSequence = new GnoteSequence (s);
		cBar = new GnoteBar ();
		cMultiplier = 1;
		cBaseScore = 0;
		cCapped = false;
		cBarsRemaining = barCount;
		barsRemainingText.text = "" + cBarsRemaining;
		StartCoroutine ("RunSequenceMonitor");
	}

	public void NewPlayerStarted(Player p){
		cPlayer = p;
	}

	public void BarFinished(){
		if (cCapped) {
			cSequence.AddScore(cMultiplier * cBaseScore);
			sequencePointsText.text = cSequence.totalScore+"";
		}
		cCapped = false;
		barsRemainingText.text = "" + cBarsRemaining;
		cBarsRemaining -= 1;
		if (cBarsRemaining < 0) {
			SequenceFinished ();
		} else {
			cSequence.AddBar (cBar);
			cBar = new GnoteBar ();
		}
	}

	public void SequenceFinished(){
		sequencePointsText.text = cSequence.totalScore+"";
		cPlayer.history.AddSequence (cSequence);
		StopCoroutine ("RunSequenceMonitor");
		Leaderboard.Instance.AddEntry(cPlayer.playerName, cSequence.totalScore);
		GameManager.Instance.EndCypher ();
	}

	private IEnumerator RunSequenceMonitor(){
		yield return null;
		while (true) {
			if (Metrognome.Instance.isLoopStart) {
				BarFinished ();
			}
			yield return null;
		}
	}


	/// <summary>
	/// / This stuff should be somewhere else
	/// </summary>

	private float totalPendingHeight = 0f;
	private int pendingIndex = 0;

	private void AddPendingTrick(TrickResults tr){
		cMultiplier += tr.multiplier;
		cBaseScore += tr.baseScore;
		GameObject newPending = GameObject.Instantiate (pendingTrickListing) as GameObject;
		newPending.transform.position = pendingPosition.position;
		newPending.transform.SetParent (pendingTricksParent);
		newPending.GetComponent<Text>().text = tr.baseScore+" "+tr.trickCaption;
		PendingTrick pt = newPending.GetComponent<PendingTrick> ();
		pt.StartTransition ();
		float ptheight = RectTransformExtensions.GetHeight (newPending.GetComponent<RectTransform> ());
//		pt.SetDestinationTop (-ptheight*0.3f*pendingIndex);
		AddToPendingHeight (ptheight);
		pendingMultiplier.text = cMultiplier.ToString ()+"x";
		pendingIndex++;
	}

	private void AddToPendingHeight(float h){
		totalPendingHeight += h;
		StopCoroutine ("ScalePendingContainer");
		StartCoroutine ("ScalePendingContainer");
	}

	private IEnumerator ScalePendingContainer(){
		float t = 0f, v = 0f, dur = 0.15f, invDur = 1f / dur;
		float startHeight = RectTransformExtensions.GetHeight (pendingTricksParent);
		float diff = totalPendingHeight-startHeight;
		while (v<1) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(invDur*t, 0, 1);
			RectTransformExtensions.SetHeight(pendingTricksParent, startHeight+v*(totalPendingHeight-startHeight));
			yield return null;
		}
	}

}



//