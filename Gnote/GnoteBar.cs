using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GnoteBar {
	
	private List<Gnote> gnotes = new List<Gnote>();
	private float _score = 0;
	public float score{
		set{
			_score = value;
		}
		get{
			return _score;
		}
	}

	public GnoteBar(){
	}

	public void AddGnote(Gnote g){
		gnotes.Add (g);
	}

	public Gnote LastNote(){
		if(gnotes.Count==0)
			return null;
		return gnotes[gnotes.Count-1];
	}
	
	
	public Gnote PreviousNote(){
		if(gnotes.Count<=1)
			return null;
		return gnotes[gnotes.Count-2];
	}

}

