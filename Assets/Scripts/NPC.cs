using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    public Text interactText;
    public static bool missionTriggered; //trigger answerbox
    public static bool missionRecieved;  //player is/not recieved the mission
    public static bool Talking; //is NPC talking to Player
    public static int currentSentence; 
    float radius = 3f;
    Quaternion ogPos;

    void Start()
    {
      missionTriggered = false;
        missionRecieved = false;
      ogPos = this.transform.rotation;
    }

    void Update()
    {
        TriggerDialogue();
        returnOGpos();
        triggerMission();
    }

    public void TriggerDialogue()
    {
        if (Talking == false)
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
            
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.tag == "Player")
                {
                    interactText.enabled = true;
                    interactText.text = "按 F 與" + dialogue.name+"開始對話";
                    if (Input.GetKeyDown(KeyCode.F) && Talking == false)
                    {
                        Talking = true;
                        
                        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                        this.transform.LookAt(col.transform);
                        PlayerController.instance._animator.SetBool("run", false);
                        PlayerController.instance.DisableControl();
                    }
                }
            }
        }
    }

    void returnOGpos()
    {
        if (Talking == false)
        {
            this.transform.rotation = ogPos;
        }
    }

    void triggerMission()
    {
        if(currentSentence == dialogue.trigger && dialogue.trigger >= 0)
        {
            missionTriggered = true;
        }
    }

    void rewardPlayer()
    {

    }
}
