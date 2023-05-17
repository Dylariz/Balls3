using System;
using System.Collections;
using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    public GameObject powerupIndicator;
    public PowerupSettings powerupSettings;
    
    // Conditions
    [NonSerialized] public Powerup.Actions currentAction;
    [NonSerialized] public bool hasPowerup;

    private Powerup currentPowerup;

    private void Start()
    {
        UI.GameOver += OnResetGame;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentAction = other.gameObject.GetComponent<RandomPowerupAction>().actionOfPowerup;
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            var materialOfIndicator = powerupIndicator.gameObject.GetComponent<Renderer>().material;

            if (currentPowerup)
            {
                Destroy(currentPowerup);
            }

            switch (currentAction)
            {
                case Powerup.Actions.Push:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(33/360f, 85/100f, 1));
                    currentPowerup = gameObject.AddComponent<PushPowerup>();
                    break;
                case Powerup.Actions.Rockets:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(230/360f, 75/100f, 1));
                    currentPowerup = gameObject.AddComponent<HomingRocketsPowerup>();
                    break;
                case Powerup.Actions.SmashAttack:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(0, 95/100f, 1));
                    currentPowerup = gameObject.AddComponent<SmashAttackPowerup>();
                    break;
            }

            currentPowerup.powerupSettings = powerupSettings;
            StopAllCoroutines();
            StartCoroutine(PowerupCountdownRoutine(currentPowerup));
            
            Destroy(other.gameObject);
        }
    }
    
    private IEnumerator PowerupCountdownRoutine(Powerup typoOfPowerup)
    {
        yield return new WaitForSeconds(powerupSettings.powerupLifeTime);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        Destroy(currentPowerup);
    }
    
    private void OnResetGame()
    {
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        
        if (currentPowerup)
        {
            Destroy(currentPowerup);
        }
    }
}