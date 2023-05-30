using UnityEngine;

[CreateAssetMenu(fileName = "Powerup Settings", menuName = "Scriptable Objects/PowerupSettings")]
public class PowerupSettings : ScriptableObject
{
    [Header("General")]
    public float powerupLifeTime = 8;
    
    [Header("Push")]
    public float pushStrength = 25;
    
    [Header("SmashAttack")]
    public float smashAttackVerticalImpulse = 10;
    public float smashAttackImpulse = 20;
    public int countOfSmashAttacks = 4;
    
    [Header("HomingRockets")]
    public GameObject rocketPrefab;
    public float rocketCooldown = 0.7f;
    public int countOfRocketsPerAttack = 4;
}