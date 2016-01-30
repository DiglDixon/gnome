using UnityEngine;
using System.Collections;

public class ReinforcedClip : MonoBehaviour {

	public AudioClip[] clips = new AudioClip[2];
	private int index;

	public AudioClip GetClip(){
		index = (index + 1) % clips.Length;
		return clips [index];
	}

}
