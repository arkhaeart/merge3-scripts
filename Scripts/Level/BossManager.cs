using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossManager 
{
    MonoBoss monoBoss;
    LevelCollectionConfig levelConfig;

    int currentBossHealth;
    int currentBoss = 0;
    System.Action OnBossesDefeated;
    Boss.Data[] bossDatas;
    [Inject]
    public BossManager(MonoBoss monoBoss,LevelCollectionConfig levelConfig)
    {
        this.monoBoss = monoBoss;
        this.levelConfig = levelConfig;

    }
    public bool AreBossesDefeated()
    {
        return currentBoss>=bossDatas.Length;
    }
    public void SetBossesDefeatedCallback(System.Action callback)
    {
        OnBossesDefeated = callback;
    }
    public void StartLevel(int level)
    {
        currentBoss = 0;
        bossDatas = levelConfig.levelConfigs[level].testBossCollection.bosses;
        PlaceNewBoss();
    }
    void PlaceNewBoss()
    {
        if(currentBoss<bossDatas.Length)
        {
            var bossData = bossDatas[currentBoss];
            currentBossHealth = bossData.maxHP - currentBossHealth;
            monoBoss.SetBoss(bossData, currentBossHealth);
        }
    }
    public void TakeDamage(int damage)
    {
        currentBossHealth -= damage;
        monoBoss.UpdateHealth(currentBossHealth);
        if(currentBossHealth<=0)
        {
            OnBossKilled();
        }
    }
    void OnBossKilled()
    {
        //TODO sfx etc
        currentBoss++;
        if(currentBoss>=bossDatas.Length)
        {
            OnBossesDefeated?.Invoke();
        }
        else
        {
            PlaceNewBoss();
        }
        
    }
}
