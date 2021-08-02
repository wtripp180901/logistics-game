using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager {

    private static bool paused = false;
    public static void togglePausePlay()
    {
        if (paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        paused = !paused;
    }

    private static bool doubleSpeed = false;
    public static void toggleDoubleSpeed()
    {
        if (doubleSpeed)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 2;
        }
        doubleSpeed = !doubleSpeed;
    }
}
