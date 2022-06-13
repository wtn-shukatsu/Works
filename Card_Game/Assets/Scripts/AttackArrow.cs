using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 攻撃対象選択時の矢印の表示
 */
public class AttackArrow : MonoBehaviour {
    LineRenderer lineRenderer;      // LineRenderer
    Vector3 defaultEndPosition;     // 矢印の先端のデフォルト座標

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        defaultEndPosition = lineRenderer.GetPosition(1);
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            var endPosition = Input.mousePosition;
            lineRenderer.SetPosition(1, endPosition);
        }

        // リセット
        if (!(Input.GetMouseButton(0))) {
            lineRenderer.SetPosition(1, defaultEndPosition);
        }
    }
}
