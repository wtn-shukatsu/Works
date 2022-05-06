using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    スクロールビューのウィンドウサイズを調整
*/
public class ContentSizeController : MonoBehaviour
{
    float diff;                 // カード画像横幅の合計と自身の横幅との差
    float contentWidth = 0;     // カード画像横幅の合計
    int contentCount = 0;       // カード画像枚数

    [SerializeField] RectTransform selectCardRect = null;   // カード画像のRectTransformコンポーネント
    [SerializeField] RectTransform myRect = null;           // 自身のRectTransformコンポーネント
    [SerializeField] HorizontalLayoutGroup layout = null;   // 自身のLayoutコンポーネント

    // スクロールビューのウィンドウサイズを調整
    public void ControlContentSize(int _num) {

        // カード画像枚数を加算
        contentCount += _num;

        // カード画像横幅の合計を計算
        contentWidth = selectCardRect.sizeDelta.x*contentCount + layout.spacing*(contentCount-1);

        // カード画像横幅の合計と自身の横幅の合計の差を計算
        diff = myRect.sizeDelta.x - contentWidth;

        if (diff < 0) {
            
            // 不足分だけ自身の横幅を伸ばす
            myRect.sizeDelta += new Vector2(-diff, 0);
        }
    }

    // スクロールビューのウィンドウサイズをリセット
    public void ResetContentSize() {

        // カード画像枚数のリセット
        contentCount = 0;

        if (diff < 0) {

            // 自身の横幅リセット
            myRect.sizeDelta += new Vector2(diff, 0);
        }
    }
}
