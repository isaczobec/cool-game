using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Enemy enemy;
    [SerializeField] private float maxHealth;
    [SerializeField] private TMP_Text textMeshPro;

    public Color color;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = enemy.GetHealth().ToString();

        textMeshPro.color = GetHealthColor();
        color = GetHealthColor();



    }

    private Color GetHealthColor() {

        Vector3 returnColor = new Vector3(0,0,0);


        float health = enemy.GetHealth();
        float percentHealth = health/maxHealth;


        if (percentHealth >= 0.5f) {
            returnColor.y = 255;
            returnColor.x = (int)(255 * (1 - percentHealth) * 2);
        } else {
            returnColor.x = 255;
            returnColor.y = (int)(255 * (percentHealth*2));
        }


            Debug.Log(returnColor);

        
        return new Color(returnColor.x,returnColor.y,0,1);

    }



}
