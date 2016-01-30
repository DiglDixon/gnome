﻿using UnityEngine;
using System.Collections;


public class Gnote{
	private string key = "";
	private int division = -1;
	private AudioClip tone;

	public Gnote(string k, int d, AudioClip c){
		SetTone (c);
		SetKey (k);
		SetDivision (d);
	}
	
	public void SetDivision(int n){
		this.division = n;
	}

	public void SetTone(AudioClip c){
		tone = c;
	}

	public void SetKey(string v){
		key = v;
	}
	
	public string GetKey(){
		return key;
	}

	public void CallNote(){
		if (tone != null) {
			GnotePlayer.Instance.RequestOneShot (tone);
		}
	}
	
}