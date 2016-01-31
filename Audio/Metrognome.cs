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
	private int cBarLength = 64;
	private float invBarLength = 1/64;

	private int keyHitLeeway = 1;

	private List<Gnote> contents = new List<Gnote>();
	private List<GnoteHolder> holders = new List<GnoteHolder>();

	public GameObject gnoteContainer;
	public GameObject gnotePrefab;
	public Slider timeTracker;

	// This is for lovely dynamic backing tracks that are freaking out.
	public BeatBar defaultBeatBar;
	private BeatBar cBeat;

	// This is for a static backing track;
	private AudioSource trackPlayer;

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
		trackPlayer = GetComponent<AudioSource> ();
		SetLoopLength (4f);
		SetLoopDivisions (64);
		ConstructGnoteHolders ();
//		SetBeat (defaultBeatBar);
		StartBeat ();
		trackPlayer.pitch = trackPlayer.clip.length / loopLength;
		trackPlayer.Play ();
	}

	public bool GnotePositionsAreProximate(int one, int two){
		int dif = one - two;
//		Debug.Log (" one : " + one + " two : " + two+" :: "+(dif <= keyHitLeeway && dif >= -keyHitLeeway));
		return (dif <= keyHitLeeway && dif >= -keyHitLeeway);
	}
	
	public bool GnotesProximate(Gnote one, Gnote two){
		return GnotePositionsAreProximate (one.GetDivision (), two.GetDivision ());
	}

	public bool GnoteIsProximateToCap(Gnote g){
		return GnotePositionsAreProximate (g.GetDivision(), cBarLength);
	}

	public bool GnoteMatches(Gnote n, GnoteBar checkGnoteBar){
		return GetMatch (n, checkGnoteBar) != null;
	}
	
	public Gnote GetMatch(Gnote n, GnoteBar checkGnoteBar){
		List<Gnote> checkGnotes = checkGnoteBar.GetGnotes();
		Gnote g;
		for (int k = 0; k< checkGnotes.Count; k++) {
			g = checkGnotes[k];
			if(g.IsMatched())
				continue;
			if(g.GetKey()==n.GetKey() && GnotesProximate(g, n))
				return g;
		}
		return null;
	}

	public void SetPlaybackBar(GnoteBar gb){
		ClearPlaybackGnotes ();
		List<Gnote> gs = gb.GetGnotes ();
		Gnote g;
		for(int k = 0; k<gs.Count;k++){
			g = gs[k];
			contents[g.GetDivision()] = gs[k];
		}
	}

	public void ClearPlaybackGnotes(){
		for(int k = 0; k<contents.Count;k++){
			contents[k] = null;
		}
	}

	public void SetBeat(BeatBar b){
		cBeat = b;
	}
	
	public void RestartBeat(){
		ResetBeatVariables ();
	}

	public void StartBeat(){
		StopLoop ();
		SetBeat (0);
		StartLoop ();
	}

	public void StopBeat(){
//		trackPlayer.Stop ();
		StopLoop ();
		ResetBeatVariables ();
	}

	private void ResetBeatVariables(){
		cDivision = 0;
		cLoopTime = 0;
		cBarCount = 0;
	}

	private void SetBeat(int index){
		return;
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
			
			holders.Add (g.GetComponent<GnoteHolder>());
			contents.Add (new Gnote(" ", k, null));
		}
	}

	public void SetHolderHit(int index){
		holders[GetDivision()].Hit();
	}

	public void SetHolderActive(){
		holders[GetDivision()].Activate();
	}

	public void SetHolderActive(int index, bool v){
		if (v) {
			holders[index].Activate();
		} else{
			holders[index].Deactivate();
		}
	}

	public void RefreshGnoteHolders(){
		GnoteHolder gh;
		for (int k = 0; k<holders.Count; k++) {
			gh = holders[k];
			if(k>cBarLength){
				gh.Deactivate();
			}else{
				gh.SetOob(true);
			}
		}
	}

	public void SetGnoteHolders(GnoteBar b){
		foreach(Gnote g in b.GetGnotes()){
			SetHolderActive(g.GetDivision(), true);
		}
	}

	public void AddGnote(string key){
		contents [GetDivision ()].SetKey (key);
		Debug.Log ("Added a Gnote");
	}

	public void AddGnote(string key, AudioClip c){
		Debug.Log ("Added a Gnote");
		Gnote g = contents [GetDivision ()]; 
		g.SetKey (key);
		g.SetTone (c);
	}

	private void SetLoopDivisions(int n){
		loopDivisions = n;
		SetBarLength (loopDivisions);
	}

	public void SetBarLength(){
		SetBarLength(GetDivision ());
	}

	public void SetBarLength(int n){
		Debug.Log ("Set bar length: " + n);
		cBarLength = n;
		invBarLength = 1f / (float)cBarLength;
	}

	public int GetBarLength(){
		return cBarLength;
	}

	private void SetLoopLength(float l){
		loopLength = l;
		invLoopLength = 1 / loopLength;
	}

	public int GetDivision(){
		return cDivision;
	}
	
	public int GetDivisionsLeft(){
		return contents.Count - cDivision;
	}

	public int CalcDivision(float t){
		return Mathf.FloorToInt (t * invLoopLength * loopDivisions);
	}

	private void StartLoop(){
		Debug.Log ("Loop started!");
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
				GameManager.Instance.LoopStarted();
				ScoreKeeper.Instance.LoopStarted();
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
			Displays.Instance.UpdateBarTimer((float)invBarLength * (float)GetDivision());
			yield return null;
		}
	}

	public float GetLoopRemainingTime(){
		return loopLength - cLoopTime;
	}

	private void HandleDivisionChange(int p, int c){
		// some sort of handler to play the sounds we may have skipped over at high speeds and/or low framerates.
		Gnote g = contents [cDivision];
		if (g != null) {
			g.CallNoteAsPlayback ();
		}
	}

	private void HandleBarChange(){
//		cBarCount = (cBarCount + 1) % 4;
//		SetBeat (cBarCount);
	}

}
