using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject hitFX;

    [Header("Hit Parameters")]
    [SerializeField] int health = 30;
    [SerializeField] int scorePerHit = 100;
    [SerializeField] int amountOfDamage = 20;

    GameObject parentObject;
    ScoreBoard scoreBoard;

    public int GetAmountOfDamage()
    {
        return amountOfDamage;
    }

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AssignParentObject();

        AddRigidBody();
    }

    void AssignParentObject()
    {
        parentObject = GameObject.FindGameObjectWithTag("SpawnAtRuntime");
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        //if the enemy's health is lower than 0, call the kill process
        if (health <= 0)
            KillEnemy();
    }

    private void ProcessHit()
    {
        //if the enemy is hit, increase score.
        scoreBoard.IncreaseScore(scorePerHit);

        //Create hit particle
        GameObject particle = Instantiate(hitFX, transform.position, Quaternion.identity);
        particle.transform.parent = parentObject.transform;

        //Decrease the enemy's health
        health -= 10;
    }

    private void KillEnemy()
    {
        //if the enemy is killed, add extra point.
        scoreBoard.IncreaseScore(scorePerHit*3);

        //Create enemy death particle
        GameObject explosionChild = Instantiate(explosionFX, transform.position, Quaternion.identity);
        explosionChild.transform.parent = parentObject.transform;
        Destroy(gameObject, 0.1f);
    }
}
