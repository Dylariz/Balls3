using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform transformOfObjectToFollow;

    private Vector3 _relativePosition;
    
    private void Start()
    {
        _relativePosition = transform.position - transformOfObjectToFollow.position;
    }
    
    private void Update()
    {
        transform.position = transformOfObjectToFollow.position + _relativePosition;
    }
}
