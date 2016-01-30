using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GnoteSequence {
	
	private List<GnoteBar> gnoteBars = new List<GnoteBar>();
	
	private int totalScore = 0;
	
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
	
}
