using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class WorldUIStateManager {

    private static List<GameObject> currentWorldUI = new List<GameObject>();

    private static Dictionary<TransportHubFeature, Text> hubTexts = new Dictionary<TransportHubFeature, Text>();
    private static Dictionary<TransportHubFeature, GameObject> hubGOs = new Dictionary<TransportHubFeature, GameObject>();
    public static void createStopUI(GameObject asset, TransportHubFeature hub, string text)
    {
        Text existingText;
        if (hubTexts.TryGetValue(hub, out existingText))
        {
            existingText.text = existingText.text + ", " + text;
        }
        else
        {
            GameObject worldUI = createWorldUI(asset, hub.parent.position);
            Text txt = worldUI.GetComponent<Text>();
            txt.text = text;
            hubTexts.Add(hub, txt);
            hubGOs.Add(hub, worldUI);
        }
    }

    public static void removeStopUIAtHub(TransportHubFeature hub)
    {
        Text text;
        if(hubTexts.TryGetValue(hub,out text))
        {
            if (text.text.Contains(","))
            {
                //Stopped more than once, truncate to previous stops only
                char[] charArray = text.text.ToCharArray();
                int commaIndex = charArray.Length - 3;
                while(charArray[commaIndex] != ',')
                {
                    commaIndex -= 1;
                }
                text.text = text.text.Substring(0, commaIndex);
            }
            else
            {
                //Stopped at once, remove entirely
                GameObject go;
                hubGOs.TryGetValue(hub, out go);
                currentWorldUI.Remove(go);
                Object.Destroy(go);
                hubTexts.Remove(hub);
                hubGOs.Remove(hub);
            }
        }
    }

    public static GameObject createWorldUI(GameObject asset, Vector2 position)
    {
        GameObject worldUI = Object.Instantiate(asset, Assets.assets.canvas);
        worldUI.GetComponent<WorldUIScript>().worldPosition = new Vector3(position.x, position.y, -0.3f);
        currentWorldUI.Add(worldUI);
        return worldUI;
    }

    public static void clearConfirmationButtons()
    {
        for(int i = currentWorldUI.Count - 1;i >= 0;i--)
        {
            if (currentWorldUI[i].tag == "confirmationButton")
            {
                Object.Destroy(currentWorldUI[i]);
                currentWorldUI.RemoveAt(i);
            }
        }
    }

    public static void clear()
    {
        hubTexts.Clear();
        hubGOs.Clear();
        for(int i = 0;i < currentWorldUI.Count; i++)
        {
            Object.Destroy(currentWorldUI[i]);
        }
        currentWorldUI.Clear();
    }
}
