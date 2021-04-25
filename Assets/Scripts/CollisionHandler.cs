using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionEffect;
    //[SerializeField] PlayableDirector masterTimeline;
    
    float loadDelay = 1f;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.name } **Trigged with** {other.gameObject.name}");
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        DeactivePlayerComponents();

        //masterTimeline.Pause(); //pause timeline

        explosionEffect.Play();

        //Invoke("ReloadLevel", 1f); //alternative 
        StartCoroutine(ReloadLevel());
    }

    private void DeactivePlayerComponents()
    {
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
