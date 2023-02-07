using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
public class Target : MonoBehaviour
{
    public int health = 5;

    public TMP_Text textHealth;

    private void Update()
    {
        textHealth.text = "Health" + health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
