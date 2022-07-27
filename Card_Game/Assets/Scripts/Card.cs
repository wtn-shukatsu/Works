using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

/*
    1. カード毎のアクションを行う
    2．現在のカード情報を保持
*/
public class Card : MonoBehaviour
{
    // 表示形式用の型
    public enum BattlePosition {
        Attack,
        FaceUpDefense,
        FaceDownDefense,
    };

    // カードの状態用の型 (1ターンで消えるもの) 
    public enum InstantCardState {
        SummonedThisTurn,
        ChangedBattlePosition,
        CannotPlayMagicTrap,
    };

    // カードの状態用の型 (永続) 
    public enum CardState {
        Summoned,
        Setting,
    };

    CardData cardData;                                      // カードの元データ
    Player controller;                                      // コントローラー
    string myName;                                          // カード名
    Type cardType;                                          // カードの種類
    CardData.Monster mns;                                   // モンスターに関するデータ (魔法・罠の場合はnull) 
    CardData.Magic mgc;                                     // 魔法に関するデータ (モンスター・罠の場合はnull) 
    CardData.Trap trp;                                      // 罠に関するデータ (モンスター・魔法の場合はnull) 
    BattlePosition bp;                                      // 表示形式
    List<InstantCardState> iState;                          // カードの状態 (1ターンで消えるもの) 
    List<CardState> state;                                  // カードの状態 (永続) 
    CardPlace place;                                        // カードの場所
    List<CardAction> effectList;                            // カードの効果

    /*
        モンスターのステータス
     */
    int atk;                                                // モンスターの攻撃力
    int def;                                                // モンスターの守備力
    int release;                                            // モンスターの召喚に必要なリリース数

    [SerializeField] GameObject faceObj = null;             // カード画像用オブジェクト (表面) 
    [SerializeField] GameObject backObj = null;             // カード画像用オブジェクト (裏面) 
    [SerializeField] Image face = null;                     // カード画像用オブジェクトのImageコンポーネント (表面) 
    [SerializeField] Image back = null;                     // カード画像用オブジェクトのImageコンポーネント (裏面) 
    [SerializeField] DragCard drg = null;                   // カードドラッグ用のコンポーネント

    public Sprite Face { get { return face.sprite; } }
    public Sprite Back { get { return back.sprite; } }
    public DragCard dragCard { get { return drg; } }
    public CardData CardData { get { return cardData; } }
    public Player Controller { get { return controller; } }
    public string Name { get { return myName; } }
    public Type CardType { get { return cardType; } }
    public CardData.Monster monster { get { return mns; } }
    public CardData.Magic magic { get { return mgc; } }
    public CardData.Trap trap { get { return trp; } }
    public BattlePosition battlePosition { get { return bp; } }
    public List<InstantCardState> IState { get { return iState; } }
    public List<CardState> State { get { return state; } }
    public CardPlace Place { get { return place; } }
    public List<CardAction> EffectList { get { return effectList; } }
    public int Atk { get { return atk; } }
    public int Def { get { return def; } }
    public int Release { get { return release; } }

    // カード情報の生成
    public void Generate(Sprite _face, Sprite _back, CardData _cardData, Player _player) {
        face.sprite = _face;
        back.sprite = _back;
        cardData = _cardData;
        controller = _player;
        myName = cardData.Name;
        mns = cardData.monster;
        mgc = cardData.magic;
        trp = cardData.trap;
        if (mns != null) {
            cardType = typeof(CardData.Monster);
            atk = mns.Atk;
            def = mns.Def;
            release = mns.Release;
        }
        else if (mgc != null) {
            cardType = typeof(CardData.Magic);
        }
        else if (trp != null) {
            cardType = typeof(CardData.Trap);
        }
        else { ; }
        effectList = cardData.EffectList;
        bp = new BattlePosition();
        iState = new List<InstantCardState>();
        state = new List<CardState>();
    }

    // カード表面／裏面の描画変更
    public void ToggleFace(bool showFace) {
        if (showFace) {
            backObj.SetActive(false);
            faceObj.SetActive(true);
        }
        else {
            faceObj.SetActive(false);
            backObj.SetActive(true);
        }
    }

    // セットされたカードの確認 (カード画像の透過) 
    public void ShowImageOfSetCard() {
        back.color = new Color32(255, 255, 255, 155);
        faceObj.SetActive(true);
    }

    // セットされたカードの確認終了
    public void EndShowImageOfSetCard() {
        back.color = new Color32(255, 255, 255, 255);
        faceObj.SetActive(false);
    }

    // カードデータの指定メソッドの存在確認
    public bool ExistCardAction(string _name) {

        // _name に対応するメソッドを取得
        var act = cardData.GetType().GetMethod(
                    _name, 
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                    null, 
                    new Type[] {}, 
                    null
                  );

        return act != null;
    }

    // カードデータの指定メソッドの実行
    public void InvokeCardAction(string _name) {

        // _name に対応するメソッドを取得
        var act = cardData.GetType().GetMethod(
                    _name, 
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                    null, 
                    new Type[] {}, 
                    null
                  );

        if (act != null) {
            act.Invoke(cardData, null);
        }
    }

    // 通常召喚 (召喚) 
    public void Summon() {
        InvokeCardAction("Summon");
    }

    // 通常召喚 (セット) 
    public void SetMonster() {
        InvokeCardAction("SetMonster");
    }

    // 魔法・罠カードの発動
    public void PlayMagicTrap() {
        InvokeCardAction("PlayMagicTrap");
    }

    // 魔法・罠カードのセット
    public void SetMagicTrap() {
        InvokeCardAction("SetMagicTrap");
    }

    // セットされた魔法・罠カードの発動
    public void PlaySetMagicTrap() {
        InvokeCardAction("PlaySetMagicTrap");
    }

    // Pゾーンへのセット
    public void SetPendulum() {
        InvokeCardAction("SetPendulum");
    }

    // 表示形式を変更
    public void ChangeBattlePosition() {
        InvokeCardAction("ChangeBattlePosition");
    }

    // 表示形式の設定
    public void SetBattlePosition(BattlePosition _bp) {
        bp = _bp;
    }

    // カードの状態 (1ターンで消えるもの) をリセット
    public void ResetIState() {
        iState.Clear();
    }

    // カードの場所の設定
    public void SetPlace(CardPlace _place) {
        place = _place;
    }
}
