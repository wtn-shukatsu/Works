using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonDialog : MonoBehaviour
{
    Card card;
    Field field;

    [SerializeField] Image attackImage = null;
    [SerializeField] Image defenseImage = null;

    public void SetCard(Card _card, Field _field) {
        attackImage.sprite = _card.Face;
        defenseImage.sprite = _card.Back;
        card = _card;
        field = _field;
    }

    public void Summon() {
        gameObject.SetActive(false);
        card.Controller.Summon(card, field);
        card.Controller.Playable(true);
    }

    public void SetMonster() {
        gameObject.SetActive(false);
        card.Controller.SetMonster(card, field);
        card.Controller.Playable(true);
    }
}
