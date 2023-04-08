using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocketController : MonoBehaviour
{
    private float speed = 20;
    private float strength = 15;
    public GameObject target;
    

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.Translate((target.gameObject.transform.position - transform.position).normalized * (speed * Time.deltaTime), Space.World);
            transform.forward = (target.gameObject.transform.position - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.gameObject.transform.position - transform.position).normalized * strength, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Rocket"))
        {
            Destroy(gameObject);
        }
    }
}
