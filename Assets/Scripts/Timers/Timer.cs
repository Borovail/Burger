using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    protected float duration = 5f;
    private float elapsedTime;
    private bool isRunning;

    public event Action OnTimerComplete;

    public virtual void StartTimer(float newDuration = -1f)
    {
        if (newDuration > 0)
            duration = newDuration;

        isRunning = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    protected virtual void FinishTimer()
    {
        isRunning = false;
        OnTimerComplete?.Invoke();
    }

    private void Update()
    {
        if (!isRunning) return;
        UpdateTimer();
    }

    protected virtual void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            FinishTimer();
        }
    }
}