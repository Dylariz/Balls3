using System;
using System.Collections;
using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    public Renderer powerupIndicatorRenderer;
    public PowerupSettings powerupSettings;
    
    // Conditions
    [NonSerialized] public Powerup.Actions currentAction;
    [NonSerialized] public bool hasPowerup;

    private Powerup _currentPowerup;

    private void Start()
    {
        UI.GameOver += OnResetGame;
        powerupIndicatorRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentAction = other.gameObject.GetComponent<RandomPowerupAction>().actionOfPowerup;
            hasPowerup = true;
            powerupIndicatorRenderer.enabled = true;
            var materialOfIndicator = powerupIndicatorRenderer.material;

            if (_currentPowerup)
            {
                Destroy(_currentPowerup);
            }

            switch (currentAction)
            {
                case Powerup.Actions.Push:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(33/360f, 85/100f, 1));
                    _currentPowerup = gameObject.AddComponent<PushPowerup>();
                    break;
                case Powerup.Actions.Rockets:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(230/360f, 75/100f, 1));
                    _currentPowerup = gameObject.AddComponent<HomingRocketsPowerup>();
                    break;
                case Powerup.Actions.SmashAttack:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(0, 95/100f, 1));
                    _currentPowerup = gameObject.AddComponent<SmashAttackPowerup>();
                    break;
            }

            _currentPowerup.powerupSettings = powerupSettings;
            StopAllCoroutines();
            StartCoroutine(PowerupCountdownRoutine());
            
            Destroy(other.gameObject);
        }
    }
    
    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupSettings.powerupLifeTime);
        hasPowerup = false;
        powerupIndicatorRenderer.enabled = false;
        Destroy(_currentPowerup);
    }
    
    private void OnResetGame()
    {
        hasPowerup = false;
        powerupIndicatorRenderer.enabled = false;
        
        if (_currentPowerup)
        {
            Destroy(_currentPowerup);
        }
    }
}