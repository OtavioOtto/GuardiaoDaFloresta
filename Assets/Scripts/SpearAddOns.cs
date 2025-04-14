using UnityEngine;

public class SpearAddOns : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    private CapsuleCollider spearCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spearCollider = gameObject.GetComponent<CapsuleCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {

            spearCollider.enabled = false;
            rb.isKinematic = true;
            
            if (other.gameObject.GetComponent<EnemyHealtManager>() != null)
            {
                EnemyHealtManager enemy = other.gameObject.GetComponent<EnemyHealtManager>();
                enemy.TakeDamage(damage);
            }

            
        }
    }
}
