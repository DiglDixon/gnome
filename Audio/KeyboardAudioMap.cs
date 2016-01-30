using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardAudioMap : MonoBehaviour {

	private List<string> keysAssigned = new List<string>();
	private List<AudioClip> soundsAssigned = new List<AudioClip>();

	public Metrognome metrognome;

	public AudioClip GetKeySound(string key){
		for (int k = 0; k<keysAssigned.Count; k++){
			if(keysAssigned[k]==key){
				return soundsAssigned[k];
			}
		}
		
		return CreateAssociation(key, SoundBank.Instance.GetRandomSample());
	}

	public void HandleKeyPress(string key){
		PlayKeySound(GetKeySound(key));
	}

	private AudioClip CreateAssociation(string key, AudioClip c){
		Debug.Log ("Created new association: " + key + " :: " + c);
		keysAssigned.Add (key);
		soundsAssigned.Add (c);
//		metrognome.AddGnote (key, c);
		return soundsAssigned [soundsAssigned.Count - 1];
	}

	private void PlayKeySound(AudioClip c){
		GnotePlayer.Instance.RequestOneShot(c);
	}

}
