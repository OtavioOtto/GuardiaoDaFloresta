using System;
using UnityEngine;

public class SpearAddOns : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    private CapsuleCollider spearCollider;
    public SpearShooter shooter;
    public GameObject trail;
    public bool isHit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spearCollider = gameObject.GetComponent<CapsuleCollider>();
        isHit = false;
    }

    private void Update()
    {
        if(!shooter.playerHasSpear && !isHit && !shooter.isReturning)
            transform.forward = Vector3.Slerp(transform.forward, rb.linearVelocity.normalized, Time.deltaTime);
        if (shooter.playerHasSpear && !isHit && !shooter.isReturning)
            transform.rotation = Camera.main.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            isHit = true;
            spearCollider.enabled = false;
            rb.isKinematic = true;
            transform.parent = other.transform;
            
            if (other.gameObject.GetComponent<EnemyHealtManager>() != null)
            {
                EnemyHealtManager enemy = other.gameObject.GetComponent<EnemyHealtManager>();
                enemy.TakeDamage(damage);
                trail.GetComponent<TrailRenderer>().enabled = false;
            }

            if (other.gameObject.GetComponent<BaseEnemyAI>() != null)
            {
                BaseEnemyAI enemy = other.gameObject.GetComponent<BaseEnemyAI>();
                enemy.TakeDamage(damage);
                trail.GetComponent<TrailRenderer>().enabled = false;
            }


        }
    }
}
