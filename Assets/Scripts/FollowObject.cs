using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform transformOfObjectToFollow;

    private Vector3 relativePosition;
    
    private void Start()
    {
        relativePosition = transform.position - transformOfObjectToFollow.position;
    }
    
    private void Update()
    {
        transform.position = transformOfObjectToFollow.position + relativePosition;
    }
}
