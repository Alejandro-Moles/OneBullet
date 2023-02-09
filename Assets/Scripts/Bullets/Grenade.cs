using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region Variables
    [Header("Explosion")]
    //variable que indica el tiempo hasta que explota
    [SerializeField] private float delay = 3f;
    private float countDown;

    //variable que indica el radio de la explosion
    [SerializeField] private float radius = 5f;
    //variable que indica la fuerza de la explosion
    [SerializeField] private float ExoplosionForze = 70f;
    //variable que indica si ha explotado o no
    private bool exploted = false;

    //variable que muestra el efecto de explosion
    [SerializeField] private GameObject ExplosionEffect;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        countDown = delay;
    }

    private void Update()
    {
        //restamos tiempo al contador
        countDown -= Time.deltaTime;
        //si el tiempo es 0 y no ha explotado entonces la explotamos
        if (countDown <= 0 && !exploted)
        {
            exploted = true;
            Exploted();
        }
    }
    #endregion

    #region Metodos Propios
    private void Exploted()
    {
        //instanciamos la explosion
        GameObject explosion =  Instantiate(ExplosionEffect, transform.position,transform.rotation);

        //comprobamos con que ha chocado
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var rangeObjet in colliders)
        {
            //le añadimos a lo que ha chocado una fuerza
            Rigidbody rb = rangeObjet.GetComponent<Rigidbody>();
            EnemyController enemy = rangeObjet.GetComponent<EnemyController>();


            if (rb != null)
            {
                rb.AddExplosionForce(ExoplosionForze * 10,transform.position, radius);
            }

            if(enemy != null)
            {
                enemy.DoDamage(1);
            }
        }

        //deactivamos tanto el meshrendered como los colliders de la granada
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //destruimos la granada en un tiempo determinado
        Destroy(gameObject, delay * 2);
    }
    #endregion
}
