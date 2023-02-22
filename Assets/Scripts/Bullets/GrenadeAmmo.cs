using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAmmo : MonoBehaviour
{
    //variable privada que nos dará acceso al scrip de player actiopns
    [SerializeField] private Player_Actions player_Actions;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAA");

        //miramos si ha entrado en contacto con el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log(player_Actions.GetSetAmmo < 1);

            //si la municion es menor a 1 entonces deja coger más municion
            if (player_Actions.GetSetGrenadeAmmo < 1)
            {
                player_Actions.ReloadGrenade();
                //destruimos el objeto
                Destroy(gameObject);
            }
        }

    }
}
