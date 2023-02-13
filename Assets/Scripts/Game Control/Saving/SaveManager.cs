using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        PlayerState.Instance.localPlayerData.PosX = Player.Instance.transform.position.x;
        PlayerState.Instance.localPlayerData.PosY = Player.Instance.transform.position.y;
        
        SaveLists.Instance.SaveData();
    }

    public void LoadGame()
    {
        Debug.Log("Start loading saved data");
        SaveLists.Instance.LoadDataFromFile();
        //GameController.Instance.isSceneBeingLoaded = true;

        //int whichScene = SaveLists.Instance.localCopyOfData.SceneID;

        //SceneManager.LoadScene(whichScene);
    }
}
