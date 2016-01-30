using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Displays : Singleton<Displays> {

//	public GameObject 
	// all our display stuff goes in here.

	public Text uniqueKeysText;
	public Text keyCountText;
	public Text keyDistanceText;

	public void SetUniqueKeys(int n){
		uniqueKeysText.text = n.ToString ();
	}
	
	public void SetKeyDistance(float n){
		keyDistanceText.text = n.ToString ();
	}
	
	public void SetKeyCount(int n){
		keyCountText.text = n.ToString ();
	}

}
