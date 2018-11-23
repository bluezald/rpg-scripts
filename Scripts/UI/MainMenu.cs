using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame() {
		LevelLoaderInfo.levelIndex = 2;
		SceneManager.LoadScene ("LevelLoader");
	}


}
