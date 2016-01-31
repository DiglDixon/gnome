using UnityEngine;
using System.Collections;


public class Gnote{
	private string key = "";
	private int division = -1;
	private AudioClip tone;
	private ReinforcedClip rc;
	private bool _matched = false;

	private string _scoreComment = "";
	private float _score = 0;
	public string scoreComment{
		set{
			_scoreComment = value;
		}
		get{
			return _scoreComment;
		}
	}
	public float score{
		set{
			_score = value;
		}
		get{
			return _score;
		}
	}

	public Gnote(string k, int d, AudioClip c){
		SetKey (k);
		SetDivision (d);
		SetTone (c);
	}

	public void SetMatched(bool v){
		_matched = v;
	}

	public bool IsMatched(){
		return _matched;
	}
	
	public void SetDivision(int n){
		this.division = n;
	}
	
	public int GetDivision(){
		return division;
	}

	public void SetTone(AudioClip c){
		tone = c;
	}

	public void SetReinforcedClip(ReinforcedClip r){
		rc = r;
	}

	public void SetKey(string v){
		key = v;
	}
	
	public string GetKey(){
		return key;
	}

	public void CallNote(){
		if (tone != null) {
			GnotePlayer.Instance.RequestOneShot (tone);
		}
	}
	public void CallNoteAsPlayback(){
		if (tone != null) {
			GnotePlayer.Instance.RequestPlaybackGnote (tone);
		}
	}
	
}