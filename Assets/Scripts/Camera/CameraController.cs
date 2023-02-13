using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    [SerializeField]
    float scrollSpeed = 5;

    Camera cam;

    
    [Range(5,13),SerializeField]
    float orthographicSize;
    float defaultOrthoSize = 9;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetOrthographicSize(float Amount)
    {
        cam.orthographicSize = Amount;
    }

    public void Zoom(float zoomAmount)
    {
        orthographicSize -= zoomAmount * scrollSpeed;
        orthographicSize = Mathf.Clamp(orthographicSize, 5, 13);
        SetOrthographicSize(orthographicSize);
    }
}
