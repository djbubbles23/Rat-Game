using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;      // max health
    
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    
    private AudioSource audioSource;     // player audiosource
    private float currentHealth;        // current health of the player

    public int score;                   // player score
    public Text scoreText;              // score text object

    public Image healthBar;

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // Prevent overheal
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        
        AudioClip clip = takeDamageSound;

        if (currentHealth <= 0)
        {
            Debug.Log("PLAYER DIED!");
            clip = deathSound;
            SceneManager.LoadScene(2);
        }
        
        audioSource.PlayOneShot(clip);
    }

    /*
    public int dealDamage(GameObject dice1, GameObject dice2, GameObject dice3){
        int[] diceRolls = new int[3];

        if (dice1 != null)
        {
            for (int i=0; i<dice1.)
        }

        return diceRoll1 + diceRoll2 + diceRoll3;
    }
    */
}
