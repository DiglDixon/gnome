using UnityEngine;
using System.Collections;


public class Gnote : MonoBehaviour{
	public string key = "";
	public AudioClip tone;

	public void SetTone(AudioClip c){
		tone = c;
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