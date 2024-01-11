using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{

    public static DeathHandler Instance {get; private set;}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }


    public void InitiatePlayerDeath() {
        TimeScaleHandler.Instance.SetTimeScale(0,1f);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
