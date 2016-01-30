using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyData : Singleton<KeyData> {


	void Start(){
		InitialiseScores ();
	}

	// 12 across, 4 down. 0,0 top left
	private Dictionary<string, Vector2> locations =
	new Dictionary<string, Vector2>() {
		{"1", new Vector2(0f, 0f)},
		{"2", new Vector2(1f, 0f)},
		{"3", new Vector2(2f, 0f)},
		{"4", new Vector2(3f, 0f)},
		{"5", new Vector2(4f, 0f)},
		{"6", new Vector2(5f, 0f)},
		{"7", new Vector2(6f, 0f)},
		{"8", new Vector2(7f, 0f)},
		{"9", new Vector2(8f, 0f)},
		{"0", new Vector2(9f, 0f)},
		{"-", new Vector2(10f, 0f)},
		{"=", new Vector2(11f, 0f)},
		{"q", new Vector2(1.5f, 1f)},
		{"w", new Vector2(2.5f, 1f)},
		{"e", new Vector2(3.5f, 1f)},
		{"r", new Vector2(4.5f, 1f)},
		{"t", new Vector2(5.5f, 1f)},
		{"y", new Vector2(6.5f, 1f)},
		{"u", new Vector2(7.5f, 1f)},
		{"i", new Vector2(8.5f, 1f)},
		{"o", new Vector2(9.5f, 1f)},
		{"p", new Vector2(10.5f, 1f)},
		{"[", new Vector2(11.5f, 1f)},
		{"]", new Vector2(12.5f, 1f)},
		{"a", new Vector2(1.8f, 2f)},
		{"s", new Vector2(2.8f, 2f)},
		{"d", new Vector2(3.8f, 2f)},
		{"f", new Vector2(4.8f, 2f)},
		{"g", new Vector2(5.8f, 2f)},
		{"h", new Vector2(6.8f, 2f)},
		{"j", new Vector2(7.8f, 2f)},
		{"k", new Vector2(8.8f, 2f)},
		{"l", new Vector2(9.8f, 2f)},
		{";", new Vector2(10.8f, 2f)},
		{"'", new Vector2(11.8f, 2f)},
		{"z", new Vector2(2.1f, 3f)},
		{"x", new Vector2(3.1f, 3f)},
		{"c", new Vector2(4.1f, 3f)},
		{"v", new Vector2(5.1f, 3f)},
		{"b", new Vector2(6.1f, 3f)},
		{"n", new Vector2(7.1f, 3f)},
		{"m", new Vector2(8.1f, 3f)},
		{",", new Vector2(9.1f, 3f)},
		{".", new Vector2(10.1f, 3f)},
		{"/", new Vector2(11.1f, 3f)}
	};

	private KeyScoreValue bullseye = new KeyScoreValue(30, "bullseye");
	private KeyScoreValue bullsballs = new KeyScoreValue(7, "bullsballs");
	private KeyScoreValue normal = new KeyScoreValue(10, "key");
	private KeyScoreValue cornerKey = new KeyScoreValue(15, "corner key");
	
	private Dictionary<string, KeyScoreValue> scores;

	public KeyScoreValue GetKeyScoreValue(string s){
		return scores [s];
	}
	
	void InitialiseScores(){
		scores =
		new Dictionary<string, KeyScoreValue>() {
			{"1", cornerKey},
			{"2", normal},
			{"3", normal},
			{"4", normal},
			{"5", normal},
			{"6", normal},
			{"7", normal},
			{"8", normal},
			{"9", normal},
			{"0", normal},
			{"-", normal},
			{"=", normal},
			{"q", normal},
			{"w", normal},
			{"e", normal},
			{"r", normal},
			{"t", bullsballs},
			{"y", bullsballs},
			{"u", normal},
			{"i", normal},
			{"o", normal},
			{"p", normal},
			{"[", normal},
			{"]", normal},
			{"a", normal},
			{"s", normal},
			{"d", normal},
			{"f", bullsballs},
			{"g", bullseye},
			{"h", bullsballs},
			{"j", normal},
			{"k", normal},
			{"l", normal},
			{";", normal},
			{"'", normal},
			{"z", cornerKey},
			{"x", normal},
			{"c", normal},
			{"v", bullsballs},
			{"b", bullsballs},
			{"n", normal},
			{"m", normal},
			{",", normal},
			{".", normal},
			{"/", normal}
		};
	}
}

public class KeyScoreValue{
	
	float points;
	string name;
	
	public KeyScoreValue(float p, string n){
		points = p;
		name = n;
	}
	
}
