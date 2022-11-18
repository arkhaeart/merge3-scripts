using Common.Samples.Persistence;
using Persistence.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PreloadMonoInstaller : MonoInstaller
{
    public DataClassesConfig dataClassesConfig;
    public override void InstallBindings()
    {
        DataHandler.Init(dataClassesConfig);
        DefaultPlayerDataInfo defaultInfo = DataHandler.GetDataSync<DefaultPlayerDataInfo>();
        NotDefaultPlayerDataInfo notDefaultInfo = DataHandler.GetDataSync<NotDefaultPlayerDataInfo>();
        PlayerData data = new PlayerData(defaultInfo, notDefaultInfo);
        Container.BindInstance(data);
    }
    public override void Start()
    {
        base.Start();
        SceneManager.LoadScene(1);
    }
}