using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            SceneManager.LoadScene(4);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapons")) 
        {
            currentHealth -= 15;
        }
    }
}
