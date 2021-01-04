using UnityEngine;

public class FishingPole : Interactable
{
    GameObject tip;
    float distanceToFish = 2f;
    private void Start()
    {
        tip = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
    }

    public override void use()
    {
        if (seaCheck() && PlayerController.instance._animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            PlayerController.instance._animator.SetTrigger("Fish");
            audioSource.PlayDelayed(0.7f);
            PlayerController.instance.DisableControl();
        }
        else if(!seaCheck())
        {
            NoticeManager.instance.setText("離水面太遠了。");
            NoticeManager.instance.noticeOnOff();
        }
    }



    bool seaCheck()
    {
        GameObject player = GameObject.Find("Player");
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, distanceToFish))
        {
            if (hit.transform.gameObject.tag == "Water")
            {
                return true;
            }
        }
        return false;
    }

    public override void OnCollisionStay(Collision col)
    {
        base.OnCollisionStay(col);
        if (pickedUp)
        {
            tip.SetActive(false);
        }
    }
}



  
