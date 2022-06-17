using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
    カードのベースアクションを保持
      各アクションの組み合わせで効果を構成
      ex) 無効にして破壊する → 無効アクション + 破壊アクション
*/
public class CardAction
{
    public enum Timing {
        None,
        MyMainPhase,
    };

    public enum Place {
        None,
        MyMainMonsterZone,
        MyPendulumZone,
        MyMagicTrapZone,
    };

    public enum Target {
        None,
        Me,
    };

    public Timing tmg;          // いつ
    public Place plc;           // どこで
    public Target tgt;          // 何に
    public Action act;          // どうする

    public Timing timing { get { return tmg; } }
    public Place place { get { return plc; } }
    public Target target { get { return tgt; } }
    public Action action { get { return act; } }

    public CardAction() {}
    public CardAction(Timing _timing, Place _place, Target _target, Action _action) {
        tmg = _timing;
        plc = _place;
        tgt = _target;
        act = _action;
    }

    public void Summon(Card _card) {
        _card.Controller.FieldMaster.SelectRelease(_card.Release, _card.Controller);
        _card.SetBattlePosition(Card.BattlePosition.Attack);
        _card.IState.Add(Card.InstantCardState.SummonedThisTurn);
    }

    public void SetMonster(Card _card) {
        _card.Controller.FieldMaster.SelectRelease(_card.Release, _card.Controller);
        _card.ToggleFace(false);
        _card.transform.Rotate(new Vector3(0, 0, -90));
        _card.SetBattlePosition(Card.BattlePosition.FaceDownDefense);
        _card.IState.Add(Card.InstantCardState.SummonedThisTurn);
        _card.State.Add(Card.CardState.Setting);
    }

    public void SetPendulum(Card _card) {
    }

    public void ChangeBattlePosition(Card _card) {
        switch (_card.battlePosition) {
            case Card.BattlePosition.Attack:
                _card.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.1f);
                _card.SetBattlePosition(Card.BattlePosition.FaceUpDefense);
                _card.IState.Add(Card.InstantCardState.ChangedBattlePosition);
                break;
            case Card.BattlePosition.FaceUpDefense:
                _card.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
                _card.SetBattlePosition(Card.BattlePosition.Attack);
                _card.IState.Add(Card.InstantCardState.ChangedBattlePosition);
                break;
            case Card.BattlePosition.FaceDownDefense:
                _card.ToggleFace(true);
                _card.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
                _card.SetBattlePosition(Card.BattlePosition.Attack);
                _card.IState.Add(Card.InstantCardState.ChangedBattlePosition);
                _card.State.Remove(Card.CardState.Setting);
                break;
            default:
                break;
        }
    }

    public void Attack(Card _card) {
    }

    public void SetMagicTrap(Card _card) {
        _card.ToggleFace(false);
        _card.State.Add(Card.CardState.Setting);
    }

    public List<Card> SearchFromDeck(Player _player, Type _cardType, string _key) {
        List<Card> cardList = new List<Card>();
        foreach (Card card in _player.Deck.CardList) {
            if (card.CardType == _cardType && card.Name.Contains(_key)) {
                cardList.Add(card);
            }
        }
        return cardList;
    }
}
