using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Graveyard : CardPlace, IPointerEnterHandler, IPointerExitHandler
{
    List<Card> cardList = new List<Card>();
    
    [SerializeField] Image image = null;

    IEnumerator AddCoroutine(Card _card) {

        // Pull()の処理待ち
        // これがないとDoMove()前のSetParent()がうまくいかない
        yield return new WaitForEndOfFrame();

        _card.transform.SetParent(transform.parent);
        _card.ToggleFace(true);
        _card.transform.DOMove(transform.position, 0.3f)
            .OnComplete(() => {
                _card.transform.SetParent(transform);
                cardList.Add(_card);
                _card.dragCard.Movable(false);
            });
    }

    public override void Add(Card _card) {
        StartCoroutine(AddCoroutine(_card));
    }

    public override Card Pull(int _position) {
        Card card = cardList[_position];
        cardList.Remove(card);
        return card;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        image.color += new Color32(0, 0, 0, 155);
    }

    public void OnPointerExit(PointerEventData eventData) {
        image.color -= new Color32(0, 0, 0, 155);
    }
}
