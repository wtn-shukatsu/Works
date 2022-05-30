using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GensoukyouEngi : CardData
{
    string myName;
    Magic mgc;
    CardAction cardAction = new CardAction();
    
    public override string Name { get { return myName; } }
    public override Magic magic { get { return mgc; } }

    public GensoukyouEngi() {
        myName = "幻想郷縁起";
        mgc = new Magic(Magic.MagicType.Nomal);
    }

    IEnumerator Effect() {
        List<Card> cardList = cardAction.SearchFromDeck(card.Controller, typeof(Monster), "幻想");
        if (cardList.Count > 0) {
            card.Controller.FieldMaster.Playable(false, card.Controller);
            card.Controller.selectDialog.gameObject.SetActive(true);
            var coroutine = card.Controller.selectDialog.SelectCard(cardList, 1);

            yield return StartCoroutine(coroutine);

            cardList = (List<Card>)coroutine.Current;
            card.Controller.Search(cardList);
            card.Controller.Deck.Shuffle();
            card.Controller.selectDialog.gameObject.SetActive(false);
            card.Controller.FieldMaster.Playable(true, card.Controller);
        }
        else {
            Debug.Log("対象無し");
        }

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Activate() {
        card.State.Remove(Card.CardState.Setting);
        yield return StartCoroutine(Effect());
        card.Controller.SendToGraveyardFromField(card);
    }

    public void PlayMagicTrap() {
        StartCoroutine(Activate());
    }

    public void PlaySetMagicTrap() {
        card.ToggleFace(true);
        StartCoroutine(Activate());
    }

    public void SetMagicTrap() {
        cardAction.SetMagicTrap(card);
    }
}
