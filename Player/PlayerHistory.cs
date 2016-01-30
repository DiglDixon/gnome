using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHistory {

	public PlayerHistory(){
	}

	public List<GnoteSequence> sequences = new List<GnoteSequence>();

	public void AddSequence(GnoteSequence s){
		sequences.Add (s);
	}

}
