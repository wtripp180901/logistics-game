using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyManager {

    private static int money = 0;
    private static float timeToGoal = 120f;
    private static int goalMoney = 100;


    public static void addMoney(int amount)
    {
        money += amount;
        ModelReciever.updateGoalBar(money,goalMoney);
    }

    public static void Update()
    {
        timeToGoal -= Time.deltaTime;
        if(timeToGoal <= 0)
        {
            if(money >= goalMoney)
            {
                money -= goalMoney;
                getNextGoal();
                getNextTimeToGoal();
                ModelReciever.updateGoalBar(money,goalMoney);
            }
            else
            {
                Debug.Log("game over");
            }
        }
    }

    private static void getNextGoal()
    {
        goalMoney = (int)(goalMoney * 1.1);
    }

    private static void getNextTimeToGoal()
    {
        timeToGoal = 10f;
    }
}
