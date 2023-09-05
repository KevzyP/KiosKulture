using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriviaElements: MonoBehaviour
{
    public Image itemImage;
    public Image panel;
    public TMP_Text itemText;
    public Animator anim;


    public void ShowElements()
    {
        GameManager.Instance.suspended = true;
        anim.SetTrigger("Appear");
    }

    public void HideElements()
    {
        StartCoroutine(WaitForAnimation());
    }

    public IEnumerator WaitForAnimation()
    {
        anim.SetTrigger("Disappear");
        yield return new WaitForSeconds( anim.GetCurrentAnimatorStateInfo(0).length+1);
        
        //panel.gameObject.SetActive(false);
        GameManager.Instance.suspended = false;
    }
}
