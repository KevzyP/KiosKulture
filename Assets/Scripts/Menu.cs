using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    private void OnEnable()
    {
    }

    public void EnableMenuScreen()
    {
        GameManager.Instance.suspended = true;
    }

    public void DisableMenuScreen()
    {
        GameManager.Instance.suspended = false;
    }



}
