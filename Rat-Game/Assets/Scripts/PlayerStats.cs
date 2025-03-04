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
            SceneManager.LoadScene(3);
        }
        
        audioSource.PlayOneShot(clip);
    }
}
