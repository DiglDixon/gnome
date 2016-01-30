using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GnoteHolder : MonoBehaviour {

	Color activeColour = Color.red;
	Color inactiveColour = Color.white;

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
}
