using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInfo : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;


    [SerializeField] private Transform playerTransform; // the transform of the player this object is following


    public static MouseInfo Instance {get; private set;}


    private void Start() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one MouseInfo!");
        }
    }


    public Vector3 GetMousePixelPosition() {
        Vector3 cursorPosition = Mouse.current.position.ReadValue();
        Vector3 adjustedCursorPosition = new Vector3(cursorPosition.x - Screen.width/2, cursorPosition.y - Screen.height/2, 0);
        return adjustedCursorPosition;
    }

    public float GetPlayerMouseAngle() { //returns the angle which the player is pointing the mouse

        Vector3 cursorPosition = Mouse.current.position.ReadValue();
        Vector3 adjustedCursorPosition = new Vector3(cursorPosition.x - Screen.width/2, cursorPosition.y - Screen.height/2, 0);

        float mouseAngleDegrees = Mathf.Atan(adjustedCursorPosition.y/adjustedCursorPosition.x) * Mathf.Rad2Deg;

        if (adjustedCursorPosition.x > 0) {
            mouseAngleDegrees *= -1;
        }

        return mouseAngleDegrees;

    }
}
