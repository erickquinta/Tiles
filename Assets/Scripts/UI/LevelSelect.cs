using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : Singleton<LevelSelect> {

	[SerializeField] private Button button;
	[SerializeField] private GameObject levelMenu;
	[SerializeField] private GameObject swipeIcon;
	[SerializeField] private GameObject scrollBox;
	private int selectedLevel;
	/* */
	private ScrollRect mScrollRect;
    private Transform mScrollTransform;
    private RectTransform mContent;
    public bool _LockX = false;
    public bool _LockY = true;

	/* */
	public Sprite lvl1;
	public Sprite lvl2;
	public Sprite lvl3;
	public Sprite lvl4;
	public Sprite lvl5;
	public Sprite lvl6;
	public Sprite lvl7;
	public Sprite lvl8;
	public Sprite lvl9;
	public Sprite lvl10;
	public Sprite lvl11;
	public Sprite lvl12;
	public Sprite lvl13;
	public Sprite lvl14;
	public Sprite lvl15;
	public Sprite lvl16;
	public Sprite lvl17;
	public Sprite lvl18;
	public Sprite lvl19;
	public Sprite lvl20;
	public Sprite lvl21;
	public Sprite lvl22;
	public Sprite lvl23;
	public Sprite lvl24;
	public Sprite lvl25;
	public Sprite lvl26;
	public Sprite lvl27;
	public Sprite lvl28;
	public Sprite lvl29;
	public Sprite lvl30;
	public Sprite lvl31;
	public Sprite lvl32;
	public Sprite lvl33;
	public Sprite lvl34;
	public Sprite lvl35;
	public Sprite lvl36;
	public Sprite lvl37;
	public Sprite lvl38;
	public Sprite lvl39;
	public Sprite lvl40;
	public Sprite lvl41;
	public Sprite lvl42;
	public Sprite lvl43;
	public Sprite lvl44;
	public Sprite lvl45;
	public Sprite lvl46;
	public Sprite lvl47;
	public Sprite lvl48;
	public Sprite lvl49;
	public Sprite lvl50;

	public int SelectLevel{
		get{
			return selectedLevel;
		}
	}
	public void Start(){
		mScrollRect = scrollBox.GetComponent<ScrollRect>();
        mScrollTransform = mScrollRect.transform;
        mContent = mScrollRect.content;	
	}

	public void ToMainMenu(){
		SceneManager.LoadScene("Menu");
	}
	
	// Easy
	public void ToLevelSelect(){
		GameObject[] s = GameObject.FindGameObjectsWithTag("LevelBtn");
		PlayerPrefs.SetInt("difficulty", 1);
		if(s == null || s.Length == 0){
			CreateScrollIcons();
		}
	}
	private void LevelSelected(int lvl){
		//GameManager.Instance.CurrentLevel = lvl;
		PlayerPrefs.SetInt("selectedLevel",lvl);
		SceneManager.LoadScene("Grid");
	}
	// Create level select icons
	public void CreateScrollIcons(){
		int x = 400;
		
		for(var i = 0; i < 50; i++){
			GameObject swipe = Instantiate(swipeIcon, transform) as GameObject;
			float width = swipe.transform.GetChild(0).GetComponent<RectTransform>().rect.width;

			if(i == 0){
				x = 400;
			}else{
				x = 400 + (i * (((int)width*2) + 100));
			}

			swipe.transform.position = new Vector2(x, (Screen.height / 2));

			swipe.transform.SetParent(scrollBox.transform.GetChild(0).transform, true);
			
			// add on click event to start level
			int idx = i + 1;
			//swipe.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{LevelSelected(idx);});
			swipe.transform.GetComponent<Button>().onClick.AddListener(delegate{LevelSelected(idx);});
			// set the level number text and update the mini icon
			swipe.transform.Find("Header").transform.GetChild(0).GetComponent<Text>().text = "Level " + (i+1).ToString();
			var lvl = "lvl" + (i+1);
			// match the string text to the variable and update the sprite
			var icon = this.GetType().GetField(lvl).GetValue(this); 
			swipe.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = icon as Sprite;

			// add the completed checkbox if the level has been completed
			if(SaveManager.Instance.GetCompletedLevels()[i] == 1){
				swipe.transform.Find("Checkbox").GetComponent<Image>().enabled = true;
			}

			// update the move count is the level has already been played
			if(SaveManager.Instance.GetScoreList()[i] != 0){
				swipe.transform.Find("Text").GetComponent<Text>().text = ("Moves: " + SaveManager.Instance.GetScoreList()[i].ToString());
			}
		}
		// scroll to the last completed level
		GameObject[] s = GameObject.FindGameObjectsWithTag("LevelBtn");
		for(var k = 0; k < s.Length; k++){
			if(SaveManager.Instance.LastCompletedLevel == 0){
				CenterOnItem(s[0]);
			}
			else if(SaveManager.Instance.LastCompletedLevel == (k+1)){
				CenterOnItem(s[k]);
				break;
			}
		}
		
	}
	// scrolls to the next playable level
	// TODO: Needs tweaking
 	public void CenterOnItem (GameObject target)
    {
        Vector3 maskCenterPos = scrollBox.GetComponent<RectTransform>().position + (Vector3)scrollBox.GetComponent<RectTransform>().rect.center;
        Vector3 itemCenterPos = target.transform.position;
        Vector3 difference = maskCenterPos - itemCenterPos;
        difference.y = 0;
 
        Vector3 newPos = mContent.position + difference;
        if (_LockX) {
            newPos.x = mContent.position.x;
        }
        if (_LockY) {
            newPos.y = mContent.position.y;
        }

		mContent.position = newPos;
    }
	// TODO: not working properly
	public void DeleteButtons(){
		//GameObject[] buttons = GameObject.FindGameObjectsWithTag("LevelBtn");
		//GameObject[] button = 
		int count = scrollBox.transform.GetChild(0).childCount;
		//GameObject panel = scrollBox.transform.GetChild(0).GetComponent<GameObject>();
		
		for(var i = 0; i < scrollBox.transform.GetChild(0).childCount; i++){
			Destroy(scrollBox.transform.GetChild(0).transform.GetChild(i).transform.GetComponent<GameObject>());
		}
	}
}
