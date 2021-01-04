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
    void Update()
    {
        if (AnswerBox.activeInHierarchy == true)
        {
            ControlChoice();
            Answer();
            PlayerController.instance.DisableControl();
        }
    }

    public void ControlText(string yesTxt, string noTxt)
    {
        AnswerBox.SetActive(true);
        answer = 0;
        yesText.text = yesTxt;
        NoText.text = noTxt;
    }

    void ControlChoice()
    {
        if(answer == 0)
        {
            yesBG.color = Color.green;
            noBG.color = Color.white;
        }
        else
        {
            yesBG.color = Color.white;
            noBG.color = Color.green;
        }
    }

    public void Answer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            answer -= 1;
            if (answer <= 0)
            {
                answer = 1;
            }

        }else if (Input.GetKey(KeyCode.S))
        {
            answer += 1;
            if (answer >= 1)
            {
                answer = 0;
            }
        }
    }
}
