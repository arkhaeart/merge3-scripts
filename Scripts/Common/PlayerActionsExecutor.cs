using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerActionsExecutor 
{
    MyCour executing;
    public Queue<System.Action> actions = new Queue<System.Action>();
    [Inject]
    public PlayerActionsExecutor(CoroutineHelper coroutineHelper)
    {
        executing = new MyCour(coroutineHelper, Executing);
    }
    public void QueueAction(System.Action action)
    {
        actions.Enqueue(action);
        if(!executing.IsActive)
        {
            executing.Run();
        }
    }
    IEnumerator Executing()
    {
        while(true)
        {
            yield return null;
            while(actions.Count>0)
            {
                var action = actions.Dequeue();
                try
                {
                    action?.Invoke();
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
