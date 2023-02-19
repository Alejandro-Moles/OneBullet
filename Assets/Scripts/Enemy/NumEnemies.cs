using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumEnemies : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NumEnemies_txt;
    [SerializeField] private int NumEnemies_num;

    private void Update()
    {
        NumEnemies_txt.text = NumEnemies_num.ToString();

        if(NumEnemies_num <= 0)
        {
            SceneManager.LoadScene("NivelSuperado");
        }
    }

    public void RestEnemies(int num)
    {
        NumEnemies_num-=num;
    }
}
