using UnityEngine;
using System.Collections;

public class GnotePlayer : Singleton<GnotePlayer> {

	private AudioSource[] outputs;
	private int outputIndex = 0;

	void Start(){
		outputs = GetComponents<AudioSource> ();
	}

	public void RequestOneShot(AudioClip c){
//		outputs [0].PlayOneShot (c);
		GetCurrentSource ().volume = 1f;
		PlayClip (c);
	}

	public void RequestPlaybackGnote(AudioClip c){
		GetCurrentSource ().volume = 0.3f;
		PlayClip (c);
	}

	private void PlayClip(AudioClip c){
		AudioSource a = GetCurrentSource ();
		a.Stop();
		a.clip = c;
		a.Play();
		outputIndex = (outputIndex + 1) % outputs.Length;
	}

	private AudioSource GetCurrentSource(){
		return outputs[outputIndex];
	}

}
