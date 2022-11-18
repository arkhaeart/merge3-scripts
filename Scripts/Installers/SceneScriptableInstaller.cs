using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SceneScriptableInstaller", menuName = "Installers/SceneScriptableInstaller")]
public class SceneScriptableInstaller : ScriptableObjectInstaller<SceneScriptableInstaller>
{
    public MonoFigure monoFigurePrefab;
    public override void InstallBindings()
    {
        BindFactories();
    }
    void BindFactories()
    {
        Container.BindFactory<Figure.Data, MonoFigure, MonoFigure.Factory>()
            .FromPoolableMemoryPool<Figure.Data, MonoFigure, MonoFigure.Pool>
            (p => p.WithInitialSize(200)
            .FromComponentInNewPrefab(monoFigurePrefab)
            .UnderTransformGroup("Figures"));
    }
}