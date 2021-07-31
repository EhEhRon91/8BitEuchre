using UnityEngine;
using UnityEngine.UI;
public class ClubSuitButton : MonoBehaviour
{
    private TurnIndicator turn;
    private Hand playerHand;
    private Button button;
    private Trump trump;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trump.trumpMade == true && trump.trump == Trump.TRUMP.CLUBS || (trump.trumpMade == false))
        {
            gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        //determines if button is interactable or not
        if (playerHand.passedKitty == false)
        {
            button.interactable = false;
        }
        else
        {
            if(playerHand.handPosition == 4)
            {
                if(turn.turnCounter == 8 && playerHand.canClubs == true)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else if(playerHand.handPosition == 3)
            {
                if (turn.turnCounter == 7 && playerHand.canClubs == true)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else if (playerHand.handPosition == 2)
            {
                if (turn.turnCounter == 6 && playerHand.canClubs == true)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else if (playerHand.handPosition == 1)
            {
                if (turn.turnCounter == 5 && playerHand.canClubs == true)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
        }
    }

    //set trump
    public void setTrump()
    {
        trump.trump = Trump.TRUMP.CLUBS;
        trump.trumpMade = true;
        trump.whoMade = Trump.PLAYERWHOMADE.PLAYER;
        trump.addedCard = true;
        trump.orderedUp = true;
    }
}
