using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject hitEffect;

    [SerializeField] Transform parent;

    [SerializeField] int health = 30;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int amountOfDamage = 10;

    ScoreBoard scoreBoard;

    public int GetAmountOfDamage()
    {
        return amountOfDamage;
    }

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        GameObject particle = Instantiate(hitEffect, transform.position, Quaternion.identity);
        particle.transform.parent = parent;

        health -= 10;
        if(health <= 0)
        {
            scoreBoard.IncreaseScore(scorePerHit);
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject particle = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        particle.transform.parent = parent;
        Destroy(gameObject, 0.1f);
    }
}
