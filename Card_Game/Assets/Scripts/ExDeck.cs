using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExDeck : CardPlace
{
    List<Card> cardList = new List<Card>();
    
    public override void Add(Card _card) {
        _card.transform.SetParent(this.transform, false);
        cardList.Add(_card);
        _card.SetPlace(this);
        _card.dragCard.Movable(false);
    }

    public override Card Pull(int _position) {
        Card card = cardList[_position];
        cardList.Remove(card);
        return card;
    }
}
