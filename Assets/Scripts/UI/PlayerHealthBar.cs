using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Material healthBarMaterial;

    [SerializeField] private float minHealthBarLevel;
    [SerializeField] private float maxHealthBarLevel;

    

    // Start is called before the first frame update
    void Start()
    {
        player.OnPlayerGotHit += Player_OnPlayerGotHit;
        healthBarMaterial.SetVector("_HealthSlider",new Vector2(maxHealthBarLevel,0));
    }

    private void Player_OnPlayerGotHit(object sender, HitInfo e)
    {
        float healthPercent = player.GetHealthPercent();
        float BarLevel = (maxHealthBarLevel - minHealthBarLevel) * healthPercent + minHealthBarLevel;  

        healthBarMaterial.SetVector("_HealthSlider",new Vector2(BarLevel,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
