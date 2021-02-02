using UnityEngine;
using UnityEngine.UI;

public class AnswerBoxController : MonoBehaviour
{
    #region Singleton
    public static AnswerBoxController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of answer box found!");
        }
        instance = this;
    }
    #endregion
    public GameObject AnswerBox;
    public Text yesText; //add in the inspector
    public Text NoText;
    public Image yesBG;
    public Image noBG;

    float answer;
    

    void Start()
    {
        AnswerBox.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AnswerBox.activeInHierarchy == true)
        {
            PlayerController.instance.DisableControl();
        }

        if (NPC.missionTriggered)
        {
            popAnswerBox();
        }
    }

    public void ControlText(string yesTxt, string noTxt)
    {
        AnswerBox.SetActive(true);
        answer = 0;
        yesText.text = yesTxt;
        NoText.text = noTxt;
    }

    public void popAnswerBox()
    {
        AnswerBox.SetActive(true);
    }

}
