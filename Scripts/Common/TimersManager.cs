using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimersManager : ITickable
{
    private float time;
    private float interpolationPeriod = 1;
    private List<Timer> processedTimers = new List<Timer>();
    
    public void AddTimer(Timer timer)
    {
        timer.timerTime.timeRemaining = Mathf.Ceil(timer.timerTime.timeRemaining);
        timer.ProcessTimer(0);
        processedTimers.Add(timer);
    }
    
    public void RemoveTimer(Timer timer)
    {
        processedTimers.Remove(timer);
    }

    public void Tick()
    {
        time += Time.deltaTime;

        if (time < interpolationPeriod) return;

        if (processedTimers.Count != 0)
        {
            processedTimers.ForEach(timer => timer.ProcessTimer(Mathf.Floor(time)));
        }

        time -= interpolationPeriod;
    }

    public bool HasTimer(Timer timer)
    {
        return processedTimers.Exists(t => timer == t);
    }
}
