using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Marisa : CardData {
    string myName;
    Monster mns;
    List<CardAction> effectList;
    CardAction cardAction = new CardAction();
    
    public override string Name { get { return myName; } }
    public override Monster monster { get { return mns; } }
    public override List<CardAction> EffectList { get { return effectList; } }

    public Marisa() {
        myName = "幻想の主人公 霧雨魔理沙";
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
                4, 1800, 1700
              );
        effectList = new List<CardAction>();
    }

    public void Summon() {
        cardAction.Summon(card);
    }

    public void SetMonster() {
        cardAction.SetMonster(card);
    }

    public void SetPendulum() {
        cardAction.SetPendulum(card);
    }

    public void ChangeBattlePosition() {
        cardAction.ChangeBattlePosition(card);
    }

    public void Attack() {
        cardAction.Attack(card);
    }
}
