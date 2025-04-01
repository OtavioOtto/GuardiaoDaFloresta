using UnityEngine;
using UnityEngine.UI;

public class EnemyHealtManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private GameObject slider;
    private void Update()
    {
        sliderHealth.value = currentHealth / health;
    }
    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(slider);
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
