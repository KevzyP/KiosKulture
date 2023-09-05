using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    public Customers customer;
    public Image panel;
    public Image hintImage;

    public void ShowHint()
    {
        var customerItemSprite = customer.wantedItem.GetComponent<Image>().sprite;

        panel.GetComponent<CanvasGroup>().alpha = 1;
        hintImage.sprite = customerItemSprite;
    }

}
