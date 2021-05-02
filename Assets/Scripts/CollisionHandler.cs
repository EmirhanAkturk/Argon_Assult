using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;
    [SerializeField] HealthBar healthBar;
    
    [Header("Win-Lose Panels")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;


    WaitForSeconds winDelayTime;
    WaitForSeconds loseDelayTime;

    private void Start()
    {
        winDelayTime = new WaitForSeconds(4f);
        loseDelayTime = new WaitForSeconds(1f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Default amount of damage
        int amountOfDamage = 10;

        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            amountOfDamage = enemy.GetAmountOfDamage();
        }

        if(other.tag == "WinCheck")
        {
            Debug.Log("WinCheck");
            PlayerControls playerControl = GetComponent<PlayerControls>();
            playerControl.IsControlActive = false;
            playerControl.SetLastInputInformation();

            StartCoroutine(OpenPanel(winDelayTime, true));
        }

        healthBar.DecreaseHealth(amountOfDamage);

        if (healthBar.GetIsDeath())
            StartCrashSequence();
    }

    public void StartCrashSequence()
    {
        DeactivePlayerComponents();

        //masterTimeline.Pause(); //pause timeline

        explosionFX.GetComponent<AudioSource>().Play();
        explosionFX.GetComponent<ParticleSystem>().Play();

        //Invoke("ReloadLevel", 1f); //alternative 
        StartCoroutine(OpenPanel(loseDelayTime, false));
    }

    private void DeactivePlayerComponents()
    {
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator OpenPanel(WaitForSeconds delayTime, bool isWin)
    {
        yield return delayTime;

        if (isWin) { 
            winPanel.SetActive(true);
            losePanel.SetActive(false);
        }
        else{ 
            losePanel.SetActive(true);
            winPanel.SetActive(false);
        }
    }
}
