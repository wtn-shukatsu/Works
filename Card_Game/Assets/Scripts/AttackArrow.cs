using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �U���ΏۑI�����̖��̕\��
 */
public class AttackArrow : MonoBehaviour {
    LineRenderer lineRenderer;      // LineRenderer
    Vector3 defaultEndPosition;     // ���̐�[�̃f�t�H���g���W

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        defaultEndPosition = lineRenderer.GetPosition(1);
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            var endPosition = Input.mousePosition;
            lineRenderer.SetPosition(1, endPosition);
        }

        // ���Z�b�g
        if (!(Input.GetMouseButton(0))) {
            lineRenderer.SetPosition(1, defaultEndPosition);
        }
    }
}
