using UnityEngine;

public class HomingRocketController : MonoBehaviour
{
    public GameObject target;

    public float speed = 20;
    public float strength = 15;

    private void Update()
    {
        if (target)
        {
            transform.Translate(
                (target.gameObject.transform.position - transform.position).normalized * (speed * Time.deltaTime),
                Space.World);

            transform.forward = (target.gameObject.transform.position - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(
                (other.gameObject.transform.position - transform.position).normalized * strength, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Rocket"))
        {
            Destroy(gameObject);
        }
    }
}