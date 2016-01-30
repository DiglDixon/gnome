using UnityEngine;
using System.Collections;

public class Trick {

	protected string _name;
	public string name{
		get{
			return _name;
		}
		set{
			_name = value;
		}
	}

	public Trick(){
		SetName ();
	}

	protected virtual void SetName(){
		// overwrite here
	}

	public virtual TrickResults CalculateResults (GnoteBar bar, GnoteSequence cSequence, PlayerHistory history){
		// overwrite me
		return new TrickResults();
	}
}

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
