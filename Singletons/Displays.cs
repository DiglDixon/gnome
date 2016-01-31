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
	public Text anchorText;
	public Text anchorTextDown;
	public GameObject anchorKeyObject;
	public GameObject anchorKeyDownObject;

	private AudioSource sounds;
	public AudioClip gnoteMatchClip;
	public AudioClip barCappedClip;

	public GameObject[] playerOneScores;
	public GameObject[] playerTwoScores;

	public GameObject playerOneTitle;
	public GameObject playerTwoTitle;
	public GameObject playerOneTitleDestination;
	public GameObject playerTwoTitleDestination;
	public int playerOneStartFontSize = 25;
	public int playerTwoStartFontSize = 25;
	public int playerOneDestinationFontSize = 50;
	public int playerTwoDestinationFontSize = 50;

	public GameObject playerOneCountdown;
	public Text playerOneCountdownText;
	public GameObject playerTwoCountdown;
	public Text playerTwoCountdownText;

	public Text currentBarScoreText;

	public Lerpable centreTwo;
	private float centreInactiveScale = 0.9f;
	public GameObject centreBlock;

	public GnoteScoreItem[] gnoteScores;
	private int gnoteScoreIndex = 0;

	public Color playerOneColour;
	public Color playerTwoColour;

	void Start(){
		sounds = GetComponent<AudioSource> ();
	}

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
		anchorText.text = a;
		anchorTextDown.text = a;
	}

	public void UpdatePlayerCountdown(bool playerOne, float amount){
		if (playerOne) {
			if(!playerOneCountdown.activeSelf)
				playerOneCountdown.SetActive(true);
			playerOneCountdownText.text = (Mathf.RoundToInt(amount*10)/10f)+"";
		} else {
			if(!playerTwoCountdown.activeSelf)
				playerTwoCountdown.SetActive(true);
			playerTwoCountdownText.text = (Mathf.RoundToInt(amount*10)/10f)+"";
		}
	}

	public void PlayerCountdownComplete(){
		playerOneCountdown.SetActive(false);
		playerTwoCountdown.SetActive(false);
	}

	public void AnchorKeyPressed(){
		anchorKeyObject.SetActive (false);
		anchorKeyDownObject.SetActive (true);
		StopCoroutine ("UnpressAnchorKey");
		StartCoroutine ("UnpressAnchorKey");
	}

	private IEnumerator UnpressAnchorKey(){
		yield return new WaitForSeconds(0.2f);
		anchorKeyObject.SetActive (true);
		anchorKeyDownObject.SetActive (false);
	}

	private float currentBarScore = 0f;
	public void GnoteMatch(Gnote g){
		sounds.PlayOneShot (gnoteMatchClip);
		gnoteScores [gnoteScoreIndex].gameObject.SetActive (true);
		gnoteScores [gnoteScoreIndex].StartTransition (g.scoreComment, (GameManager.Instance.PlayerOneUp () ? playerOneColour : playerTwoColour), 50);
		currentBarScore += g.score;
		gnoteScoreIndex = (gnoteScoreIndex + 1) % gnoteScores.Length;
		currentBarScoreText.text = currentBarScore > 0? currentBarScore+"" : " ";
	}

	public void BarCapped(){
		sounds.PlayOneShot (barCappedClip);
	}

	public void UpdateBarTimer(float v){
		CompletionWheel.Instance.SetProgress (v+0.1f);
	}

	public void BarComplete(){
		currentBarScore = 0f;
		CompletionWheel.Instance.ForceProgress (0.01f);
	}

	public void UpdateBarScore(float f){
		CompletionWheel.Instance.SetWheelWidth (f);
	}

	public void SetPlayerTurn(){

	}

	public void EnablePlayerOneTapIn(){

	}
	
	public void EnablePlayerTwoTapIn(){
		
	}

	public void PlayerOneTapsIn(){

	}

	public void PlayerTwoTapsIn(){

	}

	public void SequenceEnded(){
		centreTwo.LerpLocalScale (new Vector3 (centreInactiveScale, centreInactiveScale, centreInactiveScale), 0.1f);
		centreBlock.SetActive (true);
	}

	private void AnyPlayerBegan(){
		centreTwo.LerpLocalScale (new Vector3 (1f, 1f, 1f), 0.1f);
		centreBlock.SetActive (false);
	}

	public void BeginPlayerOne(){
		// timers
		StartCoroutine("LerpObjectTo", new CLerpData(playerOneTitle, playerOneTitleDestination.transform, 0.15f));
		StartCoroutine("LerpFontSizeTo", new CLerpData(playerOneTitle, playerOneDestinationFontSize, 0.15f));
		AnyPlayerBegan ();
	}
	
	public void BeginPlayerTwo(){
		// timers
		//		lerp into centre
		StartCoroutine("LerpObjectTo", new CLerpData(playerTwoTitle, playerTwoTitleDestination.transform, 0.15f));
		StartCoroutine("LerpFontSizeTo", new CLerpData(playerTwoTitle, playerTwoDestinationFontSize, 0.15f));
		AnyPlayerBegan ();
	}

	private IEnumerator LerpObjectTo(CLerpData ld){
		GameObject go = ld.go;
		Transform to = ld.to;
		float dur = ld.dur;
		float t = 0f, v = 0f, invDur = 1f/dur;
		Vector3 startPosition = go.transform.position;
		Vector3 difference = to.position - startPosition;
		while (t<dur) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(invDur * t, 0f, 1f);
			v = v * v;
			go.transform.position = startPosition + (difference * v);
			yield return null;
		}
	}

	
	
	private IEnumerator LerpFontSizeTo(CLerpData ld){
		Text title = ld.go.GetComponent<Text>();
		int fontSize = ld.fontSize;
		float t = 0f, v = 0f, invDur = 1f/ld.dur;
		int startSize = title.fontSize;
		int destSize = fontSize;
		int diff = destSize - startSize;
		while (t<ld.dur) {
			t+=Time.deltaTime;
			v = Mathf.Clamp(invDur * t, 0f, 1f);
			v = v * v;
			title.fontSize = Mathf.RoundToInt ((float)startSize + (float)diff * v);
			yield return null;
		}
	}

}

public class CLerpData{
	public GameObject go;
	public Transform to;
	public float dur;
	public int fontSize;
	public CLerpData(GameObject g, Transform t, float d){
		go = g;
		to = t;
		dur = d;
	}
	public CLerpData(GameObject g, int fs, float d){
		go = g;
		fontSize = fs;
		dur = d;
	}
	}
