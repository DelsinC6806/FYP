
using UnityEngine;
using UnityEngine.UI;
public class MissionManager : MonoBehaviour
{
    #region SingleTon

    public static MissionManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Player found!");
        }
        instance = this;
    }

    #endregion

    Text missionTitle;

    void Start()
    {
        missionTitle = GetComponentInChildren<Text>();
        missionTitle.text = "任務:找到村長.";
    }

    void Update()
    {
        
    }

    public void controlMissionTitle(string title)
    {
        missionTitle.text = "任務:" + title;
    }
    
    public void RewardPlayer(Interactable a,Interactable b)
    {
        Inventory.instance.Additem(a);
        Inventory.instance.Additem(b);
    }
}
