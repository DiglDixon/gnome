using UnityEngine;
using System.Collections;

public class SingleKeyTrick : Trick {

	public SingleKeyTrick() : base(){
	}

	protected override void SetName(){
		name = "Single Key";
	}
	
	public override TrickResults CalculateResults (GnoteBar bar, GnoteSequence cSequence, PlayerHistory history){
		Gnote g = bar.LastNote ();
		KeyScoreValue ksv = KeyData.Instance.GetKeyScoreValue (g.GetKey ());
		string trickCaption = ksv.name;
		float trickScore = ksv.points;
		return new TrickResults(trickScore, 0, trickCaption);
	}
}
