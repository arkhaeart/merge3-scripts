using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Configs/FiguresWithdraw")]
public class FiguresWithdrawConfig : ScriptableObject
{
    public Form[] forms;
    public Dictionary<int, int> damageDict;
    public void Initialize()
    {
        damageDict = new Dictionary<int, int>();
        foreach(var form in forms)
        {
            damageDict.TryAdd(form.figureCount, form.damage);
        }
    }
    [System.Serializable]
    public class Form
    {
        public int figureCount;
        public int damage;
    }
}
