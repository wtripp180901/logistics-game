using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public MoneyManagerData moneyManagerData;

	public GameData()
    {
        moneyManagerData = MoneyManager.getSaveData();
    }
}
