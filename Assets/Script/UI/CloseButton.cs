using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate () { transform.parent.gameObject.SetActive(false); });
    }
}
