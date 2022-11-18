using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StepManager 
{
    int currentStep;
    int maxSteps;
    LevelManager levelManager;
    BossManager bossManager;
    LevelCollectionConfig levelConfig;
    [Inject]
    public StepManager(LevelCollectionConfig levelConfig, LevelManager levelManager, BossManager bossManager)
    {
        this.levelManager = levelManager;
        levelManager.OnLevelStart += StartLevel;
        this.bossManager = bossManager;
        this.levelConfig = levelConfig;
    }
    public void StartLevel(int level)
    {
        maxSteps = levelConfig.levelConfigs[level].stepsToDefeat;

    }
    public void Step()
    {
        currentStep++;
        if(currentStep>=maxSteps)
        {
            MaxStepsReached();
        }
    }
    public void AddMaxSteps(int count)
    {
        maxSteps += count;
    }
    void MaxStepsReached()
    {
        if(!bossManager.AreBossesDefeated())
            levelManager.Defeat();
    }
}
