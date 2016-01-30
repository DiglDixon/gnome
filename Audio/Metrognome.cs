using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Metrognome : Singleton<Metrognome> {

	// this guy holds the timing stuff. Also plays the beat.

	private float loopLength, invLoopLength;

	private int loopDivisions;
	private float invLoopDivisions;

	private float timePerDivision;

	private float cLoopTime = 0;
	private int cDivision = 0;
	private int cBarCount = 0;

	private List<Gnote> contents = new List<Gnote>();

	public GameObject gnoteContainer;
	public GameObject gnotePrefab;
	public Slider timeTracker;

	public BeatBar defaultBeatBar;
	private BeatBar cBeat;

	private bool _isLoopStart;
	public bool isLoopStart{
		set{
			_isLoopStart = value;
		}
		get{
			return _isLoopStart;
		}
	}

	// Use this for initialization
	void Start () {
		SetLoopLength (0.9f);
		SetLoopDivisions (64);
		ConstructGnoteHolders ();
		SetBeat (defaultBeatBar);
		StartBeat ();
	}

	public void SetBeat(BeatBar b){
		cBeat = b;
	}

	public void StartBeat(){
		StopLoop ();
		SetBeat (0);
		StartLoop ();
	}

	public void StopBeat(){
		StopLoop ();
		ResetBeatVariables ();
	}

	private void ResetBeatVariables(){
		cDivision = 0;
		cLoopTime = 0;
		cBarCount = 0;
	}

	private void SetBeat(int index){
		AudioClip[] clips = cBeat.GetBar (index);
		for(int k = 0; k<clips.Length;k++){
			contents[k*4].SetTone(clips[k]);
		}
	}

	private void ConstructGnoteHolders(){
		float containerWidth = RectTransformExtensions.GetWidth (gnoteContainer.GetComponent<RectTransform> ());
		float containerHeight = RectTransformExtensions.GetHeight (gnoteContainer.GetComponent<RectTransform> ());
		float gnoteWidth = containerWidth / loopDivisions;
		for(int k = 0; k<loopDivisions;k++){
			GameObject g = GameObject.Instantiate(gnotePrefab) as GameObject;
			g.transform.SetParent(gnoteContainer.transform);
			RectTransformExtensions.SetWidth(g.GetComponent<RectTransform>(), gnoteWidth);
			RectTransformExtensions.SetHeight(g.GetComponent<RectTransform>(), containerHeight);
			RectTransformExtensions.SetLeftBottomPosition(g.GetComponent<RectTransform>(), new Vector2(gnoteWidth*k, 0));

			contents.Add (new Gnote(" ", k, null));
		}
	}

	public void AddGnote(string key){
		contents [GetDivision ()].SetKey (key);
	}

	public void AddGnote(string key, AudioClip c){
		Gnote g = contents [GetDivision ()]; 
		g.SetKey (key);
		g.SetTone (c);
	}

	private void SetLoopDivisions(int n){
		loopDivisions = n;
	}

	private void SetLoopLength(float l){
		loopLength = l;
		invLoopLength = 1 / loopLength;
	}

	public int GetDivision(){
		return cDivision;
	}

	public int CalcDivision(float t){
		return Mathf.FloorToInt (t * invLoopLength * loopDivisions);
	}

	private void StartLoop(){
		StartCoroutine ("RunLoop");
	}

	private void StopLoop(){
		StopCoroutine ("RunLoop");
	}

	private IEnumerator RunLoop(){
		int pDivision = 0;
		float pLoopTime = 0;
		while (true) {
			pLoopTime = cLoopTime;
			cLoopTime = (cLoopTime + Time.deltaTime) % loopLength;
			if(pLoopTime>cLoopTime){
				isLoopStart = true;
				HandleBarChange();
			}else{
				isLoopStart = false;
			}
			timeTracker.value = cLoopTime * invLoopLength;
			pDivision = cDivision;
			cDivision = CalcDivision(cLoopTime);
			if(cDivision!=pDivision){
				HandleDivisionChange(pDivision, cDivision);
			}
			yield return null;
		}
	}

	private void HandleDivisionChange(int p, int c){
		// some sort of handler to play the sounds we may have skipped over at high speeds and/or low framerates.
		contents [cDivision].CallNote ();
	}

	private void HandleBarChange(){
		cBarCount = (cBarCount + 1) % 4;
		SetBeat (cBarCount);
	}

}
