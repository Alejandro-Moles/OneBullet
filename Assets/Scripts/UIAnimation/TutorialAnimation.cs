using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI TextBox;
    [SerializeField] private AudioSource audioSource;

    //variable que me dirá si el jugador se puede mover
    private bool playerCanMove = false;
    //variable que me dirá si el jugador se ha movido por primera vez
    private bool isMoveDone = false;

    private void Update()
    {
        if(playerCanMove) 
        {
            if(Input.GetAxisRaw("Vertical")>0 || Input.GetAxisRaw("Horizontal") > 0) 
            {
                MoveDonne();
            }
        }
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

    //funcion que se activa la primera vez que el jugador se mueve
    public void MoveDonne()
    {
        if (!isMoveDone)
        {
            isMoveDone= true;
            animator.SetTrigger("MoveDone");
        }
    }
}
