using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public PlayerMovement playerMovement;

    //array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;
    //reference to the sprite renderer to change sprites
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void OnMouseDown()
    {
        StartCoroutine(RollTheDice());
    }

    private IEnumerator RollTheDice()
    {
        //variable to contain random dice side number
        int randomDiceSide = 0;

        //final side that the dice reads in the end of coroutine
        int finalSide = 0;

        /*loop to switch dice sides randomly
         * 20 iterations*/
        for (int i = 0; i <= 20; i++)
        {   
            //pick up random value from 0 to 6 (all inclusive)
            randomDiceSide = Random.Range(0, 6);

            //set the sprite to upper face of dice from the array according to random value
            spriteRenderer.sprite = diceSides[randomDiceSide];

            //pause before next iteration
            yield return new WaitForSeconds(0.05f);
        }

        //assign final side value (randomDiceSide + 1 because array is 0-indexed)
        finalSide = randomDiceSide + 1;
        Debug.Log(finalSide);

        if(playerMovement != null)
        {
            playerMovement.MovePlayer(finalSide);
        }
    }
}
