using UnityEngine;
using System.Collections;

public class BeatBar : MonoBehaviour {

	public AudioClip[] barOne = new AudioClip[16];
	public AudioClip[] barTwo = new AudioClip[16];
	public AudioClip[] barThree = new AudioClip[16];
	public AudioClip[] barFour = new AudioClip[16];

	void Start(){
		AssociateNullArray (barTwo, barOne);
		AssociateNullArray (barThree, barOne);
		AssociateNullArray (barFour, barOne);
	}

	private void AssociateNullArray(AudioClip[] checkArray, AudioClip[] defaultArray){
		for(int k = 0; k<checkArray.Length;k++){
			if(checkArray[k]!=null)
				return;
		}
		
		for(int k = 0; k<checkArray.Length;k++){
			checkArray[k] = defaultArray[k];
		}
//		checkArray = defaultArray;
	}

	public AudioClip[] GetBar(int b){
		switch (b) {
		case 0:
			return barOne;
		case 1:
			return barTwo;
		case 2:
			return barThree;
		case 3:
			return barFour;
		}
		Debug.LogError ("Could't get bar at index: "+b);
		return null;
	}

}
