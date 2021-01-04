using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoticeManager : MonoBehaviour
{
    #region SingleTon

    public static NoticeManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of notice manager found!");
        }
        instance = this;
    }

    #endregion
    public Image noticeIMG;
    public Text noticeText;

    void Start()
    {
        noticeIMG.enabled = false;
        noticeText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void noticeOnOff()
    {
        noticeIMG.enabled = true;
        noticeText.enabled = true;
        StartCoroutine("wait");

    }

    public void setText(string text)
    {
        noticeText.text = text;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        noticeIMG.enabled = false;
        noticeText.enabled = false;
    }
}
