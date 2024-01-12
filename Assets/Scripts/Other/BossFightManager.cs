using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{


    [SerializeField] private PlayerHealthBar bossHealthBar;
    [SerializeField] private Animator bossHealthBarAnimator;
    [SerializeField] private string barSlideInString = "SlideIn";

    private Enemy currentBossEnemy;

    public static BossFightManager Instance {get; private set;}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    
    public void BossEnemyGetHit() {
        SetBossHealthBarTargetLevel();
    }


    public void SetNewBossEnemy(Enemy enemy) {
        currentBossEnemy = enemy;
        ShowBossHealthBar(enemy);
    }

    public void ShowBossHealthBar(Enemy bossEnemy) {
        bossHealthBarAnimator.SetTrigger(barSlideInString);

        bossHealthBar.SetMaxDisplayNumber(bossEnemy.maxHealth);
        
        SetBossHealthBarTargetLevel();
    }

    private void SetBossHealthBarTargetLevel() {
        if (currentBossEnemy != null) {
            float bossHealth = currentBossEnemy.GetHealth();
            bossHealthBar.SetNewTargetLevel(bossHealth/currentBossEnemy.maxHealth,bossHealth);

        } 
        
    }

}
