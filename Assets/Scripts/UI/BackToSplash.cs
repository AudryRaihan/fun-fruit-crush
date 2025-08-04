using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToSplash : MonoBehaviour {

    public string sceneToLoad;
    private GameData gameData;
    private Board board;

    public void WinOK()
    {
        if(gameData != null)
        {
            gameData.saveData.isActive[board.level] = true;
            gameData.saveData.isActive[board.level + 1] = true;
            gameData.Save();
        }
        SceneState.tunjukkanMenu = "Level";
        SoundManager.instance.FindAndSetupButtons();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoseOK()
    {
        SceneState.tunjukkanMenu = "Level";
        SoundManager.instance.FindAndSetupButtons();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void BackToLevel()
    {
        
        if (gameData != null)
        {
            gameData.saveData.isActive[board.level] = true;
            gameData.Save();
        }
        SceneState.tunjukkanMenu = "Level";
        SoundManager.instance.FindAndSetupButtons();
        SceneManager.LoadScene(sceneToLoad);
    }

	// Use this for initialization
	void Start () {
        gameData = FindObjectOfType<GameData>();
        board = FindObjectOfType<Board>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
