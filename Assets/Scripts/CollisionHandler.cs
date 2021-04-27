using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] HealthBar healthBar;

    float loadDelay = 1f;

    void OnTriggerEnter(Collider other)
    {
        // Default amount of damage
        int amountOfDamage = 10;

        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            amountOfDamage = enemy.GetAmountOfDamage();
        }

        healthBar.DecreaseHealth(amountOfDamage);
        
        if (healthBar.GetIsDeath())
            StartCrashSequence();
    }

    public void StartCrashSequence()
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
