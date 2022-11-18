using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PreloadScriptableInstaller", menuName = "Installers/PreloadScriptableInstaller")]
public class PreloadScriptableInstaller : ScriptableObjectInstaller<PreloadScriptableInstaller>
{
    public FigureKeyPresetConfig figureKeyPresetConfig;
    public FiguresWithdrawConfig figuresWithdrawConfig;
    public LevelCollectionConfig levelConfig;
    public FigureGraphicSet testGraphicSet;
    public ColorConfig colorConfig;

    public override void InstallBindings()
    {
        figureKeyPresetConfig.Initialize();
        Container.BindInstance(figureKeyPresetConfig);
        figuresWithdrawConfig.Initialize();
        Container.BindInstance(figuresWithdrawConfig);
        Container.BindInstance(levelConfig);
        testGraphicSet.Initialize();
        Container.BindInstance(testGraphicSet);
        colorConfig.Initialize();
        Container.BindInstance(colorConfig);

    }

}