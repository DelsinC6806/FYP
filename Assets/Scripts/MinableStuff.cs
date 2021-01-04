
using UnityEngine;

public class MinableStuff : MonoBehaviour
{
    public int Health = 5;
    public GameObject product, DeadState;
    public AudioSource audiosource;
    public AudioClip HittingSound, BreakSound;
    public string requiredTool;
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == requiredTool && PlayerController.instance.isAttacking)
        {
            Health--;
            PlayerController.instance.isAttacking = false;
            audiosource.PlayOneShot(HittingSound);
        }

        if(Health == 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Collider>().enabled = false;
        audiosource.PlayOneShot(BreakSound);
        if (DeadState != null)
        {
            Instantiate(DeadState, transform.position, transform.rotation);
        }
        GetComponent<MeshRenderer>().enabled = false;

        int rand = Random.Range(1, 5);
        for (int i = 0; i < rand; i++)
        {
            var position = new Vector3(Random.Range(-2, 2), 5, Random.Range(-2, 2));
            GameObject productclone = Instantiate(product,transform.position+position, Quaternion.identity);
        }
    }

}
