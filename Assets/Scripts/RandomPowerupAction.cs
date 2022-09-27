using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupActions { Push, Rockets, SmashAttack }
public class RandomPowerupAction : MonoBehaviour
{
    public PowerupActions actionOfPowerup;
    private Material _powerupMaterial;
    private int _mainTex;

    // Start is called before the first frame update
    void Start()
    {
        _mainTex = Shader.PropertyToID("_MainTex");
        _powerupMaterial = GetComponent<Renderer>().material;
        switch (Random.Range(0,3))
        {
            case 0:
                actionOfPowerup = PowerupActions.Push;
                // Yellow
                _powerupMaterial.SetTextureOffset(_mainTex, new Vector2(0, 0.9f));
                break;
            case 1:
                actionOfPowerup = PowerupActions.Rockets;
                // Blue
                _powerupMaterial.SetTextureOffset(_mainTex, new Vector2(0, 0.8f));
                break;
            case 2:
                actionOfPowerup = PowerupActions.SmashAttack;
                // Red
                _powerupMaterial.SetTextureOffset(_mainTex, new Vector2(0, 0.7f));
                break;
        }
    }
}
