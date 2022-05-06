using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
    手札にあるカードのドラッグ操作を管理
*/
public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentTransform;
    GameObject dummyCardObj;
    bool movable;
    bool played = false;
    bool toFace = false;
    bool toBack = false;
    bool onDrag = false;
    int siblingIndex;

    [SerializeField] GameObject dummyCardPrefab = null;
    [SerializeField] CanvasGroup canvasGroup = null;
    [SerializeField] Card card = null;

    public bool ToFace { get { return toFace; } }
    public bool ToBack { get { return toBack; } }
    public bool IsOnDrag { get { return onDrag;} }

    void Start() {
        parentTransform = transform.parent;
    }

    public void Movable(bool _movable) {
        movable = _movable;
    }

    public void Played() {
        played = true;
    }

    public void OnBeginDrag(PointerEventData data){
        if (movable) {
            toFace = Input.GetMouseButton(0);
            toBack = Input.GetMouseButton(1);
            if (toFace || toBack) {
                if (toBack) {
                    card.ToggleFace(false);
                    card.ShowImageOfSetCard();
                }
                canvasGroup.blocksRaycasts = false;
                parentTransform = transform.parent;

                /*
                    手札整列用
                */

                // ドラッグ前のカード位置を記憶
                siblingIndex = transform.GetSiblingIndex();

                // ドラッグ前のカード位置にダミーを生成
                dummyCardObj = Instantiate(dummyCardPrefab);
                dummyCardObj.transform.SetParent(parentTransform);
                dummyCardObj.transform.SetSiblingIndex(siblingIndex);

                /*
                    end 手札整列用
                */

                transform.SetParent(parentTransform.parent.parent);
                card.Controller.BeforePlayCard(card);
                onDrag = true;
            }
        }
    }

    public void OnDrag(PointerEventData data) {
        if (onDrag) {
            transform.position = data.position;
        }
    }

    public void OnEndDrag(PointerEventData data) {
        if (onDrag) {
            if (!played) {
                transform.SetParent(parentTransform);
                transform.SetSiblingIndex(siblingIndex);
                if (toBack) {
                    card.EndShowImageOfSetCard();
                    card.ToggleFace(true);
                }
            }
            Destroy(dummyCardObj);
            canvasGroup.blocksRaycasts = true;
            card.Controller.AfterPlayCard();
            toFace = false;
            toBack = false;
            played = false;
            onDrag = false;
        }
    }
}