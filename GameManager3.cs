using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
    public Button DealButton;
    public Button CallCheckButton;
    public Button RaiseButton;
    public Button River;
    public Button RaiseBTN;
    

    //player and dealer hand
    public Player playerscript;
    public Player dealerscript1;
    public Player dealerscript2;
    public Player dealerscript3;
    public Player riverscript;


    //Texts
    public Text CashText;
    public Text PotText;
    public Text AmountText;
    public Text betseven;
    public Text Check1;
    public Text Check2;
    public Text Check3;
    public int raise;

    //HideCards
    public GameObject HideCard;
    public GameObject HideCard2;
    public GameObject HideCard3;
    public GameObject HideCard4;
    public GameObject HideCard5;
    public GameObject HideCard6;
    public GameObject RiverHide;
    public GameObject RiverHide2;
    public GameObject RiverHide3;
    public GameObject RiverHide4;
    public GameObject RiverHide5;

    private int raiseclicks = 0;
    private int Input = 0;
    public int money = 1000;
    public int riverclicks = 1;
    public int callclicks = 1;
    public int AIraise = 20;
    public int startingbet = 40;
    public int rbtn = 0;
    public int amount = 0;



    int pot = 0;


    void Start()
    {
        //Listeners
        DealButton.onClick.AddListener(() => DealClicked());
        CallCheckButton.onClick.AddListener(() => CallClicked());
        RaiseButton.onClick.AddListener(() => RaiseClicked());
        River.onClick.AddListener(() => RiverClicked());
        RaiseBTN.onClick.AddListener(() => RaiseBTNClicked());
    }
 
    private void DealClicked()
    {
        playerscript.ResetHand();
        dealerscript1.ResetHand();
        dealerscript2.ResetHand();
        dealerscript3.ResetHand();
        riverscript.ResetHand();
        GameObject.Find("Deck").GetComponent<Deck1>().Shuffle();
        betseven.gameObject.SetActive(false);
        Check1.gameObject.SetActive(false);
        Check2.gameObject.SetActive(false);
        Check3.gameObject.SetActive(false);
        River.gameObject.SetActive(false);
        RaiseBTN.gameObject.SetActive(false);
        playerscript.StartHand();// 0, 1
        dealerscript1.StartHand();//2, 3
        dealerscript2.StartHand();//4, 5
        dealerscript3.StartHand();//6, 7
        riverscript.GetRiver(); //8, 9, 10, 11, 12
        money = money - startingbet;
        CashText.text = "Cash:$" + money.ToString();
        PotText.text = "Pot:$160";
        AmountText.text = "Amount:$40";
        amount = 40;
        pot = 160;
        RiverHide.gameObject.SetActive(true);
        RiverHide2.gameObject.SetActive(true);
        RiverHide3.gameObject.SetActive(true);
        RiverHide4.gameObject.SetActive(true);
        RiverHide5.gameObject.SetActive(true);
        HideCard.gameObject.SetActive(true);
        HideCard2.gameObject.SetActive(true);
        HideCard3.gameObject.SetActive(true);
        HideCard4.gameObject.SetActive(true);
        HideCard5.gameObject.SetActive(true);
        HideCard6.gameObject.SetActive(true);

    }

    private void RaiseBTNClicked()
    {
        money = money - 20;
        pot = pot + 20;
        amount = amount + 20;
        rbtn++;
        RaiseBTN.gameObject.SetActive(true);
        CashText.text = "Cash:$" + money.ToString();
        PotText.text = "Pot:$" + pot.ToString();
        AmountText.text = "Amount:$" + amount.ToString();
        

    }

    private void CallClicked()
    {
        betseven.gameObject.SetActive(true);
        River.gameObject.SetActive(true);
        RaiseBTN.gameObject.SetActive(false);
        raiseclicks = 0;
        Check1.gameObject.SetActive(true);
        Check2.gameObject.SetActive(true);
        Check3.gameObject.SetActive(true);
    }

    private void RaiseClicked()
    {
        betseven.gameObject.SetActive(false);
        RaiseBTN.gameObject.SetActive(true);
        raiseclicks++;
        if (raiseclicks == 2)
        {
            Check1.gameObject.SetActive(true);
            Check2.gameObject.SetActive(true);
            Check3.gameObject.SetActive(true);
           
        }
        if(raiseclicks == 2)
        {
            RaiseBTN.gameObject.SetActive(false);
            pot = pot + (rbtn * 60);
            PotText.text = "Pot:$" + pot.ToString();
            rbtn = 0;
        }
        
    }

    private void RiverClicked()
    {
        Check1.gameObject.SetActive(false);
        Check2.gameObject.SetActive(false);
        Check3.gameObject.SetActive(false);
        River.gameObject.SetActive(false);
        if (riverclicks == 1)
        {
            RiverHide.gameObject.SetActive(false);
            RiverHide2.gameObject.SetActive(false);
            RiverHide3.gameObject.SetActive(false);
            betseven.gameObject.SetActive(false);
            riverclicks++;
        }
        else if (riverclicks == 2)
        {
            RiverHide.gameObject.SetActive(false);
            RiverHide2.gameObject.SetActive(false);
            RiverHide3.gameObject.SetActive(false);
            RiverHide4.gameObject.SetActive(false);
            betseven.gameObject.SetActive(false);
            ++riverclicks;
        }
        else if (riverclicks == 3)
        {
            RiverHide.gameObject.SetActive(false);
            RiverHide2.gameObject.SetActive(false);
            RiverHide3.gameObject.SetActive(false);
            RiverHide4.gameObject.SetActive(false);
            RiverHide5.gameObject.SetActive(false);
            HideCard.gameObject.SetActive(false);
            HideCard2.gameObject.SetActive(false);
            HideCard3.gameObject.SetActive(false);
            HideCard4.gameObject.SetActive(false);
            HideCard5.gameObject.SetActive(false);
            HideCard6.gameObject.SetActive(false);
            riverclicks = 1;
            betseven.gameObject.SetActive(false);
            money = money + pot;
            CashText.text = "Cash:$" + money.ToString();
            RoundOver();
        }


    }

    void RoundOver()
    {
        int playhand = HandEvaluator(playerscript);
        int com1hand = HandEvaluator(dealerscript1);
        int com2hand = HandEvaluator(dealerscript2);
        int com3hand = HandEvaluator(dealerscript3);
        if(playhand > com1hand && playhand > com2hand && playhand > com3hand)
        {
            betseven.text = "You Win!";
            money = money + pot;
        }
        else if( com1hand > playhand || com2hand > playhand || com3hand > playhand)
        {
            betseven.text = "You Lose";
        }
        
    }


    public int HandEvaluator(Player play)
    {
        int Nothing = 0;
        int HasPair = 0;
        int HasThree = 0;
        int HasFour = 0;
        int HasFlush = 0;
        int HasFullHouse = 0;
        int sum = 0;

        if (play.card1[1] == riverscript.river1[1] || play.card1[1] == riverscript.river2[1] || play.card1[1] == riverscript.river3[1]
            || play.card1[1] == riverscript.river4[1] || play.card1[1] == riverscript.river5[1] || play.card2[1] == riverscript.river1[1] || play.card2[1] == riverscript.river2[1] || play.card2[1] == riverscript.river3[1]
            || play.card2[1] == riverscript.river4[1] || play.card2[1] == riverscript.river5[1] || play.card1[1] == play.card2[1])
        {
            HasPair++;
        }
        else if (play.card1[1] == play.card2[1] && play.card1[1] == riverscript.river1[1] || play.card1[1] == play.card2[1] && play.card1[1] == riverscript.river2[1] ||
            play.card1[1] == play.card2[1] && play.card1[1] == riverscript.river3[1] || play.card1[1] == play.card2[1] && play.card1[1] == riverscript.river4[1] ||
            play.card1[1] == play.card2[1] && play.card1[1] == riverscript.river5[1] || play.card1[1] == riverscript.river1[1] && play.card1[1] == riverscript.river2[1] ||
             play.card1[1] == riverscript.river1[1] && play.card1[1] == riverscript.river3[1] || play.card1[1] == riverscript.river1[1] && play.card1[1] == riverscript.river4[1] ||
              play.card1[1] == riverscript.river1[1] && play.card1[1] == riverscript.river5[1] || play.card1[1] == riverscript.river2[1] && play.card1[1] == riverscript.river3[1] ||
               play.card1[1] == riverscript.river2[1] && play.card1[1] == riverscript.river4[1] || play.card1[1] == riverscript.river2[1] && play.card1[1] == riverscript.river5[1] ||
             play.card1[1] == riverscript.river3[1] && play.card1[1] == riverscript.river4[1] || play.card1[1] == riverscript.river3[1] && play.card1[1] == riverscript.river5[1] ||
              play.card1[1] == riverscript.river4[1] && play.card1[1] == riverscript.river5[1] || play.card2[1] == riverscript.river1[1] && play.card2[1] == riverscript.river2[1] ||
             play.card2[1] == riverscript.river1[1] && play.card2[1] == riverscript.river3[1] || play.card2[1] == riverscript.river1[1] && play.card2[1] == riverscript.river4[1] ||
              play.card2[1] == riverscript.river1[1] && play.card2[1] == riverscript.river5[1] || play.card2[1] == riverscript.river2[1] && play.card2[1] == riverscript.river3[1] ||
               play.card2[1] == riverscript.river2[1] && play.card2[1] == riverscript.river4[1] || play.card2[1] == riverscript.river2[1] && play.card2[1] == riverscript.river5[1] ||
             play.card2[1] == riverscript.river3[1] && play.card2[1] == riverscript.river4[1] || play.card2[1] == riverscript.river3[1] && play.card2[1] == riverscript.river5[1] ||
              play.card2[1] == riverscript.river4[1] && play.card2[1] == riverscript.river5[1])
        {
            HasThree++;
        }
        else if (play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] ||
            play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river3[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river4[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river5[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river4[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river5[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river5[1] ||
           play.card1[1] == play.card2[1] && play.card2[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1] ||
           play.card1[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] ||
           play.card1[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river4[1] ||
           play.card1[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river5[1] ||
           play.card1[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] ||
           play.card1[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river5[1] ||
           play.card1[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1] ||
           play.card1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] ||
           play.card1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river5[1] ||
           play.card1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1] ||
           play.card1[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1] ||
           play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] ||
           play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river4[1] ||
           play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river5[1] ||
           play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] ||
           play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river5[1] ||
           play.card2[1] == riverscript.river1[1] && riverscript.river1[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1] ||
           play.card2[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] ||
           play.card2[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river5[1] ||
           play.card2[1] == riverscript.river2[1] && riverscript.river2[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1] ||
           play.card2[1] == riverscript.river3[1] && riverscript.river3[1] == riverscript.river4[1] && riverscript.river4[1] == riverscript.river5[1])
        {
            HasFour++;
        }
       else if (play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river4[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river5[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river5[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river5[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card1[0] == play.card2[0] && play.card2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card1[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] ||
           play.card1[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river5[0] ||
           play.card1[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card1[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] ||
           play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river5[0] ||
           play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card2[0] == riverscript.river1[0] && riverscript.river1[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0] ||
           play.card2[0] == riverscript.river2[0] && riverscript.river2[0] == riverscript.river3[0] && riverscript.river3[0] == riverscript.river4[0] && riverscript.river4[0] == riverscript.river5[0])
        {
            HasFlush++;
        }
       else if(HasPair == HasThree)
        {
            HasFullHouse++;
        }
        else
        {
            Nothing++;
        }
        sum = Nothing + HasPair + HasThree + HasFour + HasFlush + HasFullHouse;
        return sum;

        

    }
    
 
    void Update()
    {
        
    }

}
