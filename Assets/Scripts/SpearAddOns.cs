using UnityEngine;

public class SpearAddOns : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    public bool itHit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        itHit = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            rb.isKinematic = true;
            
            if (other.gameObject.GetComponent<EnemyHealtManager>() != null)
            {
                EnemyHealtManager enemy = other.gameObject.GetComponent<EnemyHealtManager>();
                enemy.TakeDamage(damage);
                itHit = true;
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemies"))
            if (itHit)
                rb.isKinematic = false;
    }
}
