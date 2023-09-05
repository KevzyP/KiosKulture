using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public Score score;
    public TriviaElements triviaElements;
    
    [TextArea (5, 10)]
    public string itemDescription;
    
    [TextArea (5, 10)]
    public string trivia;

    public List<string> hints;
    [TextArea (5, 10)]

    private Button _button;

    [HideInInspector] public Customers customer;
    [HideInInspector] public bool isAsked = false;
    [HideInInspector] public bool isBought = false;

    // Start is called before the first frame update
    void Awake()
    {

        this.gameObject.SetActive(true);
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => StartCoroutine(AddScore()));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => StartCoroutine(AddScore()));
    }


    private IEnumerator AddScore()
    {
        //if condition is met, make the item "invisible" and non interactable, run SetScore with 1 parameter and change is bought to true.
        //then run "portrait disappear" coroutine until it finishes, before disabling this object.
        if (isAsked && !GameManager.Instance.suspended)
        {
            _button.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            _button.interactable = false;


            score.SetScore(1);
            isBought = true;

            SetTriviaProperties();
            triviaElements.ShowElements();
            yield return customer.PortraitDisappear();
            //StartCoroutine(GameManager.Instance.WaitForTrivia());


            gameObject.SetActive(false);
            Debug.Log("Done AddScore");
        }
    }

    void SetTriviaProperties()
    {
        triviaElements.itemText.text = trivia;
        triviaElements.itemImage.sprite = GetComponent<Image>().sprite;
    }
}
