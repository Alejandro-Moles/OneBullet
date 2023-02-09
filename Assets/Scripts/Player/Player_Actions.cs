using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Municion")]
    //variable que indica la municion de balas
    private int Ammo = 1;
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Granada")]
    //variable que indica la fuerza de lanzamiento de la granada
    private float ThrowForce = 550f;
    [SerializeField] private GameObject grenadePrefab;
    //variable que indica la municion de las granadas
    private int GrenadeAmmo = 0;
    [SerializeField] private TextMeshProUGUI GrenadeAmmoText;

    #endregion

    #region Metodos Unity
    private void Update()
    {
        //linea que indica donde esta apuntando la camara es de color rojo
        Debug.DrawRay(TransformCam.position, TransformCam.forward * 100f, Color.red);
        //linea que indica donde esta apuntando la pistola es de color azul
        Debug.DrawRay(TransformGun.position, TransformGun.forward * 100f, Color.blue);

        ammoText.text = Ammo.ToString();
        GrenadeAmmoText.text = GrenadeAmmo.ToString();

        Shoot();
        ThrowGrenade();
    }
    #endregion

    #region Metodos Propios
    private void Shoot()
    {
        //si se ha pulsado el click izquierdo del raton, entonces dispara
        if (Input.GetMouseButtonDown(0) && Ammo > 0)
        {
            //decimos que se active la animacion de disparo
            PlayerAnimator.SetTrigger("DoShoot");
            //restamos 1 a la municion
            Ammo--;
            //instanciamos la explosion que sale del arma
            GameObject exp = Instantiate(explosion,TransformGun.position, Quaternion.identity);
            //destruimos la explosion a los 0.5f
            Destroy(exp, 0.5f);
            //desactivamos el movimiento
            DesactivateMovement();
            //llamamos a la funcion de hacer el disparo
            DoShoot();
        }
    }

    //funcion que desactiva el movimiento
    private void DesactivateMovement()
    {
        //descativamos el movimiento del jugador al disparar
        var CHmove = transform.right * 0 + transform.forward * 0;
        CHcontroller.Move(CHmove);
        CanMove= false;
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

    //funcion que recarga el arma
    public void ReloadAmmo()
    {
        StartCoroutine(DoReloadAmmo());
    }

    //metodo que lanza la granada
    private void ThrowGrenade()
    {
        //si se pulsa el click derecho se lanza la granada
        if (Input.GetMouseButtonDown(1) && GrenadeAmmo >= 1)
        {
            GrenadeAmmo--;
            //instanciamos la granada en la posicion de la pistola para poder asi lanzarla hacia arriba tambien
            GameObject nuevaGranada = Instantiate (grenadePrefab, TransformGun.position, TransformGun.rotation);
            //le añadimos una fuerza de lanzamiento
            nuevaGranada.GetComponent<Rigidbody>().AddForce(TransformGun.forward * ThrowForce);
        }
    }

    //funcion que recarga las granadas
    public void ReloadGrenade()
    {
        GrenadeAmmo++;
    }
    #endregion

    #region Corrutinas
    //corrutina que esperará a qe el jugador recargue para que se le añada la municion
    private IEnumerator DoReloadAmmo()
    {
        //esperamos 1.1 segundos
        yield return new WaitForSeconds(1.1f);
        //sumamos 1 a la municion
        Ammo++;
    }
    #endregion

    #region Metodos GetSet
    public bool GetSetCanMove { get => CanMove; set => CanMove = value; }
    public int GetSetAmmo { get => Ammo; set => Ammo = value; }
    public int GetSetGrenadeAmmo { get => GrenadeAmmo; set => GrenadeAmmo = value; }
    #endregion
}
