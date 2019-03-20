using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
	[SerializeField] AudioClip levelCompleteFX;
	[SerializeField] AudioClip tileClickFX;
	[SerializeField] AudioClip buttonClickFX;

	private void Awake(){
		DontDestroyOnLoad(this);
	}
	public AudioClip LevelCompleteFX{
		get{
			return levelCompleteFX;
		}
	}
	public AudioClip TileClickFX{
		get{
			return tileClickFX;
		}
	}

	public AudioClip ButtonClickFX{
		get{
			return buttonClickFX;
		}
	}
}
