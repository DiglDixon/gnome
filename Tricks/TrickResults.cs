using UnityEngine;
using System.Collections;

public class TrickResults{
	public float multiplier;
	public float baseScore;
	public string trickCaption;
	public TrickResults(float score, float multi, string caption){
		baseScore = score;
		multiplier = multi;
		trickCaption = caption;
	}
	public TrickResults(){
		// this is our null constructor
		multiplier = 0;
		baseScore = 0;
		trickCaption = "null";
	}
}

public class WasAnchorKeyResult : TrickResults{
	public WasAnchorKeyResult() : base(0, 0, "DERP ANCHOR KEY") {
	}
}

public class WasSameKeyResult : TrickResults{
	public WasSameKeyResult() : base(0, 0, "DERPPP SAME KEY") {
	}
}
