using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardAudioMap : MonoBehaviour {

	private List<string> keysAssigned = new List<string>();
	private List<AudioClip> soundsAssigned = new List<AudioClip>();

	public Metrognome metrognome;

	public AudioClip GetKeySound(string key){
		int keyIndex = -1;
		for (int k = 0; k<keysAssigned.Count; k++){
			if(keysAssigned[k]==key){
				keyIndex = k;
			}
		}
		if (keyIndex==-1) {
			CreateAssociation(key, SoundBank.Instance.GetRandomSample());
		}
		
		return soundsAssigned[keyIndex];
	}

	public void HandleKeyPress(string key){
		PlayKeySound(GetKeySound(key));
	}

	private void CreateAssociation(string key, AudioClip c){
		Debug.Log ("Created new association: " + key + " :: " + c);
		keysAssigned.Add (key);
		soundsAssigned.Add (c);
		metrognome.AddGnote (key, c);
	}

	private void PlayKeySound(AudioClip c){
		GnotePlayer.Instance.RequestOneShot(c);
	}

}
