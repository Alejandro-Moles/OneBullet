using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    private Animator UIAnimator;
    [SerializeField] private TextMeshProUGUI Tutorial_Text;
    [SerializeField] private TextMeshProUGUI TextBox;

    private void Start()
    {
        UIAnimator = GetComponent<Animator>();
    }

    public void MoveMessage()
    {

    }

    public void ViewUIMessage()
    {
        UIAnimator.SetTrigger("ViewUI");
    }

    public void ShowBullet()
    {
        UIAnimator.SetTrigger("Ammo");
    }

    public void ShowGrenade()
    {
        UIAnimator.SetTrigger("Grenade");
    }

    public void ShowWeapon()
    {
        UIAnimator.SetTrigger("Weapon");
    }

    public void ShowEnemies()
    {
        UIAnimator.SetTrigger("Enemies");
    }



    public void ChangeText(string text)
    {
        Tutorial_Text.text = text;
    }


    public void ChangeTextBox(string text)
    {
        TextBox.text = TextBox.text + text;
    }

    public void WriteSpace()
    {
        TextBox.text = TextBox.text + " ";
    }
}
