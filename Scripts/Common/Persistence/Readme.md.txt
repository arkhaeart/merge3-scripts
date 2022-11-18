HOW TO USE THIS PERSISTENCE SYSTEM
1. Create your data storing classes. They should inherit from Saveable<YourClass> and must be in "Persistence" 
namespace. 
2. All data should be stored in public sub-class, which inherits SaveableData and has [System.Serializable] attribute
2a.You also must specify a constructor for class from p.1 with no parameters, which will be used to
create data storing class sample. This constructor should create public sub class from p.2. Following is sufficient:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Persistence.Systems;
namespace Common.Samples.Persistence
{
    public class DefaultPlayerDataInfo : Saveable<DefaultPlayerDataInfo>
    {
        public DefaultData defaultData;
        public DefaultPlayerDataInfo()
        {
            defaultData = new DefaultData();
        }
    }
    [System.Serializable]
    public class DefaultData:SaveableData
    {
        public int count;
        public string name;
    }
}

3. If you want precise control over how data is saved, you should create your own append class and override 
Append and AppendAsync methods. Otherwise just use DefaultAppend
4. Create DataClassesConfig with rmb/Create/Configs/DataClasses and place it in folder Assets/Resources/Configs.
Dont change its name!
5. Fill config with your used classes names from the dropdown menu and names for save files. 
6. Press Create Samples. Empty sample json files will be created. Default folder is Assets/Resources/Samples. You may 
edit these samples as you want. Make sure to update them as your data classes change.
7. In a entry point of your game, initialize Data Handler with your DataClassesConfig. Then Load every data storing class
via DataHandler. Store data classes in PlayerData, which will be used to retrieve these classes by dictionary.
Example:
            DataHandler.Init(dataClassesConfig);
            DefaultPlayerDataInfo defaultInfo = DataHandler.GetDataSync<DefaultPlayerDataInfo>();
            NotDefaultPlayerDataInfo notDefaultInfo = DataHandler.GetDataSync<NotDefaultPlayerDataInfo>();
            PlayerData data = new PlayerData(defaultInfo,notDefaultInfo);
            // BindInstance if using DI Framework otherwise pass data to where you can later have access to it

8. As your game progress, take needed data class from PlayerData, change data, and save it with dataClass.Save(or SaveAsync). 
You absolutely should save on exit/scene change and it is recommended to save periodically.
9. ???
10.PROFIT!
