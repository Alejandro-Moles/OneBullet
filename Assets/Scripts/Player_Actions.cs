using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Actions : MonoBehaviour
{
    #region Variables
    [Header("Disparo")]
    //variable que indica el transform de la pistola
    [SerializeField] private Transform TransformGun;
    //variable que indica el transform de la camara
    [SerializeField] private Transform TransformCam;
    [SerializeField] private GameObject explosion;


    //variable que nos indica el Raycast
    private RaycastHit hit;
    [SerializeField] private LayerMask IgnoreLayer;


    [Header("Animaciones")]
    [SerializeField] private Animator PlayerAnimator;

    [Header("Movimiento")]
    //esta variable hace referencia a componente charactercontroller de nuestro personje
    [SerializeField] private CharacterController CHcontroller;
    private bool CanMove = true;

    
    #endregion

    #region Metodos Unity
    private void Update()
    {
        //linea que indica donde esta apuntando la camara es de color rojo
        Debug.DrawRay(TransformCam.position, TransformCam.forward * 100f, Color.red);
        //linea que indica donde esta apuntando la pistola es de color azul
        Debug.DrawRay(TransformGun.position, TransformGun.forward * 100f, Color.blue);


        Shoot();
    }
    #endregion

    #region Metodos Propios
    private void Shoot()
    {
        //si se ha pulsado el click izquierdo del raton, entonces dispara
        if (Input.GetMouseButtonDown(0))
        {   
            GameObject exp = Instantiate(explosion,TransformGun.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            DesactivateMovement();
            Invoke("DoShoot", 0.1f);
        }
        else
        {
            //desactivamos la animacion de disparar
            PlayerAnimator.SetBool("Shoot", false);
        }
    }

    private void DesactivateMovement()
    {
        var CHmove = transform.right * 0 + transform.forward * 0;
        CHcontroller.Move(CHmove);

        CanMove= false;

        //activamos la animacion de disparar
        PlayerAnimator.SetBool("Shoot", true);
    }


    private void DoShoot()
    {
        
        //esta variable Vector3 nos indicará hacia que parte ira la bala, le añadirá un pequeño rango donde pude impactar la bala
        Vector3 direction = TransformCam.TransformDirection(new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 1));

        //esta direcion es la precisa
        Vector3 directionPrecisa = TransformCam.TransformDirection(new Vector3(0, 0, 1));

        //creamos un game obect que es la instancia de el prefab de la bala
        GameObject bulletObject = ObjectPollingManager.instance.GetBullet();

        //hacemos que la posicion de donde sale la bala es la del arma
        bulletObject.transform.position = TransformGun.position;

        //comprobamos si el rayo ha chocado con algun objeto (como pared, arbol, enemigo), si es asi entonces la bala ira hacia ese objeto
        if (Physics.Raycast(TransformCam.position, directionPrecisa, out hit, Mathf.Infinity, ~IgnoreLayer)) // aqui le estamos diciendo que ignore la capa de player, para que la bala no salga hacia arriba
        {
            bulletObject.transform.LookAt(hit.point);
        }
        //si no es asi y no choca contra nada (cielo) entonces la bala ira hacia el centro de la camra
        else
        {
            //hacemos que la posicion a donde se dirija la bala sea la posicion de la camara
            Vector3 dir = TransformCam.position + TransformCam.forward * 10f;
            bulletObject.transform.LookAt(dir);
        }
    }
    #endregion


    #region Metodos GetSet
    public bool GetSetCanMove { get => CanMove; set => CanMove = value; }
    #endregion
}
