using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class test_info : MonoBehaviour
{

    public static void interactwith(Collider2D hits)
    {
        if(hits != null)
        {
            GameObject.Find("interact").GetComponent<TextMeshProUGUI>().text = "Interacting with: " + hits.gameObject.name;
        }
        else
        {
            GameObject.Find("interact").GetComponent<TextMeshProUGUI>().text = "Interacting with: nothing";
        }
    }

    public void FixedUpdate()
    {
        GameObject.Find("time").GetComponent<TextMeshProUGUI>().text = "Time: " + daynight.clocktime;
        GameObject.Find("daytype").GetComponent<TextMeshProUGUI>().text = "time period: " + daynight.timeperiod;
    }
    public static void holding(Items item)
    {
        if(item != null){
            Debug.Log(item.itemName);
            GameObject.Find("holding").GetComponent<TextMeshProUGUI>().text = "holding: " + item.itemName;
        }
        else
        {
            GameObject.Find("holding").GetComponent<TextMeshProUGUI>().text = "holding: nothing"; 
        }
        
    }

}
