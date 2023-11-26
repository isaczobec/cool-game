using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameObject : MonoBehaviour
{
    [SerializeField] GameObject objectToDelete;

    [SerializeField] private bool deleteAfterTime = false;
    [SerializeField] private float timeUntilDelete = 2f;   

    public void DeleteProjectile() {
        Destroy(objectToDelete);
    }

    private void Update() {
        if (deleteAfterTime) {
            timeUntilDelete -= Time.deltaTime;
            if (timeUntilDelete <= 0f) {
                Destroy(objectToDelete);
            }
        }
    }

    
}
