using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GnoteBar {
	
	private List<Gnote> gnotes = new List<Gnote>();
	private float score = 0;

	public GnoteBar(){
	}

	public void AddGnote(Gnote g){
		gnotes.Add (g);
	}

	public void SetScore(float s){
		score = s;
	}

	public Gnote LastNote(){
		if(gnotes.Count==0)
			return null;
		return gnotes[gnotes.Count-1];
	}

}

