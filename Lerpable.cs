using UnityEngine;
using System.Collections;

public class Lerpable : MonoBehaviour {
	
	private GameObject go;
	public GameObject customGameObject;
	
	protected virtual void Start(){
		SetGameObject ();
	}

	protected virtual void SetGameObject(){
		if (customGameObject != null) {
			go = customGameObject;
		} else {
			go = gameObject;
		}
	}
	
	public void StopAllLerps(){
		StopLerpPosition ();
		StopLerpRotation ();
		StopSlerpRotation ();
		// bleurgh, the rest.
	}

	public void LerpCustom(float dur){
		StopCoroutine ("LerpCustomTo");
		StartCoroutine ("LerpCustomTo", new LerpData(dur));
	}

	public void StopLerpCustom(){
		StopCoroutine ("LerpCustomTo");
	}
	
	public void LerpLocalPosition(Vector3 pos, float dur){
		StopCoroutine ("LerpLocalPositionTo");
		StartCoroutine("LerpLocalPositionTo", new LerpData(pos, dur));
	}
	
	public void StopLerpLocalPosition(){
		StopCoroutine ("LerpLocalPositionTo");
	}
	
	public void LerpPosition(Vector3 pos, float dur){
		StopCoroutine ("LerpPositionTo");
		StartCoroutine("LerpPositionTo", new LerpData(pos, dur));
	}
	
	public void StopLerpPosition(){
		StopCoroutine ("LerpPositionTo");
	}

	public void LerpLocalRotation(Vector3 eul, float dur){
		StopCoroutine ("LerpLocalRotationTo");
		StartCoroutine("LerpLocalRotationTo", new LerpData(eul, dur));
	}
	
	public void StopLerpLocalRotation(){
		StopCoroutine ("LerpLocalRotationTo");
	}
	
	public void LerpRotation(Vector3 eul, float dur){
		StopCoroutine ("LerpRotationTo");
		StartCoroutine("LerpRotationTo", new LerpData(eul, dur));
	}
	
	public void StopLerpRotation(){
		StopCoroutine ("LerpRotationTo");
	}
	
	public void SlerpLocalRotation(Vector3 eul, float dur){
		StopCoroutine ("SlerpRotationTo");
		StartCoroutine("SlerpRotationTo", new LerpData(eul, dur));
	}
	
	public void StopSlerpLocalRotation(){
		StopCoroutine ("SlerpLocalRotationTo");
	}
	
	public void SlerpRotation(Vector3 eul, float dur){
		StopCoroutine ("SlerpRotationTo");
		StartCoroutine("SlerpRotationTo", new LerpData(eul, dur));
	}
	
	public void StopSlerpRotation(){
		StopCoroutine ("SlerpRotationTo");
	}
	
	public void LerpLocalScale(Vector3 val, float dur){
		StopCoroutine ("LerpLocalScaleTo");
		StartCoroutine("LerpLocalScaleTo", new LerpData(val, dur));
	}
	
	public void StopLerpLocalScale(){
		StopCoroutine ("LerpLocalScaleTo");
	}

	private IEnumerator LerpCustomTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			LerpCustomStep(v);
			yield return null;
		}
		LerpCustomComplete ();
	}
	
	private IEnumerator LerpLocalScaleTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Vector3 start = go.transform.localScale;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			LerpLocalScaleStep(v);
			go.transform.localScale = Vector3.Lerp(start, data.vectorValue, v);
			yield return null;
		}
		LerpLocalScaleComplete ();
	}
	
	private IEnumerator LerpLocalPositionTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Vector3 start = go.transform.localPosition;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			LerpLocalPositionStep(v);
			go.transform.localPosition = Vector3.Lerp(start, data.vectorValue, v);
			yield return null;
		}
		LerpLocalPositionComplete ();
	}
	
	private IEnumerator LerpPositionTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Vector3 start = go.transform.position;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			LerpPositionStep(v);
			go.transform.position = Vector3.Lerp(start, data.vectorValue, v);
			yield return null;
		}
		LerpPositionComplete ();
	}
	
	private IEnumerator LerpRotationTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Quaternion start = go.transform.rotation;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			LerpRotationStep(v);
			go.transform.rotation = Quaternion.Lerp(start, Quaternion.Euler(data.vectorValue), v);
			yield return null;
		}
		LerpRotationComplete ();
	}
	
	private IEnumerator SlerpRotationTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Quaternion start = go.transform.rotation;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			SlerpRotationStep(v);
			go.transform.rotation = Quaternion.Slerp(start, Quaternion.Euler(data.vectorValue), v);
			yield return null;
		}
		SlerpRotationComplete ();
	}
	
	private IEnumerator SlerpLocalRotationTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Quaternion start = go.transform.localRotation;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			SlerpLocalRotationStep(v);
			go.transform.localRotation = Quaternion.Slerp(start, Quaternion.Euler(data.vectorValue), v);
			yield return null;
		}
		SlerpLocalRotationComplete ();
	}
	
	private IEnumerator LerpLocalRotationTo(LerpData data){
		float ivDuration = 1f / data.duration;
		float t = 0, v = 0;
		Quaternion start = go.transform.localRotation;
		while (t<data.duration) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(t*ivDuration, 0, 1);
			LerpLocalRotationStep(v);
			go.transform.localRotation = Quaternion.Lerp(start, Quaternion.Euler(data.vectorValue), v);
			yield return null;
		}
		LerpLocalRotationComplete ();
	}

	// COMPLETES
	
	protected virtual void LerpCustomComplete(){
		// Override me.
	}
	
	protected virtual void LerpPositionComplete(){
		// Override me.
	}
	
	protected virtual void LerpLocalPositionComplete(){
		// Override me.
	}
	
	protected virtual void LerpRotationComplete(){
		// Override me.
	}
	
	protected virtual void SlerpRotationComplete(){
		// Override me.
	}
	
	protected virtual void SlerpLocalRotationComplete(){
		// Override me.
	}
	
	protected virtual void LerpLocalRotationComplete(){
		// Override me.
	}
	
	protected virtual void LerpLocalScaleComplete(){
		// Override me.
	}
	
	// STEPS

	protected virtual void LerpCustomStep(float v){
		// Override me.
	}
	
	protected virtual void LerpPositionStep(float v){
		// Override me.
	}
	
	protected virtual void LerpLocalPositionStep(float v){
		// Override me.
	}
	
	protected virtual void LerpRotationStep(float v){
		// Override me.
	}
	
	protected virtual void SlerpRotationStep(float v){
		// Override me.
	}
	
	protected virtual void SlerpLocalRotationStep(float v){
		// Override me.
	}
	
	protected virtual void LerpLocalRotationStep(float v){
		// Override me.
	}
	
	protected virtual void LerpLocalScaleStep(float v){
		// Override me.
	}
	
	
}


public class LerpData{
	public Vector3 vectorValue;
	public float duration;
	public LerpData(){}
	public LerpData(float d){
		duration = d;
	}
	public LerpData(Vector3 vec, float d){
		vectorValue = vec;
		duration = d;
	}
}