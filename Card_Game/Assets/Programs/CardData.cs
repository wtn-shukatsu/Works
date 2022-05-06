using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
    カード情報の基底クラス
*/
public class CardData : MonoBehaviour
{
    // モンスター
    public class Monster {
        public List<MonsterType> monsterType;
        public List<Type> type;
        public List<Attribute> attribute;
        public int lv;
        public int atk;
        public int def;
        public int release;

        public Monster(List<MonsterType> _monsterType, List<Type> _type, List<Attribute> _attribute, int _lv, int _atk, int _def) {
            monsterType = _monsterType;
            type = _type;
            attribute = _attribute;
            lv = _lv;
            atk = _atk;
            def = _def;
            if (lv <= 4) {
                release = 0;
            }
            else if (lv <= 6) {
                release = 1;
            }
            else {
                release = 2;
            }
        }

        // 種類
        public enum MonsterType {
            None,
            Nomal,
            Effect,
            Fusion,
            Ritual,
            Synchro,
            Xyz,
            Pendulum,
            Link,
        }

        // 種族
        public enum Type {
            None,
            Dragon,
            Spellcaster,
            Zombie,
            Warrior,
            BeastWarrior,
            Beast,
            WingedBeast,
            Fiend,
            Fairy,
            Insect,
            Dinosaur,
            Reptile,
            Fish,
            SeaSerpent,
            Machine,
            Thunder,
            Aqua,
            Pyro,
            Rock,
            Plant,
            Psychic,
            Wyrm,
            Cyberse,
            DivineBeast,
        }

        // 属性
        public enum Attribute {
            None,
            Dark,
            Light,
            Earth,
            Water,
            Fire,
            Wind,
            Divine,
        }
    };

    // 魔法
    public class Magic {
        public MagicType magicType;

        public Magic(MagicType _magicType) {
            magicType = _magicType;
        }

        // 種類
        public enum MagicType {
            None,
            Nomal,             
        }
    };

    // 罠
    public class Trap {
        public TrapType trapType;

        public Trap(TrapType _trapType) {
            trapType = _trapType;
        }

        // 種類
        public enum TrapType {
            None,
            Nomal,             
        }
    };

    [SerializeField] Card myCard;

    public Card card { get { return myCard; } }

    public virtual string Name { get { return null; } }
    public virtual Monster monster { get { return null; } }
    public virtual Magic magic { get { return null; } }
    public virtual Trap trap { get { return null; } }
    public virtual List<CardAction> EffectList { get { return null; } }

    public CardData() {}

    public void SetCard(Card _card) {
        myCard = _card;
    }
}
