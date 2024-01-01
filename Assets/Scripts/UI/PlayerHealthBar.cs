using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Material healthBarMaterial;

    [SerializeField] private Animator animator;
    
    [SerializeField] string takeDamageReferenceString = "TakeDamage";
    [SerializeField] string healthAlertSliderReferenceString = "_HealthAlertSlider";

    [SerializeField] string healthSliderReferenceString = "_HealthSlider";
    [SerializeField] string voronoiStrengthReferenceString = "_VoronoiStrength";
    [SerializeField] float voronoiStrength = 1;

    [SerializeField] private float maxAlertHealthBarLevel;
    [SerializeField] private float minAlertHealthBarLevel;

    [SerializeField] private float maxHealthBarLevel;
    [SerializeField] private float minHealthBarLevel;

    /// <summary>
    /// How long in seconds after taking damage the health bar will begin ticking down.
    /// </summary>
    [SerializeField] private float healthTickDownDelay;
    private float healthTickDownCooldown;
    

    /// <summary>
    /// The speed factor with which the health ticks down.
    /// </summary>
    [SerializeField] private float healthTickDownSpeed;

    private float currentHealthLevel;
    private float targetHealthLevel;

    private float targetFraction = 1; // the fraction the bar is lerping towards


    [SerializeField] private TextMeshProUGUI textMeshPro;


    /// <summary>
    /// The max stat number that is always displayed
    /// </summary>
    public float maxDisplayNumber;
    private float targetDisplayNumber;
    private float currentHealthDisplayNumber;
    [SerializeField] private float HealthNumberTickDownSpeed = 8f;


    


    // Start is called before the first frame update
    void Start()
    {
        healthBarMaterial.SetVector(healthAlertSliderReferenceString,new Vector2(maxAlertHealthBarLevel,0));
        healthBarMaterial.SetVector(healthSliderReferenceString,new Vector2(maxHealthBarLevel,0));

        currentHealthLevel = maxHealthBarLevel;
        targetHealthLevel = maxHealthBarLevel;

        healthTickDownCooldown = healthTickDownDelay;


        textMeshPro.text = maxDisplayNumber.ToString() + "/" + maxDisplayNumber.ToString();

        if (maxDisplayNumber != 0) {
            currentHealthDisplayNumber = maxDisplayNumber;
            targetDisplayNumber = maxDisplayNumber;

            
        }
    }

    public void SetMaxDisplayNumber(float number) {
        maxDisplayNumber = number;
        targetDisplayNumber = maxDisplayNumber;
        
    }




    /// <summary>
    /// Sets the target level to a fraction [0,1]
    /// </summary>
    /// <param name="fraction"></param>
    public void SetNewTargetLevel(float fraction, float displayNumber)
    {

        if (fraction != targetFraction) {


            SetDecreaseInBar(fraction, displayNumber, flashHealthBar:fraction < targetFraction,changeTickDownCooldown:fraction < targetFraction);

            targetFraction = fraction;
        }

    }


    private void SetDecreaseInBar(float fraction, float displayNumber,bool changeTickDownCooldown = true,bool flashHealthBar = true)
    {
        // set the alert level instantly when taken damage
        float AlertHealthLevel = (maxAlertHealthBarLevel - minAlertHealthBarLevel) * fraction + minAlertHealthBarLevel;

        healthBarMaterial.SetVector(healthAlertSliderReferenceString, new Vector2(AlertHealthLevel, 0));

        targetHealthLevel = (maxHealthBarLevel - minHealthBarLevel) * fraction + minHealthBarLevel;

        if (changeTickDownCooldown) {healthTickDownCooldown = healthTickDownDelay;}

        if (flashHealthBar) {animator.SetTrigger(takeDamageReferenceString);}

        targetDisplayNumber = displayNumber;
    }







    // Update is called once per frame
    private void Update()
    {
        healthTickDownCooldown -= Time.deltaTime;

        if (healthTickDownCooldown <= 0) {
            currentHealthLevel = Mathf.Lerp(currentHealthLevel, targetHealthLevel, Time.deltaTime * healthTickDownSpeed);
            healthBarMaterial.SetVector(healthSliderReferenceString,new Vector2(currentHealthLevel,0));

            
        }


        healthBarMaterial.SetFloat(voronoiStrengthReferenceString,voronoiStrength);


        currentHealthDisplayNumber = Mathf.Lerp(currentHealthDisplayNumber, targetDisplayNumber, Time.deltaTime * HealthNumberTickDownSpeed);
        textMeshPro.text = Mathf.Round(currentHealthDisplayNumber).ToString() + "/" + maxDisplayNumber;
    }
}
