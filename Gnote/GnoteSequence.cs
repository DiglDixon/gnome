using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GnoteSequence {
	
	private List<GnoteBar> gnoteBars = new List<GnoteBar>();
	
	private float _totalScore = 0;
	public float totalScore{
		set{
			_totalScore = value;
		}
		get{
			return _totalScore;
		}
	}

	public void AddScore(float s){
		totalScore += s;
	}
	
	private string _anchorKey;
	public string anchorKey{
		get {
			return _anchorKey;
		}
		set {
			_anchorKey = value;
		}
	}
	
	public GnoteSequence(string a){
		anchorKey = a;
	}

	public void AddBar(GnoteBar b){
		gnoteBars.Add (b);
	}

	public GnoteBar GetPreviousBar(){
		if (gnoteBars.Count == 0)
			return null;
		return gnoteBars[gnoteBars.Count-1];
	}
	
}
