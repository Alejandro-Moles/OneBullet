using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //variable privada que nos dará acceso al scrip de player actiopns
    [SerializeField] private Player_Actions player_Actions;
    [SerializeField] private Animator player_Animator;

    private void OnTriggerEnter(Collider other)
    {
        //miramos si ha entrado en contacto con el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log(player_Actions.GetSetAmmo < 1);

            //si la municion es menor a 1 entonces deja coger más municion
            if(player_Actions.GetSetAmmo < 1)
            {
                player_Animator.SetTrigger("Reload");
                //sumamos 1 a la municion
                player_Actions.ReloadAmmo();
                //destruimos el objeto
                Destroy(gameObject);
            }
        }
      
    }
}
