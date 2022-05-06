using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    1. カード選択ダイアログを表示
    2. 選択されたカードのリストを返す
*/
public class SelectDialog : MonoBehaviour
{
    int selectNum;              // 選択するカードの数
    bool selected;              // 選択終了フラグ
    List<Card> cardList;        // 選択されたカードのリスト
    List<GameObject> objList;   // ダイアログに表示するカード画像用オブジェクトのリスト

    [SerializeField] GameObject selectCardPrefab = null;    // ダイアログに表示するカード画像用オブジェクトのプレハブ
    [SerializeField] Button ok = null;                      // OKボタンのButtonコンポーネント
    [SerializeField] ContentSizeController content = null;  // スクロールビューのウィンドウサイズ調整用コンポーネント

    // 受け取ったカードリストをダイアログに表示＆選択されたカードのリストを返す
    public IEnumerator SelectCard(List<Card> _cardList, int _num) {
        selectNum = _num;
        selected = false;
        cardList = new List<Card>();
        objList = new List<GameObject>();

        // ダイアログ表示＆カード選択待機
        yield return StartCoroutine(SelectCardCoroutine(_cardList));

        // スクロールビューのウィンドウサイズをリセット
        content.ResetContentSize();

        // 生成したカード画像オブジェクトを破棄
        foreach (GameObject obj in objList) {
            Destroy(obj);
        }

        yield return cardList;
    }

    // カード選択待機用コルーチン
    IEnumerator SelectCardCoroutine(List<Card> _cardList) {
        ShowCardList(_cardList);

        // カード選択終了まで待機
        yield return new WaitUntil(() => { return selected; });
    }

    // ダイアログにカードリストを表示
    void ShowCardList(List<Card> _cardList) {
        foreach (Card card in _cardList) {

            // スクロールビューのウィンドウサイズを調整
            content.ControlContentSize(1);

            // カード画像を生成
            GameObject selectCardObj = Instantiate(selectCardPrefab);
            objList.Add(selectCardObj);
            selectCardObj.transform.SetParent(content.transform);
            selectCardObj.transform.localScale = Vector3.one;

            // 生成したオブジェクトにカード情報を関連付け
            selectCardObj.GetComponent<SelectCard>().SetCard(card, this);
        }
    }

    // それ以上カードを選択できるかを判定
    public bool Selectable() {
        return cardList.Count < selectNum;
    }

    // カード選択時の動作
    public void Selected(Card _card, bool _selected) {
        if (_selected) {

            // 選択されたカードの登録
            cardList.Add(_card);
        }
        else {

            // 選択されたカードの選択解除
            cardList.Remove(_card);
        }

        if (!Selectable()) {

            // 指定数だけカードが選択されていればOKボタンを有効化
            ok.interactable = true;
        }
        else {

            // 指定数だけカードが選択されていなければOKボタンを無効化
            ok.interactable = false;
        }
    }

    // カード選択終了時の動作
    public void OK() {

        // 選択終了フラグをON
        selected = true;

        // OKボタンを無効化
        ok.interactable = false;
    }
}
