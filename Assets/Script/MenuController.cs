using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelMenus;

    public void ShowSelectedMenu(int index)
    {
        for(int i = 0; i < panelMenus.Count; i++)
        {
            if (i == index)
                panelMenus[i].SetActive(true);
            else
                panelMenus[i].SetActive(false);
        }
    }
}
