using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PermanentUIManager {

    private static Slider goalSlider;
    private static Text goalText;

    public static void initialise()
    {
        goalSlider = Assets.assets.goalBarPrefab.GetComponent<Slider>();
        goalText = Assets.assets.goalBarPrefab.transform.Find("Text").GetComponent<Text>();
    }

    public static void updateGoalBar(int money,int goal)
    {
        goalText.text = money + " / " + goal;
        goalSlider.value = (float)money / (float)goal;
    }
}
