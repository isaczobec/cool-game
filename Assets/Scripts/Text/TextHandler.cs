using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    /// <summary>
    /// Prefab of the damage popup prefab.
    /// </summary>
    [SerializeField] private GameObject damagePopup;

    /// <summary>
    /// Singleton pattern instance of this class. Can be referenced by other classes to make everything more efficient.
    /// </summary>
    public static TextHandler Instance {private set; get;}

    


    void Awake()
    {
        if (Instance == null) {
        Instance = this;
        } else {
            Debug.LogWarning("There cannot be more than one TextHandling Instance!");
        }
            
    }

    public GameObject GetDamagePopup() {
        return damagePopup;
    }

        
}
