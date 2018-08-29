using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

	[SerializeField] private Sprite white;
	[SerializeField] private Sprite blue;
	[SerializeField] private Sprite yellow;
	[SerializeField] private Sprite purple;
	[SerializeField] private Sprite orange;
	[SerializeField] private Sprite green;

	public int id = 1;
	private int x;
	private int y;
	private int tileStatus;
	private Sprite toFlip;
	public Transform tile;
	
	public int X{
		get{
			return x;
		}
		set{
			x = value;
		}
	}
	public int Y{
		get{
			return y;
		}
		set{
			y = value;
		}
	}
	public int TileStatus{
		set{
			tileStatus = value;
		}
	}
	void Start () {
		tile = GetComponent<Transform>();
		
		SetPosition();
		SetTileStatus();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !GameManager.Instance.IsPaused){
			Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
			// check against current tile
			if(hit.collider != null){
				if(hit.collider.GetComponent<Transform>() == tile){
					Flip();
					GameManager.Instance.TileClicked(x, y);
				}
			}
		}
	}

	public void Flip(){
		if(tileStatus == 0){
			tileStatus = 1;
			this.GetComponent<SpriteRenderer>().sprite = toFlip;
			GameManager.Instance.PositiveTiles += 1;
		}else if(tileStatus == 1){
			tileStatus = 0;
			this.GetComponent<SpriteRenderer>().sprite = white;
			GameManager.Instance.PositiveTiles -= 1;
		}
	}

	void SetPosition(){
		double posX = LevelManager.Instance.XArray[x];
		double posY = LevelManager.Instance.YArray[y];

		transform.position = new Vector2((float)posX, (float)posY);
	}

	private void SetTileStatus(){
		switch (PlayerPrefs.GetInt("difficulty")){
			case 1:
				if(tileStatus == 1){
					this.GetComponent<SpriteRenderer>().sprite = blue;
				}
				toFlip = blue;
				break;
			case 2:
				if(tileStatus == 1){
					this.GetComponent<SpriteRenderer>().sprite = purple;
				}
				toFlip = purple;
				break;
			case 3:
				if(tileStatus == 1){
					this.GetComponent<SpriteRenderer>().sprite = yellow;
				}
				toFlip = yellow;
				break;
			case 4:
				if(tileStatus == 1){
					this.GetComponent<SpriteRenderer>().sprite = orange;
				}
				toFlip = orange;
				break;
			default:
				this.GetComponent<SpriteRenderer>().sprite = white;
			break;
		}
	}
}
