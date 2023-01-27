using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollingManager : MonoBehaviour
{
    //hacemos que se pueda acceder a este script de la parte de la escena que queramos
    public static ObjectPollingManager instance;

    //variable a la que le asignamos el preFab de la bala que vamos a cargar
    [SerializeField] private GameObject bulletPreFab;
    //variable que nos indica la cantidad de balas que se van a cargar por defecto en la polling
    [SerializeField] private int bulletAmount = 5;

    //variable que guardara la lista de balas 
    private List<GameObject> bulletList;

    private void Awake()
    {
        instance= this;
        //creamos la lista con el tamaño de la cantidad de balas que se van a cargar
        bulletList= new List<GameObject>(bulletAmount);

        //recorremos la lista
        for(int i = 0; i < bulletAmount; i++)
        {
            //creamos las balas
            GameObject prefabInstance = Instantiate(bulletPreFab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bulletList.Add(prefabInstance);
        }
    }

    //funcoin que nos devuelve una bala
    public GameObject GetBullet()
    {
        //sacamos el numneor de balas total
        int BulletTotal = bulletList.Count;
        //recorremos la lista de balas
        for(int i = 0; i< BulletTotal; i++)
        {
            //comprobamos que esa bala no este ya en la escena
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].SetActive(true);
                //----
                return bulletList[i];
            }
        }
        GameObject prefabInstance = Instantiate(bulletPreFab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.SetActive(true);
        bulletList.Add(prefabInstance);
        return prefabInstance;
    }
}
