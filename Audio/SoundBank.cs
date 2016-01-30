using UnityEngine;
using System.Collections;

public class SoundBank : Singleton<SoundBank> {

	public AudioClip[] bassSamplesSmall;
	public AudioClip[] bassSamplesMedium;
	public AudioClip[] bassSamplesLarge;
	public AudioClip[] hipHopSamples;

	public AudioClip GetRandomSample(){
		return GetRandomSample (hipHopSamples);
//		return GetBassSample (0, Random.Range (0, 7));
	}

	public AudioClip GetBassSample(int length, int tone){
		tone = Mathf.Clamp (tone, 0, 7);
		switch (length) {
		case 0:
			return bassSamplesSmall[tone];
		case 1:
			return bassSamplesMedium[tone];
		case 2:
			return bassSamplesLarge[tone];
		}
		Debug.LogWarning("Couldn't switch bass length: "+length);
		return null;
	}

	private AudioClip GetRandomSample(AudioClip[] ar){
		return ar[Random.Range(0, ar.Length)];
	}
}
