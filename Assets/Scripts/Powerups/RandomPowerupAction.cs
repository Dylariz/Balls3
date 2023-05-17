using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPowerupAction : MonoBehaviour
{
    [NonSerialized] public Powerup.Actions actionOfPowerup;
    private Material _powerupMaterial;
    private int _mainTex;

    private void Start()
    {
        _mainTex = Shader.PropertyToID("_MainTex");
        _powerupMaterial = GetComponent<Renderer>().material;
        actionOfPowerup = (Powerup.Actions)Random.Range(0, 3);
        switch (actionOfPowerup)
        {
            case Powerup.Actions.Push:
                // Yellow
                _powerupMaterial.SetTextureOffset(_mainTex, new Vector2(0, 0.9f));
                break;
            case Powerup.Actions.Rockets:
                // Blue
                _powerupMaterial.SetTextureOffset(_mainTex, new Vector2(0, 0.8f));
                break;
            case Powerup.Actions.SmashAttack:
                // Red
                _powerupMaterial.SetTextureOffset(_mainTex, new Vector2(0, 0.7f));
                break;
        }
    }
}
