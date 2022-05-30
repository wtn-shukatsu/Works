using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    1. カード画像オブジェクトのクリック判定
    2. カード画像オブジェクトとカード情報を関連付け
*/
public class SelectCard : MonoBehaviour
{
    Card card;                                          // カード情報
    SelectDialog selectDialog;                          // カード選択ダイヤログ (選択通知用) 
    bool selected = false;                              // 選択判定フラグ

    [SerializeField] Image cardImage = null;            // カード画像のImage
    [SerializeField] GameObject selectedImage = null;   // カード選択枠のGameObject

    // カード情報等の登録
    public void SetCard(Card _card, SelectDialog _selectDialog) {
        card = _card;
        selectDialog = _selectDialog;
        cardImage.sprite = card.Face;
    }

    // カード選択時の動作
    public void Selected() {
        if (selected) {

            // 既に選択されたいる場合は選択解除
            selected = false;
            selectedImage.SetActive(false);
            selectDialog.Selected(card, selected);
        }
        else {

            // それ以上カードを選択できるか判定
            selected = selectDialog.Selectable();
            if (selected) {

                // 選択可能なら選択通知
                selectedImage.SetActive(true);
                selectDialog.Selected(card, selected);
            }
        }
    }
}
