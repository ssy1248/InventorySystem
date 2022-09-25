using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food, //회복 아이템
    Helmet, //플레이어 착용 장비
    Weapon, //무기
    Shield, //방패
    Boots, //신발
    Chest, //가슴방어구
    Default //그외 물건
}

public enum Attributes //능력치
{
    Agility,
    Intellect,
    Stamina,
    Strength
}
public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite uiDisplay; //장비 프리팹
    public ItemType type; //장비 열거형
    [TextArea(15, 20)]
    public string description; //설명
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item() //아이템 기본 생성자
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item) //아이템 오브젝트에 의거한 생성자
    {
        Name = item.name;
        Id = item.id;
        buffs = new ItemBuff[item.buffs.Length];
        for(int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
            {
                attribute = item.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff //아이템 능력치
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max) //아이템 버프 주기
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue() //랜덤 생성
    {
        value = Random.Range(min, max);
    }
}