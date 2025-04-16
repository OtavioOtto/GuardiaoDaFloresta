using UnityEngine;
using UnityEngine.UI;

public class EnemyHealtManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject spear;
    [SerializeField] private Rigidbody spearRB;

    private void Update()
    {
        sliderHealth.value = currentHealth / health;
    }
    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            spear.GetComponent<TrailRenderer>().enabled = true;
            spear.GetComponent<CapsuleCollider>().enabled = true;
            Destroy(gameObject);
            Destroy(slider);
            spearRB.isKinematic = false;    
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            slider.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            slider.SetActive(false);
    }
}
