using UnityEngine;

public class SpearAddOns : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    private bool targethit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (targethit)
            return;
        else
            targethit = true;

        if (other.gameObject.GetComponent<EnemyHealtManager>() != null)
        {
            EnemyHealtManager enemy = other.gameObject.GetComponent<EnemyHealtManager>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        rb.isKinematic = true;
        Invoke(nameof(DestroySpear), 3);
    }

    private void DestroySpear() 
    {
        Destroy(gameObject);
    }
}
