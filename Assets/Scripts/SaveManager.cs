using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager> {
	public SaveState state;

	private void Awake(){
		DontDestroyOnLoad(this);
		Load();
	}

	public void Save(){
		PlayerPrefs.SetString("save", Serializer.Serialize<SaveState>(state));
	}

	// load previous save state
	public void Load(){
		if(PlayerPrefs.HasKey("save")){
			state = Serializer.Deserializer<SaveState>(PlayerPrefs.GetString("save"));
		}else{
			state = new SaveState();
			Save();
			Debug.Log("no save file");
		}
	}

	public void OnApplicationPause(bool pauseStatus){
		if(pauseStatus){
			if(SceneManager.GetActiveScene().name == "Grid"){
				GameManager.Instance.PauseGame();
			}
		}else{
			//Debug.Log("resume");
		}
	}

	public int[] GetScoreList(){
		return state.scoreArray;
	}
	public void SetScoreList(int idx, int value){
		state.scoreArray[idx] = value;
	}
	public int[] GetCompletedLevels(){
		return state.completedLevels;
	}
	public void SetCompletedLevels(int idx, int value){
		state.completedLevels[idx] = value;
	}

	public int LastCompletedLevel{
		get{
			return state.lastCompletedLevel;
		}set{
			state.lastCompletedLevel = value;
		}
	}

	public void ResetSave(){
		PlayerPrefs.DeleteKey("save");
	}
}
