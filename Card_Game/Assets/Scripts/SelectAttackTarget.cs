using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ?U???Ώۂ̑I??
 */
public class SelectAttackTarget : MonoBehaviour
{
    [SerializeField] AttackArrow attackArrow = null;

    public void ShowAttackArrow(Field _field) {
        gameObject.SetActive(true);
        attackArrow.ResetPositions(_field);
    }
}
