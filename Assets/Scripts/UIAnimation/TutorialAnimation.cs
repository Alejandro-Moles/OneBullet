using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI TextBox;

    public void MoveMessage()
    {

    }

    public void ViewUIMessage()
    {
        animator.SetTrigger("UI");
    }

    public void ShowBottonPart()
    {
        animator.SetTrigger("Botton");
    }

    public void ShowTopPart()
    {
        animator.SetTrigger("Top");
    }

    public void ChangeTextBox(string text)
    {
        TextBox.text = TextBox.text + text;
    }

    public void WriteSpace()
    {
        TextBox.text = TextBox.text + " ";
    }

    public void DeleteText()
    {
        TextBox.text = "";
    }
}
