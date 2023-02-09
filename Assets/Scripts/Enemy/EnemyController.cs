using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

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
    //esta variable es el agent que obtendremos del componente del enemigo
    private NavMeshAgent agent;
    //esta variable nos indica la distancia a la que el enemigo parará de seguirnos
    [SerializeField] private float DistanceToStop;
    //esta variable nos indicará la velocidad a la que se movera el enemigo
    [SerializeField] private float WalkSpeed;
    //esta variable nos indicará la velocidad a la que se movera el enemigo al correr
    [SerializeField] private float RunSpeed;


    [Header("Ataque")]
    [SerializeField] private float AttackDistance;
    private string[] Animations = new string[2] { "Attack 01", "Attack 02" };
    private bool canAttack = true;

    [Header("Animacion")]
    //variable que hace referenci al animatoe del enemigo
    private Animator EnemyAnimator;

    [Header("Muerte")]
    private bool isDeath = false;
    [SerializeField] private NumEnemies numEnemies;
    #endregion

    #region Metodos Unity

    private void Start()
    {
        //obtenemos el componente del enemigo
        EnemyAnimator= GetComponent<Animator>();

        agent= GetComponent<NavMeshAgent>();
        //le decimos al mesh agent que la distancia para parar será la que se ha indicado el el editor de unity (para que cada enemigo tenga la suya propia)
        agent.stoppingDistance = DistanceToStop;
    }
    void Update()
    {
        //sacamos constantemente la distancia que hay entre el jugador y en enemigo
        DistanceToPlayer = Vector3.Distance(transform.position, target.position);
        
        //se saca este vector para que el enemigo solo pueda rotar en el eje x 
        Vector3 posNoRotacion = new Vector3(target.position.x, transform.position.y, target.position.z);
        //le decimos al enemigo que mire hacia la posicion indicada
        transform.LookAt(posNoRotacion);

        EnemyActions();
    }
    #endregion

    #region Metodos Propios

    private void EnemyActions()
    {
        //compruebo si esta andando
        EnemyWalk();
        //compruebo si esta corriendo
        EnemyRun();
        //compruebo si esta atacando
        EnemyAttack();
    }

    //funcion que comprueba la distancia del enemigo hacia el jugador y hace que el enemigo active la animacion de andar
    private void EnemyWalk()
    {
        //si la distancia del jugador es menor o igual a la distancia de andar y mayor a la distancia de correr activo la animacion de andar
        if (DistanceToPlayer <= WalkDistance && DistanceToPlayer > RunDistance && DistanceToPlayer > AttackDistance && !isDeath)
        {
            //desactivo la animacion de correr (por si algun caso esta activada)
            EnemyAnimator.SetBool("Run Forward", false);
            //activo la animacion de andar
            EnemyAnimator.SetBool("Walk Forward", true);

            //indicamos que el enemigo tendra esa velocidad
            agent.speed = WalkSpeed;
            //le decimos que siga a nuestro jugador
            agent.SetDestination(target.position);
        }
        //si no vuelvo a la animacion idle
        else if (DistanceToPlayer > WalkDistance)
        {
            //desactivo la animacion de andar
            EnemyAnimator.SetBool("Walk Forward", false);
            //si el enemigo esta fuera del rango de seguirnos, entonces le decimos que su velocidad se 0, para que no nos siga
            agent.speed = 0;
        }
    }

    //funcion que comprueba la distancia del enemigo hacia el jugador y hace que el enemigo active la animacion de correr
    private void EnemyRun()
    {
        //si la distancia del jugador es menor o igual a la distancia de andar activo la animacion de correr
        if (DistanceToPlayer <= RunDistance && DistanceToPlayer > AttackDistance && !isDeath)
        {
            //desactivo la animacion de andar (por si algun caso esta activada)
            EnemyAnimator.SetBool("Walk Forward", false);
            //activo la animacion de correr
            EnemyAnimator.SetBool("Run Forward", true);
            //indicamos que el enemigo tendra esa velocidad
            agent.speed = RunSpeed;
            //le decimos que siga a nuestro jugador
            agent.SetDestination(target.position);
        }
        //si no vuelvo a la animacion idle
        else if (DistanceToPlayer > RunDistance)
        {
            //desactivo la animacion de correr
            EnemyAnimator.SetBool("Run Forward", false);
        }
    }

    private void EnemyAttack()
    {
        //si la distancia del jugador es menor o igial a la distancia de ataque, entonces el enemigo ejecuta el ataque
        if(DistanceToPlayer <= AttackDistance && canAttack && !isDeath)
        {
            canAttack= false;
            //desactivo la animacion de andar (por si algun caso esta activada)
            EnemyAnimator.SetBool("Walk Forward", false);
            //desactivo la animacion de correr
            EnemyAnimator.SetBool("Run Forward", false);

            //le asignamos l avelocidad a 0, para que no se mueva mientras ataca
            agent.speed = 0;

            //sacamos un numero aleatorio que marcará la animacion de ataque que tendrá el enemigo
            int randomAnimation = Random.Range(0, 2);

            //indicamos que animacion va a usar para atacar
            EnemyAnimator.SetTrigger(Animations[randomAnimation]);

            //empezamos la rutina del tiempo de espera del ataque
            StartCoroutine("ColdownAttack");
        }
    }
    #endregion

    #region Metodos Interfaz
    public void DoDamage(int vid)
    {
        isDeath= true;

        //le decimos que su velocidad sea cero;
        agent.speed = 0;

        //decimos que no pueda atacar
        canAttack = false;
        //desactivo la animacion de andar (por si algun caso esta activada)
        EnemyAnimator.SetBool("Walk Forward", false);
        //desactivo la animacion de correr
        EnemyAnimator.SetBool("Run Forward", false);

        EnemyAnimator.SetTrigger("Die");
        Destroy(gameObject, 1.4f);

        numEnemies.RestEnemies(1);
    }
    #endregion

    #region Metodos Corrutinas
    //corrutina que sirve para dar un timpo de espera entre ataques
    private IEnumerator ColdownAttack()
    {
        //esperamos un segundo
        yield return new WaitForSeconds(1.5f);
        //ponemos la variable de que puede atacar en verdadero
        canAttack = true;
    }
    #endregion
}
