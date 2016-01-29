using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompletionWheel : Singleton<CompletionWheel> {

	public GameObject wheelInside;
	public GameObject wheelOutside;
	public Image wheelBacker;

	private float outsideZeroWidth, insideZeroWidth;
	private float cProgress = 0f;
	private float destinationProgress = 0f;

	private float defaultUpdateTime = 0.5f;


	void Start(){
		ForceProgress (0f);
	}

	// screw it.
//	void Update(){
//		if (cProgress - destinationProgress < 0.01 && cProgress - destinationProgress > -0.01) {
//			cProgress = destinationProgress;
//		} else {
//			cProgress = Mathf.Lerp(cProgress, destinationProgress, progressSpeedScalar);
//			UpdateProgress();
//		}
//	}

	private void ForceProgress(float f){
		cProgress = f;
		destinationProgress = f;
		UpdateProgress (0);
	}

	public void SetProgress(float f){
		Debug.Log ("Setting progress to " + f);
		destinationProgress = f;
		UpdateProgress ();
	}

	private void UpdateProgress(){
		UpdateProgress (defaultUpdateTime);
	}

	private void UpdateProgress(float time){
		StopCoroutine ("RunUpdateProgress");
		StartCoroutine ("RunUpdateProgress", time);
	}

	public void SetWheelWidth(float f){

	}

	private IEnumerator RunUpdateProgress(float dur){
		bool forced = (dur == 0);
		float t = 0, v = 0;
		float invDur = forced? float.MaxValue : (1 / dur);
		float startProgress = cProgress;
		float diff = destinationProgress - startProgress;
		while (v<1) {
			t+=Time.deltaTime;
			v = Mathf.Clamp (invDur * t, 0f, 1f);
			v = 1-( (1-v) * (1-v) );
			cProgress = startProgress+diff*v;
			wheelBacker.fillAmount = cProgress;
			if(!forced)
				yield return null;
		}
	}



}
