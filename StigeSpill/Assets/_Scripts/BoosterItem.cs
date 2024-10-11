using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterItem : MonoBehaviour
{
    [SerializeField] private bool targetsSelf = false;
    [SerializeField] private bool swapPlace = false;
    [SerializeField] private bool doubleRoll = false;
    [SerializeField] private bool stepMod = false;
    public GameObject secondDice;
    private DiceRoller secondDiceRoller;
    public int stepModifier;
    public PlayerMovement targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (targetsSelf)
        {
            targetPlayer = this.gameObject.GetComponent<PlayerMovement>();
        }
    }

    public void useItem()
    {
        if (targetsSelf) {
            if (doubleRoll)
            {
                secondDice.SetActive(true);
                secondDiceRoller = secondDice.GetComponent<DiceRoller>();
                //secondDiceRoller.RollTheDice() -> player choose between the two die rolls, then player moves that many steps
            }
        }
        else
        {
            //choosePlayer() -> choose your target

            if (swapPlace)
            {
                //change place with chosen player. Both players instantly warps to each others original position.
            }
        }

        if (stepMod)
        {
            moveTarget(); //Needs to be able to handle negative numbers.
        }
    }

    void moveTarget()
    {
        targetPlayer.MovePlayer(stepModifier);
    }

    void choosePlayer(PlayerMovement target)
    {
        targetPlayer = target;
    }
}
