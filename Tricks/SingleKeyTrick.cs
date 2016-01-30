using UnityEngine;
using System.Collections;

public class SingleKeyTrick : Trick {

	public SingleKeyTrick() : base(){
	}

	protected override void SetName(){
		name = "Single Key";
	}

	public override void UpdateTrickChain(GnoteBar bar, GnoteSequence cSequence, PlayerHistory history){
		Gnote hitNote = bar.LastNote ();
		Gnote previousNote = bar.PreviousNote ();
		if (previousNote == null) {
			// this is the first note, so we cool.
			return;
		}
		string hitString = hitNote.GetKey ();
		string previousString = previousNote.GetKey ();
	
		if (bar.ContainsNote (hitString)) {

		}
	}

	// this is old
	public override TrickResults CalculateResults (GnoteBar bar, GnoteSequence cSequence, PlayerHistory history){
		Gnote gPrev = bar.PreviousNote ();
		Gnote g = bar.LastNote ();
		if (gPrev != null) {
			if (g.GetKey () == gPrev.GetKey ()) {
				return new WasSameKeyResult();
			}
		}
		
		if (g != null) {
			if (g.GetKey () == cSequence.anchorKey) {
				return new WasAnchorKeyResult ();
			}
		}

		KeyScoreValue ksv = KeyData.Instance.GetKeyScoreValue (g.GetKey ());
		if (ksv != null) {
			string trickCaption = ksv.name;
			float trickScore = ksv.points;
			return new TrickResults(trickScore, 0, trickCaption);
		} else {
			return new TrickResults();
		}
	}
}
