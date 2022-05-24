using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closethis : MonoBehaviour
{
    public GameObject readySetGoPanel;


    public void CloseThis()
    {
        readySetGoPanel.SetActive(false);
    }
}
