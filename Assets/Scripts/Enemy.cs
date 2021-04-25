using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] Transform parent;

    private void OnParticleCollision(GameObject other)
    {
        GameObject particle;
        particle = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        particle.transform.parent = parent;
        Destroy(gameObject, 0.1f);
    }
}
