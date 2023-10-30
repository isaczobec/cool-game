using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameObject : MonoBehaviour
{
    [SerializeField] GameObject objectToDelete;

    public void DeleteProjectile() {
        Destroy(objectToDelete);
    }
}
