using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    カードの追加・削除に関する抽象クラス
    ex) デッキから手札に加える → Deck.Pull(Card); Hand.Add(Card);
*/
public class CardPlace : MonoBehaviour
{
    public virtual void Add(Card _card) {}
    public virtual Card Pull() { return null; }
    public virtual Card Pull(Card _card) { return null; }
    public virtual Card Pull(int _position) { return null; }
}
