using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private Vector2 directionalInput;
    private float cameraZoom;

    // Update is called once per frame
    void Update()
    {
        directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Player.Instance.BeginMove(directionalInput);

        if(Input.GetKeyDown(KeyCode.E))
        {
            UIController.Instance.ToggleCraftingUI();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            UIController.Instance.ToggleInventoryUI();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            UIController.Instance.ToggleResearchUI();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveManager.Instance.SaveGame();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.LoadGame();
        }

        cameraZoom = Input.GetAxisRaw("Mouse ScrollWheel");
        if (cameraZoom != 0 && Input.GetKey(KeyCode.LeftControl))
        {
            CameraController.Instance.Zoom(cameraZoom);
        }
    }
}
