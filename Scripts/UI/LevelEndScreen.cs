using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelEndScreen : MonoBehaviour
{
    UIManager ui;
    [Inject]
    public void Initialize(UIManager ui)
    {
        this.ui = ui;
    }
    
    public void ReloadLevel()
    {
        ui.TestReloadLevel();
    }
    public void ToLevelMenu()
    {

    }
    public void ToNextLevel()
    { 
    
    }
    public void ShowAdForSteps()
    {

    }
}
