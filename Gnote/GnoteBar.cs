using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GnoteBar {
	
	private List<Gnote> gnotes = new List<Gnote>();
	public List<Gnote> GetGnotes(){
		return gnotes;
	}
	private List<string> strings = new List<string>();
	private int _uniqueKeyCount = 0;
	private int _keyCount = 0;
	private float _keyDistances = 0;

	private int keyHitLeeway = 2;

	private Texture myText;
	
	private int uniqueKeyCount{
		set{_uniqueKeyCount = value;
			Displays.Instance.SetUniqueKeys(_uniqueKeyCount);
		}get{return _uniqueKeyCount;}}
	private int keyCount{
		set{_keyCount = value;
			Displays.Instance.SetKeyCount(_keyCount);
		}get{return _keyCount;}}
	private float keyDistances{
		set{_keyDistances = value;
			Displays.Instance.SetKeyDistance(_keyDistances);
		}get{return _keyDistances;}}





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
		keyCount = 0;
		keyDistances = 0f;
		uniqueKeyCount = 0;
	}

	public void AddGnote(Gnote g){
		gnotes.Add (g);
		strings.Add (g.GetKey ());
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

	public bool ContainsNote(string s){
		return strings.Contains (s);
	}

	public void AnalyseNote(Gnote cGnote, GnoteBar cBar, GnoteBar pBar){
		Gnote matchGnote = Metrognome.Instance.GetMatch (cGnote, pBar);
		if (matchGnote==null) {
			// No match
			// some sort of sound if they were on a streak.
		} else {
			// Match!
			matchGnote.SetMatched(true);
			KeyScoreValue ksv = KeyData.Instance.GetKeyScoreValue(cGnote.GetKey());
			string comment = "+"+ksv.points+"\n"+ksv.name;
			keyCount++;
			if(gnotes.Contains(cGnote)){
			}else{
				uniqueKeyCount++;
				cGnote.scoreComment += "\nUNIQUE";
			}
			cGnote.scoreComment = comment;
			cGnote.score = ksv.points;
			keyDistances += DistanceBetweenKeys (cGnote.GetKey(), LastNote().GetKey ());
			Metrognome.Instance.SetHolderHit (cGnote.GetDivision ());
			Displays.Instance.GnoteMatch (cGnote);
		}
	}

	private float DistanceBetweenKeys(string one, string two){
		Vector2 oneValue = KeyData.Instance.GetLocation (one);
		Vector2 twoValue = KeyData.Instance.GetLocation (two);
		return Vector2.Distance(oneValue, twoValue);
	}

}

