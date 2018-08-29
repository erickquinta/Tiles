﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {
	
	[SerializeField] private Tile Tile;
	private double[] yArray = new double[]{4.5, 3.5, 2.5, 1.5, 0.5, -0.5, -1.5, -2.5};
	private double[] xArray = new double[]{-3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, 3.5};
	
	//Level layout
	private int[,] levelList;
	private int[][,] allLevels;
	private int difficultyList;

	public int[,] LevelList{
		get{
			return levelList;
		}
	}
	public double[] XArray{
		get{
			return xArray;
		}
	}
	public double[] YArray{
		get{
			return yArray;
		}
	}
	void Start () {
		CreateAllLevels();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartLevel(){
		SetLevelArray(); // move this to a start game. check if the game has already been started
		GameManager.Instance.TileCount = levelList.GetLength(0);
		SetTotalPositiveTiles();
		CreateTiles();
	}
	// counts up all the tiles in the "on" state (1) from the level list array
	private void SetTotalPositiveTiles(){
		//int[] item = new int[]{tile[0], tile[1]};
		GameManager.Instance.PositiveTiles = 0;
		for(var i = 0; i < levelList.GetLength(0); i++){
			if(levelList[i, 2] == 1){
				GameManager.Instance.PositiveTiles += 1;
			}
		}
	}
	// creates all the tiles for the level
	private void CreateTiles(){
		for(var i = 0; i < levelList.GetLength(0); i++){
			Tile newTile = Instantiate(Tile) as Tile;
			newTile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
			newTile.X = levelList[i, 0];
			newTile.Y = levelList[i, 1];
			newTile.TileStatus = levelList[i, 2];
		}
	}
	public void SetLevelArray(){
		levelList = new int[,]{};
		int i = GameManager.Instance.CurrentLevel - 1;
		levelList = allLevels[i];
		// change this to single int.
		//GameManager.Instance.Difficulty = difficultyList;
		int l = allLevels.GetLength(0);
		GameManager.Instance.ScoreList = new int[l];
	}
	private void CreateAllLevels(){
		//difficultyList = new int[]{
		//	1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,2,1,2,2,2,3,2,2,2,1,2,3,3,3,2,1,3,2,2
		//};
		allLevels = new int[][,]{
			new int[,] {{1,4,0},{2,3,0},{2,4,0},{2,5,0},{3,2,0},{3,4,0},{4,1,0},{4,2,0},{4,3,0},{5,2,0}},
			new int[,] {{2,2,0},{2,3,0},{3,2,0},{3,4,0},{4,3,0},{4,4,0}},
			new int[,] {{1,1,0},{1,2,0},{1,4,0},{1,5,0},{2,1,0},{2,3,0},{2,5,0},{3,2,0},{3,3,0},{3,4,0},{4,1,0},{4,3,0},{4,5,0},{5,1,0},{5,2,0},{5,4,0},{5,5,0}},
			new int[,] {{2,2,0},{2,3,0},{2,4,0},{3,3,0},{4,3,0},{5,2,0},{5,3,0},{5,4,0}},
			new int[,] {{2,2,0},{3,1,0},{3,2,0},{3,3,0},{4,3,0},{4,4,0},{4,5,0},{5,4,0}},
			new int[,] {{2,4,0},{3,2,0},{3,3,0},{3,4,0},{3,5,0},{4,2,0},{4,3,0},{4,4,0},{5,2,0}},
			new int[,] {{1,5,0},{2,3,0},{2,4,0},{2,5,0},{3,3,0},{4,2,0},{4,3,0},{5,1,0},{5,2,0}},
			new int[,] {{1,2,0},{1,4,0},{1,5,0},{2,1,0},{2,2,0},{2,3,0},{2,5,0},{3,2,0},{3,3,0},{4,2,0},{4,3,0},{4,4,0},{5,3,0}},
			new int[,] {{2,2,0},{2,3,0},{2,4,0},{3,1,0},{3,2,0},{3,4,0},{3,5,0},{4,1,0},{4,5,0}},
			new int[,] {{2,3,0},{3,2,0},{3,3,0},{3,4,0},{4,1,0},{4,2,0},{4,3,0},{4,4,0},{4,5,0}},
			new int[,] {{1,4,0},{2,1,0},{2,2,0},{2,3,0},{2,4,0},{3,1,0},{3,4,0},{4,1,0},{5,1,0},{5,2,0}},
			new int[,] {{2,1,0},{2,5,0},{3,1,0},{3,2,0},{3,3,0},{3,4,0},{3,5,0},{4,1,0},{4,5,0},{5,1,0},{5,5,0}},
			new int[,] {{2,1,0},{2,2,0},{2,3,0},{2,4,0},{2,5,0},{3,1,0},{3,2,0},{3,4,0},{3,5,0},{4,2,0},{4,3,0},{4,4,0}},
			new int[,] {{2,1,0},{2,2,0},{2,3,0},{2,4,0},{3,1,0},{3,3,0},{3,4,0},{4,1,0},{4,2,0},{4,4,0},{5,1,0},{5,2,0},{5,3,0},{5,4,0}},
			new int[,] {{2,3,0},{2,4,0},{3,3,1},{4,2,0},{4,3,0}},
			new int[,] {{1,1,1},{1,2,0},{2,1,0},{2,2,1},{4,4,0},{4,5,1},{5,4,1},{5,5,0}},
			new int[,] {{2,2,0},{2,4,0},{3,1,0},{3,2,0},{3,3,1},{3,4,0},{3,5,0},{4,2,0},{4,4,0}},
			new int[,] {{1,1,0},{1,2,0},{1,3,0},{2,1,0},{2,2,0},{3,1,0},{3,5,0},{4,4,0},{4,5,0},{5,3,0},{5,4,0},{5,5,0}},
			new int[,] {{1,3,0},{2,1,0},{2,2,0},{2,3,0},{2,4,0},{3,1,0},{3,2,0},{3,3,0},{3,4,0},{4,1,0},{4,3,0},{4,4,0}},
			new int[,] {{1,2,0},{2,1,0},{2,2,0},{2,4,0},{3,1,0},{3,2,0},{3,3,0},{3,4,0},{4,2,0},{4,3,0},{4,4,0}},
			new int[,] {{2,3,0},{3,2,1},{3,3,0},{3,4,0},{4,1,0},{4,2,0},{4,3,1},{5,2,0}},
			new int[,] {{1,3,0},{1,4,1},{2,3,0},{2,4,0},{2,5,0},{3,2,0},{3,3,0},{3,4,1},{4,3,0}},
			new int[,] {{2,1,1},{2,2,0},{2,4,0},{2,5,0},{3,1,0},{3,2,0},{3,3,1},{3,4,0},{4,3,0}},
			new int[,] {{1,4,0},{2,3,0},{2,4,0},{2,5,0},{3,4,0},{4,1,0},{4,2,1},{4,3,0},{4,4,0}},
			new int[,] {{2,1,0},{2,2,1},{2,4,1},{2,5,0},{3,2,0},{3,3,1},{3,4,0}},
			new int[,] {{0,1,0},{1,1,0},{2,1,1},{2,3,0},{2,5,1},{3,0,0},{3,1,0},{3,2,1},{3,3,0},{3,4,0},{3,5,1},{4,1,1},{4,2,0},{4,4,0},{5,2,0}},
			new int[,] {{1,1,0},{1,2,0},{1,3,0},{1,4,0},{1,5,0},{2,1,0},{2,3,0},{2,5,0},{3,1,0},{3,5,0},{4,1,0},{4,2,0},{4,4,0},{4,5,0},{5,1,0},{5,2,0},{5,3,0},{5,4,0},{5,5,0}},
			new int[,] {{2,2,0},{3,1,0},{3,2,0},{3,3,0},{4,2,0},{4,3,0},{4,4,0},{5,3,0}},
			new int[,] {{2,1,0},{2,2,0},{2,3,0},{2,4,0},{2,5,0},{3,1,0},{3,2,0},{3,4,0},{3,5,0},{3,6,0},{4,2,0},{4,3,0},{4,4,0},{4,5,0},{5,3,0}},
			new int[,] {{1,3,0},{1,4,0},{1,5,0},{2,1,0},{2,2,0},{2,3,0},{2,4,0},{2,5,0},{3,1,0},{3,2,0},{3,3,0},{4,1,0},{4,3,0},{5,2,0},{5,3,0},{5,4,0}},
			new int[,] {{1,1,0},{1,2,0},{1,4,0},{2,1,0},{2,2,0},{2,3,0},{2,4,0},{3,1,0},{3,2,0},{3,3,0},{3,4,0},{4,1,0},{4,4,0},{5,1,0},{5,4,0}},
			new int[,] {{2,3,0},{3,3,0},{3,4,0},{3,5,0},{4,1,0},{4,2,0},{4,3,0},{4,4,0},{5,2,0}},
			new int[,] {{2,2,0},{2,3,0},{2,4,0},{3,2,0},{3,3,0},{3,4,0},{4,2,0},{4,3,1},{4,4,0}},
			new int[,] {{1,4,0},{1,5,0},{2,4,0},{3,2,0},{3,3,1},{3,4,0},{4,2,1},{5,1,0},{5,2,0}},
			new int[,] {{1,3,0},{2,2,0},{2,3,1},{2,4,0},{3,2,0},{3,3,0},{3,4,0},{4,2,1},{4,3,1},{4,4,0},{5,2,0}},
			new int[,] {{1,4,0},{2,2,0},{2,3,0},{2,4,0},{3,2,0},{3,3,0},{3,4,0},{3,5,0},{4,2,0},{4,4,0}},
			new int[,] {{1,2,0},{2,1,0},{2,2,1},{2,3,0},{2,4,0},{3,2,1},{3,3,1},{3,4,0},{3,5,0},{4,2,0},{4,4,0}},
			new int[,] {{1,2,0},{1,3,1},{2,2,0},{2,3,0},{2,4,0},{3,1,0},{3,2,0},{3,3,0},{4,2,1},{4,3,0},{4,4,0},{5,3,0}},
			new int[,] {{2,2,0},{2,3,0},{3,2,0},{3,3,0},{3,4,0},{4,2,0},{4,4,1},{5,1,0},{5,2,0},{5,3,1},{5,4,0},{5,5,0}},
			new int[,] {{0,1,0},{0,5,0},{1,1,0},{1,2,0},{1,4,0},{1,5,0},{2,2,0},{2,3,1},{2,4,0},{3,2,0},{3,4,0},{4,2,0},{4,4,0},{5,1,0},{5,2,0},{5,4,0},{5,5,0},{6,2,0},{6,4,0}},
			new int[,] {{1,2,0},{1,3,1},{2,1,0},{2,2,1},{2,3,0},{2,4,1},{3,1,1},{3,2,0},{3,3,1},{3,4,0},{4,2,1},{4,3,0}},
			new int[,] {{2,2,0},{2,4,0},{3,2,1},{3,3,1},{3,4,1},{4,3,0},{5,2,1},{5,3,1},{5,4,1}},
			new int[,] {{0,5,0},{1,4,1},{1,5,0},{2,3,1},{2,4,0},{2,5,1},{3,2,1},{3,3,0},{3,4,1},{4,1,1},{4,2,0},{4,3,1},{4,4,0},{5,0,0},{5,1,0},{5,2,1},{5,3,0},{5,4,0}},
			new int[,] {{2,1,0},{2,2,0},{2,3,1},{3,2,0},{3,3,0},{3,4,0},{3,5,0},{4,1,0},{4,2,0},{4,3,1},{4,4,1},{4,5,0},{5,0,0},{5,1,0},{5,2,0},{5,3,0},{5,4,0},{5,5,1},{6,4,0}},
			new int[,] {{2,1,0},{2,3,0},{2,4,1},{3,1,0},{3,2,1},{3,3,1},{3,4,0},{3,5,0},{4,2,0},{4,3,0}},
			new int[,] {{1,2,1},{1,3,0},{2,3,1},{3,3,0},{3,4,0},{4,3,0},{5,2,1},{5,3,0},{5,4,1}},
			new int[,] {{0,0,0},{0,1,0},{0,2,0},{0,3,0},{1,0,0},{1,1,0},{1,2,0},{1,3,0},{2,3,0},{3,3,0},{3,5,0},{3,6,0},{4,2,0},{4,3,0},{4,4,0},{4,5,0},{4,6,0},{5,1,0},{5,2,0},{5,3,0},{5,4,1},{6,3,0}},
			new int[,] {{1,4,0},{2,3,0},{2,4,1},{2,5,0},{3,3,0},{3,4,0},{4,2,0},{4,3,0},{4,4,0},{5,4,0}},
			new int[,] {{1,1,1},{1,2,0},{1,3,1},{1,5,1},{2,1,0},{2,2,0},{2,4,1},{2,5,1},{3,1,1},{3,3,1},{3,4,1},{3,5,0},{4,2,1},{4,3,0},{4,4,1},{4,5,1}},
			new int[,] {{1,3,1},{1,4,1},{1,5,0},{2,2,1},{2,3,0},{2,5,1},{3,3,1},{3,4,0},{3,5,1},{4,2,0},{4,3,0},{4,5,0}},
		};
	}
}
