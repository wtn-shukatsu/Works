using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    1. カードの操作を行う
    2. FieldMasterへの窓口
    3. EffectMasterへの窓口
*/
public class Player : MonoBehaviour
{
    // フェイズ管理用
    public enum Phase {
        MyDraw,
        MyStandby,
        MyMain1,
        MyBattle,
        MyMain2,
        MyEnd,
        OpDraw,
        OpStandby,
        OpMain1,
        OpBattle,
        OpMain2,
        OpEnd,
    };

    int lp = 8000;
    Phase phase;                                            // 現在のフェイズ
    EffectMaster effectMaster = new EffectMaster();         // カード効果の管理 (発動タイミング, チェーン, etc.) 
    FieldMaster fieldMaster = new FieldMaster();            // 盤面情報の管理 (召喚権, etc.) 

    [SerializeField] string deckID = null;                  // デッキID
    [SerializeField] Deck deck = null;                      // デッキ
    [SerializeField] ExDeck exDeck = null;                  // EXデッキ
    [SerializeField] Hand hand = null;                      // 手札
    [SerializeField] List<Field> exMonsterZone = null;      // EXモンスターゾーン
    [SerializeField] List<Field> mainMonsterZone = null;    // メインモンスターゾーン
    [SerializeField] List<Field> magicTrapZone = null;      // 魔法＆罠ゾーン
    [SerializeField] List<Field> pendulumZone = null;       // Pゾーン
    [SerializeField] Field fieldZone = null;                // フィールドゾーン
    [SerializeField] List<Field> myField = null;            // 自分フィールド
    [SerializeField] Graveyard graveyard = null;            // 墓地
    [SerializeField] BanishZone banishZone = null;          // 除外ゾーン
    [SerializeField] Player opponent = null;                // 相手プレイヤー
    [SerializeField] ActionDialog actDlog = null;           // カードアクション選択ダイアログ
    [SerializeField] SelectDialog slctDlog = null;          // カード選択ダイアログ

    public FieldMaster FieldMaster { get { return fieldMaster; } }
    public EffectMaster EffectMaster { get { return effectMaster; } }
    public int LP { get { return lp; } }
    public string DeckID { get { return deckID; } }
    public Deck Deck { get { return deck; } }
    public Hand Hand { get { return hand; } }
    public List<Field> MyField { get { return myField; } }
    public List<Field> ExMonsterZone { get { return exMonsterZone; } }
    public List<Field> MainMonsterZone { get { return mainMonsterZone; } }
    public List<Field> PendulumZone { get { return pendulumZone; } }
    public List<Field> MagicTrapZone { get { return magicTrapZone; } }
    public Field FieldZone { get { return fieldZone; } }
    public ActionDialog actionDialog { get { return actDlog; } }
    public SelectDialog selectDialog { get { return slctDlog; } }

    // デッキからカードをドロー
    public void Draw(int _num) {
        for (int i=0; i<_num; i++) {
            Card card = deck.Pull(0);
            card.transform.SetAsLastSibling();
            hand.Add(card);
        }
    }

    // 表示形式を変更
    public void ChangeBattlePosition(Card _card) {
        _card.ChangeBattlePosition();
        switch (_card.battlePosition) {
            case Card.BattlePosition.Attack:
                Debug.Log($"{gameObject.name} が {_card.Name} を攻撃表示に変更");
                break;
            case Card.BattlePosition.FaceUpDefense:
                Debug.Log($"{gameObject.name} が {_card.Name} を守備表示に変更");
                break;
            case Card.BattlePosition.FaceDownDefense:
                Debug.Log($"{gameObject.name} が {_card.Name} を反転召喚");
                break;
            default:
                break;
        }
    }

    // 通常召喚 (召喚) 
    public void Summon(Card _card, Field _field) {
        Card card = hand.Pull(_card);
        _field.Add(card);
        card.Summon();
        Debug.Log($"{gameObject.name} が {_field.gameObject.name} に {card.Name} を召喚");

        // 召喚権を消費
        fieldMaster.Summoned();
    }

    // 通常召喚 (セット) 
    public void SetMonster(Card _card, Field _field) {
        Card card = hand.Pull(_card);
        _field.Add(card);
        card.SetMonster();
        Debug.Log($"{gameObject.name} が {_field.gameObject.name} にモンスターをセット");

        // 召喚権を消費
        fieldMaster.Summoned();
    }

