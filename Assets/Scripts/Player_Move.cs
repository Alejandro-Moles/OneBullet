using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    #region Variables
    [Header("Movimiento de la Camara")]
    [SerializeField] private Transform TransformCamera;
    //estas variables hacen referencia a la posiciones vertical y horizontal de la camara respectivamente
    private float vMouse, hMouse;
    //esta variable es la rotacion de la camara inicial, y la iniciamos en 0.0
    private float yReal = 0.0f;
    //variables que definen la velocidad que tendra nuestro personje en el eje vertical y horizontal
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [Header("Movimiento del Jugador")]
    //esta variable hace referencia a componente charactercontroller de nuestro personje
    [SerializeField] private CharacterController CHcontroller;
    //esta variable indica la velocidad a la que se movera nuestro personaje
    [SerializeField] private float CHspeed = 12f;
    //esta variables nos indican los ejes en los que nos movemos
    private float CHx, CHz;
    //esta variable es el vector de nuestro movimiento
    private Vector3 CHmove;

    [Header("Gravedad")]
    //esta variable es la que hace que la velocidad al caer sea exponencial
    private Vector3 gravitySpeed;
    [SerializeField] private float gravity =-15f;
    private bool isGrounded = false;

    [Header("Salto")]
    //esta vraiable indica la fueza de salto que tendra nuestro personaje
    [SerializeField] private float jumpForce = 1f;
    //esta variable nos servirá para las fisicas del salto
    private float jumpValue;

    [Header("Animaciones")]
    [SerializeField] private Animator PlayerAnimator;

    [Header("Scripts Externos")]
    private Player_Actions player_Actions;
    #endregion

    #region Metodos Unity
    void Start()
    {
        //esto hace que nuestro cursor no se vea en tiempo de ejecucion
        Cursor.lockState= CursorLockMode.Locked;

        //asignamos las fisicas al salto
        jumpValue = Mathf.Sqrt(jumpForce * -2  * gravity);

        player_Actions = GetComponent<Player_Actions>();
    }

    void Update()
    {
        if (player_Actions.GetSetCanMove)
        {
            LookMouse();
            //comprobamos si esta en el suelo, en el caso de que si, resetamos la velocidad de la gravedad, para que no siga incrementandose
            if (isGrounded && gravitySpeed.y < 0)
            {
                gravitySpeed.y = gravity;
            }

            Movement();
            Jump();
        }
        else
        {
            Invoke("CanMove", 0.5f);
        }

        //hacemos que la gravedad se incremente
        gravitySpeed.y += gravity * Time.deltaTime;
        CHcontroller.Move(gravitySpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //si estamos tocando el suelo entonces cambia la variable de isGrounded a true
        if (hit.collider.CompareTag("Ground"))
        {
            //si es falsa la cambiamos a verdadera
            if (!isGrounded)
            {
                isGrounded = true;
            }
        }
    }
    #endregion

    #region Metodos Propios
    private void LookMouse()
    {
        //recogemos respecto al centro de la pantalla el movimiento del raton
        hMouse = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
        vMouse = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

        //esto se hace para que si queremos que al mover el cursor hacia arriba se mueva hacia arriba la camara, tenemos que restar
        yReal -= vMouse;

        //limitamos la rotacion de yReal, que este entre -90 grados y 90 grados
        yReal = Mathf.Clamp(yReal, -90f, 90f);

        //decimos que rote el cuerpo de nuestro personaje
        transform.Rotate(0f, hMouse, 0f);
        //le decimos a la camara que se rote de forma local
        TransformCamera.localRotation = Quaternion.Euler(yReal, 0f, 0f);
    }

    private void Movement()
    {
        //sacamos si el jugador esta pulsando las teclas de moverse tanto verticalmente como horizontalmente
        CHx = Input.GetAxis("Horizontal");
        CHz = Input.GetAxis("Vertical");

        //le asignamos al CharacterController el movimiento
        CHmove = transform.right * CHx + transform.forward * CHz;
        CHcontroller.Move(CHmove * CHspeed * Time.deltaTime);

        
        if(CHx != 0f || CHz != 0)
        {
            PlayerAnimator.SetBool("Walk", true);
        }
        else
        {
            PlayerAnimator.SetBool("Walk", false);
        }
        
    }

    private void Jump()
    {
        //comprobamos si ha pulsado la tecla del espacio y si esta en el suelo
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            //si se cumplen esas condiciones entonces hacemos que nuestro personaje salte
            isGrounded= false;
            gravitySpeed.y = jumpValue;
        }
    }


    private void CanMove()
    {
        player_Actions.GetSetCanMove = true;
    }


  
    #endregion
}
