using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class Saver {

	public static void save()
    {
        FileStream fs = new FileStream("SaveData/savedata.dat", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        try
        {
            formatter.Serialize(fs, new GameData());
        }
        catch (SerializationException e)
        {
            Debug.Log("Save failed: " + e.Message);
        }
        finally
        {
            fs.Close();
        }
    }
}
