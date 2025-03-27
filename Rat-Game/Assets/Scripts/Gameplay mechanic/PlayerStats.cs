using System;
using UnityEngine;
using UnityEngine.SceneManagement;  // Ensure this is included for Scene management
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

    private int unlockedLevel = 1;  // Starting unlocked level (Level 1 is always unlocked)

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // Prevent overheal
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Get the unlocked level from PlayerPrefs
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
            SceneManager.LoadScene(5);  // Scene 5 for death
        }

        audioSource.PlayOneShot(clip);
    }

    // Method to unlock the next level
    public void UnlockNextLevel()
    {
        unlockedLevel = Mathf.Min(unlockedLevel + 1, 3); // Unlock next level (up to level 3)
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);  // Save the unlocked level in PlayerPrefs
    }

    // Method to be called when the EndLevelTrigger is triggered
    public void EndLevel()
    {
        SceneManager.LoadScene(1);  // Load Scene 1 (Main Menu or start)
    }

    // Add OnTriggerEnter for EndLevelTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLevel"))
        {
            EndLevel(); // Call EndLevel to switch to Scene 1
        }

        if (other.CompareTag("LevelUnlocked"))
        {
            UnlockNextLevel(); // Unlock the next level when LevelUnlockTrigger is entered
        }
    }
}
