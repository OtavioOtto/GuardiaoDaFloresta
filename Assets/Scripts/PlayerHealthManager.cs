using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthTxt;
    void Update()
    {
        healthSlider.value = currentHealth / playerHealth;
        healthTxt.text = "" + currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapons")) 
        {
            currentHealth -= 15;
        }
    }
}
