using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Displays : Singleton<Displays> {

//	public GameObject 
	// all our display stuff goes in here.

	public Text uniqueKeysText;
	public Text keyCountText;
	public Text keyDistanceText;
	public Text BarHitText;
	public Text BarMissedText;
	public Text AnchorText;

	public void SetUniqueKeys(int n){
		uniqueKeysText.text = n.ToString ();
	}
	
	public void SetKeyDistance(float n){
		keyDistanceText.text = n.ToString ();
	}
	
	public void SetKeyCount(int n){
		keyCountText.text = n.ToString ();
	}

	public void SetBarHit(bool v){
		BarHitText.gameObject.SetActive (v);
		BarMissedText.gameObject.SetActive (!v);
	}

	public void SetAnchorText(string a){
		AnchorText.text = a;
	}

}
