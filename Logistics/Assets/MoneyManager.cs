using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyManager {

    private static int money = 0;

    public static void addMoney(int amount) { money += amount; Debug.Log(money); }
}
