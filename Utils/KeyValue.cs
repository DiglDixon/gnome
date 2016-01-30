using UnityEngine;
using System.Collections;

public class KeyValue {

	private string value;
	private KeyCode keyCode;

	public KeyValue(string v, KeyCode k){
		value = v;
		keyCode = k;
	}

	
	public void SetValue(string s){
		value = s;
	}
	
	public void SetKeyCode(KeyCode k){
		keyCode = k;
	}

	public string GetValue(){
		return value;
	}

	public KeyCode GetKeyCode(){
		return keyCode;
	}

}
