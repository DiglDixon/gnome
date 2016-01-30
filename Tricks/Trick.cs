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

	public virtual void UpdateTrickChain(GnoteBar bar, GnoteSequence cSequence, PlayerHistory history){
		// over
	}
}
