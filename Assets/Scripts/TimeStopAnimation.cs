using UnityEngine;

public class TimeStopAnimation : MonoBehaviour
{
    private TimeManager timeManager;

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    public void StopTimeAnimation(float stopTimeDuration)
    {
        timeManager.stopTimeDuration = stopTimeDuration;
        timeManager.StopTime();
    }
}
