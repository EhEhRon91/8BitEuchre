using UnityEngine;

public class Card : MonoBehaviour
{

    //these are some cool variables
    private bool hasMoved = false;
    private bool hovered = false;
    private Vector3 cardPos;
    private string nameOfCard;
    private bool playable = false;
    private bool activeCard = false;
    public Trump trump;
    public Hand playerHand;
    public SELECTABLE select;
    public GameObject selectedCard;
    public TurnIndicator turnIndicator;
    public GameObject kittyObj;
    public Kitty kitty;
    public ScreenTransitionGame transition;
    public GameObject transitionObj;

    //Can select card or not
    public enum SELECTABLE
    {
        YES,
        NO
    }

    //init position and selectable
    void Start()
    {
        cardPos = gameObject.transform.position;
        select = SELECTABLE.NO;
    }

    //setting Trump
    public void setTrump()
    {
        if (kitty.cardObj.name.Contains("DIAMONDS"))
        {
            trump.trump = Trump.TRUMP.DIAMONDS;
        }
        else if (kitty.cardObj.name.Contains("HEARTS"))
        {
            trump.trump = Trump.TRUMP.HEARTS;
        }
        else if (kitty.cardObj.name.Contains("CLUBS"))
        {
            trump.trump = Trump.TRUMP.CLUBS;
        }
        else if (kitty.cardObj.name.Contains("SPADES"))
        {
            trump.trump = Trump.TRUMP.SPADES;
        }
    }
    private bool cardMade = false;
	public bool cardFoundPlayer = false;
    private int heartCount = 0;
    private int diamondCount = 0;
    private int clubCount = 0;
    private int spadeCount = 0;
    public bool canPlayCard = false;
    private int countSpadeForColor = 0;
    private int countClubForColor = 0;
    private int countDiamondForColor = 0;
    private int countHeartForColor = 0;

