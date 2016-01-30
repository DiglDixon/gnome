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

	private Trick[] trickList = {
		new SingleKeyTrick()
	};

	public GameObject pendingTrickListing;


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
		Debug.Log ("Diff: " + diff);
		while (v<1) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(invDur*t, 0, 1);
			RectTransformExtensions.SetHeight(pendingTricksParent, startHeight+v*(totalPendingHeight-startHeight));
			yield return null;
		}
	}

}



//