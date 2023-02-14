using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    private Animator UIAnimator;
    [SerializeField] private TextMeshProUGUI Tutorial_Text;

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

    public void ChangeText(string text)
    {
        Tutorial_Text.text = text;
    }
}
