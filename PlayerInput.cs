using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {


	public KeyboardMap keyboard;
	// Update is called once per frame
	void Update () {
		string input = Input.inputString;
		if (Input.inputString.Length > 0) {
//			keyboard.HandleKeyPress(input);
//			Debug.Log ("inputString: "+input);
		}
	}
	
	
}
