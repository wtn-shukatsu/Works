using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MagicTrapZone : Field, IDropHandler
{
    public override bool IsPlayableZone(Card _card) {
        if (Get() == null) {
            if (_card.Controller.MagicTrapZone.Contains(this)) {
                if (_card.ExistCardAction("PlayMagicTrap") || _card.ExistCardAction("SetMagicTrap")) {
                    return true;
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
                card.Controller.PlayMagicTrap(card, this);
            }
            else if (card.dragCard.ToBack) {
                card.dragCard.Played();
                card.Controller.SetMagicTrap(card, this);
            }
            else { ; }
        }
    }
}
