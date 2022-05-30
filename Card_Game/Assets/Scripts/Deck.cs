using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardPlace
{
    List<Card> cardList = new List<Card>();

    public List<Card> CardList { get { return cardList; } }
    
    public override void Add(Card _card) {
        _card.transform.SetParent(transform, false);
        cardList.Add(_card);
        _card.SetPlace(this);
        _card.dragCard.Movable(false);
    }

    public override Card Pull(Card _card) {
        cardList.Remove(_card);
        return _card;
    }

    public override Card Pull(int _position) {
        Card card = cardList[_position];
        cardList.Remove(card);
        return card;
    }

    public void Shuffle() {
        for (int i=0; i<cardList.Count; i++) {
            int randomIndex = UnityEngine.Random.Range(0, cardList.Count);
            Card temp = cardList[i];
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
    }
}
