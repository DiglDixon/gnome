using UnityEngine;
using System.Collections;


public class Gnote{
	private string key = "";
	private int division = -1;
	private AudioClip tone;
	private ReinforcedClip rc;

	public Gnote(string k, int d, AudioClip c){
		SetKey (k);
		SetDivision (d);
		SetTone (c);
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
	
}