using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public void Move(Vector2 inputDirection, float moveSpeed)
    {
        Vector3 translation = new Vector3(inputDirection.x, 0, inputDirection.y);
        transform.Translate(translation * Time.deltaTime * moveSpeed);
    }
}
