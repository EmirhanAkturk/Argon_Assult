using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] Transform parent;
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
        KillEnemy();
    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }

    private void KillEnemy()
    {
        GameObject particle = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        particle.transform.parent = parent;
        Destroy(gameObject, 0.1f);
    }
}
