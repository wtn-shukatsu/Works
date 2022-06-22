using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

/*
    デッキの生成を行う
*/
public class DeckGenerater : MonoBehaviour
{
    Hashtable faceList;                                     // カード画像のキャッシュ
    Sprite back;                                            // カード裏面

    string cardImageDirectory = "./Assets/CardImages/";     // カード画像の保存場所
    string deckDirectory = "./Assets/Decks/";               // デッキデータの保存場所
    int cardWidth = 1180;                                   // カード画像の横幅
    int cardHeight = 1720;                                  // カード画像の高さ

    [SerializeField] GameObject cardPrefab = null;          // カードオブジェクトのプレハブ
    
    // デッキのJsonデータ格納用
    [Serializable]
    class InputDeckData
    {
        public int id;              // デッキID
        public string name;         // デッキ名
        public string[] list;       // デッキリスト
    }

    void Awake() {
        faceList = new Hashtable();

        // カード裏面画像を取得
        Texture2D backTexture = ReadTexture($"{cardImageDirectory}カード裏面.png");
        back = Sprite.Create(backTexture, new Rect(0, 0, cardWidth, cardHeight), new Vector2(0, 1), 1);
    }

    // デッキを生成
    public void Generate(Player _player) {
        string[] deckData = GetDeckList(_player.DeckID);
        foreach (string cardID in deckData) {
            _player.Deck.Add(GenerateCardFromID(cardID, _player));
        }

        // デッキをシャッフル
        _player.Deck.Shuffle();
    }

    // デッキデータを取得
    string[] GetDeckList(string _deckID) {

        // _deckIDと同名のjsonファイルを取得
        FileInfo fi = new FileInfo($"{deckDirectory}{_deckID}.json");
        StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
        string deckDataJson = sr.ReadToEnd();
        sr.Close();

        InputDeckData deckData = JsonUtility.FromJson<InputDeckData>(deckDataJson);

        // デッキリストを返す
        return deckData.list;
    }

    // カードを生成
    Card GenerateCardFromID(string _cardID, Player _player) {
        GameObject cardObj = Instantiate(cardPrefab);
        Card card = cardObj.GetComponent<Card>();

        // カードIDから該当のクラスを取得＆カードオブジェクトにアタッチ
        card.gameObject.AddComponent(Type.GetType(_cardID));
        CardData cardData = (CardData)card.GetComponent(Type.GetType(_cardID));
        cardData.SetCard(card);

        // カードを生成
        card.Generate(GetImage(cardData.Name), back, cardData, _player);
        cardObj.name = card.Name;

        // カードの効果を登録
        _player.EffectMaster.SetCardEffect(card);

        return card;
    }

    // カード画像を取得
    Sprite GetImage(string _name) {
        Sprite face;

        // 高速化のため, 1度読み込んだカード画像データを記憶＆再利用
        if (faceList.ContainsKey(_name)) {

            // キャッシュに該当データがあればそれを取得
            face = (Sprite)faceList[_name];
        }
        else {

            // カード画像データを取得＆キャッシュに保存
            Texture2D faceTexture = ReadTexture($"{cardImageDirectory}{_name}.png");
            face = Sprite.Create(faceTexture, new Rect(0, 0, cardWidth, cardHeight), new Vector2(0, 1), 1);
            faceList.Add(_name, face);
        }

        return face;
    }

    // PNGファイルをTexture2Dに変換
    Texture2D ReadTexture(string _path) {

        // PNGファイル読み込み
        byte[] readBinary = ReadPngFile(_path);

        // バイナリをTexture2Dに変換
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(readBinary);


        return texture;
    }

    // PNGファイル読み込み
    byte[] ReadPngFile(string _path) {
        FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read);
        BinaryReader bin = new BinaryReader(fileStream);
        byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();

        return values;
    }
}
