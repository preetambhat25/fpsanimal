using UnityEngine;

public class ElephantHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    public GameObject healthBarForeground;
    public Transform player;

    public GameObject collectiblePrefab; // Assign the collectible prefab in the Unity Editor

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBarRotation();
    }

    void UpdateHealthBarRotation()
    {
        healthBarForeground.transform.LookAt(player);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarForeground.transform.localScale = new Vector3(fillAmount, 1f, 1f);
    }

    void Die()
    {
        Debug.Log("Elephant died!");

        // Instantiate collectible at the position of the elephant
        Instantiate(collectiblePrefab, transform.position, Quaternion.identity);

        // Destroy the elephant GameObject
        Destroy(gameObject);
    }
}
