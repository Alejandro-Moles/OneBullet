using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoInMap : MonoBehaviour
{
    [SerializeField] private Player_Actions player_Actions;
    [SerializeField] private Ammo ammo;

    void Update()
    {
        if (player_Actions.GetSetAmmoInMap <= 0)
        {
            Invoke("LoseGame", 10f);
        }
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("Death_Scene");
    }
}
