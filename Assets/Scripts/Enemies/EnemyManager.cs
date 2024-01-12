using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{


    public static EnemyManager Instance {get; private set;}

    private List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There are more than one enemyManager!");
        }
    }

    private void Start() {
        ScreenFade.Instance.fadeInFinnished += ScreenFade_FadeInFinnished;
    }

    private void ScreenFade_FadeInFinnished(object sender, EventArgs e)
    {
        enemies.Clear(); // clear all enemies when moving to a new level
    }

    public void AddEnemyToEnemyList(Enemy enemy) {
        enemies.Add(enemy);
    }
    public void RemoveEnemyFromEnemyList(Enemy enemy) {
        enemies.Remove(enemy);
    }

    public Enemy GetClosestEnemyTo(Vector3 targetPosition) {

        Enemy targetEnemy = null;
        float lowestDistance = -1; // set to -1 because its cool
        foreach (Enemy enemy in enemies) {
            float distance = Vector3.Distance(enemy.transform.position,targetPosition);

            if (lowestDistance > distance || lowestDistance < 0) {
                lowestDistance = distance;
                targetEnemy = enemy;
            }
        }

        return targetEnemy;
    }



    
}
