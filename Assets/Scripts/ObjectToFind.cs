using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToFind : MonoBehaviour
{
    public List<Items> itemList;
    private bool randomized = false;

    // Start is called before the first frame update
    void Start()
    {
        AddItemsToList();

    }

    public void AddItemsToList()
    {
        //add every object with the items tag into the itemList list. and then randomize it. 
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Items"))
        {
            if (item.GetComponent<Items>().isAsked == false)
            {
                itemList.Add(item.GetComponent<Items>());
            }
        }

        if (!randomized)
        {
            RandomizeList(itemList);
            randomized = true;
        }
        Debug.Log("Done AddItemsToList");
    }

    private void RandomizeList(List<Items> list)
    {
        var count = list.Count;
        var last = count - 1;

        for (var i = 0; i < last; ++i)
        {
            var random = Random.Range(i, count);
            var temp = list[i];
            list[i] = list[random];
            list[random] = temp;
;       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
