using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GnoteHolder : MonoBehaviour {

	Color activeColour = Color.red;
	Color hitColour = Color.green;
	Color inactiveColour = Color.white;
	Color oobColor = Color.black;

	private bool oob = false;

	private Image myImage;

	void Start(){
		myImage = GetComponent<Image> ();
	}

	public void Activate(){
		myImage = GetComponent<Image> ();
		myImage.color = activeColour;
	}

	public void Deactivate(){
		myImage.color = inactiveColour;
	}

	public void Reset(){
		SetOob (oob);
	}
	
	public void Hit(){
		myImage.color = hitColour;
	}

	public void SetOob(bool v){
		oob = v;
		myImage.color = v ? oobColor : inactiveColour;
	}
}
