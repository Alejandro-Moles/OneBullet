using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamage
{
    #region Variables
    [Header("Movimiento")]
    //esta variable hace referencia al objetivo al que va a seguir el enemigo
    [SerializeField] private Transform target;
    //esta variable marca la distancia a la que el enemigo empezará a andar hacia el jugador
    [SerializeField] private float WalkDistance;
    //esta variable marca la distancia a la que el enemigo empezará a correr hacia el jugador
    [SerializeField] private float RunDistance;
    //esta variabla nos indica a la distancia que esta el enemigo del jugador
    [SerializeField] private float DistanceToPlayer;

    [Header("Animacion")]
    //variable que hace referenci al animatoe del enemigo
    private Animator EnemyAnimator;
    #endregion

    #region Metodos Unity

    private void Start()
    {
        //obtenemos el componente del enemigo
        EnemyAnimator= GetComponent<Animator>();
    }
    void Update()
    {
        //se saca este vector para que el enemigo solo pueda rotar en el eje x 
        Vector3 posNoRotacion = new Vector3(target.position.x, transform.position.y, target.position.z);
        //le decimos al enemigo que mire hacia la posicion indicada
        transform.LookAt(posNoRotacion);

        //sacamos constantemente la distancia que hay entre el jugador y en enemigo
        DistanceToPlayer = Vector3.Distance(transform.position, target.position);


        EnemyMove();
    }
    #endregion

    #region Metodos Propios

    private void EnemyMove()
    {
        //compruebo si esta andando
        EnemyWalk();
        //compruebo si esta corriendo
        EnemyRun();
    }

    //funcion que comprueba la distancia del enemigo hacia el jugador y hace que el enemigo active la animacion de andar
    private void EnemyWalk()
    {
        //si la distancia del jugador es menor o igual a la distancia de andar y mayor a la distancia de correr activo la animacion de andar
        if (DistanceToPlayer <= WalkDistance && DistanceToPlayer > RunDistance)
        {
            //desactivo la animacion de correr (por si algun caso esta activada)
            EnemyAnimator.SetBool("Run Forward", false);
            //activo la animacion de andar
            EnemyAnimator.SetBool("Walk Forward", true);
        }
        //si no vuelvo a la animacion idle
        else if (DistanceToPlayer > WalkDistance)
        {
            //desactivo la animacion de andar
            EnemyAnimator.SetBool("Walk Forward", false);
        }
    }

    //funcion que comprueba la distancia del enemigo hacia el jugador y hace que el enemigo active la animacion de correr
    private void EnemyRun()
    {
        //si la distancia del jugador es menor o igual a la distancia de andar activo la animacion de correr
        if (DistanceToPlayer <= RunDistance)
        {
            //desactivo la animacion de andar (por si algun caso esta activada)
            EnemyAnimator.SetBool("Walk Forward", false);
            //activo la animacion de correr
            EnemyAnimator.SetBool("Run Forward", true);
        }
        //si no vuelvo a la animacion idle
        else if (DistanceToPlayer > RunDistance)
        {
            //desactivo la animacion de correr
            EnemyAnimator.SetBool("Run Forward", false);
        }
    }

    #endregion

    #region Metodos Interfaz
    public void DoDamage(int vid)
    {
        Debug.Log("He recibido daño = " + vid);
    }
    #endregion
}
