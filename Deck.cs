using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    public List<Card> deck;
    public List<GameObject> cards;
    public GameObject[] objArray; 

    public enum Card
    {
        NINE_HEARTS,
        TEN_HEARTS,
        JACK_HEARTS,
        QUEEN_HEARTS,
        KING_HEARTS,
        ACE_HEARTS,
        NINE_DIAMONDS,
        TEN_DIAMONDS,
        JACK_DIAMONDS,
        QUEEN_DIAMONDS,
        KING_DIAMONDS,
        ACE_DIAMONDS,
        NINE_CLUBS,
        TEN_CLUBS,
        JACK_CLUBS,
        QUEEN_CLUBS,
        KING_CLUBS,
        ACE_CLUBS,
        NINE_SPADES,
        TEN_SPADES,
        JACK_SPADES,
        QUEEN_SPADES,
        KING_SPADES,
        ACE_SPADES,
        NONE
    }

    // Start is called before the first frame update
    void Awake()
    {
        generateDeck();
        shuffleDeck();
    }
 
    public void generateDeck()
    {
        
        cards.Clear();
        //HEARTS
        deck.Add(Card.NINE_HEARTS);
        deck.Add(Card.TEN_HEARTS);
        deck.Add(Card.JACK_HEARTS);
        deck.Add(Card.QUEEN_HEARTS);
        deck.Add(Card.KING_HEARTS);
        deck.Add(Card.ACE_HEARTS);

        //DIAMONDS
        deck.Add(Card.NINE_DIAMONDS);
        deck.Add(Card.TEN_DIAMONDS);
        deck.Add(Card.JACK_DIAMONDS);
        deck.Add(Card.QUEEN_DIAMONDS);
        deck.Add(Card.KING_DIAMONDS);
        deck.Add(Card.ACE_DIAMONDS);

        //CLUBS
        deck.Add(Card.NINE_CLUBS);
        deck.Add(Card.TEN_CLUBS);
        deck.Add(Card.JACK_CLUBS);
        deck.Add(Card.QUEEN_CLUBS);
        deck.Add(Card.KING_CLUBS);
        deck.Add(Card.ACE_CLUBS);

        //SPADES
        deck.Add(Card.NINE_SPADES);
        deck.Add(Card.TEN_SPADES);
        deck.Add(Card.JACK_SPADES);
        deck.Add(Card.QUEEN_SPADES);
        deck.Add(Card.KING_SPADES);
        deck.Add(Card.ACE_SPADES);

        for(int i = 0; i < 24; ++i)
        {
            cards.Add(objArray[i]);
            cards[i].name = deck[i].ToString();
        }
    }
	public List<GameObject> playedCards;
	public List<GameObject> nonPlayedCards;
	
    public void OnApplicationQuit()
    {
       // cards.Clear();
    }

    public void shuffleDeck()
    {
        for (int i = 0; i < 1000; ++i)
        {
            int num = Random.Range(0, 24);
            int num2 = Random.Range(0, 24);

            Card card = deck[num];
            deck[num] = deck[num2];
            deck[num2] = card;

            GameObject cardObj = cards[num];
            cards[num] = cards[num2];
            cards[num2] = cardObj;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
