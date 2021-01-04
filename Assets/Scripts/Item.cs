using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "item")]
public class Item: ScriptableObject
{
    public new string name;
    public GameObject Model;
    public Vector3 pickUpPosition, pickUpRotation;
    public int maxDurability = 10, maxcapacity = 1;
    public float weight;

    public enum itemType
    {
        Tool,
        Food,
        Environment,
        Materials,
        SpecialItem,
        Fish

    }
    public itemType EitemType;
    public Sprite itemImage;


}