    // Update is called once per frame
    void Update()
    {
        //if in player's hand, can select card
        for(int i = 0; i < playerHand.hand.Count; ++i)
        {
            if(gameObject.name.ToString().Contains(playerHand.hand[i].ToString()))
            {
                select = SELECTABLE.YES;
            }
        }
        //if selectable
        if (select == SELECTABLE.YES)
        {
            //if player turn == true
            if(turnIndicator.playerTurn == true)
            {
                //determining if player can play that card
                if(playerHand.suitToFollow == Hand.SUIT_TO_FOLLOW.DIAMONDS)
                {
                    for(int i = 0; i < playerHand.diamondCardsToPlay.Count; ++i)
                    {
                        if(playerHand.diamondCardsToPlay[i].ToString().Contains(gameObject.name.ToString()))
                        {
                            canPlayCard = true;
                        }
                    }
                    if(playerHand.diamondCardsToPlay.Count == 0)
                    {
                        canPlayCard = true;
                    }
                    else if (!gameObject.name.ToString().Contains("DIAMOND"))
                    {
                        canPlayCard = false;
                    }

                    if(trump.trump == Trump.TRUMP.DIAMONDS && playerHand.diamondCardsToPlay.Count > 0)
                    {
                        if(gameObject.name.ToString().Contains("JACK_HEARTS"))
                        {
                            canPlayCard = false;
                        }
                    }
                    if(trump.trump == Trump.TRUMP.HEARTS)
                    {
                        if(gameObject.name.ToString().Contains("JACK_DIAMONDS") && playerHand.diamondCardsToPlay.Count > 0)
                        {
                            canPlayCard = false;
                        }
                    }
                    if (trump.trump == Trump.TRUMP.DIAMONDS)
                    {
                        if (gameObject.name.ToString().Contains("JACK_HEARTS"))
                        {
                            canPlayCard = true;
                        }
                    }
                }
                if (playerHand.suitToFollow == Hand.SUIT_TO_FOLLOW.HEARTS)
                {
                    for (int i = 0; i < playerHand.heartCardsToPlay.Count; ++i)
                    {
                        if (playerHand.heartCardsToPlay[i].ToString().Contains(gameObject.name.ToString()))
                        {
                            canPlayCard = true;
                        }
                    }
                    if(playerHand.heartCardsToPlay.Count == 0)
                    {
                        canPlayCard = true;
                    }
                    else if (!gameObject.name.ToString().Contains("HEART"))
                    {
                        canPlayCard = false;
                    }

                    if (trump.trump == Trump.TRUMP.DIAMONDS && playerHand.heartCardsToPlay.Count > 0)
                    {
                        if (gameObject.name.ToString().Contains("JACK_HEARTS"))
                        {
                            canPlayCard = false;
                        }
                    }
                    if (trump.trump == Trump.TRUMP.HEARTS)
                    {
                        if (gameObject.name.ToString().Contains("JACK_DIAMONDS") && playerHand.heartCardsToPlay.Count > 0)
                        {
                            canPlayCard = false;
                        }
                    }
                    if (trump.trump == Trump.TRUMP.HEARTS)
                    {
                        if (gameObject.name.ToString().Contains("JACK_DIAMONDS"))
                        {
                            canPlayCard = true;
                        }
                    }
                }
                if (playerHand.suitToFollow == Hand.SUIT_TO_FOLLOW.SPADES)
                {
                    for (int i = 0; i < playerHand.spadeCardsToPlay.Count; ++i)
                    {
                        if (playerHand.spadeCardsToPlay[i].ToString().Contains(gameObject.name.ToString()))
                        {
                            canPlayCard = true;
                        }
                    }
                    if(playerHand.spadeCardsToPlay.Count == 0)
                    {
                        canPlayCard = true;
                    }
                    else if (!gameObject.name.ToString().Contains("SPADE"))
                    {
                        canPlayCard = false;
                    }
                    if (trump.trump == Trump.TRUMP.CLUBS && playerHand.spadeCardsToPlay.Count > 0)
                    {
                        if (gameObject.name.ToString().Contains("JACK_SPADES"))
                        {
                            canPlayCard = false;
                        }
                    }
                    if (trump.trump == Trump.TRUMP.SPADES)
                    {
                        if (gameObject.name.ToString().Contains("JACK_CLUBS") && playerHand.spadeCardsToPlay.Count > 0) 
                        {
                            canPlayCard = false;
                        }
                    }
                    if (trump.trump == Trump.TRUMP.SPADES)
                    {
                        if (gameObject.name.ToString().Contains("JACK_CLUBS"))
                        {
                            canPlayCard = true;
                        }
                    }
                }
                if (playerHand.suitToFollow == Hand.SUIT_TO_FOLLOW.CLUBS)
                {
                    for (int i = 0; i < playerHand.clubCardsToPlay.Count; ++i)
                    {
                        if (playerHand.clubCardsToPlay[i].ToString().Contains(gameObject.name.ToString()))
                        {
                            canPlayCard = true;
                        }
                    }
                    if(playerHand.clubCardsToPlay.Count == 0)
                    {
                        canPlayCard = true;
                    }
                    else if(!gameObject.name.ToString().Contains("CLUB"))
                    {
                        canPlayCard = false;
                    }

                    if (trump.trump == Trump.TRUMP.SPADES && playerHand.clubCardsToPlay.Count > 0)
                    {
                        if (gameObject.name.ToString().Contains("JACK_CLUBS"))
                        {
                            canPlayCard = false;
                        }
                    }
                    if (trump.trump == Trump.TRUMP.SPADES)
                    {
                        if (gameObject.name.ToString().Contains("JACK_SPADES") && playerHand.clubCardsToPlay.Count > 0)
                        {
                            canPlayCard = false;
                        }
                    }
                    if(trump.trump == Trump.TRUMP.CLUBS)
                    {
                        if(gameObject.name.ToString().Contains("JACK_SPADES"))
                        {
                            canPlayCard = true;
                        }
                    }
                }
            }
            //display properly when not player's turn
            if(turnIndicator.playerTurn != true)
            {
                canPlayCard = true;
            }
            if (turnIndicator.playerTurn == true && turnIndicator.trumpMadeCounter == 0)
            {
                canPlayCard = true;
            }
            //reassign tag
            if (turnIndicator.cardCount == 5)
            {
                gameObject.tag = "Card";
            }
            //if player hand has one card, can always play that card
           if(playerHand.hand.Count == 1)
            {
                canPlayCard = true;
            }
           //if ally made alone, can't play cards
           if(turnIndicator.whoMadeAlone == TurnIndicator.WHO_MADE_ALONE.ALLY && trump.orderedUp == true)
            {
                canPlayCard = false;
            }
            if ((select == SELECTABLE.YES && (playerHand.handPosition == 4 && (turnIndicator.turnCounter == 4) || (playerHand.handPosition == 3 && turnIndicator.turnCounter == 3) ||
                (playerHand.handPosition == 2 && turnIndicator.turnCounter == 2) || (playerHand.handPosition == 1 && (turnIndicator.turnCounter == 0 || turnIndicator.turnCounter == 1)))) || trump.trumpMade == true)
            {
                //check if hovering over card
                if (hovered == true)
                {
                    //selected Card is assigned current gameObject
                    selectedCard = gameObject;

                    //selected Trump
                    if (Input.GetMouseButtonDown(0) && hasMoved == false && playerHand.cardIsActive == false && trump.orderedUp == false && playerHand.handPosition == 4)
                    {
                        if (((kitty.hand[0].ToString().Contains("HEART") && playerHand.canHearts == true) || (kitty.hand[0].ToString().Contains("DIAMOND") && playerHand.canDiamonds == true) || (kitty.hand[0].ToString().Contains("CLUB") && playerHand.canClubs == true)
                             || (kitty.hand[0].ToString().Contains("SPADE") && playerHand.canSpades == true)) && turnIndicator.trumpMadeCounter == 0)
                        {
                            hasMoved = true;
                            activeCard = true;
                            trump.addedCard = true;
                            gameObject.tag = "SelectedTrump";
                            setTrump();
                        }
                    }
                    if (Input.GetMouseButtonDown(0) && trump.trumpMade == true && trump.addedCard == false && playerHand.handPosition == 4)
                    {
                        gameObject.tag = "SelectedTrump";
                        trump.addedCard = true;
                    }
                    else if (Input.GetMouseButtonDown(0) && trump.trumpMade == true && turnIndicator.playerTurn == true)
                    {
                        gameObject.tag = "Selected";

                        if(canPlayCard == true)
                        {
                            gameObject.tag = "Selected";
                        }
                        else
                        {
                            gameObject.tag = "Card";
                        }
                        if(playerHand.suitToFollow == Hand.SUIT_TO_FOLLOW.NONE)
                        {
                            gameObject.tag = "Selected";
                        }
                        if (playerHand.handPosition == 1)
                        {
                            gameObject.tag = "Selected";
                        }
                        if (gameObject.tag == "Selected")
                        {
                            for (int i = 0; i < playerHand.hand.Count; ++i)
                            {
                                if (gameObject.ToString().Contains(playerHand.hand[i].ToString()))
                                {
                                    playerHand.hand.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        heartCount = 0;
                        diamondCount = 0;
                        clubCount = 0;
                        spadeCount = 0;
                    }
                }
            }
            if (trump.trumpMade == true && cardMade == false)
            {
                if (gameObject.tag == "SelectedTrump")
                {
                    gameObject.tag = "Card";
                }
                cardMade = true;
            }
            //grey out or display cards normally
            if (canPlayCard == false)
            {
                for (int i = 0; i < playerHand.hand.Count; ++i)
                {
                    if (gameObject.name.ToString().Contains(playerHand.hand[i].ToString()))
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                    }
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    public void OnMouseOver()
    {
        hovered = true;
    }

    public void OnMouseExit()
    {
        hovered = false;
    }
}
