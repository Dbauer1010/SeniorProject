using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Card1 cardScript;
    public Deck1 deckScript;
    public char[] card1 = new char[2];
    public char[] card2 = new char[2];
    public char[] river1 = new char[2];
    public char[] river2 = new char[2];
    public char[] river3 = new char[2];
    public char[] river4 = new char[2];
    public char[] river5 = new char[2];


    private int money = 1000;

    public GameObject[] hand;

    public int cardIndex = 0;

    public void StartHand()
    {
         string t = GetCard();
        card1 = t.ToCharArray();
        string s = GetCard();
        card2 = s.ToCharArray();

        
    }

    public void GetRiver()
    {
         string a = GetCard();
        river1 = a.ToCharArray();
         string b = GetCard();
        river2 = b.ToCharArray();
         string c = GetCard();
        river3 = c.ToCharArray();
         string d = GetCard();
        river4 = d.ToCharArray();
         string e = GetCard();
        river5 = e.ToCharArray();
    }

    
    public string GetCard()
    {
        string ca = deckScript.DealCard(hand[cardIndex].GetComponent<Card1>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        cardIndex++;
        return ca;
    }

    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<Card1>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
    }
}
