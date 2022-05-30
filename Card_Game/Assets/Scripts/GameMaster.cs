using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public enum Phase {
        Draw,
        Standby,
        Main1,
        Battle,
        Main2,
        End,
    };
    Player currentPlayer, waitPlayer;
    Phase phase;

    [SerializeField] Player[] playerList = null;
    [SerializeField] DeckGenerater deckGenerater = null;
    [SerializeField] Button turnChangeButton = null;

    void Start() {
        deckGenerater.Generate(playerList[0]);
        deckGenerater.Generate(playerList[1]);
        currentPlayer = playerList[0];
        waitPlayer    = playerList[1];
        currentPlayer.Draw(2);
        waitPlayer.Draw(2);
        StartCoroutine(DrawPhase());
    }

    public void ChangePhase() {
        switch (phase) {
            case Phase.Draw:
                StopCoroutine(DrawPhase());
                StartCoroutine(StandbyPhase());
                break;
            case Phase.Standby:
                StopCoroutine(StandbyPhase());
                StartCoroutine(MainPhase1());
                break;
            case Phase.Main1:
                StopCoroutine(MainPhase1());
                StartCoroutine(BattlePhase());
                break;
            case Phase.Battle:
                StopCoroutine(BattlePhase());
                StartCoroutine(MainPhase2());
                break;
            case Phase.Main2:
                StopCoroutine(MainPhase2());
                StartCoroutine(EndPhase());
                break;
            case Phase.End:
                StopCoroutine(EndPhase());
                StartCoroutine(DrawPhase());
                break;
            default:
                break;
        }
    }

    IEnumerator DrawPhase() {
        phase = Phase.Draw;
        currentPlayer.SetPhase(phase, true);
        waitPlayer.SetPhase(phase, false);
        Debug.Log($"{currentPlayer.gameObject.name} DrawPhase");

        yield return new WaitForSeconds(0.5f);

        ChangePhase();
    }

    IEnumerator StandbyPhase() {
        phase = Phase.Standby;
        currentPlayer.SetPhase(phase, true);
        waitPlayer.SetPhase(phase, false);
        Debug.Log($"{currentPlayer.gameObject.name} StandbyPhase");

        yield return new WaitForSeconds(0.5f);

        ChangePhase();
    }

    IEnumerator MainPhase1() {
        phase = Phase.Main1;
        currentPlayer.SetPhase(phase, true);
        waitPlayer.SetPhase(phase, false);
        Debug.Log($"{currentPlayer.gameObject.name} MainPhase1");
        turnChangeButton.interactable = true;

        yield return new WaitWhile(() => { return true; });
    }

    IEnumerator BattlePhase() {
        phase = Phase.Battle;
        currentPlayer.SetPhase(phase, true);
        waitPlayer.SetPhase(phase, false);
        Debug.Log($"{currentPlayer.gameObject.name} BattlePhase");

        yield return new WaitWhile(() => { return true; });
    }

    IEnumerator MainPhase2() {
        phase = Phase.Main2;
        currentPlayer.SetPhase(phase, true);
        waitPlayer.SetPhase(phase, false);
        Debug.Log($"{currentPlayer.gameObject.name} MainPhase2");

        yield return new WaitWhile(() => { return true; });
    }

    IEnumerator EndPhase() {
        phase = Phase.End;
        currentPlayer.SetPhase(phase, true);
        waitPlayer.SetPhase(phase, false);
        Debug.Log($"{currentPlayer.gameObject.name} EndPhase");
        turnChangeButton.interactable = false;

        yield return new WaitForSeconds(0.5f);

        Player tmpPlayer = waitPlayer;
        waitPlayer = currentPlayer;
        currentPlayer = tmpPlayer;

        ChangePhase();
    }
}
