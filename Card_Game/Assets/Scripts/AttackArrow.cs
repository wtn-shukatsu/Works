using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 攻撃対象選択時の矢印の表示
 */
public class AttackArrow : MonoBehaviour {
    LineRenderer lineRenderer;      // LineRenderer
    Vector3 defaultStartPosition;
    Vector3 defaultEndPosition;
    Field field;
    float height;
    float defaultStartWidth;
    float defaultEndWidth;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        defaultStartWidth = lineRenderer.startWidth;
        defaultEndWidth = lineRenderer.endWidth;
    }

    public void ResetPositions(Field _field) {
        field = _field;
        height = _field.GetComponent<RectTransform>().rect.height;

        //StartCoroutine(Wait());
        //lineRenderer.SetPosition(0, _field.transform.position);
        //defaultStartPosition = _field.transform.position - new Vector3(0, height / 2, 0);
        defaultStartPosition = _field.transform.position;
        defaultEndPosition = _field.transform.position + new Vector3(0, height / 2, 0);
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            lineRenderer.SetPosition(1, Input.mousePosition);
            //lineRenderer.SetPosition(1, field.transform.position);
            lineRenderer.startWidth = defaultStartWidth;
            lineRenderer.endWidth = defaultEndWidth;
        }

        // リセット
        if (!(Input.GetMouseButton(0))) {
            lineRenderer.SetPosition(0, defaultStartPosition);
            lineRenderer.SetPosition(1, defaultEndPosition);
            lineRenderer.startWidth = 0;
            lineRenderer.endWidth = 0;
        }
    }

    IEnumerator Wait() {
        yield return new WaitForEndOfFrame();
    }
}
