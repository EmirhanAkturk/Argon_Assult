using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float timeTillExplosion = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTillExplosion);   
    }
}
