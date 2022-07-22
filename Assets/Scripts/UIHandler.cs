using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Animator sizeIconAnimator;
    [SerializeField] private Animator healthIconAnimator;
    [SerializeField] private Image sizeBar;
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator[] starAnimators = new Animator[3];
    [SerializeField] private PlayerController playerController;
  
    

    void Update()
    {
        UiUpdate();
    }
    private void UiUpdate()
    {
        float playerHealth = playerController.playerHealth;
        float startPlayerHealth = playerController.startPlayerHealth;
        int nubmerOfStars = playerController.nubmerOfStars;

        bool isGroving = playerController.isGroving;
        bool isMoving = playerController.isMoving;
        bool isGround = playerController.isGround;
        bool isDead = playerController.isDead;

        sizeBar.fillAmount = (playerController.transform.localScale.x - 0.6f) * 2.5f;

        healthBar.fillAmount = (playerHealth / startPlayerHealth);

        if (isGroving) sizeIconAnimator.SetBool("hasChange", true);
        else sizeIconAnimator.SetBool("hasChange", false);

        switch (nubmerOfStars)
        {
            case 1:
                starAnimators[nubmerOfStars - 1].SetBool("isAppearing", true);
                break;
            case 2:
                starAnimators[nubmerOfStars - 1].SetBool("isAppearing", true);
                break;
            case 3:
                starAnimators[nubmerOfStars - 1].SetBool("isAppearing", true);
                break;
            default:
                break;
        }
    }
}
