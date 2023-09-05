using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    
    public List<Customers> customers;
    [Space]
    public ObjectToFind toFind;
    public GameObject customerPrefab;

    public int customerLimit;
    public int customerAmount;
    public int currentCustomerAmount;

    // Start is called before the first frame update
    void Start()
    {
        //set variables, start first spawner coroutine
        currentCustomerAmount = customerAmount;
        StartCoroutine(FirstSpawner());  
    }

    public IEnumerator FirstSpawner()
    {
        //start and wait for "spawn initial customers" coroutine, and then start the "show customers" coroutine.
        yield return SpawnInitialCustomers();
        StartCoroutine(ShowCustomers());

        yield return null;
        Debug.Log("Done DoneFirstSpawner");
    }

    public IEnumerator SpawnInitialCustomers()
    {
        //spawns a customer prefab, set its parent to the object attached to this script, and then set that customer's alpha to 0. 
        //also add the prefab into the customers list. repeat this until it reaches the customer limit
        for (int d1 = 0; d1 < customerLimit; d1++)
        {
            var currentPrefab = Instantiate(customerPrefab, Vector3.zero, Quaternion.identity);
            currentPrefab.transform.SetParent(gameObject.transform);
            currentPrefab.GetComponent<CanvasGroup>().alpha = 0;
            currentPrefab.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            currentPrefab.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 1);

            customers.Add(currentPrefab.GetComponent<Customers>());
            yield return null;
        }

        Debug.Log("Done DoneSpawnInitialCustomers");
    }

    //run and wait for the "portrait appear" coroutine until it finishes and if it meets the conditions, repeat for the amount of customers inside the customer list.
    //and also decrement current customer amount by 1 every time it loops
    public IEnumerator ShowCustomers()
    {
        for (int d1 = 0; d1 < customers.Count; d1++)
        {

            if (customers[d1].isBuying == false && currentCustomerAmount > 0)
            {

                yield return GameManager.Instance.WaitForTrivia();

                currentCustomerAmount--;
                yield return customers[d1].PortraitAppear();
            }
        }
        Debug.Log("Done CheckForCustomers");
    }

}
