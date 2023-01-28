using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //variable que nos indica la velocidad de la bala
    [SerializeField] private float Bulletspeed = 8f;
    //variable que nos indica la "vida" que tendra la bala
    [SerializeField] private float lifeDuration = 2f;

    //controlador del tiempo de la vida de la bala
    private float lifeTimer;

    private void OnEnable()
    {
        //hacemos que el controlador de la vida sea igual a la duracion de la vida
        lifeTimer = lifeDuration;
    }

    private void Update()
    {
        //restamos al tiempo de vida
        lifeTimer -= Time.deltaTime;

        //si es cero entonces destruimos el objeto
        if (lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Bulletspeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //si choca con algo, hacemos una variable que haga referencia a la interface, y que la coja del componente al que ha chocado
        IDamage damage = other.GetComponent<IDamage>();
        //si no es null, quiere decir que ese componente tiene relacion con la interface, por lo tanto puede recibir daño
        if (damage != null)
        {
            //llamamos a la funcion de recibir daño
            damage.DoDamage(1);
        }
        //desactivamos el objeto para que si se ha chocado que no siga avanzando
        gameObject.SetActive(false);
    }
}
