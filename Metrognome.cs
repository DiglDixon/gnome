using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Metrognome : MonoBehaviour {

	private float loopLength, invLoopLength;

	private int loopDivisions;
	private float invLoopDivisions;

	private float timePerDivision;

	private float cLoopTime = 0;
	private int cDivision = 0;

	private List<Gnote> contents = new List<Gnote>();

	public GameObject gnoteContainer;
	public GameObject gnotePrefab;
	public Slider timeTracker;

	// Use this for initialization
	void Start () {
		SetLoopLength (1.5f);
		SetLoopDivisions (32);
		StartCoroutine ("RunLoop");
		ConstructGnotes ();
	}

	private void ConstructGnotes(){
		float containerWidth = RectTransformExtensions.GetWidth (gnoteContainer.GetComponent<RectTransform> ());
		float containerHeight = RectTransformExtensions.GetHeight (gnoteContainer.GetComponent<RectTransform> ());
		float gnoteWidth = containerWidth / loopDivisions;
		for(int k = 0; k<loopDivisions;k++){
			GameObject g = GameObject.Instantiate(gnotePrefab) as GameObject;
			g.transform.SetParent(gnoteContainer.transform);
			RectTransformExtensions.SetWidth(g.GetComponent<RectTransform>(), gnoteWidth);
			RectTransformExtensions.SetHeight(g.GetComponent<RectTransform>(), containerHeight);
			RectTransformExtensions.SetLeftBottomPosition(g.GetComponent<RectTransform>(), new Vector2(gnoteWidth*k, 0));
			contents.Add (g.GetComponent<Gnote>());
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

	private IEnumerator RunLoop(){
		int pDivision = 0;
		while (true) {
			cLoopTime = (cLoopTime + Time.deltaTime) % loopLength;
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

}
