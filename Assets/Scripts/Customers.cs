using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Customers : MonoBehaviour
{
    public Image hintImage;
    public TMP_Text descriptionBox;
    
    public float customerTimer;
    
    [HideInInspector] public CustomerController controllerRef;
    [HideInInspector] public ObjectToFind items;
    [HideInInspector] public Items wantedItem;
    [HideInInspector] public bool isBuying = false;


    private void Start()
    {
        controllerRef = FindObjectOfType<CustomerController>();
        items = FindObjectOfType<ObjectToFind>();
    }


    public IEnumerator PortraitAppear()
    {
        //run these coroutines, set isbuying to true and remove the first entry of the current item list
        yield return SetCustomerItem();
        StartCoroutine(StartCustomerTimer());
        StartCoroutine(FadeIn());

        isBuying = true;

        items.itemList.RemoveAt(0);
        yield return new WaitForSeconds(1f);
        Debug.Log("Done PortraitAppear");
    }

    public IEnumerator PortraitDisappear()
    {
        //hides the customer portrait and starts the "show customers" coroutine, which is the "spawner" 

        StopCoroutine(FadeIn());
        StopCoroutine(StartCustomerTimer());
        yield return GameManager.Instance.WaitForTrivia();
        yield return FadeOut();

        isBuying = false;

        
        StartCoroutine(controllerRef.ShowCustomers());

        yield return null;

        Debug.Log("Done PortraitDisappear");
    }

    IEnumerator FadeIn()
    {
        float counter = 0f;
        float duration = 2f;

        var alpha = GetComponent<CanvasGroup>();

        while (counter < duration)
        {
            counter += Time.deltaTime;
            alpha.alpha += counter / duration;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float counter = 0f;
        float duration = 1f;

        var alpha = GetComponent<CanvasGroup>();


        while (counter < duration)
        {
            counter += Time.deltaTime;
            alpha.alpha -= counter / duration;
            yield return null;
        }

        hintImage.GetComponentInParent<CanvasGroup>().alpha = 0;
        hintImage.sprite = null;

    }

    public IEnumerator SetCustomerItem()
    {
        //set the wanted item variable to the first entry of the item list. assign references, set is asked to true, and set the textbox to the item's description
        wantedItem = items.itemList[0];

        wantedItem.customer = gameObject.GetComponent<Customers>();

        wantedItem.isAsked = true;

        descriptionBox.SetText("" + wantedItem.itemDescription);
        
        yield return null;
    }
    
    public IEnumerator StartCustomerTimer()
    {
        //start the timer after spawning. every x seconds the textbox will update with a better description.
        //if the timer reaches 0, it will display the item's sillhouete instead.
        var curTimer = customerTimer;

        while (curTimer > 0 && GameManager.Instance.suspended == false)
        {
            var hintIndex = 0;
            curTimer -= Time.deltaTime;

            if (curTimer <= 20 && hintIndex == 0)
            {
                descriptionBox.SetText("" + wantedItem.hints[hintIndex]);
                hintIndex++;
            }

            if (curTimer <= 10 && hintIndex == 1)
            {
                descriptionBox.SetText("" + wantedItem.hints[hintIndex]);
                hintIndex++;
            }

            if (curTimer <= 0 && hintIndex == 2)
            {
                descriptionBox.SetText("");
                hintImage.GetComponentInParent<CanvasGroup>().alpha = 1;
                hintImage.sprite = wantedItem.GetComponent<Image>().sprite;
            }
            yield return null;  
        }
        Debug.Log("Done StartCustomerTimer");
    }

}
