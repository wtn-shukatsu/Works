using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 defaultEndPosition;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        defaultEndPosition = lineRenderer.GetPosition(1);
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            var endPosition = Input.mousePosition;
            lineRenderer.SetPosition(1, endPosition);
        }

        // ƒŠƒZƒbƒg
        if (!(Input.GetMouseButton(0))) {
            lineRenderer.SetPosition(1, defaultEndPosition);
        }
    }
}
