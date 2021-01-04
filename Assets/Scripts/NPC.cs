using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;

    public Text interactText;
    public float trigger;
    public static bool Talking;
    float radius = 3f;
    Quaternion ogPos;

    void Start()
    {
      ogPos = this.transform.rotation;
    }

    void Update()
    {
        TriggerDialogue();
        returnOGpos();
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

    public void returnOGpos()
    {
        if (Talking == false)
        {
            this.transform.rotation = ogPos;
        }
    }

    public void triggerMission()
    {
        if(dialogue.sentences.Length == trigger)
        {

        }
    }
}
