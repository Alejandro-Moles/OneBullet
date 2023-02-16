using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    [Header("Controlador de texto")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI TextBox;
    [SerializeField] private AudioSource audioSource;

    [Header("Moverse")]
    //variable que me dirá si el jugador se puede mover
    private bool playerCanMove = false;
    //variable que me dirá si el jugador se ha movido por primera vez
    private bool isMoveDone = false;

    [Header("Disparo")]
    //variable que nos indica si el jugador puede disparar
    private bool playerCanShoot = false;
    //variable que nos indica si el jugador ha disparado por primera vez
    private bool isShootDone = false;

    [Header("Salto")]
    //variable que nos indica si el jugador puede disparar
    private bool playerCanRun = false;
    //variable que nos indica si el jugador ha disparado por primera vez
    private bool isRunDone = false;

    [Header("Enemigo")]
    private bool EnemyCanDie = false;
    [SerializeField] private GameObject Enemy;

    [Header("Player Actions")]
    [SerializeField] private Player_Move playerMove;
    [SerializeField] private Player_Actions playerActions;


    private void Update()
    {
        if (!isMoveDone)
        {
            playerMove.GetSetTutorialCanMove = false;
        }

        if (!isShootDone)
        {
            playerActions.GetSetTutorialShoot = false;
        }

        if(playerCanMove) 
        {
            if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) 
            {
                MoveDonne();
            }
        }

        if (playerCanShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootDone();
            }
        }

        if (playerCanRun)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
            {
                RunJumpDone();
            }
        }
    }

    public void KillEnemyMessage()
    {
        animator.SetTrigger("KillEnemy");
        Enemy.SetActive(true);
    }

    public void JumpRunMessage()
    {
        animator.SetTrigger("JumpRun");
    }


    //funcion que muestra el mensaje para que el jugador dispare
    public void ShootMenssage()
    {
        animator.SetTrigger("Shoot");
    }

    //funcion que muestra el mensaje para que el jugador se mueva
    public void MoveMessage()
    {
        animator.SetTrigger("StartMove");
    }

    //funcion que muestra el mensaje que enseña la UI
    public void ViewUIMessage()
    {
        animator.SetTrigger("UI");
    }

    //funcion que muestra el mensaje que muestra la parte de abajo de la UI
    public void ShowBottonPart()
    {
        animator.SetTrigger("Botton");
    }

    //funcion que muestra el mensaje que muestra la parte de arriba de la UI
    public void ShowTopPart()
    {
        animator.SetTrigger("Top");
    }

    //funcion que cambia el texto de la caja de texto
    public void ChangeTextBox(string text)
    {
        audioSource.Play();
        TextBox.text = TextBox.text + text;
    }

    //funcion que da un espacio en blanco
    public void WriteSpace()
    {
        TextBox.text = TextBox.text + " ";
    }

    //funcion que borra el texto
    public void DeleteText()
    {
        TextBox.text = "";
    }

    //funcion que muestra el texto que indica al jugador que se mueva
    public void Move()
    {
        animator.SetTrigger("Move");
    }

    //funcion que dice que el jugador se pueda mover
    public void PlayerDoMove()
    {
        playerCanMove = true;
    }

    public void PlayerDoShoot()
    {
        playerCanShoot = true;
    }

    public void PlayerCanRun()
    {
        playerCanRun = true;
    }

    //funcion que se activa la primera vez que el jugador se mueve
    public void MoveDonne()
    {
        if (!isMoveDone)
        {
            playerMove.GetSetTutorialCanMove = true;
            isMoveDone = true;
            animator.SetTrigger("MoveDone");
        }
    }

    //funcion que se activa la primera vez que el jugador ha disparado
    public void ShootDone()
    {
        if (!isShootDone)
        {
            playerActions.GetSetTutorialShoot = true;
            isShootDone = true;
            animator.SetTrigger("ShootDone");
        }
    }

    //funcion que se activa la primera vez que el jugador ha corrido o saltado
    public void RunJumpDone()
    {
        if (!isRunDone)
        {
            isRunDone = true;
            animator.SetTrigger("JumpRunDone");
        }
    }

    public void EnemyDie()
    {
        Debug.Log(EnemyCanDie);
        if(EnemyCanDie) 
        {
            Debug.Log("Die");
        }
    }

    public void TheEnemyCanDie()
    {
        EnemyCanDie = true;
    }
}
