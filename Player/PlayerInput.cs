using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {


	public KeyboardAudioMap keyboard;
	// Update is called once per frame
	void Update () {
		string input = Input.inputString;
		if (Input.inputString.Length > 0) {
//			kv.SetValue(input);
			if(KeyData.Instance.IsValid(input)){
				GameManager.Instance.KeyReceived(input);
				Debug.Log ("inputString: "+input);
			}
		}
	}
	
	
}
