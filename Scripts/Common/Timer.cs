using System;
using UnityEngine;
using Zenject;

public class Timer : MonoBehaviour, IPoolable<TimerTime, IMemoryPool>
{
    public Action OnTimerContinue;
    public Action OnTimerEnds;

    public TimerTime timerTime;
    
    private MyCour myCour;
    private IMemoryPool pool;
    private TimersManager timersManager;

    private bool paused;

    [Inject]
    public void Init(TimersManager timersManager)
    {
        this.timersManager = timersManager;
    }
    
    public void Despawn()
    {
        OnTimerEnds = null;
        OnTimerContinue = null;

        if (this == null) return;
        
        pool.Despawn(this);
    }
    
    public void OnDespawned()
    {
        pool = null;
    }

    public void OnSpawned(TimerTime timerTime, IMemoryPool pool)
    {
        this.timerTime = timerTime;
        this.pool = pool;
    }
    
    public void StartTimer()
    {
        if (timersManager.HasTimer(this))
        {
            paused = false;
            return;
        }
        
        timersManager.AddTimer(this);
    }
    
    public void StopTimer()
    {
        timersManager.RemoveTimer(this);
    }
    
    public void PauseTimer()
    {
        paused = true;
    }

    public void ResetTimer()
    {
        timerTime.RefreshTime();
    }
    
    public void SetNewTime(TimerTime timerTime)
    {
        this.timerTime = timerTime;
    }

    public void ProcessTimer(float time)
    {
        if (paused)
        {
            return;
        }

        timerTime.timeRemaining -= time;

        if (timerTime.timeRemaining >= 0)
        {
            OnTimerContinue?.Invoke();
            return;
        }
        
        OnTimerEnds?.Invoke();
    }

    public string GetTime(bool includeHours)
    {
        var span = TimeSpan.FromSeconds(timerTime.timeRemaining);

        var time = "";

        if (includeHours) time += $"{FormatTime(span.Hours)}:";

        time += $"{FormatTime(span.Minutes)}:";
        time += $"{FormatTime(span.Seconds)}";

        return time;
    }

    private string FormatTime(int time)
    {
        return time < 10 ? $"0{time}" : $"{time}";
    }

    public bool IsTimerActive()
    {
        if (myCour == null) return false;
        
        return myCour.IsActive;
    }

    public class Factory : PlaceholderFactory<TimerTime, Timer>
    {
        
    }
    
    public class Pool: MonoPoolableMemoryPool<TimerTime, IMemoryPool, Timer>
    {

    }
}

[Serializable]
public class TimerTime
{
    public float initialTime;
    public float timeRemaining;

    public TimerTime(float initialTime)
    {
        this.initialTime = initialTime;
        timeRemaining = initialTime;
    }

    public TimerTime(float initialTime, float timeRemaining)
    {
        this.initialTime = initialTime;
        this.timeRemaining = timeRemaining;
    }

    public void RefreshTime()
    {
        timeRemaining = initialTime;
    }

    public float GetPassedTime()
    {
        return Math.Abs(timeRemaining - initialTime);
    }
}
