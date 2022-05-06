using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMonsterZone : Field, IDropHandler
{
    public override bool IsPlayableZone(Card _card) {
        if (Get() == null) {
            if (_card.Controller.MainMonsterZone.Contains(this)) {
                if (_card.ExistCardAction("Summon") || _card.ExistCardAction("SetMonster")) {
                    if (_card.Controller.CanSummon()) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void OnDrop(PointerEventData data) {
        Card card = data.pointerDrag.GetComponent<Card>();
        if (card.dragCard != null && card.dragCard.IsOnDrag && IsPlayableZone(card)) {
            if (card.dragCard.ToFace) {
                card.dragCard.Played();
                card.Controller.Summon(card, this);
            }
            else if (card.dragCard.ToBack) {
                card.dragCard.Played();
                card.Controller.SetMonster(card, this);
            }
            else { ; }
        }
    }
}
