using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Reimu : CardData
{
    string myName;
    Monster mns;
    List<CardAction> effectList;
    CardAction cardAction = new CardAction();
    
    public override string Name { get { return myName; } }
    public override Monster monster { get { return mns; } }
    public override List<CardAction> EffectList { get { return effectList; } }

    public Reimu() {
        myName = "幻想の主人公 博麗霊夢";
        mns = new Monster(
                new List<Monster.MonsterType>() {
                    Monster.MonsterType.Effect,
                    Monster.MonsterType.Pendulum
                },
                new List<Monster.Type>() {
                    Monster.Type.Spellcaster
                },
                new List<Monster.Attribute>() {
                    Monster.Attribute.Light
                },
                4, 1700, 1800
              );
        effectList = new List<CardAction>() {
                        new CardAction(
                            CardAction.Timing.MyMainPhase,
                            CardAction.Place.MyMainMonsterZone,
                            CardAction.Target.Me,
                            new Action(Summon)
                        )
                     };
    }

    void Summon() {
        cardAction.Summon(card);
    }

    void SetMonster() {
        cardAction.SetMonster(card);
    }

    void SetPendulum() {
        cardAction.SetPendulum(card);
    }

    void ChangeBattlePosition() {
        cardAction.ChangeBattlePosition(card);
    }

    public void Attack() {
        cardAction.Attack(card);
    }
}
