using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnIndicator : MonoBehaviour
{
    public float nonPlayerTurnTimerLimit = 1.0f;
    private float timer = 0.0f;
    public GameObject activeCard;
    private int turnCounter = 0;
    public Trump trump;
    public Kitty kitty;
    public Toggle toggle;
    public Deck deck;
    public GameObject kittyObj;

    private ScreenTransitionGame transition;

    private Deck.Card firstPlayedCard;
    private Deck.Card secondPlayedCard;
    private Deck.Card thirdPlayedCard;
    private Deck.Card fourthPlayedCard;
    public Hand hand5;
    public GameObject endGamePanel;
    public List<Deck.Card> playedCards;
    public Hand playerHand;
    public Hand enemy1;
    public Hand parter;
    public Hand enemy2;
    private bool hasFoundPlayedCard = false;
    private Vector3 startRotation;
    private float zRot = -90.0f;
    private int counter = 0;
    private bool firstGo = false;
    public int startPosForHand;
    private bool goingAlone = false;
    private bool playerWon = false;
    private bool enemyWon = false;
    public WHO_MADE_ALONE whoMadeAlone = WHO_MADE_ALONE.NONE;
    private float aloneChangeTimer = 0.0f;
    private bool isFirstTime = false;
    private bool setUpFirst = false;
    public enum WHO_MADE_ALONE
    {
        NONE,
        PLAYER,
        ENEMY1,
        ALLY,
        ENEMY2
    }

    public void setAlone()
    {
        if(goingAlone == true)
        {
            goingAlone = false;
            whoMadeAlone = WHO_MADE_ALONE.NONE;
        }
        else
        {
            goingAlone = true;
            whoMadeAlone = WHO_MADE_ALONE.PLAYER;
        }
    }
    private void Awake()
    {
        playerHand.handPosition = 4;
        startPosForHand = 4;
        enemy1.handPosition = 1;
        parter.handPosition = 2;
        enemy2.handPosition = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        kittyObj = GameObject.FindGameObjectWithTag("KittyCard");
        if (playerHand.handPosition == 1)
        {
            startRotation = new Vector3(0.0f, 0.0f, zRot);
        }
        else if (playerHand.handPosition == 2)
        {
            startRotation = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (playerHand.handPosition == 3)
        {
            startRotation = new Vector3(0.0f, 0.0f, zRot * 3);
        }
        else if (playerHand.handPosition == 4)
        {
            startRotation = new Vector3(0.0f, 0.0f, zRot * 2);
        }
   

        transform.Rotate(startRotation);

        if (playerHand.handPosition == 1)
        {
            playerTurn = true;
        }

        if (playerHand.handPosition != 1)
        {
            playerTurn = false;
        }
    }

    public Vector3 rotateAfterTrumpMade;
    public float trumpMadeTimer = 0.0f;
    public float trumpMadeLimit = 1.0f;
    public bool firstTrumpMadeRotation = false;
    public int trumpMadeCounter = 1;

    public bool playerTurn = false;
    public bool enemy1Turn = false;
    public bool allyTurn = false;
    public bool enemy2Turn = false;
    public bool hasMadePlayer = false;

    public GameObject firstPlayedCardObj;
    public GameObject secondPlayedCardObj;
    public GameObject thirdPlayedCardObj;
    public GameObject fourthPlayedCardObj;
    public int cardCount = 0;
    public bool madeToPlayer = false;
    public int index = 0;
    public bool foundWhoWonGame = false;
    public bool doneTurnForNow = false;
    public bool genEnemy2Hand = false;
    public bool genEnemy1Hand = false;
    public bool genAllyHand = false;
    public bool doneGoingAloneUp = false;
    public bool foundCardsForPlayer = false;
    public bool resetEnemies = false;

    public bool resetAllyHand = false;
    public bool resetEnemy1Hand = false;
    public bool resetEnemy2Hand = false;
    public bool generatedCards = false;
    public bool resetCardsEnd = false;
    public bool resetCardsEnd2 = false;
    // Update is called once per frame
    void Update()
    {
        if (foundCardsForPlayer == false && trump.trumpMade == true)
        {
            playerHand.spadeCardsToPlay.Clear();
            playerHand.clubCardsToPlay.Clear();
            playerHand.diamondCardsToPlay.Clear();
            playerHand.heartCardsToPlay.Clear();
            for (int i = 0; i < playerHand.hand.Count; ++i)
            {
                if (playerHand.hand[i].ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && playerHand.hand[i].ToString().Contains("JACK_DIAMONDS")))
                {
                    if (trump.trump == Trump.TRUMP.DIAMONDS && playerHand.hand[i].ToString().Contains("JACK_HEARTS"))
                    {
                        continue;
                    }
                    playerHand.heartCardsToPlay.Add(playerHand.hand[i]);
                }
                if (playerHand.hand[i].ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && playerHand.hand[i].ToString().Contains("JACK_HEARTS")))
                {
                    if(trump.trump == Trump.TRUMP.HEARTS && playerHand.hand[i].ToString().Contains("JACK_DIAMONDS"))
                    {
                        continue;
                    }
                    playerHand.diamondCardsToPlay.Add(playerHand.hand[i]);
                }
                if (playerHand.hand[i].ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && playerHand.hand[i].ToString().Contains("JACK_CLUBS")))
                {
                    if (trump.trump == Trump.TRUMP.CLUBS && playerHand.hand[i].ToString().Contains("JACK_SPADES"))
                    {
                        continue;
                    }
                    playerHand.spadeCardsToPlay.Add(playerHand.hand[i]);
                }
                if (playerHand.hand[i].ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && playerHand.hand[i].ToString().Contains("JACK_SPADES")))
                {
                    if (trump.trump == Trump.TRUMP.SPADES && playerHand.hand[i].ToString().Contains("JACK_CLUBS"))
                    {
                        continue;
                    }
                    playerHand.clubCardsToPlay.Add(playerHand.hand[i]);
                }
            }
            foundCardsForPlayer = true;
        }
        if (trump.trumpMade == true && doneGoingAloneUp == false && playerHand.passedKitty == false)
        {
            if (trump.whoMade == Trump.PLAYERWHOMADE.PLAYER)
            {
                if (startPosForHand == 2)
                {
                    whoMadeAlone = WHO_MADE_ALONE.PLAYER;
                    goingAlone = true;
                }
            }
            if (trump.whoMade == Trump.PLAYERWHOMADE.ALLY)
            {
                if (startPosForHand == 4)
                {
                    whoMadeAlone = WHO_MADE_ALONE.ALLY;
                    goingAlone = true;
                }
            }
            if (trump.whoMade == Trump.PLAYERWHOMADE.ENEMY1)
            {
                if (startPosForHand == 3)
                {
                    whoMadeAlone = WHO_MADE_ALONE.ENEMY1;
                    goingAlone = true;
                }
            }
            if (trump.whoMade == Trump.PLAYERWHOMADE.ENEMY2)
            {
                if (startPosForHand == 1)
                {
                    whoMadeAlone = WHO_MADE_ALONE.ENEMY2;
                    goingAlone = true;
                }
            }
            doneGoingAloneUp = true;
        }
        if (playerHand.pointsEnemy >= 10 || playerHand.pointsPlayer >= 10)
        {
            endGamePanel.SetActive(true);
            if(GameObject.FindGameObjectWithTag("Enemy2Card") != null)
            {
                GameObject.FindGameObjectWithTag("Enemy2Card").tag = "Card";
            }
            if (GameObject.FindGameObjectWithTag("Enemy1Card") != null)
            {
                GameObject.FindGameObjectWithTag("Enemy1Card").tag = "Card";
            }
            if (GameObject.FindGameObjectWithTag("AllyCard") != null)
            {
                GameObject.FindGameObjectWithTag("AllyCard").tag = "Card";
            }
        }
        if(GameObject.FindGameObjectWithTag("KittyCard") != null)
        {
            kittyObj = GameObject.FindGameObjectWithTag("KittyCard");
        }
        if (turnCounter > 4)// || (trump.trumpMade == true && playerHand.handPosition == 4) && trump.selectedCard != null)
        {
             kittyObj.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
        }

        if(trump.trumpMade == true && genEnemy2Hand == true && genEnemy1Hand == true && genAllyHand == true)
        {
            doneTurnForNow = false;
        }
        //if(resetCardsEnd == true && turnCounter == 1)
        //{
        //    resetEnemy1();
        //    resetCardsEnd = false;
        //}
        //if(turnCounter == 2 && resetCardsEnd2 == true)
        //{
        //    resetEnemy2();
        //    resetCardsEnd2 = false;
        //}
        if (trumpMadeCounter == 0 && generatedCards == false)
        {
            foundWhoWonGame = false;
            playerHand.genCards();
            enemy1.genCards();
            parter.genCards();
            enemy2.genCards();
            for(int i = 0; i < deck.nonPlayedCards.Count; ++i)
            {
                deck.nonPlayedCards[i].transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
            }
           // hand5.genCards();
            generatedCards = true;
        }
        if (trump.trumpMade == true)
        {
            if (trumpMadeCounter > 3 && foundWhoWonGame == false)
            {
                playerHand.whoWon(playerHand.whoPlayedFirst);
                playerHand.cardsToMove.Clear();

                for (int i = 0; i < playerHand.hand.Count; ++i)
                {
                    if (GameObject.Find(playerHand.hand[i].ToString()) != null)
                    {
                        playerHand.cardsToMove.Add(GameObject.Find(playerHand.hand[i].ToString()));
                        //GameObject.Find(hand[i].ToString()).transform.position = new Vector3(0, 0, 0);
                    }
                }
                playerHand.nonMatchingCardsKitty.Clear();
                playerHand.getNonTrumpCards();

                enemy1.cardsToMove.Clear();

                for (int i = 0; i < enemy1.hand.Count; ++i)
                {
                    if (GameObject.Find(enemy1.hand[i].ToString()) != null)
                    {
                        enemy1.cardsToMove.Add(GameObject.Find(enemy1.hand[i].ToString()));
                        //GameObject.Find(hand[i].ToString()).transform.position = new Vector3(0, 0, 0);
                    }
                }
                enemy1.nonMatchingCardsKitty.Clear();
                enemy1.getNonTrumpCards();

                hand5.cardsToMove.Clear();

                for (int i = 0; i < hand5.hand.Count; ++i)
                {
                    if (GameObject.Find(hand5.hand[i].ToString()) != null)
                    {
                        hand5.cardsToMove.Add(GameObject.Find(hand5.hand[i].ToString()));
                        //GameObject.Find(hand[i].ToString()).transform.position = new Vector3(0, 0, 0);
                    }
                }

                parter.cardsToMove.Clear();
                parter.nonMatchingCardsKitty.Clear();
                parter.getNonTrumpCards();
                for (int i = 0; i < parter.hand.Count; ++i)
                {
                    if (GameObject.Find(parter.hand[i].ToString()) != null)
                    {
                        parter.cardsToMove.Add(GameObject.Find(parter.hand[i].ToString()));
                        //GameObject.Find(hand[i].ToString()).transform.position = new Vector3(0, 0, 0);
                    }
                }
                enemy2.cardsToMove.Clear();
                enemy2.nonMatchingCardsKitty.Clear();
                enemy2.getNonTrumpCards();
                for (int i = 0; i < enemy2.hand.Count; ++i)
                {
                    if (GameObject.Find(enemy2.hand[i].ToString()) != null)
                    {
                        enemy2.cardsToMove.Add(GameObject.Find(enemy2.hand[i].ToString()));
                        //GameObject.Find(hand[i].ToString()).transform.position = new Vector3(0, 0, 0);
                    }
                }

                //enemy1.whoWon(enemy1.whoPlayedFirst);
                //parter.whoWon(parter.whoPlayedFirst);
                //enemy2.whoWon(enemy2.whoPlayedFirst);
                cardCount++;
                foundWhoWonGame = true;
                generatedCards = false;
            }
            if (playerHand.enemy1Won == true)
            {
                aloneChangeTimer = 0.0f;
                hasCheckedEnemy1 = false;
                hasCheckedEnemy2 = false;
                hasCheckedAlly = false;
                turnCounter = 0;
                counter = 0;
                enemy1.handPosition = 1;
                parter.handPosition = 2;
                enemy2.handPosition = 3;
                playerHand.handPosition = 4;
                playerHand.cardIsActive = false;
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -180.0f);
                playerHand.enemy1Won = false;
                enemy1Turn = true;
                enemy2Turn = false;
                playerTurn = false;
                allyTurn = false;
                hand5.timerToDelete = 0.0f;
                playerHand.timerToDelete = 0.0f;
                enemy1.timerToDelete = 0.0f;
                enemy2.timerToDelete = 0.0f;
                parter.timerToDelete = 0.0f;
                if (whoMadeAlone == WHO_MADE_ALONE.ALLY)
                {
                    parter.handPosition = 2;
                    enemy2.handPosition = 3;
                    playerHand.handPosition = 4;
                    enemy1.handPosition = 4;
                }
                if (firstPlayedCardObj != null)
                {
                    firstPlayedCardObj.tag = "Card";
                    firstPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (secondPlayedCardObj != null)
                {
                    secondPlayedCardObj.tag = "Card";
                    secondPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (thirdPlayedCardObj != null)
                {
                    thirdPlayedCardObj.tag = "Card";
                    thirdPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (fourthPlayedCardObj != null)
                {
                    fourthPlayedCardObj.tag = "Card";
                    fourthPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }

                playerHand.currentPlayingCardObjPlayer = null;
                playerHand.currentPlayingCardObjEnemy1 = null;
                playerHand.currentPlayingCardObjAlly = null;
                playerHand.currentPlayingCardObjEnemy2 = null;
                enemy1.currentPlayingCardObjPlayer = null;
                enemy1.currentPlayingCardObjEnemy1 = null;
                enemy1.currentPlayingCardObjAlly = null;
                enemy1.currentPlayingCardObjEnemy2 = null;
                parter.currentPlayingCardObjPlayer = null;
                parter.currentPlayingCardObjEnemy1 = null;
                parter.currentPlayingCardObjAlly = null;
                parter.currentPlayingCardObjEnemy2 = null;
                enemy2.currentPlayingCardObjPlayer = null;
                enemy2.currentPlayingCardObjEnemy1 = null;
                enemy2.currentPlayingCardObjAlly = null;
                enemy2.currentPlayingCardObjEnemy2 = null;

                playerHand.foundNonTrump = false;
                enemy1.foundNonTrump = false;
                parter.foundNonTrump = false;
                enemy2.foundNonTrump = false;
                playerHand.handHasSameSuit = false;
                enemy1.handHasSameSuit = false;
                parter.handHasSameSuit = false;
                enemy2.handHasSameSuit = false;
                firstPlayedCardObj = null;
                secondPlayedCardObj = null;
                thirdPlayedCardObj = null;
                fourthPlayedCardObj = null;

                playerHand.foundCardToPlay = false;
                enemy1.foundCardToPlay = false;
                parter.foundCardToPlay = false;
                enemy2.foundCardToPlay = false;
                enemy1.foundLowest = false;
                parter.foundLowest = false;
                enemy2.foundLowest = false;
                playerHand.foundLowest = false;

                //ENEMY1
                for (int i = 0; i < enemy1.hand.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.hand[i].ToString()))
                    {
                        enemy1.hand.RemoveAt(i);
                        enemy1.cardObjects.RemoveAt(i);
                        enemy1.cardsToMove.RemoveAt(i);
                    }
                }
                for(int i = 0; i < enemy1.nonMatchingCardsKitty.Count; ++i)
                {
                    if(enemy1.currentPlayingCard.ToString().Contains(enemy1.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy1.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }


                //PARTNER
                for (int i = 0; i < parter.hand.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.hand[i].ToString()))
                    {
                        parter.hand.RemoveAt(i);
                        parter.cardObjects.RemoveAt(i);
                        parter.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < parter.nonMatchingCardsKitty.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.nonMatchingCardsKitty[i].ToString()))
                    {
                        parter.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }

                //ENEMY2
                for (int i = 0; i < enemy2.hand.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.hand[i].ToString()))
                    {
                        enemy2.hand.RemoveAt(i);
                        enemy2.cardObjects.RemoveAt(i);
                        enemy2.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy2.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy2.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }
                trumpMadeCounter = 0;
            }
            if (playerHand.allyWon == true && trumpMadeCounter > 3)
            {
                aloneChangeTimer = 0.0f;
                hasCheckedEnemy1 = false;
                hasCheckedEnemy2 = false;
                hasCheckedAlly = false;
                turnCounter = 0;
                counter = 0;
                parter.handPosition = 1;
                enemy2.handPosition = 2;
                playerHand.handPosition = 3;
                enemy1.handPosition = 4;
                playerHand.cardIsActive = false;
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                playerHand.allyWon = false;
                enemy1Turn = false;
                enemy2Turn = false;
                playerTurn = false;
                allyTurn = true;
                hand5.timerToDelete = 0.0f;
                playerHand.timerToDelete = 0.0f;
                enemy1.timerToDelete = 0.0f;
                enemy2.timerToDelete = 0.0f;
                parter.timerToDelete = 0.0f;
                if(whoMadeAlone == WHO_MADE_ALONE.ALLY)
                {
                    parter.handPosition = 1;
                    enemy2.handPosition = 2;
                    playerHand.handPosition = 3;
                    enemy1.handPosition = 4;
                }
                if (firstPlayedCardObj != null)
                {
                    firstPlayedCardObj.tag = "Card";
                    firstPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (secondPlayedCardObj != null)
                {
                    secondPlayedCardObj.tag = "Card";
                    secondPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (thirdPlayedCardObj != null)
                {
                    thirdPlayedCardObj.tag = "Card";
                    thirdPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (fourthPlayedCardObj != null)
                {
                    fourthPlayedCardObj.tag = "Card";
                    fourthPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                playerHand.currentPlayingCardObjPlayer = null;
                playerHand.currentPlayingCardObjEnemy1 = null;
                playerHand.currentPlayingCardObjAlly = null;
                playerHand.currentPlayingCardObjEnemy2 = null;
                enemy1.currentPlayingCardObjPlayer = null;
                enemy1.currentPlayingCardObjEnemy1 = null;
                enemy1.currentPlayingCardObjAlly = null;
                enemy1.currentPlayingCardObjEnemy2 = null;
                parter.currentPlayingCardObjPlayer = null;
                parter.currentPlayingCardObjEnemy1 = null;
                parter.currentPlayingCardObjAlly = null;
                parter.currentPlayingCardObjEnemy2 = null;
                enemy2.currentPlayingCardObjPlayer = null;
                enemy2.currentPlayingCardObjEnemy1 = null;
                enemy2.currentPlayingCardObjAlly = null;
                enemy2.currentPlayingCardObjEnemy2 = null;
                playerHand.foundNonTrump = false;
                enemy1.foundNonTrump = false;
                parter.foundNonTrump = false;
                enemy2.foundNonTrump = false;
                playerHand.handHasSameSuit = false;
                enemy1.handHasSameSuit = false;
                parter.handHasSameSuit = false;
                enemy2.handHasSameSuit = false;
                firstPlayedCardObj = null;
                secondPlayedCardObj = null;
                thirdPlayedCardObj = null;
                fourthPlayedCardObj = null;

                playerHand.foundCardToPlay = false;
                enemy1.foundCardToPlay = false;
                parter.foundCardToPlay = false;
                enemy2.foundCardToPlay = false;
                enemy1.foundLowest = false;
                parter.foundLowest = false;
                enemy2.foundLowest = false;
                playerHand.foundLowest = false;

                //ENEMY1
                for (int i = 0; i < enemy1.hand.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.hand[i].ToString()))
                    {
                        enemy1.hand.RemoveAt(i);
                        enemy1.cardObjects.RemoveAt(i);
                        enemy1.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy1.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy1.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }


                //PARTNER
                for (int i = 0; i < parter.hand.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.hand[i].ToString()))
                    {
                        parter.hand.RemoveAt(i);
                        parter.cardObjects.RemoveAt(i);
                        parter.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < parter.nonMatchingCardsKitty.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.nonMatchingCardsKitty[i].ToString()))
                    {
                        parter.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }

                //ENEMY2
                for (int i = 0; i < enemy2.hand.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.hand[i].ToString()))
                    {
                        enemy2.hand.RemoveAt(i);
                        enemy2.cardObjects.RemoveAt(i);
                        enemy2.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy2.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy2.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }
                trumpMadeCounter = 0;
            }
            if (playerHand.enemy2Won == true && trumpMadeCounter > 3)
            {
                aloneChangeTimer = 0.0f;
                hasCheckedEnemy1 = false;
                hasCheckedEnemy2 = false;
                hasCheckedAlly = false;
                turnCounter = 0;
                counter = 0;
                enemy2.handPosition = 1;
                playerHand.handPosition = 2;
                enemy1.handPosition = 3;
                parter.handPosition = 4;
                playerHand.cardIsActive = false;
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                playerHand.enemy2Won = false;
                enemy1Turn = false;
                enemy2Turn = true;
                playerTurn = false;
                allyTurn = false;
                hand5.timerToDelete = 0.0f;
                playerHand.timerToDelete = 0.0f;
                enemy1.timerToDelete = 0.0f;
                enemy2.timerToDelete = 0.0f;
                parter.timerToDelete = 0.0f;
                if (whoMadeAlone == WHO_MADE_ALONE.ALLY)
                {
                    parter.handPosition = 4;
                    enemy2.handPosition = 1;
                    playerHand.handPosition = 2;
                    enemy1.handPosition = 3;
                }
                if (firstPlayedCardObj != null)
                {
                    firstPlayedCardObj.tag = "Card";
                    firstPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (secondPlayedCardObj != null)
                {
                    secondPlayedCardObj.tag = "Card";
                    secondPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (thirdPlayedCardObj != null)
                {
                    thirdPlayedCardObj.tag = "Card";
                    thirdPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (fourthPlayedCardObj != null)
                {
                    fourthPlayedCardObj.tag = "Card";
                    fourthPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }

                playerHand.currentPlayingCardObjPlayer = null;
                playerHand.currentPlayingCardObjEnemy1 = null;
                playerHand.currentPlayingCardObjAlly = null;
                playerHand.currentPlayingCardObjEnemy2 = null;
                enemy1.currentPlayingCardObjPlayer = null;
                enemy1.currentPlayingCardObjEnemy1 = null;
                enemy1.currentPlayingCardObjAlly = null;
                enemy1.currentPlayingCardObjEnemy2 = null;
                parter.currentPlayingCardObjPlayer = null;
                parter.currentPlayingCardObjEnemy1 = null;
                parter.currentPlayingCardObjAlly = null;
                parter.currentPlayingCardObjEnemy2 = null;
                enemy2.currentPlayingCardObjPlayer = null;
                enemy2.currentPlayingCardObjEnemy1 = null;
                enemy2.currentPlayingCardObjAlly = null;
                enemy2.currentPlayingCardObjEnemy2 = null;
                playerHand.foundNonTrump = false;
                enemy1.foundNonTrump = false;
                parter.foundNonTrump = false;
                enemy2.foundNonTrump = false;
                playerHand.handHasSameSuit = false;
                enemy1.handHasSameSuit = false;
                parter.handHasSameSuit = false;
                enemy2.handHasSameSuit = false;
                firstPlayedCardObj = null;
                secondPlayedCardObj = null;
                thirdPlayedCardObj = null;
                fourthPlayedCardObj = null;

                playerHand.foundCardToPlay = false;
                enemy1.foundCardToPlay = false;
                parter.foundCardToPlay = false;
                enemy2.foundCardToPlay = false;
                enemy1.foundLowest = false;
                parter.foundLowest = false;
                enemy2.foundLowest = false;
                playerHand.foundLowest = false;

                //ENEMY1
                for (int i = 0; i < enemy1.hand.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.hand[i].ToString()))
                    {
                        enemy1.hand.RemoveAt(i);
                        enemy1.cardObjects.RemoveAt(i);
                        enemy1.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy1.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy1.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }


                //PARTNER
                for (int i = 0; i < parter.hand.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.hand[i].ToString()))
                    {
                        parter.hand.RemoveAt(i);
                        parter.cardObjects.RemoveAt(i);
                        parter.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < parter.nonMatchingCardsKitty.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.nonMatchingCardsKitty[i].ToString()))
                    {
                        parter.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }

                //ENEMY2
                for (int i = 0; i < enemy2.hand.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.hand[i].ToString()))
                    {
                        enemy2.hand.RemoveAt(i);
                        enemy2.cardObjects.RemoveAt(i);
                        enemy2.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy2.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy2.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }
                trumpMadeCounter = 0;
            }
            if (playerHand.playerWon == true && trumpMadeCounter > 3)
            {
                aloneChangeTimer = 0.0f;
                hasCheckedEnemy1 = false;
                hasCheckedEnemy2 = false;
                hasCheckedAlly = false;
                turnCounter = 0;
                counter = 0;
                playerHand.handPosition = 1;
                enemy1.handPosition = 2;
                parter.handPosition = 3;
                enemy2.handPosition = 4;
                playerHand.cardIsActive = false;
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                playerHand.playerWon = false;
                enemy1Turn = false;
                enemy2Turn = false;
                playerTurn = true;
                allyTurn = false;
                hand5.timerToDelete = 0.0f;
                playerHand.timerToDelete = 0.0f;
                enemy1.timerToDelete = 0.0f;
                enemy2.timerToDelete = 0.0f;
                parter.timerToDelete = 0.0f;
                if (whoMadeAlone == WHO_MADE_ALONE.ALLY)
                {
                    //parter.handPosition = 1;
                    //enemy2.handPosition = 2;
                    //playerHand.handPosition = 3;
                    //enemy1.handPosition = 4;
                }
                if (firstPlayedCardObj != null)
                {
                    firstPlayedCardObj.tag = "Card";
                    firstPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (secondPlayedCardObj != null)
                {
                    secondPlayedCardObj.tag = "Card";
                    secondPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (thirdPlayedCardObj != null)
                {
                    thirdPlayedCardObj.tag = "Card";
                    thirdPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }
                if (fourthPlayedCardObj != null)
                {
                    fourthPlayedCardObj.tag = "Card";
                    fourthPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
                }

                playerHand.currentPlayingCardObjPlayer = null;
                playerHand.currentPlayingCardObjEnemy1 = null;
                playerHand.currentPlayingCardObjAlly = null;
                playerHand.currentPlayingCardObjEnemy2 = null;
                enemy1.currentPlayingCardObjPlayer = null;
                enemy1.currentPlayingCardObjEnemy1 = null;
                enemy1.currentPlayingCardObjAlly = null;
                enemy1.currentPlayingCardObjEnemy2 = null;
                parter.currentPlayingCardObjPlayer = null;
                parter.currentPlayingCardObjEnemy1 = null;
                parter.currentPlayingCardObjAlly = null;
                parter.currentPlayingCardObjEnemy2 = null;
                enemy2.currentPlayingCardObjPlayer = null;
                enemy2.currentPlayingCardObjEnemy1 = null;
                enemy2.currentPlayingCardObjAlly = null;
                enemy2.currentPlayingCardObjEnemy2 = null;
                playerHand.foundNonTrump = false;
                enemy1.foundNonTrump = false;
                parter.foundNonTrump = false;
                enemy2.foundNonTrump = false;
                playerHand.handHasSameSuit = false;
                enemy1.handHasSameSuit = false;
                parter.handHasSameSuit = false;
                enemy2.handHasSameSuit = false;
                firstPlayedCardObj = null;
                secondPlayedCardObj = null;
                thirdPlayedCardObj = null;
                fourthPlayedCardObj = null;

                playerHand.foundCardToPlay = false;
                enemy1.foundCardToPlay = false;
                parter.foundCardToPlay = false;
                enemy2.foundCardToPlay = false;
                enemy1.foundLowest = false;
                parter.foundLowest = false;
                enemy2.foundLowest = false;
                playerHand.foundLowest = false;

                //ENEMY1
                for (int i = 0; i < enemy1.hand.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.hand[i].ToString()))
                    {
                        enemy1.hand.RemoveAt(i);
                        enemy1.cardObjects.RemoveAt(i);
                        enemy1.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy1.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy1.currentPlayingCard.ToString().Contains(enemy1.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy1.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }


                //PARTNER
                for (int i = 0; i < parter.hand.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.hand[i].ToString()))
                    {
                        parter.hand.RemoveAt(i);
                        parter.cardObjects.RemoveAt(i);
                        parter.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < parter.nonMatchingCardsKitty.Count; ++i)
                {
                    if (parter.currentPlayingCard.ToString().Contains(parter.nonMatchingCardsKitty[i].ToString()))
                    {
                        parter.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }

                //ENEMY2
                for (int i = 0; i < enemy2.hand.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.hand[i].ToString()))
                    {
                        enemy2.hand.RemoveAt(i);
                        enemy2.cardObjects.RemoveAt(i);
                        enemy2.cardsToMove.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemy2.nonMatchingCardsKitty.Count; ++i)
                {
                    if (enemy2.currentPlayingCard.ToString().Contains(enemy2.nonMatchingCardsKitty[i].ToString()))
                    {
                        enemy2.nonMatchingCardsKitty.RemoveAt(i);
                    }
                }

                trumpMadeCounter = 0;
            }
            if(goingAlone == true && trump.trumpMade == true && trump.orderedUp == true)
            {
                if(setUpFirst == false)
                {
                    if(whoMadeAlone == WHO_MADE_ALONE.PLAYER)
                    {
                        if(startPosForHand == 4)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
                        }
                        else if(startPosForHand == 3)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY2;
                        }
                        else if(startPosForHand == 2)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY2;
                        }
                        else if(startPosForHand == 1)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
                        }
                    }
                    else if(whoMadeAlone == WHO_MADE_ALONE.ALLY)
                    {
                        if (startPosForHand == 4)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
                        }
                        else if (startPosForHand == 3)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ALLY;
                        }
                        else if (startPosForHand == 2)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY2;
                        }
                        else if (startPosForHand == 1)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
                        }
                    }
                    else if(whoMadeAlone == WHO_MADE_ALONE.ENEMY1)
                    {
                        if (startPosForHand == 4)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
                        }
                        else if (startPosForHand == 3)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ALLY;
                        }
                        else if (startPosForHand == 2)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
                        }
                        else if (startPosForHand == 1)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
                        }
                    }
                    else if(whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
                    {
                        if (startPosForHand == 4)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
                        }
                        else if (startPosForHand == 3)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ALLY;
                        }
                        else if (startPosForHand == 2)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
                        }
                        else if (startPosForHand == 1)
                        {
                            playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
                        }
                    }
                    setUpFirst = true;
                }
                if(whoMadeAlone == WHO_MADE_ALONE.ALLY)
                {
                    if (playerHand.handPosition == 4 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayOne();
                          //  enemy1.getWorstCard();
                            hasCheckedEnemy1 = true;
                            firstPlayedCard = enemy1.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                                // cardCount++;
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if(trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if(trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if(trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if(trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }

                        }
                        else if (trumpMadeCounter == 1 && hasCheckedAlly == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayAlone();
                           // parter.getWorstCard();
                            secondPlayedCard = parter.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayThree();
                          //  enemy2.getWorstCard();
                            thirdPlayedCard = enemy2.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 3 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = parter.trumpString;
                        playerHand.trumpStringOther = parter.trumpStringOther;
                        playerHand.toFollowString = parter.toFollowString;
                        playerHand.toFollowStringOther = parter.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedAlly == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayAlone();
                            firstPlayedCard = parter.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayTwo();
                            secondPlayedCard = enemy2.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayThree();
                            thirdPlayedCard = enemy1.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 2 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy2.trumpString;
                        playerHand.trumpStringOther = enemy2.trumpStringOther;
                        playerHand.toFollowString = enemy2.toFollowString;
                        playerHand.toFollowStringOther = enemy2.toFollowStringOther;
                        if (trumpMadeCounter == 0 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayOne();
                            firstPlayedCard = enemy2.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());

                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy2Card";
                                //enemy1.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayTwo();
                            secondPlayedCard = enemy1.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedAlly == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayAlone();
                            thirdPlayedCard = parter.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 1 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 ||trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayOne();
                            firstPlayedCard = enemy1.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedAlly == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayAlone();
                            secondPlayedCard = parter.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayThree();
                            thirdPlayedCard = enemy2.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                }
                if(whoMadeAlone == WHO_MADE_ALONE.ENEMY1)
                {
                    if (playerHand.handPosition == 4 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayAlone();
                            hasCheckedEnemy1 = true;
                            firstPlayedCard = enemy1.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                                // cardCount++;
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedAlly == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayTwo();
                            secondPlayedCard = parter.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                        }
                      
                        else if (trumpMadeCounter == 2 || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            thirdPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.deck.Count; ++i)
                            {
                                if (thirdPlayedCardObj != null)
                                {
                                    if (thirdPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        thirdPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                            //enemy1.foundCardToPlay = false;
                            //enemy2.foundCardToPlay = false;
                            //parter.foundCardToPlay = false;
                        }
                    }
                    else if (playerHand.handPosition == 3 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = parter.trumpString;
                        playerHand.trumpStringOther = parter.trumpStringOther;
                        playerHand.toFollowString = parter.toFollowString;
                        playerHand.toFollowStringOther = parter.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedAlly == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayOne();
                            firstPlayedCard = parter.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            secondPlayedCard = playerHand.currentPlayingCard;
                            secondPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (secondPlayedCardObj != null)
                                {
                                    if (secondPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        secondPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayAlone();
                            thirdPlayedCard = enemy1.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 2 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy2.trumpString;
                        playerHand.trumpStringOther = enemy2.trumpStringOther;
                        playerHand.toFollowString = enemy2.toFollowString;
                        playerHand.toFollowStringOther = enemy2.toFollowStringOther;

                        if (trumpMadeCounter == 0)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            firstPlayedCard = playerHand.currentPlayingCard;
                            firstPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (firstPlayedCardObj != null)
                                {
                                    if (firstPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        firstPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                            if (firstPlayedCardObj != null)
                            {
                                if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";

                                    if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        playerHand.toFollowString = "DIAMOND";
                                        playerHand.toFollowStringOther = "HEART";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        enemy1.toFollowString = "DIAMOND";
                                        enemy1.toFollowStringOther = "HEART";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        parter.toFollowString = "DIAMOND";
                                        parter.toFollowStringOther = "HEART";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        enemy2.toFollowString = "DIAMOND";
                                        enemy2.toFollowStringOther = "HEART";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";

                                    if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        playerHand.toFollowString = "HEART";
                                        playerHand.toFollowStringOther = "DIAMOND";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        enemy1.toFollowString = "HEART";
                                        enemy1.toFollowStringOther = "DIAMOND";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        parter.toFollowString = "HEART";
                                        parter.toFollowStringOther = "DIAMOND";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        enemy2.toFollowString = "HEART";
                                        enemy2.toFollowStringOther = "DIAMOND";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";

                                    if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        playerHand.toFollowString = "CLUB";
                                        playerHand.toFollowStringOther = "SPADE";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        enemy1.toFollowString = "CLUB";
                                        enemy1.toFollowStringOther = "SPADE";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        parter.toFollowString = "CLUB";
                                        parter.toFollowStringOther = "SPADE";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        enemy2.toFollowString = "CLUB";
                                        enemy2.toFollowStringOther = "SPADE";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";

                                    if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        playerHand.toFollowString = "SPADE";
                                        playerHand.toFollowStringOther = "CLUB";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        enemy1.toFollowString = "SPADE";
                                        enemy1.toFollowStringOther = "CLUB";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        parter.toFollowString = "SPADE";
                                        parter.toFollowStringOther = "CLUB";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        enemy2.toFollowString = "SPADE";
                                        enemy2.toFollowStringOther = "CLUB";
                                    }
                                }
                            }
                            if (secondPlayedCardObj != null)
                            {
                                //playerHand.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayAlone();
                            secondPlayedCard = enemy1.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedAlly == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayThree();
                            thirdPlayedCard = parter.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 1 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            firstPlayedCard = playerHand.currentPlayingCard;
                            firstPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (firstPlayedCardObj != null)
                                {
                                    if (firstPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        firstPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                            if (firstPlayedCardObj != null)
                            {
                                //playerHand.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 ||trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayAlone();
                            secondPlayedCard = enemy1.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedAlly == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayThree();
                            thirdPlayedCard = parter.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                }
                if(whoMadeAlone == WHO_MADE_ALONE.PLAYER)
                {
                    if (playerHand.handPosition == 4 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayOne();
                            hasCheckedEnemy1 = true;
                            firstPlayedCard = enemy1.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                                // cardCount++;
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }

                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayTwo();
                            secondPlayedCard = enemy2.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            thirdPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.deck.Count; ++i)
                            {
                                if (thirdPlayedCardObj != null)
                                {
                                    if (thirdPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        thirdPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                            //enemy1.foundCardToPlay = false;
                            //enemy2.foundCardToPlay = false;
                            //parter.foundCardToPlay = false;
                        }
                    }
                    else if (playerHand.handPosition == 3 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayOne();
                            hasCheckedEnemy2 = true;
                            firstPlayedCard = enemy2.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy2Card";
                                //enemy1.playCards();
                                // cardCount++;
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }

                        }
                        else if (trumpMadeCounter == 1)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            secondPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.deck.Count; ++i)
                            {
                                if (secondPlayedCardObj != null)
                                {
                                    if (secondPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        secondPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayThree();
                            hasCheckedEnemy1 = true;
                            thirdPlayedCard = enemy1.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                                // cardCount++;
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                            //enemy1.foundCardToPlay = false;
                            //enemy2.foundCardToPlay = false;
                            //parter.foundCardToPlay = false;
                        }
                    }
                    else if (playerHand.handPosition == 2 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy2.trumpString;
                        playerHand.trumpStringOther = enemy2.trumpStringOther;
                        playerHand.toFollowString = enemy2.toFollowString;
                        playerHand.toFollowStringOther = enemy2.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayOne();
                            firstPlayedCard = enemy2.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            secondPlayedCard = playerHand.currentPlayingCard;
                            secondPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (secondPlayedCardObj != null)
                                {
                                    if (secondPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        secondPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }

                            if (secondPlayedCardObj != null)
                            {
                                //playerHand.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayThree();
                            thirdPlayedCard = enemy1.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 1 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            firstPlayedCard = playerHand.currentPlayingCard;
                            firstPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (firstPlayedCardObj != null)
                                {
                                    if (firstPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        firstPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                            if (firstPlayedCardObj != null)
                            {
                                //playerHand.playCards();
                            }
                            if (firstPlayedCardObj != null)
                            {
                                if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";

                                    if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        playerHand.toFollowString = "DIAMOND";
                                        playerHand.toFollowStringOther = "HEART";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        enemy1.toFollowString = "DIAMOND";
                                        enemy1.toFollowStringOther = "HEART";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        parter.toFollowString = "DIAMOND";
                                        parter.toFollowStringOther = "HEART";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        enemy2.toFollowString = "DIAMOND";
                                        enemy2.toFollowStringOther = "HEART";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";

                                    if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        playerHand.toFollowString = "HEART";
                                        playerHand.toFollowStringOther = "DIAMOND";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        enemy1.toFollowString = "HEART";
                                        enemy1.toFollowStringOther = "DIAMOND";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        parter.toFollowString = "HEART";
                                        parter.toFollowStringOther = "DIAMOND";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        enemy2.toFollowString = "HEART";
                                        enemy2.toFollowStringOther = "DIAMOND";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";

                                    if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        playerHand.toFollowString = "CLUB";
                                        playerHand.toFollowStringOther = "SPADE";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        enemy1.toFollowString = "CLUB";
                                        enemy1.toFollowStringOther = "SPADE";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        parter.toFollowString = "CLUB";
                                        parter.toFollowStringOther = "SPADE";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        enemy2.toFollowString = "CLUB";
                                        enemy2.toFollowStringOther = "SPADE";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";

                                    if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        playerHand.toFollowString = "SPADE";
                                        playerHand.toFollowStringOther = "CLUB";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        enemy1.toFollowString = "SPADE";
                                        enemy1.toFollowStringOther = "CLUB";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        parter.toFollowString = "SPADE";
                                        parter.toFollowStringOther = "CLUB";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        enemy2.toFollowString = "SPADE";
                                        enemy2.toFollowStringOther = "CLUB";
                                    }
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 ||trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedEnemy1 = true;
                            playerTurn = false;
                            enemy1Turn = true;
                            allyTurn = false;
                            enemy2Turn = false;
                            enemy1.cardToPlayTwo();
                            secondPlayedCard = enemy1.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy1Card";
                                //enemy1.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayThree();
                            thirdPlayedCard = enemy2.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                }
                if(whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
                {
                    if(isFirstTime == false)
                    {
                        playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ALLY;
                        isFirstTime = true;
                    }
                    if (playerHand.handPosition == 4 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedAlly == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayOne();
                            firstPlayedCard = parter.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayAlone();
                            secondPlayedCard = enemy2.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            thirdPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.deck.Count; ++i)
                            {
                                if (thirdPlayedCardObj != null)
                                {
                                    if (thirdPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        thirdPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }


                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                            //enemy1.foundCardToPlay = false;
                            //enemy2.foundCardToPlay = false;
                            //parter.foundCardToPlay = false;
                        }
                    }
                    else if (playerHand.handPosition == 3 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = parter.trumpString;
                        playerHand.trumpStringOther = parter.trumpStringOther;
                        playerHand.toFollowString = parter.toFollowString;
                        playerHand.toFollowStringOther = parter.toFollowStringOther;

                        if (trumpMadeCounter == 0 && hasCheckedAlly == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayOne();
                            firstPlayedCard = parter.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "AllyCard";
                                //parter.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayAlone();
                            secondPlayedCard = enemy2.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "Enemy2Card";
                                //enemy2.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            thirdPlayedCard = playerHand.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (thirdPlayedCardObj != null)
                                {
                                    if (thirdPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        thirdPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }

                    }
                    else if (playerHand.handPosition == 2 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy2.trumpString;
                        playerHand.trumpStringOther = enemy2.trumpStringOther;
                        playerHand.toFollowString = enemy2.toFollowString;
                        playerHand.toFollowStringOther = enemy2.toFollowStringOther;
                        if (trumpMadeCounter == 0 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayAlone();
                            firstPlayedCard = enemy2.currentPlayingCard;
                            firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                            if (firstPlayedCardObj != null)
                            {
                                firstPlayedCardObj.tag = "Enemy2Card";
                             //   //enemy2.playCards();
                            }
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            secondPlayedCard = playerHand.currentPlayingCard;
                            secondPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (secondPlayedCardObj != null)
                                {
                                    if (secondPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        secondPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                        }
                        else if (trumpMadeCounter == 3 && hasCheckedAlly == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayThree();
                            thirdPlayedCard = parter.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "AllyCard";
                             //   //parter.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                    else if (playerHand.handPosition == 1 && foundWhoWonGame == false)
                    {
                        playerHand.trumpString = enemy1.trumpString;
                        playerHand.trumpStringOther = enemy1.trumpStringOther;
                        playerHand.toFollowString = enemy1.toFollowString;
                        playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                        if (trumpMadeCounter == 0)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                        {
                            playerTurn = true;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = false;
                            firstPlayedCard = playerHand.currentPlayingCard;
                            firstPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                            for (int i = 0; i < deck.cards.Count; ++i)
                            {
                                if (firstPlayedCardObj != null)
                                {
                                    if (firstPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                    {
                                        firstPlayedCard = deck.deck[i];
                                        break;
                                    }
                                }
                            }
                            if (firstPlayedCardObj != null)
                            {
                                //////playerHand.playCards();
                            }
                            if (firstPlayedCardObj != null)
                            {
                                if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";

                                    if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        playerHand.toFollowString = "DIAMOND";
                                        playerHand.toFollowStringOther = "HEART";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        enemy1.toFollowString = "DIAMOND";
                                        enemy1.toFollowStringOther = "HEART";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        parter.toFollowString = "DIAMOND";
                                        parter.toFollowStringOther = "HEART";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                        enemy2.toFollowString = "DIAMOND";
                                        enemy2.toFollowStringOther = "HEART";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";

                                    if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        playerHand.toFollowString = "HEART";
                                        playerHand.toFollowStringOther = "DIAMOND";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        enemy1.toFollowString = "HEART";
                                        enemy1.toFollowStringOther = "DIAMOND";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        parter.toFollowString = "HEART";
                                        parter.toFollowStringOther = "DIAMOND";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                        enemy2.toFollowString = "HEART";
                                        enemy2.toFollowStringOther = "DIAMOND";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";

                                    if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        playerHand.toFollowString = "CLUB";
                                        playerHand.toFollowStringOther = "SPADE";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        enemy1.toFollowString = "CLUB";
                                        enemy1.toFollowStringOther = "SPADE";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        parter.toFollowString = "CLUB";
                                        parter.toFollowStringOther = "SPADE";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                        enemy2.toFollowString = "CLUB";
                                        enemy2.toFollowStringOther = "SPADE";
                                    }
                                }
                                else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";

                                    if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                    {
                                        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        playerHand.toFollowString = "SPADE";
                                        playerHand.toFollowStringOther = "CLUB";

                                        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        enemy1.toFollowString = "SPADE";
                                        enemy1.toFollowStringOther = "CLUB";

                                        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        parter.toFollowString = "SPADE";
                                        parter.toFollowStringOther = "CLUB";

                                        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                        enemy2.toFollowString = "SPADE";
                                        enemy2.toFollowStringOther = "CLUB";
                                    }
                                }
                            }
                        }
                        else if (trumpMadeCounter == 1 && hasCheckedAlly == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                        {
                            hasCheckedAlly = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = true;
                            enemy2Turn = false;
                            parter.cardToPlayTwo();
                            secondPlayedCard = parter.currentPlayingCard;
                            secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                            if (secondPlayedCardObj != null)
                            {
                                secondPlayedCardObj.tag = "AllyCard";
                                ////parter.playCards();
                            }
                        }
                        else if (trumpMadeCounter == 2 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                        {
                            hasCheckedEnemy2 = true;
                            playerTurn = false;
                            enemy1Turn = false;
                            allyTurn = false;
                            enemy2Turn = true;
                            enemy2.cardToPlayAlone();
                            thirdPlayedCard = enemy2.currentPlayingCard;
                            thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                            if (thirdPlayedCardObj != null)
                            {
                                thirdPlayedCardObj.tag = "Enemy2Card";
                              //  //enemy2.playCards();
                            }

                            if (thirdPlayedCardObj != null)
                            {
                                aloneChangeTimer += Time.deltaTime;
                                if (aloneChangeTimer > 0.5f)
                                {
                                    trumpMadeCounter = 4;
                                }
                                //playerHand.playCards();
                            }
                        }
                    }
                }
            }
            if (goingAlone == false && trump.trumpMade == true && trump.orderedUp == true)
            {
                if (playerHand.handPosition == 4 && foundWhoWonGame == false)
                {
                    playerHand.trumpString = enemy1.trumpString;
                    playerHand.trumpStringOther = enemy1.trumpStringOther;
                    playerHand.toFollowString = enemy1.toFollowString;
                    playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                    if (trumpMadeCounter == 0 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                    {
                        playerTurn = false;
                        enemy1Turn = true;
                        allyTurn = false;
                        enemy2Turn = false;
                        enemy1.cardToPlayOne();
                      //  enemy1.getWorstCard();
                        hasCheckedEnemy1 = true;
                        firstPlayedCard = enemy1.currentPlayingCard;
                        firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                        if (firstPlayedCardObj != null)
                        {
                            firstPlayedCardObj.tag = "Enemy1Card";
                           // //enemy1.playCards();
                            // cardCount++;
                        }
                        if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            playerHand.toFollowString = "HEART";
                            playerHand.toFollowStringOther = "DIAMOND";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            enemy1.toFollowString = "HEART";
                            enemy1.toFollowStringOther = "DIAMOND";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            parter.toFollowString = "HEART";
                            parter.toFollowStringOther = "DIAMOND";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            enemy2.toFollowString = "HEART";
                            enemy2.toFollowStringOther = "DIAMOND";

                            if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            playerHand.toFollowString = "DIAMOND";
                            playerHand.toFollowStringOther = "HEART";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            enemy1.toFollowString = "DIAMOND";
                            enemy1.toFollowStringOther = "HEART";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            parter.toFollowString = "DIAMOND";
                            parter.toFollowStringOther = "HEART";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            enemy2.toFollowString = "DIAMOND";
                            enemy2.toFollowStringOther = "HEART";

                            if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            playerHand.toFollowString = "SPADE";
                            playerHand.toFollowStringOther = "CLUB";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            enemy1.toFollowString = "SPADE";
                            enemy1.toFollowStringOther = "CLUB";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            parter.toFollowString = "SPADE";
                            parter.toFollowStringOther = "CLUB";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            enemy2.toFollowString = "SPADE";
                            enemy2.toFollowStringOther = "CLUB";

                            if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            playerHand.toFollowString = "CLUB";
                            playerHand.toFollowStringOther = "SPADE";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            enemy1.toFollowString = "CLUB";
                            enemy1.toFollowStringOther = "SPADE";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            parter.toFollowString = "CLUB";
                            parter.toFollowStringOther = "SPADE";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            enemy2.toFollowString = "CLUB";
                            enemy2.toFollowStringOther = "SPADE";

                            if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";
                            }
                        }

                    }
                    else if (trumpMadeCounter == 1 && hasCheckedAlly == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                    {
                        hasCheckedAlly = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = true;
                        enemy2Turn = false;
                        parter.cardToPlayTwo();
                       // parter.getWorstCard();
                        secondPlayedCard = parter.currentPlayingCard;
                        secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                        if (secondPlayedCardObj != null)
                        {
                            secondPlayedCardObj.tag = "AllyCard";
                           // //parter.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 2 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                    {
                        hasCheckedEnemy2 = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = true;
                        enemy2.cardToPlayThree();
                      //  enemy2.getWorstCard();
                        thirdPlayedCard = enemy2.currentPlayingCard;
                        thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                        if (thirdPlayedCardObj != null)
                        {
                            thirdPlayedCardObj.tag = "Enemy2Card";
                          //  //enemy2.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 3 || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                    {
                        playerTurn = true;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = false;
                        fourthPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                        for (int i = 0; i < deck.deck.Count; ++i)
                        {
                            if (fourthPlayedCardObj != null)
                            {
                                if (fourthPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                {
                                    fourthPlayedCard = deck.deck[i];
                                    break;
                                }
                            }
                        }

                        if (fourthPlayedCardObj != null)
                        {
                           // //playerHand.playCards();
                        }
                        //enemy1.foundCardToPlay = false;
                        //enemy2.foundCardToPlay = false;
                        //parter.foundCardToPlay = false;
                    }
                }
                else if (playerHand.handPosition == 3 && foundWhoWonGame == false)
                {
                    playerHand.trumpString = parter.trumpString;
                    playerHand.trumpStringOther = parter.trumpStringOther;
                    playerHand.toFollowString = parter.toFollowString;
                    playerHand.toFollowStringOther = parter.toFollowStringOther;

                    if (trumpMadeCounter == 0 && hasCheckedAlly == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                    {
                        hasCheckedAlly = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = true;
                        enemy2Turn = false;
                        parter.cardToPlayOne();
                        firstPlayedCard = parter.currentPlayingCard;
                        firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                        if (firstPlayedCardObj != null)
                        {
                            firstPlayedCardObj.tag = "AllyCard";
                       //     //parter.playCards();
                        }
                        if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            playerHand.toFollowString = "HEART";
                            playerHand.toFollowStringOther = "DIAMOND";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            enemy1.toFollowString = "HEART";
                            enemy1.toFollowStringOther = "DIAMOND";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            parter.toFollowString = "HEART";
                            parter.toFollowStringOther = "DIAMOND";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            enemy2.toFollowString = "HEART";
                            enemy2.toFollowStringOther = "DIAMOND";

                            if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            playerHand.toFollowString = "DIAMOND";
                            playerHand.toFollowStringOther = "HEART";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            enemy1.toFollowString = "DIAMOND";
                            enemy1.toFollowStringOther = "HEART";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            parter.toFollowString = "DIAMOND";
                            parter.toFollowStringOther = "HEART";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            enemy2.toFollowString = "DIAMOND";
                            enemy2.toFollowStringOther = "HEART";

                            if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            playerHand.toFollowString = "SPADE";
                            playerHand.toFollowStringOther = "CLUB";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            enemy1.toFollowString = "SPADE";
                            enemy1.toFollowStringOther = "CLUB";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            parter.toFollowString = "SPADE";
                            parter.toFollowStringOther = "CLUB";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            enemy2.toFollowString = "SPADE";
                            enemy2.toFollowStringOther = "CLUB";

                            if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            playerHand.toFollowString = "CLUB";
                            playerHand.toFollowStringOther = "SPADE";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            enemy1.toFollowString = "CLUB";
                            enemy1.toFollowStringOther = "SPADE";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            parter.toFollowString = "CLUB";
                            parter.toFollowStringOther = "SPADE";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            enemy2.toFollowString = "CLUB";
                            enemy2.toFollowStringOther = "SPADE";

                            if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";
                            }
                        }
                    }
                    else if (trumpMadeCounter == 1 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                    {
                        hasCheckedEnemy2 = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = true;
                        enemy2.cardToPlayTwo();
                        secondPlayedCard = enemy2.currentPlayingCard;
                        secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                        if (secondPlayedCardObj != null)
                        {
                            secondPlayedCardObj.tag = "Enemy2Card";
                           // //enemy2.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 2)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                    {
                        playerTurn = true;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = false;
                        thirdPlayedCard = playerHand.currentPlayingCard;
                        thirdPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                        for (int i = 0; i < deck.cards.Count; ++i)
                        {
                            if (thirdPlayedCardObj != null)
                            {
                                if (thirdPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                {
                                    thirdPlayedCard = deck.deck[i];
                                    break;
                                }
                            }
                        }
                        if (thirdPlayedCardObj != null)
                        {
                            ////playerHand.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 3 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                    {
                        hasCheckedEnemy1 = true;
                        playerTurn = false;
                        enemy1Turn = true;
                        allyTurn = false;
                        enemy2Turn = false;
                        enemy1.cardToPlayFour();
                        fourthPlayedCard = enemy1.currentPlayingCard;
                        fourthPlayedCardObj = GameObject.Find(fourthPlayedCard.ToString());
                        if (fourthPlayedCardObj != null)
                        {
                            fourthPlayedCardObj.tag = "Enemy1Card";
                            ////enemy1.playCards();
                        }
                    }
                }
                else if (playerHand.handPosition == 2 && foundWhoWonGame == false)
                {
                    playerHand.trumpString = enemy2.trumpString;
                    playerHand.trumpStringOther = enemy2.trumpStringOther;
                    playerHand.toFollowString = enemy2.toFollowString;
                    playerHand.toFollowStringOther = enemy2.toFollowStringOther;
                    if (trumpMadeCounter == 0 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                    {
                        hasCheckedEnemy2 = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = true;
                        enemy2.cardToPlayOne();
                        firstPlayedCard = enemy2.currentPlayingCard;
                        firstPlayedCardObj = GameObject.Find(firstPlayedCard.ToString());
                        if (firstPlayedCardObj != null)
                        {
                            firstPlayedCardObj.tag = "Enemy2Card";
                         //   //enemy2.playCards();
                        }
                        if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            playerHand.toFollowString = "HEART";
                            playerHand.toFollowStringOther = "DIAMOND";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            enemy1.toFollowString = "HEART";
                            enemy1.toFollowStringOther = "DIAMOND";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            parter.toFollowString = "HEART";
                            parter.toFollowStringOther = "DIAMOND";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                            enemy2.toFollowString = "HEART";
                            enemy2.toFollowStringOther = "DIAMOND";

                            if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            playerHand.toFollowString = "DIAMOND";
                            playerHand.toFollowStringOther = "HEART";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            enemy1.toFollowString = "DIAMOND";
                            enemy1.toFollowStringOther = "HEART";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            parter.toFollowString = "DIAMOND";
                            parter.toFollowStringOther = "HEART";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                            enemy2.toFollowString = "DIAMOND";
                            enemy2.toFollowStringOther = "HEART";

                            if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            playerHand.toFollowString = "SPADE";
                            playerHand.toFollowStringOther = "CLUB";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            enemy1.toFollowString = "SPADE";
                            enemy1.toFollowStringOther = "CLUB";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            parter.toFollowString = "SPADE";
                            parter.toFollowStringOther = "CLUB";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                            enemy2.toFollowString = "SPADE";
                            enemy2.toFollowStringOther = "CLUB";

                            if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";
                            }
                        }
                        else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                        {
                            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            playerHand.toFollowString = "CLUB";
                            playerHand.toFollowStringOther = "SPADE";

                            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            enemy1.toFollowString = "CLUB";
                            enemy1.toFollowStringOther = "SPADE";

                            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            parter.toFollowString = "CLUB";
                            parter.toFollowStringOther = "SPADE";

                            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                            enemy2.toFollowString = "CLUB";
                            enemy2.toFollowStringOther = "SPADE";

                            if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";
                            }
                        }
                    }
                    else if (trumpMadeCounter == 1)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 || trumpMadeCounter == 16 || trumpMadeCounter == 21)
                    {
                        playerTurn = true;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = false;
                        secondPlayedCard = playerHand.currentPlayingCard;
                        secondPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                        for (int i = 0; i < deck.cards.Count; ++i)
                        {
                            if (secondPlayedCardObj != null)
                            {
                                if (secondPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                {
                                    secondPlayedCard = deck.deck[i];
                                    break;
                                }
                            }
                        }

                        if (secondPlayedCardObj != null)
                        {
                          //  //playerHand.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 2 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                    {
                        hasCheckedEnemy1 = true;
                        playerTurn = false;
                        enemy1Turn = true;
                        allyTurn = false;
                        enemy2Turn = false;
                        enemy1.cardToPlayThree();
                        thirdPlayedCard = enemy1.currentPlayingCard;
                        thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                        if (thirdPlayedCardObj != null)
                        {
                            thirdPlayedCardObj.tag = "Enemy1Card";
                        //   //enemy1.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 3 && hasCheckedAlly == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                    {
                        hasCheckedAlly = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = true;
                        enemy2Turn = false;
                        parter.cardToPlayFour();
                        fourthPlayedCard = parter.currentPlayingCard;
                        fourthPlayedCardObj = GameObject.Find(fourthPlayedCard.ToString());
                        if (fourthPlayedCardObj != null)
                        {
                            fourthPlayedCardObj.tag = "AllyCard";
                        //    //parter.playCards();
                        }
                    }
                }
                else if (playerHand.handPosition == 1 && foundWhoWonGame == false)
                {
                    playerHand.trumpString = enemy1.trumpString;
                    playerHand.trumpStringOther = enemy1.trumpStringOther;
                    playerHand.toFollowString = enemy1.toFollowString;
                    playerHand.toFollowStringOther = enemy1.toFollowStringOther;

                    if (trumpMadeCounter == 0)// || trumpMadeCounter == 5 || trumpMadeCounter == 10 || trumpMadeCounter == 15 || trumpMadeCounter == 20)
                    {
                        playerTurn = true;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = false;
                        firstPlayedCard = playerHand.currentPlayingCard;
                        firstPlayedCardObj = GameObject.FindGameObjectWithTag("Selected");

                        for (int i = 0; i < deck.cards.Count; ++i)
                        {
                            if (firstPlayedCardObj != null)
                            {
                                if (firstPlayedCardObj.ToString().Contains(deck.deck[i].ToString()))
                                {
                                    firstPlayedCard = deck.deck[i];
                                    break;
                                }
                            }
                        }
                        if (firstPlayedCardObj != null)
                        {
                         //   playerHand.playCards();
                        }
                        if (firstPlayedCardObj != null)
                        {
                            if (firstPlayedCard.ToString().Contains("HEART") || (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                playerHand.toFollowString = "HEART";
                                playerHand.toFollowStringOther = "DIAMOND";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy1.toFollowString = "HEART";
                                enemy1.toFollowStringOther = "DIAMOND";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                parter.toFollowString = "HEART";
                                parter.toFollowStringOther = "DIAMOND";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                enemy2.toFollowString = "HEART";
                                enemy2.toFollowStringOther = "DIAMOND";

                                if (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    playerHand.toFollowString = "DIAMOND";
                                    playerHand.toFollowStringOther = "HEART";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy1.toFollowString = "DIAMOND";
                                    enemy1.toFollowStringOther = "HEART";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    parter.toFollowString = "DIAMOND";
                                    parter.toFollowStringOther = "HEART";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                    enemy2.toFollowString = "DIAMOND";
                                    enemy2.toFollowStringOther = "HEART";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("DIAMOND") || (trump.trump == Trump.TRUMP.DIAMONDS && firstPlayedCard.ToString().Contains("JACK_HEARTS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                playerHand.toFollowString = "DIAMOND";
                                playerHand.toFollowStringOther = "HEART";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy1.toFollowString = "DIAMOND";
                                enemy1.toFollowStringOther = "HEART";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                parter.toFollowString = "DIAMOND";
                                parter.toFollowStringOther = "HEART";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.DIAMONDS;
                                enemy2.toFollowString = "DIAMOND";
                                enemy2.toFollowStringOther = "HEART";

                                if (trump.trump == Trump.TRUMP.HEARTS && firstPlayedCard.ToString().Contains("JACK_DIAMONDS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    playerHand.toFollowString = "HEART";
                                    playerHand.toFollowStringOther = "DIAMOND";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy1.toFollowString = "HEART";
                                    enemy1.toFollowStringOther = "DIAMOND";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    parter.toFollowString = "HEART";
                                    parter.toFollowStringOther = "DIAMOND";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.HEARTS;
                                    enemy2.toFollowString = "HEART";
                                    enemy2.toFollowStringOther = "DIAMOND";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("SPADE") || (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                playerHand.toFollowString = "SPADE";
                                playerHand.toFollowStringOther = "CLUB";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy1.toFollowString = "SPADE";
                                enemy1.toFollowStringOther = "CLUB";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                parter.toFollowString = "SPADE";
                                parter.toFollowStringOther = "CLUB";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                enemy2.toFollowString = "SPADE";
                                enemy2.toFollowStringOther = "CLUB";

                                if (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    playerHand.toFollowString = "CLUB";
                                    playerHand.toFollowStringOther = "SPADE";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy1.toFollowString = "CLUB";
                                    enemy1.toFollowStringOther = "SPADE";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    parter.toFollowString = "CLUB";
                                    parter.toFollowStringOther = "SPADE";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                    enemy2.toFollowString = "CLUB";
                                    enemy2.toFollowStringOther = "SPADE";
                                }
                            }
                            else if (firstPlayedCard.ToString().Contains("CLUB") || (trump.trump == Trump.TRUMP.CLUBS && firstPlayedCard.ToString().Contains("JACK_SPADES")))
                            {
                                playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                playerHand.toFollowString = "CLUB";
                                playerHand.toFollowStringOther = "SPADE";

                                enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy1.toFollowString = "CLUB";
                                enemy1.toFollowStringOther = "SPADE";

                                parter.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                parter.toFollowString = "CLUB";
                                parter.toFollowStringOther = "SPADE";

                                enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.CLUBS;
                                enemy2.toFollowString = "CLUB";
                                enemy2.toFollowStringOther = "SPADE";

                                if (trump.trump == Trump.TRUMP.SPADES && firstPlayedCard.ToString().Contains("JACK_CLUBS"))
                                {
                                    playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    playerHand.toFollowString = "SPADE";
                                    playerHand.toFollowStringOther = "CLUB";

                                    enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy1.toFollowString = "SPADE";
                                    enemy1.toFollowStringOther = "CLUB";

                                    parter.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    parter.toFollowString = "SPADE";
                                    parter.toFollowStringOther = "CLUB";

                                    enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.SPADES;
                                    enemy2.toFollowString = "SPADE";
                                    enemy2.toFollowStringOther = "CLUB";
                                }
                            }
                        }
                    }
                    else if (trumpMadeCounter == 1 && hasCheckedEnemy1 == false)// || trumpMadeCounter == 6 || trumpMadeCounter == 11 ||trumpMadeCounter == 16 || trumpMadeCounter == 21)
                    {
                        hasCheckedEnemy1 = true;
                        playerTurn = false;
                        enemy1Turn = true;
                        allyTurn = false;
                        enemy2Turn = false;
                        enemy1.cardToPlayTwo();
                        secondPlayedCard = enemy1.currentPlayingCard;
                        secondPlayedCardObj = GameObject.Find(secondPlayedCard.ToString());
                        if (secondPlayedCardObj != null)
                        {
                            secondPlayedCardObj.tag = "Enemy1Card";
                          //  //enemy1.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 2 && hasCheckedAlly == false)// || trumpMadeCounter == 7 || trumpMadeCounter == 12 || trumpMadeCounter == 17 || trumpMadeCounter == 22)
                    {
                        hasCheckedAlly = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = true;
                        enemy2Turn = false;
                        parter.cardToPlayThree();
                        thirdPlayedCard = parter.currentPlayingCard;
                        thirdPlayedCardObj = GameObject.Find(thirdPlayedCard.ToString());
                        if (thirdPlayedCardObj != null)
                        {
                            thirdPlayedCardObj.tag = "AllyCard";
                          //  //parter.playCards();
                        }
                    }
                    else if (trumpMadeCounter == 3 && hasCheckedEnemy2 == false)// || trumpMadeCounter == 8 || trumpMadeCounter == 13 || trumpMadeCounter == 18 || trumpMadeCounter == 23)
                    {
                        hasCheckedEnemy2 = true;
                        playerTurn = false;
                        enemy1Turn = false;
                        allyTurn = false;
                        enemy2Turn = true;
                        enemy2.cardToPlayFour();
                        fourthPlayedCard = enemy2.currentPlayingCard;
                        fourthPlayedCardObj = GameObject.Find(fourthPlayedCard.ToString());
                        if (fourthPlayedCardObj != null)
                        {
                            fourthPlayedCardObj.tag = "Enemy2Card";
                          //  //enemy2.playCards();
                        }
                    }
                }
            }
        }
        if (goingAlone == false)
        {
            if (trump.addedCard == true)
            {
                if (playerHand.handPosition == 4)
                {
                    if (playerTurn == true && fourthPlayedCardObj == null && trumpMadeCounter > 1)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 3)
                {
                    if (playerTurn == true && thirdPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 2)
                {
                    if (playerTurn == true && secondPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 1)
                {
                    if (playerTurn == true && firstPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
            }
            else
            {
                readyToGo = true;
            }
        }
        else
        {
            if(whoMadeAlone == WHO_MADE_ALONE.PLAYER)
            {
                if(playerHand.handPosition == 1)
                {
                    if (playerTurn == true && firstPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if(playerHand.handPosition == 2)
                {
                    if (playerTurn == true && secondPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if(playerHand.handPosition == 3)
                {
                    if (playerTurn == true && secondPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }

                }
                else if(playerHand.handPosition == 4)
                {
                    if (playerTurn == true && thirdPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
            }
            else if(whoMadeAlone == WHO_MADE_ALONE.ENEMY1)
            {
                if (playerHand.handPosition == 1)
                {
                    if (playerTurn == true && firstPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 2)
                {
                    if (playerTurn == true && firstPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 3)
                {
                    if (playerTurn == true && secondPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }

                }
                else if (playerHand.handPosition == 4)
                {
                    if (playerTurn == true && thirdPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
            }
            else if(whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
            {
                if (playerHand.handPosition == 1)
                {
                    if (playerTurn == true && firstPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 2)
                {
                    if (playerTurn == true && firstPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
                else if (playerHand.handPosition == 3)
                {
                    if (playerTurn == true && secondPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }

                }
                else if (playerHand.handPosition == 4)
                {
                    if (playerTurn == true && thirdPlayedCardObj == null)
                    {
                        readyToGo = true;
                    }
                    else
                    {
                        readyToGo = false;
                    }
                }
            }
        }
        if(goingAlone == true && whoMadeAlone == WHO_MADE_ALONE.ALLY)
        {
            readyToGo = false;
        }
        if (trumpMadeCounter > 4)
        {
            trumpMadeTimer = 0.0f;
            if (firstPlayedCardObj != null)
            {
                firstPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
            }
            if (secondPlayedCardObj != null)
            {
                secondPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
            }
            if (thirdPlayedCardObj != null)
            {
                thirdPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
            }
            if (fourthPlayedCardObj != null)
            {
                fourthPlayedCardObj.transform.localPosition = playerHand.moveCardOffScreen;
            }
        }
        if (trump.trumpMade == true && trump.orderedUp == true && readyToGo == false)
        {
            trumpMadeTimer += Time.deltaTime;

            if (trumpMadeTimer > trumpMadeLimit)
            {
                if (doneTurn == false && trumpMadeCounter < 4)
                {
                    transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
                }
                trumpMadeCounter++;
                trumpMadeTimer = 0.0f;
            }
        }
        startRotation = new Vector3(0.0f, 0.0f, zRot);

        if (playerTurnNotMade == false && trump.trumpMade == false && transition.done == true)
        {
            if (timer == 0.00)
            {
                turnCounter++;
            }

            timer += Time.deltaTime;
            if (timer >= nonPlayerTurnTimerLimit)
            {
                transform.Rotate(new Vector3(0.0f, 0.0f, zRot));
                timer = 0.00f;
            }
        }
        if (trump.trumpMade == false && transition.done == true)
        {
            if (playerHand.handPosition == 1 && (turnCounter == 1 || turnCounter == 5) && counter == 0)
            {
                playerTurnNotMade = true;
                trump.whoMade = Trump.PLAYERWHOMADE.PLAYER;
                counter++;
            }
            if (playerHand.handPosition == 2 && (turnCounter == 2 || turnCounter == 6) && counter == 0)
            {
                playerTurnNotMade = true;
                trump.whoMade = Trump.PLAYERWHOMADE.PLAYER;
                counter++;
            }
            if (playerHand.handPosition == 3 && (turnCounter == 3 || turnCounter == 7) && counter == 0)
            {
                playerTurnNotMade = true;
                trump.whoMade = Trump.PLAYERWHOMADE.PLAYER;
                counter++;
            }
            if (playerHand.handPosition == 4 && (turnCounter == 4 || turnCounter == 8) && counter == 0)
            {
                playerTurnNotMade = true;
                trump.whoMade = Trump.PLAYERWHOMADE.PLAYER;
                counter++;
            }
        }
        else if(hasMadePlayer == false)
        {
            playerTurnNotMade = false;
            hasMadePlayer = true;
        }

        if ((cardCount == 5 || turnCounter > 8) && (playerHand.pointsPlayer < 10 && playerHand.pointsEnemy < 10))
        {
            setUpFirst = false;
            resetEnemies = false;
            genEnemy2Hand = false;
            foundCardsForPlayer = false;
            playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
            enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
            parter.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
            enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
            trump.aloneDecided = false;
            playerHand.passedKitty = false;
            enemy1.passedKitty = false;
            parter.passedKitty = false;
            enemy2.passedKitty = false;
            playerHand.passedGame = false;
            enemy1.passedGame = false;
            parter.passedGame = false;
            enemy2.passedGame = false;

            if (startPosForHand == 4 && trump.trumpMade == true)
            {
                if (goingAlone == true)
                {
                    if (whoMadeAlone == WHO_MADE_ALONE.ENEMY1 || whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 4;
                        }
                        else if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        else
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (whoMadeAlone == WHO_MADE_ALONE.PLAYER || whoMadeAlone == WHO_MADE_ALONE.ALLY)
                    {
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 4;
                        }
                        else if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        else
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
                else
                {
                    if (trump.whoMade == Trump.PLAYERWHOMADE.ALLY || trump.whoMade == Trump.PLAYERWHOMADE.PLAYER)
                    {
                        if (playerHand.actualPointsPlayer <= 2)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                        if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (trump.whoMade == Trump.PLAYERWHOMADE.ENEMY1 || trump.whoMade == Trump.PLAYERWHOMADE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy <= 2)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                        if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
            }
            else if (startPosForHand == 3 && trump.trumpMade == true)
            {
                if (goingAlone == true)
                {
                    if (whoMadeAlone == WHO_MADE_ALONE.ENEMY1 || whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 4;
                        }
                        else if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        else
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (whoMadeAlone == WHO_MADE_ALONE.PLAYER || whoMadeAlone == WHO_MADE_ALONE.ALLY)
                    {
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 4;
                        }
                        else if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        else
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
                else
                {
                    if (trump.whoMade == Trump.PLAYERWHOMADE.ALLY || trump.whoMade == Trump.PLAYERWHOMADE.PLAYER)
                    {
                        if (playerHand.actualPointsPlayer <= 2)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                        if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (trump.whoMade == Trump.PLAYERWHOMADE.ENEMY1 || trump.whoMade == Trump.PLAYERWHOMADE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy <= 2)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                        if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
            }
            else if (startPosForHand == 2 && trump.trumpMade == true)
            {
                if (goingAlone == true)
                {
                    if (whoMadeAlone == WHO_MADE_ALONE.ENEMY1 || whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 4;
                        }
                        else if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        else
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (whoMadeAlone == WHO_MADE_ALONE.PLAYER || whoMadeAlone == WHO_MADE_ALONE.ALLY)
                    {
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 4;
                        }
                        else if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        else
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
                else
                {
                    if (trump.whoMade == Trump.PLAYERWHOMADE.ALLY || trump.whoMade == Trump.PLAYERWHOMADE.PLAYER)
                    {
                        if (playerHand.actualPointsPlayer <= 2)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                        if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (trump.whoMade == Trump.PLAYERWHOMADE.ENEMY1 || trump.whoMade == Trump.PLAYERWHOMADE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy <= 2)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                        if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
            }
            else if (startPosForHand == 1 && trump.trumpMade == true)
            {
                if (goingAlone == true)
                {
                    if (whoMadeAlone == WHO_MADE_ALONE.ENEMY1 || whoMadeAlone == WHO_MADE_ALONE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 4;
                        }
                        else if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        else
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (whoMadeAlone == WHO_MADE_ALONE.PLAYER || whoMadeAlone == WHO_MADE_ALONE.ALLY)
                    {
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 4;
                        }
                        else if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        else
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
                else
                {
                    if (trump.whoMade == Trump.PLAYERWHOMADE.ALLY || trump.whoMade == Trump.PLAYERWHOMADE.PLAYER)
                    {
                        if (playerHand.actualPointsPlayer <= 2)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                        if (playerHand.actualPointsPlayer > 2 && playerHand.actualPointsPlayer < 5)
                        {
                            playerHand.pointsPlayer += 1;
                        }
                        if (playerHand.actualPointsPlayer == 5)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                    }
                    else if (trump.whoMade == Trump.PLAYERWHOMADE.ENEMY1 || trump.whoMade == Trump.PLAYERWHOMADE.ENEMY2)
                    {
                        if (playerHand.actualPointsEnemy <= 2)
                        {
                            playerHand.pointsPlayer += 2;
                        }
                        if (playerHand.actualPointsEnemy > 2 && playerHand.actualPointsEnemy < 5)
                        {
                            playerHand.pointsEnemy += 1;
                        }
                        if (playerHand.actualPointsEnemy == 5)
                        {
                            playerHand.pointsEnemy += 2;
                        }
                    }
                }
            }
            doneTurnForNow = true;
            doneGoingAloneUp = false;
            toggle.isOn = false;
            playerHand.actualPointsPlayer = 0;
            playerHand.actualPointsEnemy = 0;

            if (playerHand.pointsEnemy < 10 && playerHand.pointsPlayer < 10)
            {
                deck.playedCards.Clear();
                deck.shuffleDeck();

                kitty.cardObjects[0].tag = "Card";
                kitty.cardObjects.Clear();
                kitty.hand.Clear();

                kitty.hand.Add(deck.deck[kitty.handNum * 5]);
                kitty.cardObjects.Add(deck.cards[kitty.handNum * 5]);

                kitty.cardObjects[0].tag = "KittyCard";
                kitty.card = null;
                kitty.cardToMove = null;
                kitty.cardObj = kitty.cardObjects[0];

    
                kitty.cardToMove = GameObject.Find(kitty.hand[0].ToString());

                trump.trumpMade = false;
                trump.orderedUp = false;
                trump.addedCard = false;
                hasMadePlayer = false;
                trump.trump = Trump.TRUMP.NONE;
                trump.whoMade = Trump.PLAYERWHOMADE.NONE;
                trump.cardToSwitch = null;
                playerTurnNotMade = false;
               // trump.kittyObj = GameObject.FindGameObjectWithTag("KittyCard");
                //trump.kittySprite = kittyObj.GetComponent<SpriteRenderer>().sprite;
                trump.selectedCard = null;

                trumpMadeCounter = 0;
                turnCounter = 0;
                counter = 0;
                playerTurn = false;
                enemy1Turn = false;
                allyTurn = false;
                enemy2Turn = false;
                firstPlayedCardObj = null;
                secondPlayedCardObj = null;
                thirdPlayedCardObj = null;
                fourthPlayedCardObj = null;
                if (kitty.hand[0].ToString().Contains("DIAMONDS"))
                {
                    playerHand.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                    enemy1.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                    parter.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                    enemy2.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                }
                else if (kitty.hand[0].ToString().Contains("HEARTS"))
                {
                    playerHand.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                    enemy1.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                    parter.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                    enemy2.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                }
                else if (kitty.hand[0].ToString().Contains("SPADES"))
                {
                    playerHand.kittyTrump = Hand.KITTYTRUMP.SPADES;
                    enemy1.kittyTrump = Hand.KITTYTRUMP.SPADES;
                    parter.kittyTrump = Hand.KITTYTRUMP.SPADES;
                    enemy2.kittyTrump = Hand.KITTYTRUMP.SPADES;
                }
                else if (kitty.hand[0].ToString().Contains("CLUBS"))
                {
                    playerHand.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                    enemy1.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                    parter.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                    enemy2.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                }
                enemy1.foundLowest = false;
                parter.foundLowest = false;
                enemy2.foundLowest = false;
                playerHand.foundLowest = false;
                enemy1.foundTheCard = false;
                enemy2.foundTheCard = false;
                parter.foundTheCard = false;
                playerHand.foundTheCard = false;

                //playerHand.getNonTrumpCards();
                //enemy1.getNonTrumpCards();
                //parter.getNonTrumpCards();
                //enemy2.getNonTrumpCards();

                if (startPosForHand == 4)
                {
                    playerHand.handPosition = 3;
                    enemy1.handPosition = 4;
                    parter.handPosition = 1;
                    enemy2.handPosition = 2;
                    startPosForHand = 3;
                    playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ALLY;
                }
                else if (startPosForHand == 3)
                {
                    playerHand.handPosition = 2;
                    enemy1.handPosition = 3;
                    parter.handPosition = 4;
                    enemy2.handPosition = 1;
                    startPosForHand = 2;
                    playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY2;
                }
                else if (startPosForHand == 2)
                {
                    playerHand.handPosition = 1;
                    enemy1.handPosition = 2;
                    parter.handPosition = 3;
                    enemy2.handPosition = 4;
                    startPosForHand = 1;
                    playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
                }
                else if (startPosForHand == 1)
                {
                    playerHand.handPosition = 4;
                    enemy1.handPosition = 1;
                    parter.handPosition = 2;
                    enemy2.handPosition = 3;
                    startPosForHand = 4;
                    playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
                }
                //if (startPosForHand == 4)
                //{
                //    resetEnemy1();
                //}
                //if (startPosForHand == 3)
                //{
                //    resetAlly();
                //}
                //if (startPosForHand == 2)
                //{
                //    resetEnemy2();
                //}
                //if (startPosForHand == 1)
                //{
                //   // resetPlayer();
                //}
                deck.nonPlayedCards.Clear();
                resetHand5();
                resetPlayer();
                resetEnemy1();
                resetAlly();
                resetEnemy2();
                resetCardsEnd = true;
                resetCardsEnd2 = true;
                //resetEnemy1();
                //resetEnemy2();
                // resetAlly();
                goingAlone = false;
                cardCount = 0;
                whoMadeAlone = WHO_MADE_ALONE.NONE;

            }
        }
    }

    public void resetHand5()
    {
        hand5.deleteHand();
        hand5.generateHand();
        for (int i = 0; i < hand5.hand.Count; ++i)
        {
            if (GameObject.Find(hand5.hand[i].ToString()) != null)
            {
                hand5.cardsToMove.Add(GameObject.Find(hand5.hand[i].ToString()));
            }
        }
        hand5.genCards();
    }

    public void resetPlayer()
    {
        playerHand.deleteHand();
        playerHand.generateHand();
        playerHand.handScore();
        playerHand.canTrump();
        playerHand.getNonTrumpCards();
        for (int i = 0; i < playerHand.hand.Count; ++i)
        {
            if (GameObject.Find(playerHand.hand[i].ToString()) != null)
            {
                playerHand.cardsToMove.Add(GameObject.Find(playerHand.hand[i].ToString()));
            }
        }
        playerHand.genCards();
        for(int i = 0; i < playerHand.cardObjects.Count; ++ i)
        {
            playerHand.cardObjects[i].tag = "Card";
        }
    }

    public void resetAlly()
    {
        parter.deleteHand();
        parter.generateHand();
        parter.handScore();
        parter.canTrump();
        parter.getNonTrumpCards();
        for (int i = 0; i < parter.hand.Count; ++i)
        {
            if (GameObject.Find(parter.hand[i].ToString()) != null)
            {
                parter.cardsToMove.Add(GameObject.Find(parter.hand[i].ToString()));
            }
        }
       
        for (int i = 0; i < parter.cardObjects.Count; ++i)
        {
            parter.cardObjects[i].tag = "Card";
        }
        parter.genCards();
    }

    public void resetEnemy1()
    {
        enemy1.deleteHand();
        enemy1.generateHand();
        enemy1.handScore();
        enemy1.canTrump();
        enemy1.getNonTrumpCards();
        enemy1.numOfEachSuitNonKitty();
        for (int i = 0; i < enemy1.hand.Count; ++i)
        {
            if (GameObject.Find(enemy1.hand[i].ToString()) != null)
            {
                enemy1.cardsToMove.Add(GameObject.Find(enemy1.hand[i].ToString()));
            }
        }
       
        for (int i = 0; i < enemy1.cardObjects.Count; ++i)
        {
            enemy1.cardObjects[i].tag = "Card";
        }
        enemy1.genCards();
    }

    public void resetEnemy2()
    {
        enemy2.deleteHand();
        enemy2.generateHand();
        enemy2.handScore();
        enemy2.canTrump();
        enemy2.getNonTrumpCards();
        for (int i = 0; i < enemy2.hand.Count; ++i)
        {
            if (GameObject.Find(enemy2.hand[i].ToString()) != null)
            {
                enemy2.cardsToMove.Add(GameObject.Find(enemy2.hand[i].ToString()));
            }
        }
        for (int i = 0; i < enemy2.cardObjects.Count; ++i)
        {
            enemy2.cardObjects[i].tag = "Card";
        }
        enemy2.genCards();
    }

    public void PlayAgain()
    {
        hasCheckedAlly = false;
        hasCheckedEnemy1 = false;
        hasCheckedEnemy2 = false;
        hasCheckedPlayer = false;
        playerHand.playerWon = false;
        playerHand.allyWon = false;
        playerHand.enemy1Won = false;
        playerHand.enemy2Won = false;
        playerTurn = false;
        enemy1Turn = false;
        enemy2Turn = false;
        allyTurn = false;
        playerHand.pointsPlayer = 0;
        playerHand.pointsEnemy = 0;
        setUpFirst = false;
        resetEnemies = false;
        genEnemy2Hand = false;
        foundCardsForPlayer = false;
        playerHand.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
        enemy1.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
        parter.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
        enemy2.suitToFollow = Hand.SUIT_TO_FOLLOW.NONE;
        trump.aloneDecided = false;
        playerHand.passedKitty = false;
        enemy1.passedKitty = false;
        parter.passedKitty = false;
        enemy2.passedKitty = false;
        playerHand.passedGame = false;
        enemy1.passedGame = false;
        parter.passedGame = false;
        enemy2.passedGame = false;
        doneTurnForNow = true;
        doneGoingAloneUp = false;
        toggle.isOn = false;
        playerHand.actualPointsPlayer = 0;
        playerHand.actualPointsEnemy = 0;
            deck.playedCards.Clear();
            deck.shuffleDeck();

            kitty.cardObjects[0].tag = "Card";
            kitty.cardObjects.Clear();
            kitty.hand.Clear();

            kitty.hand.Add(deck.deck[kitty.handNum * 5]);
            kitty.cardObjects.Add(deck.cards[kitty.handNum * 5]);

            kitty.cardObjects[0].tag = "KittyCard";
            kitty.card = null;
            kitty.cardToMove = null;
            kitty.cardObj = kitty.cardObjects[0];


            kitty.cardToMove = GameObject.Find(kitty.hand[0].ToString());

            trump.trumpMade = false;
            trump.orderedUp = false;
            trump.addedCard = false;
            hasMadePlayer = false;
            trump.trump = Trump.TRUMP.NONE;
            trump.whoMade = Trump.PLAYERWHOMADE.NONE;
            trump.cardToSwitch = null;
            playerTurnNotMade = false;
            // trump.kittyObj = GameObject.FindGameObjectWithTag("KittyCard");
            //trump.kittySprite = kittyObj.GetComponent<SpriteRenderer>().sprite;
            trump.selectedCard = null;

            trumpMadeCounter = 0;
            turnCounter = 0;
            counter = 0;
            playerTurn = false;
            enemy1Turn = false;
            allyTurn = false;
            enemy2Turn = false;
            firstPlayedCardObj = null;
            secondPlayedCardObj = null;
            thirdPlayedCardObj = null;
            fourthPlayedCardObj = null;
            if (kitty.hand[0].ToString().Contains("DIAMONDS"))
            {
                playerHand.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                enemy1.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                parter.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
                enemy2.kittyTrump = Hand.KITTYTRUMP.DIAMONDS;
            }
            else if (kitty.hand[0].ToString().Contains("HEARTS"))
            {
                playerHand.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                enemy1.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                parter.kittyTrump = Hand.KITTYTRUMP.HEARTS;
                enemy2.kittyTrump = Hand.KITTYTRUMP.HEARTS;
            }
            else if (kitty.hand[0].ToString().Contains("SPADES"))
            {
                playerHand.kittyTrump = Hand.KITTYTRUMP.SPADES;
                enemy1.kittyTrump = Hand.KITTYTRUMP.SPADES;
                parter.kittyTrump = Hand.KITTYTRUMP.SPADES;
                enemy2.kittyTrump = Hand.KITTYTRUMP.SPADES;
            }
            else if (kitty.hand[0].ToString().Contains("CLUBS"))
            {
                playerHand.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                enemy1.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                parter.kittyTrump = Hand.KITTYTRUMP.CLUBS;
                enemy2.kittyTrump = Hand.KITTYTRUMP.CLUBS;
            }
            enemy1.foundLowest = false;
            parter.foundLowest = false;
            enemy2.foundLowest = false;
            playerHand.foundLowest = false;
            enemy1.foundTheCard = false;
            enemy2.foundTheCard = false;
            parter.foundTheCard = false;
            playerHand.foundTheCard = false;

            //playerHand.getNonTrumpCards();
            //enemy1.getNonTrumpCards();
            //parter.getNonTrumpCards();
            //enemy2.getNonTrumpCards();

            if (startPosForHand == 4)
            {
                playerHand.handPosition = 3;
                enemy1.handPosition = 4;
                parter.handPosition = 1;
                enemy2.handPosition = 2;
                startPosForHand = 3;
                playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ALLY;
            }
            else if (startPosForHand == 3)
            {
                playerHand.handPosition = 2;
                enemy1.handPosition = 3;
                parter.handPosition = 4;
                enemy2.handPosition = 1;
                startPosForHand = 2;
                playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY2;
            }
            else if (startPosForHand == 2)
            {
                playerHand.handPosition = 1;
                enemy1.handPosition = 2;
                parter.handPosition = 3;
                enemy2.handPosition = 4;
                startPosForHand = 1;
                playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.PLAYER;
            }
            else if (startPosForHand == 1)
            {
                playerHand.handPosition = 4;
                enemy1.handPosition = 1;
                parter.handPosition = 2;
                enemy2.handPosition = 3;
                startPosForHand = 4;
                playerHand.whoPlayedFirst = Hand.WHO_PLAYED_FIRST.ENEMY1;
            }
            //if (startPosForHand == 4)
            //{
            //    resetEnemy1();
            //}
            //if (startPosForHand == 3)
            //{
            //    resetAlly();
            //}
            //if (startPosForHand == 2)
            //{
            //    resetEnemy2();
            //}
            //if (startPosForHand == 1)
            //{
            //   // resetPlayer();
            //}
            deck.nonPlayedCards.Clear();
            resetHand5();
            resetPlayer();
            resetEnemy1();
            resetAlly();
            resetEnemy2();
            resetCardsEnd = true;
            resetCardsEnd2 = true;
            //resetEnemy1();
            //resetEnemy2();
            // resetAlly();
            goingAlone = false;
            cardCount = 0;
            whoMadeAlone = WHO_MADE_ALONE.NONE;

            endGamePanel.SetActive(false);
    }
    public bool hasCheckedEnemy1 = false;
    public bool hasCheckedAlly = false;
    public bool hasCheckedEnemy2 = false;
    public bool hasCheckedPlayer = false;
    public bool trumpMadeForPlayer = false;
    public bool playerTurnNotMade = false;
    public bool doneTurn = false;
    public float doneTurnTimer = 0.0f;
    public bool readyToGo = false;
    public float rotForAfter;
    public float timerToUpdateEnd = 0.0f;
    public void rotateArrow()
    {
        if (playerTurnNotMade == true)
        {
            trump.whoMade = Trump.PLAYERWHOMADE.PLAYER;
            transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
            playerTurn = false;
           // enemy1Turn = true;
            playerTurnNotMade = false;
            counter = 0;
            turnCounter++;
            kittyObj.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
        }

    }
}
