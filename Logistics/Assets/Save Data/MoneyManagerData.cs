using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoneyManagerData {

    public readonly int money;
    public readonly float timeToGoal;
    public readonly int goalMoney;

    public MoneyManagerData(int money,float timeToGoal,int goalMoney)
    {
        this.money = money;
        this.timeToGoal = timeToGoal;
        this.goalMoney = goalMoney;
    }
}
