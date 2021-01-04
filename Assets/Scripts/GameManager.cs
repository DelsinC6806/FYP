
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }
        instance = this;
    }
    #endregion
    Interactable[] interactables;
    public Interactable[] fishes;
   
    void Start()
    {
 
    }

   
    void Update()
    {
        interactables = FindObjectsOfType<Interactable>();
        for(int i = 0; i < interactables.Length; i++)
        {
            if(interactables[i].currentQuantity == 0)
            {
                Destroy(interactables[i].gameObject);
            }
        }
    }

   
}
