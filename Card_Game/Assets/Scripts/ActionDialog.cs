using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDialog : MonoBehaviour
{
    Card card;
    Field field;

    [SerializeField] Image cardImage = null;
    [SerializeField] Button changeBattlePosition = null;
    [SerializeField] Button playSetMagicTrap = null;
    [SerializeField] Button attack= null;
    [SerializeField] Button activateEffect = null;
    [SerializeField] SelectAttackTarget selectAttackTarget = null;

    public void SetCard(Card _card, Field _field) {
        cardImage.sprite = _card.Face;
        card = _card;
        field = _field;
        if (card.ExistCardAction("ChangeBattlePosition")) {
            changeBattlePosition.gameObject.SetActive(true);
            if (field.CanChangeBattlePosition()) {
                changeBattlePosition.interactable = true;
            }
            else {
                changeBattlePosition.interactable = false;
            }
        }
        else {
            changeBattlePosition.gameObject.SetActive(false);
        }
        if (card.ExistCardAction("PlaySetMagicTrap")) {
            playSetMagicTrap.gameObject.SetActive(true);
            if (field.CanPlaySetMagicTrap()) {
                playSetMagicTrap.interactable = true;
            }
            else {
                playSetMagicTrap.interactable = false;
            }
        }
        else {
            playSetMagicTrap.gameObject.SetActive(false);
        }
        if (card.ExistCardAction("Attack")) {
            attack.gameObject.SetActive(true);
            if (field.CanAttack()) {
                attack.interactable = true;
            }
            else {
                attack.interactable = false;
            }
        }
        else {
            attack.gameObject.SetActive(false);
        }
        card.Controller.FieldMaster.Playable(false, card.Controller);
    }

    public void ChangeBattlePosition() {
        gameObject.SetActive(false);
        card.Controller.ChangeBattlePosition(card);
        card.Controller.FieldMaster.Playable(true, card.Controller);
    }

    public void PlaySetMagicTrap() {
        gameObject.SetActive(false);
        card.Controller.PlaySetMagicTrap(card);
        card.Controller.FieldMaster.Playable(true, card.Controller);
    }

    public void SelectAttakTarget() {
        gameObject.SetActive(false);
        selectAttackTarget.ShowAttackArrow(field);
        card.Controller.FieldMaster.Playable(true, card.Controller);
    }

    public void Cancel() {
        gameObject.SetActive(false);
        card.Controller.FieldMaster.Playable(true, card.Controller);
    }
}
