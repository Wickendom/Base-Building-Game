using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceCanvas : MonoBehaviour {

    public static ScreenSpaceCanvas Instance;

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
}