    // 魔法・罠カードの発動
    public void PlayMagicTrap(Card _card, Field _field) {
        Card card = hand.Pull(_card);
        _field.Add(card);
        Debug.Log($"{gameObject.name} が {_field.gameObject.name} に {card.Name} を発動");
        card.PlayMagicTrap();
    }

    // 魔法・罠カードのセット
    public void SetMagicTrap(Card _card, Field _field) {
        Card card = hand.Pull(_card);
        _field.Add(card);
        card.SetMagicTrap();
        Debug.Log($"{gameObject.name} が {_field.gameObject.name} に魔法・罠カードをセット");
    }

    // セットされた魔法・罠カードの発動
    public void PlaySetMagicTrap(Card _card) {
        Debug.Log($"{gameObject.name} がセットされた {_card.Name} を発動");
        _card.PlaySetMagicTrap();
    }

    // Pゾーンへのセット
    public void SetPendulum(Card _card, Field _field) {
        Card card = hand.Pull(_card);
        _field.Add(card);
        card.SetPendulum();
        Debug.Log($"{gameObject.name} が {_field.gameObject.name} (Pゾーン) に {card.Name} をセット");
    }

    // カードを手札に加える
    public void Search(List<Card> _cardList) {
        foreach (Card card in _cardList) {
            Card searchCard = card.Place.Pull(card);
            hand.Add(searchCard);
        }
    }

    // フィールドのカードを墓地へ送る
    public void SendToGraveyardFromField(Card _card) {
        Card card = _card.Place.Pull();
        graveyard.Add(card);
    }

    // モンスターに攻撃
    public void AttackToMonster(Card _card, Card _enemy) {
        ;
    }

    // 直接攻撃
    public void AttackToPlayer(Card _card) {
        opponent.DamagedFromDirectAttack(_card);
    }

    // 直接攻撃による被ダメージ
    public void DamagedFromDirectAttack(Card _card) {
        lp -= _card.Atk;
        Debug.Log($"{gameObject.name}の残りLP{lp}");
    }

    // フェイズ毎のアクションを管理
    public void SetPhase(GameMaster.Phase _phase, bool _myTurn) {
        switch (_phase) {
            case GameMaster.Phase.Draw:
                if (_myTurn) {
                    phase = Phase.MyDraw;
                }
                else {
                    phase = Phase.OpDraw;
                }
                DrawPhaseAction(_myTurn);
                break;
            case GameMaster.Phase.Standby:
                if (_myTurn) {
                    phase = Phase.MyStandby;
                }
                else {
                    phase = Phase.OpStandby;
                }
                StandbyPhaseAction(_myTurn);
                break;
            case GameMaster.Phase.Main1:
                if (_myTurn) {
                    phase = Phase.MyMain1;
                }
                else {
                    phase = Phase.OpMain1;
                }
                MainPhase1Action(_myTurn);
                break;
            case GameMaster.Phase.Battle:
                if (_myTurn) {
                    phase = Phase.MyBattle;
                }
                else {
                    phase = Phase.OpBattle;
                }
                BattlePhaseAction(_myTurn);
                break;
            case GameMaster.Phase.Main2:
                if (_myTurn) {
                    phase = Phase.MyMain2;
                }
                else {
                    phase = Phase.OpMain2;
                }
                MainPhase2Action(_myTurn);
                break;
            case GameMaster.Phase.End:
                if (_myTurn) {
                    phase = Phase.MyEnd;
                }
                else {
                    phase = Phase.OpEnd;
                }
                EndPhaseAction(_myTurn);
                break;
            default:
                break;
        }
    }

    void DrawPhaseAction(bool _myTurn) {
        if (_myTurn) {
            fieldMaster.SetSummonCount(1);
            Draw(1);
        }
        fieldMaster.ResetIState(this);
        effectMaster.GetEffectList(phase);
    }

    void StandbyPhaseAction(bool _myTurn) {
        effectMaster.GetEffectList(phase);
    }

    void MainPhase1Action(bool _myTurn) {
        if (_myTurn) {
            fieldMaster.Playable(true, this);
        }
        effectMaster.GetEffectList(phase);
    }

    void BattlePhaseAction(bool _myTurn) {
        if (_myTurn) {
            fieldMaster.Playable(false, this);
        }
        effectMaster.GetEffectList(phase);
    }

    void MainPhase2Action(bool _myTurn) {
        if (_myTurn) {
            fieldMaster.Playable(true, this);
        }
        effectMaster.GetEffectList(phase);
    }

    void EndPhaseAction(bool _myTurn) {
        if (_myTurn) {
            fieldMaster.Playable(false, this);
        }
        effectMaster.GetEffectList(phase);
    }
}
