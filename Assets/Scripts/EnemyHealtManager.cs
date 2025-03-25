using UnityEngine;

public class EnemyHealtManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float health;
    public void TakeDamage(int damage) 
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
