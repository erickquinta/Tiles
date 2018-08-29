using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] private Text moveCountLbl;
	[SerializeField] private Text levelLbl;
	[SerializeField] private Image pauseMenu;
	private bool isPaused = false;
	private int tileCount = 0;
	private int positiveTiles = 0;
	private int currentLevel = 0;
	private int moveCount = 0;
	private int[] scoreList;
	//private GameObject[] t;
	public bool IsPaused{
		get{
			return isPaused;
		}set{
			isPaused = value;
		}
	}
	public int TileCount{
		get{
			return tileCount;
		}set{
			tileCount = value;
		}
	}
	public int PositiveTiles{
		get{
			return positiveTiles;
		}set{
			positiveTiles = value;
		}
	}
	public int CurrentLevel{
		get{
			return currentLevel;
		}set{
			currentLevel = value;
		}
	}
	public int[] ScoreList{
		get{
			return scoreList;
		}set{
			scoreList = value;
		}
	}
	void Start () {
		
		// start the game on the selected level
		currentLevel = PlayerPrefs.GetInt("selectedLevel");
		if(currentLevel == 0){currentLevel = 1;}
		levelLbl.text = "Level " + currentLevel;
		
		// call function to create tiles
		LevelManager.Instance.StartLevel();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TileClicked(int x, int y){
		moveCount += 1;
		moveCountLbl.text = "Moves: " + moveCount.ToString();

		var adjTiles = FindAllAdjacent(x, y);

		var top = GetAdjacent(adjTiles[0,0], adjTiles[0,1]);
		var bottom = GetAdjacent(adjTiles[1,0], adjTiles[1,1]);
		var right = GetAdjacent(adjTiles[2,0], adjTiles[2,1]);
		var left = GetAdjacent(adjTiles[3,0], adjTiles[3,1]);

		var levelList = LevelManager.Instance.LevelList;

		CheckList(top, levelList);
		CheckList(bottom, levelList);
		CheckList(right, levelList);
		CheckList(left, levelList);

		if(positiveTiles >= tileCount){
			StartCoroutine(CompleteLevelPause());
		}
	}
	private int[,] FindAllAdjacent(int x, int y){
		int[,] tiles = new int[,]{
			{x, y-1},
			{x, y+1},
			{x+1, y},
			{x-1, y}
		};
		return tiles;
	}
	private int[] GetAdjacent(int x, int y){
		int[] tile = new int[]{x, y};
		return tile;
	}
	static bool CheckList(int[] tile, int[,] list){
		for(var i = 0; i < list.GetLength(0); i++){
			var x = list[i, 0];
			var y = list[i, 1];
			// if tile exists in level list
			if(tile[0] == x && tile[1] == y){
				FlipTiles(tile);
				return true;	
			}
			
		}
		return false;
	}
	private static void FlipTiles(int[] tile){
		// find all the tile game objects
		GameObject[] t = GameObject.FindGameObjectsWithTag("Tile");
		
		foreach(GameObject go in t){
			Tile currentTile = go.GetComponent<Tile>();
			int x = currentTile.X;
			int y = currentTile.Y;

			if(tile[0] == x && tile[1] == y){
				currentTile.Flip();
			}
		}
	}
	public void ResetTiles(){
		if(!isPaused){
			GameObject[] t = GameObject.FindGameObjectsWithTag("Tile");
			
			foreach(GameObject go in t){
				Destroy(go);
			}
			moveCount = 0;
			moveCountLbl.text = "Moves: " + moveCount.ToString();
			LevelManager.Instance.StartLevel();
		}
	}
	public void SkipLevel(){
		if(!isPaused){
			currentLevel += 1;
			levelLbl.text = "Level " + currentLevel;
			GameObject[] t = GameObject.FindGameObjectsWithTag("Tile");
			
			foreach(GameObject go in t){
				Destroy(go);
			}
			moveCount = 0;
			moveCountLbl.text = "Moves: " + moveCount.ToString();
			LevelManager.Instance.StartLevel();
		}
	}
	// add a small pause after completing level
	// TODO: add some effect
	IEnumerator CompleteLevelPause(){
		// update the save state variables
		int idx = (currentLevel - 1);
		SaveManager.Instance.LastCompletedLevel = idx;

		SaveManager.Instance.SetScoreList(idx, moveCount);
		
		// save after completing each level
		SaveManager.Instance.Save();
		double sec = 0.5;
		yield return new WaitForSeconds((float)sec);
		CompleteLevel();
	}
	private void CompleteLevel(){
		// update score to save state
		SaveManager.Instance.SetCompletedLevels(currentLevel - 1, 1);

		currentLevel += 1;
		levelLbl.text = "Level " + currentLevel;
		GameObject[] t = GameObject.FindGameObjectsWithTag("Tile");
		
		foreach(GameObject go in t){
			Destroy(go);
		}
		moveCount = 0;
		moveCountLbl.text = "Moves: " + moveCount.ToString();
		LevelManager.Instance.StartLevel();
		// pass the move count to the score list, at the same position as the level
		scoreList[currentLevel] = moveCount;
	}

	public void PauseGame(){
		if(!isPaused){
			isPaused = true;
	
			Image menu = Instantiate(pauseMenu, transform) ;
			menu.transform.position = new Vector2(0, 100);
			menu.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
					
			var menuBtn = menu.transform.Find("Main Menu Button").GetComponent<Button>();
			menuBtn.onClick.AddListener(ToMainMenu);

			var levelBtn = menu.transform.Find("Select Level Button").GetComponent<Button>();
			levelBtn.onClick.AddListener(ToLevelSelect);

			var resumeBtn = menu.transform.Find("Resume Button").GetComponent<Button>();
			resumeBtn.onClick.AddListener(UnPauseGame);
		}
	}
	public void UnPauseGame(){
		isPaused = false;
		GameObject pm = GameObject.FindGameObjectWithTag("PauseMenu");
		Destroy(pm);
	}
	public void ToMainMenu(){
		SceneManager.LoadScene("Menu");
	}
	public void ToLevelSelect(){
		SceneManager.LoadScene("Level Select");
	}
}
