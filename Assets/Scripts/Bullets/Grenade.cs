using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region Variables
    [Header("Explosion")]
    [SerializeField] private float delay = 3f;
    private float countDown;

    [SerializeField] private float radius = 5f;
    [SerializeField] private float ExoplosionForze = 70f;
    private bool exploted = false;

    [SerializeField] private GameObject ExplosionEffect;
    #endregion


    private void Start()
    {
        countDown = delay;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0 && !exploted)
        {
            exploted = true;
            Exploted();
        }
    }

    private void Exploted()
    {
        Instantiate(ExplosionEffect, transform.position,transform.rotation);


        Collider[] colliders = Physics.
            OverlapSphere(transform.position, radius);
        foreach (var rangeObjet in colliders)
        {
            Rigidbody rb = rangeObjet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(ExoplosionForze * 10,transform.position, radius);
            }
        }

        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;


        Destroy(gameObject, delay * 2);
    }
}
