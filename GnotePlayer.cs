using UnityEngine;
using System.Collections;

public class GnotePlayer : Singleton<GnotePlayer> {

	private AudioSource[] outputs;
	private int outputIndex = 0;

	void Start(){
		outputs = GetComponents<AudioSource> ();
	}

	public void RequestOneShot(AudioClip c){
		outputs[outputIndex].clip = c;
		outputs[outputIndex].Play();
		outputIndex = (outputIndex + 1) % outputs.Length;
	}

}
