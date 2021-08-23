using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class Loader {

	public static void load(string dataPath)
    {
        GameData data;
        FileStream fs = new FileStream(dataPath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        try
        {
            data = (GameData)formatter.Deserialize(fs);
            MoneyManager.setData(data.moneyManagerData);
        }
        catch (SerializationException e)
        {
            Debug.Log("Load failed: " + e.Message);
        }
        finally
        {
            fs.Close();
        }
    }
}
