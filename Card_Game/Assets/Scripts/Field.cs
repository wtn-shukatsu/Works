using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Field : CardPlace, IPointerEnterHandler, IPointerExitHandler
{
    Card card;
    bool isPlayable = false;
    GameObject playableEffectObj;
    bool showPlayable;

    [SerializeField] GameObject playableEffectPrefab = null;
    [SerializeField] Image image = null;

    public bool IsPlayable { get { return isPlayable; } }
    public virtual bool IsPlayableZone(Card _card) { return false; }

    public override void Add(Card _card) {
        _card.transform.SetParent(transform);
        card = _card;
        card.SetPlace(this);
        card.dragCard.Movable(false);
    }

    public override Card Pull() {
        Card returnCard = card;
        card = null;

        return returnCard;
    }

    public Card Get() {
        return card;
    }

    public void ResetIState() {
        if (card != null) {
            card.ResetIState();
        }
    }

    public void Playable(bool _playable) {
        isPlayable = _playable;
    }

    public bool CanChangeBattlePosition() {
        return isPlayable &&
               !card.IState.Contains(Card.InstantCardState.SummonedThisTurn) &&
               !card.IState.Contains(Card.InstantCardState.ChangedBattlePosition);
    }

    public bool CanPlaySetMagicTrap() {
        return isPlayable && 
               !card.IState.Contains(Card.InstantCardState.CannotPlayMagicTrap);
    }

    IEnumerator ShowPlayableZoneCoroutine() {
        playableEffectObj = Instantiate(playableEffectPrefab);
        playableEffectObj.transform.SetParent(transform);
        showPlayable = true;
        var tweener = playableEffectObj.transform.DOScale(new Vector3(2.92f, 2.92f, 0), 1f).SetLoops(-1, LoopType.Restart);

        yield return new WaitWhile(() => { return showPlayable; });

        tweener.Kill();
        Destroy(playableEffectObj);
    }

    public void ShowPlayableZone() {
        StartCoroutine(ShowPlayableZoneCoroutine());
    }

    public void ResetShowPlayableZone() {
        showPlayable = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        image.color += new Color32(0, 0, 0, 155);
        if (card != null && card.State.Contains(Card.CardState.Setting)) {
            card.ShowImageOfSetCard();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        image.color -= new Color32(0, 0, 0, 155);
        if (card != null && card.State.Contains(Card.CardState.Setting)) {
            card.EndShowImageOfSetCard();
        }
    }

    public void OnClick() {
        Card card = Get();
        if (card != null && IsPlayable) {
            card.Controller.actionDialog.gameObject.SetActive(true);
            card.Controller.actionDialog.SetCard(card, this);
        }
    }
}
