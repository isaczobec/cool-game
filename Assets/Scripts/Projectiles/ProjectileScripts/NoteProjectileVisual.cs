using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectileVisual : ProjectileVisual
{

    [SerializeField] private GameObject[] visualGameObjects;

    public override void InitializeProjectileVisual() {

        int visualObjectIndex = Random.Range(0,visualGameObjects.Length);

        visualGameObjects[visualObjectIndex].SetActive(true);

    }

    
}
