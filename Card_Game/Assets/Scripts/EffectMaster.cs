using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMaster
{
    List<CardAction> myDrawEffectList = new List<CardAction>();         // 自分ドローフェイズに使用可能な効果
    List<CardAction> myStandbyEffectList = new List<CardAction>();      // 自分スタンバイフェイズに使用可能な効果
    List<CardAction> myMain1EffectList = new List<CardAction>();        // 自分メインフェイズ1に使用可能な効果
    List<CardAction> myBattleEffectList = new List<CardAction>();       // 自分バトルフェイズに使用可能な効果
    List<CardAction> myMain2EffectList = new List<CardAction>();        // 自分メインフェイズ2に使用可能な効果
    List<CardAction> myEndEffectList = new List<CardAction>();          // 自分エンドフェイズに使用可能な効果
    List<CardAction> opDrawEffectList = new List<CardAction>();         // 相手ドローフェイズに使用可能な効果
    List<CardAction> opStandbyEffectList = new List<CardAction>();      // 相手スタンバイフェイズに使用可能な効果
    List<CardAction> opMain1EffectList = new List<CardAction>();        // 相手メインフェイズ1に使用可能な効果
    List<CardAction> opBattleEffectList = new List<CardAction>();       // 相手バトルフェイズに使用可能な効果
    List<CardAction> opMain2EffectList = new List<CardAction>();        // 相手メインフェイズ2に使用可能な効果
    List<CardAction> opEndEffectList = new List<CardAction>();          // 相手エンドフェイズに使用可能な効果

    public void SetCardEffect(Card _card) {
        if (_card.EffectList != null) {
            foreach (CardAction effect in _card.EffectList) {
                switch (effect.timing) {
                    case CardAction.Timing.MyMainPhase:
                        myMain1EffectList.Add(effect);
                        myMain2EffectList.Add(effect);
                        break;
                }
            }
        }
    }

    public void GetEffectList(Player.Phase _phase) {
        List<CardAction> effectList = new List<CardAction>();
        switch (_phase) {
            case Player.Phase.MyDraw:
                effectList = myDrawEffectList;
                break;
            case Player.Phase.MyStandby:
                effectList = myStandbyEffectList;
                break;
            case Player.Phase.MyMain1:
                effectList = myMain1EffectList;
                break;
            case Player.Phase.MyBattle:
                effectList = myBattleEffectList;
                break;
            case Player.Phase.MyMain2:
                effectList = myMain2EffectList;
                break;
            case Player.Phase.MyEnd:
                effectList = myEndEffectList;
                break;
            case Player.Phase.OpDraw:
                effectList = opDrawEffectList;
                break;
            case Player.Phase.OpStandby:
                effectList = opStandbyEffectList;
                break;
            case Player.Phase.OpMain1:
                effectList = opMain1EffectList;
                break;
            case Player.Phase.OpBattle:
                effectList = opBattleEffectList;
                break;
            case Player.Phase.OpMain2:
                effectList = opMain2EffectList;
                break;
            case Player.Phase.OpEnd:
                effectList = opEndEffectList;
                break;
        }
    }
}
