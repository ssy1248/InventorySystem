using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath; // 저장 위치
    public ItemDatabaseObject database; // 아이템 정보 가져올 곳
    public Inventory Container; // 인벤토리가 생성된 부모를 가질 객체

    public void AddItem(Item _item, int _amount) //아이템이 비어있는지 아닌지 체크 후 넣는 함수
    {
        if (_item.buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;
        }

        for (int i = 0; i < Container.Items.Length; i++) // container안에 들어간 만큼 반복을하면서 같은 아이템이 있다면 amount증가
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount) //인벤토리가 비어있는지 체크하는 함수
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2) //아이템 이동과 스왑에 관련된 함수
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(Item _item) //아이템 드래그 다운을 해서 바닥에 버릴 경우 버려짐
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save() //유저 함수 저장
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load() //유저 함수 로드
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for(int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear() //유저 함수 클리어
    {
        Container.Clear();
    }
}

[System.Serializable]
public class Inventory //인벤토리 초기화 클래스
{
    public InventorySlot[] Items = new InventorySlot[28];

    public void Clear()
    {
        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(-1, new Item(), 0);
        }
    }
}

[System.Serializable]
public class InventorySlot //인벤토리 슬롯에 관련한 클래스
{
    public ItemType[] AllowedItems = new ItemType[0];
    public UserInterface parent;
    public int ID = -1;
    public Item item; //아이템 정보를 가져오기 위하여
    public int amount; // 갯수
    public InventorySlot() //기본 생성자
    {
        ID = -1;
        item = null;
        amount = 0;
    }

    public InventorySlot(int _id, Item _item, int _amount) 
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void UpdateSlot(int _id, Item _item, int _amount) //인벤토리 슬롯 생성자
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value) //갯수 증가
    {
        amount += value;
    }

    public bool CanPlaceInSlot(ItemObject _item) //장비창에 넣을 수 있는지 체크하는 함수
    {
        if (AllowedItems.Length <= 0)
            return true;

        for(int i = 0; i < AllowedItems.Length; i++)
        {
            if (_item.type == AllowedItems[i])
                return true;
        }
        return false;
    }
}