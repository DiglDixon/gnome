using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leaderboard : Singleton<Leaderboard> {

	private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

	public void AddEntry(string n, float sc){
		entries.Add(new LeaderboardEntry(n, sc));
	}

}


// Could store the sequences in these too.
public class LeaderboardEntry{
	private string playerName;
	private float score;

	public LeaderboardEntry(string p, float s){
		playerName = p;
		score = s;
	}

}