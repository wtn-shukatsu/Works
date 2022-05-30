using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMaster
{
    int summonCount;
    bool canSummon = false;

    public void Playable(bool _able, Player _player) {
        canSummon = _able;
        _player.Hand.Movable(_able);
        foreach (Field field in _player.MyField) {
            field.Playable(_able);
        }
    }

    public void SetSummonCount(int _num) {
        summonCount = _num;
    }

    public void Summoned() {
        summonCount--;
    }

    public bool CanSummon() {
        return canSummon && summonCount > 0;
    }

    public void ResetIState(Player _player) {
        foreach (Field field in _player.MainMonsterZone) {
            field.ResetIState();
        }
        foreach (Field field in _player.ExMonsterZone) {
            field.ResetIState();
        }
    }

    public void BeforePlayCard(Card _card, Player _player) {
        foreach (Field field in _player.MyField) {
            if (field.IsPlayableZone(_card)) {
                field.ShowPlayableZone();
            }
        }
    }

    public void AfterPlayCard(Player _player) {
        foreach (Field field in _player.MyField) {
            field.ResetShowPlayableZone();
        }
    }

    public void SelectRelease(int _numOfRelease, Player _player) {
        for (int i=0; i<_numOfRelease; i++) {
            ;
        }
    }
}
