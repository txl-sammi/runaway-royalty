using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //todo: new input system?
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            
            inputVector.y = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
            //Debug.Log("pressing S");
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool GetAttackKeyDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool GetInteractKeyDown()
    {
        return Input.GetKey(KeyCode.F);
    }
}
