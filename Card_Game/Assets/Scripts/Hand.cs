using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hand : CardPlace {
    List<Card> cardList = new List<Card>();
    float handWidth = 0;
    int handCount = 0;

    [SerializeField] GameObject dummyCardPrefab = null;
    [SerializeField] RectTransform dummyRect = null;
    [SerializeField] RectTransform myRect = null;
    [SerializeField] HorizontalLayoutGroup layout = null;

    void ControlCardSpace(int _num) {
        handCount += _num;
        handWidth = dummyRect.sizeDelta.x*handCount + layout.spacing*(handCount-1);
        float diff = myRect.sizeDelta.x - handWidth;
        if (diff < 0) {
            layout.spacing += diff / (handCount-1);
        }
    }

    IEnumerator AddCoroutine(Card _card, GameObject _dummyCardObj) {
        _dummyCardObj.transform.SetParent(transform);

        // 自動レイアウトの計算待ち
        // これがないとdummyCardObjのpositionがおかしくなる
        yield return new WaitForEndOfFrame();

        _card.ToggleFace(true);
        _card.transform.DOMove(_dummyCardObj.transform.position, 0.3f)
            .OnComplete(() => {
                _card.transform.SetParent(transform);
                cardList.Add(_card);
                _card.SetPlace(this);
                Destroy(_dummyCardObj);
                _card.dragCard.Movable(true);
            });
    }

    public override void Add(Card _card) {
        ControlCardSpace(1);
        StartCoroutine(AddCoroutine(_card, Instantiate(dummyCardPrefab)));
    }

    public void Movable(bool _movable) {
        foreach (Card card in cardList) {
            card.dragCard.Movable(_movable);
        }
    }

    public override Card Pull(Card _card) {
        cardList.Remove(_card);
        ControlCardSpace(-1);
        return _card;
    }
}
