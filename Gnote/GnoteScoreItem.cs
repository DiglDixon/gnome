using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GnoteScoreItem : Lerpable {
	
	private float startScale = 0.4f;
	private float peakScale = 1f;
	private float restScale = 0.1f;
	private Vector3 peakPosition;
	private Vector3 restPosition;
	
	private float peakTime = 0.15f;
	private float restTime = 0.3f;
	
	private Vector3 dest;
	
	private Transform fParent;
	
	private RectTransform rt;

	private Text myText;
	
	public void StartTransition(string t, Color c, int fontSize){
		SetGameObject ();
		myText = GetComponent<Text> ();
		restPosition = transform.position;
		peakPosition = transform.position + new Vector3 (Random.Range(-10f, 10f), Random.Range (-20f, 10f), 0f);
		myText.text = t;
		myText.color = c;
		myText.fontSize = fontSize;
		StartCoroutine (ScaleToPeak());
		rt = GetComponent<RectTransform> ();
	}
	
	public void SetFutureParent(Transform t){
//		fParent = t;
	}
	
	public void SetDestination(Vector3 d){
		dest = d;
	}
	
	public void SetDestinationTop(float f){
		dest = new Vector2 (transform.localPosition.x, f);
	}
	
	private IEnumerator ScaleToPeak(){
		float t = 0, v = 0, dur = peakTime, invDur = 1/dur;
		LerpPosition(peakPosition, dur);
		Vector3 sc = new Vector3 (startScale, startScale, startScale);
		transform.localScale = sc;
		float diff = peakScale - startScale;
		while (v<1) {
			t+=Time.deltaTime;
			v = Mathf.Clamp (t*invDur, 0f, 1f);
			v = v*v;
			sc.x = sc.y = sc.z = startScale+diff*v;
			transform.localScale = sc;
			yield return null;
		}
		StartCoroutine(ScaleToRest());
	}
	
	private IEnumerator ScaleToRest(){
		float t = 0, v = 0, dur = restTime, invDur = 1/dur;
		LerpPosition(restPosition, dur);
		Vector3 sc = new Vector3 (peakScale, peakScale, peakScale);
		float diff = restScale - peakScale;
		Vector2 startPosition = transform.localPosition;
		while (v<1) {
			t+=Time.deltaTime;
			v = Mathf.Clamp (t*invDur, 0f, 1f);
			v = v*v;
			sc.x = sc.y = sc.z = peakScale+diff*v;
			//			RectTransformExtensions.SetLeftTopPosition(rt, Vector2.Lerp (startPosition, dest, v));
			transform.localScale = sc;
			yield return null;
		}
		gameObject.SetActive (false);
		//		transform.SetParent (fParent);
		//		Debug.Log ("Resting at: " + rt.rect.y);
	}
}
