using UnityEngine;
using UnityEngine.UI;

public class fishing : MonoBehaviour
{
    public GameObject fishingBait;
    private GameObject bait, onBaitFish;
    bool isCreated, onBait,isFishing, baitEat;
    LineRenderer lineRenderer;
    float fishingLineLength = 10f, radius = 0.5f;
    Vector3 baitPos;

    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (PlayerController.instance._animator.GetCurrentAnimatorStateInfo(0).IsName("Fishing_idle"))
        {
            isFishing = true;
        }
        else
        {
            isFishing = false;
        }

        if (isFishing)
        {
            createBait();
            controlOnBaitFish();
            attractFish();
        }

        if(isFishing && baitEat && Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Catching a fish");
            catchFish();
        }

        //if (Input.GetMouseButtonDown(0) && PlayerController.instance._animator.GetCurrentAnimatorStateInfo(0).IsName("Fishing_idle") && !baitEat)
        //{
            //print("stop fishing");
            //nonFishingState();
        //}

            PlayerController.instance.GetComponentInChildren<Image>().enabled = baitEat;
    }

    void createFishingLine()
    {
        if (bait != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, bait.transform.position);
        }
    }

    void controlOnBaitFish()
    {
        if (onBait)
        {
            onBaitFish.GetComponent<fish>().enabled = false;
            onBaitFish.transform.LookAt(bait.transform);
            if (Vector3.Distance(onBaitFish.transform.position, bait.transform.position) > radius)
            {
                onBaitFish.transform.Translate(Vector3.forward * Time.deltaTime);
            }
            else if (Vector3.Distance(onBaitFish.transform.position, bait.transform.position) <= radius)
            {
                baitEat = true;
            }
        }
    }

    void catchFish()
    {
        PlayerController.instance._animator.Play("GetFish");
        bait.GetComponent<Rigidbody>().isKinematic = false;
        onBaitFish.SetActive(false);
        onBaitFish.GetComponent<Interactable>().weight = Random.Range(1, 10);
        Interactable fish = onBaitFish.GetComponent<Interactable>();
        NoticeManager.instance.setText("成功捕捉"+fish.item.name+"。\n打開背包查收。");
        NoticeManager.instance.noticeOnOff();
        nonFishingState();
        Inventory.instance.Additem(fish);
    }

    void createBait()
    {
        if (!isCreated)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, transform.forward, out hit, fishingLineLength))
            {
                if (hit.transform.gameObject.name == "WaterSurface")
                {
                    bait = Instantiate(fishingBait, hit.point, Quaternion.identity);
                    isCreated = true;
                }
                else
                {
                    NoticeManager.instance.setText("離水面太遠了。");
                    NoticeManager.instance.noticeOnOff();
                    
                }
            }
        }
         Invoke("createFishingLine", 2f);


    }

    void nonFishingState()
    {
        if (onBaitFish != null && !baitEat)
        {
            onBaitFish.GetComponent<fish>().enabled = true;
            onBaitFish = null;
        }

        if (bait != null)
        {
            Destroy(bait);
        }

        PlayerController.instance.EnableControl();
        lineRenderer.positionCount = 0;
        onBait = false;
        isCreated = false;
        baitEat = false;
        isFishing = false;
    }

    void attractFish()
    {
        if(!onBait)
        {
            onBait = true;
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");
            int index = Random.Range(0, fishes.Length-1);
            onBaitFish = fishes[index];
        }
    }
}
