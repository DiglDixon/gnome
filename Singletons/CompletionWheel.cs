using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompletionWheel : Singleton<CompletionWheel> {

	public GameObject wheelInside;
	public GameObject wheelOutside;
	public Image wheelBacker;

	private float outsideZeroWidth = 0.9217157f, insideZeroWidth = 1.05f;
	private float outsideMaxWidth = 1.2648f, insideMaxWidth = 0.63165f;
	private float cProgress = 0f;
	private float destinationProgress = 0f;

	private float defaultUpdateTime = 0.5f;


	void Start(){
//		ForceProgress (0f);
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

	public void ForceProgress(float f){
		cProgress = f;
		destinationProgress = f;
//		wheelBacker.fillAmount = f;
		UpdateProgress (0f);
	}

	public void SetProgress(float f){
//		Debug.Log ("Setting progress to " + f);
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
		float innerScale = Remap (f, 0f, 1f, insideZeroWidth, insideMaxWidth);
		float outerScale = Remap (f, 0f, 1f, outsideZeroWidth, outsideMaxWidth);
		wheelInside.transform.localScale = Vector3.Lerp (wheelInside.transform.localScale, new Vector3 (innerScale, innerScale, innerScale), 0.1f);
		wheelOutside.transform.localScale = Vector3.Lerp (wheelOutside.transform.localScale, new Vector3 (outerScale, outerScale, outerScale), 0.1f);
	}

	public void SetWheelScale(float f){
		transform.localScale = new Vector3 (f, f, f);
	}

	private IEnumerator RunUpdateProgress(float dur){
		bool forced = (dur == 0f);
		float t = 0f, v = 0f;
		float invDur = forced? float.MaxValue : (1f / dur);
		float startProgress = cProgress;
		float diff = destinationProgress - startProgress;
		while (t<dur) {
			t+=Time.deltaTime;
			v = Mathf.Clamp (invDur * t, 0f, 1f);
			v = 1f-( (1f-v) * (1f-v) );
			cProgress = startProgress+diff*v;
			wheelBacker.fillAmount = cProgress;
			if(!forced)
				yield return null;
		}
	}

	float Remap(float value, float low1, float high1, float low2, float high2) {
		return low2 + (high2 - low2) * (value - low1) / (high1 - low1);
	}


}
