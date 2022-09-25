using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory; //인벤토리 스크립터블 오브젝트 접근
    //public InventoryObject equipment; //장비 스크립터블 오브젝트 접근

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if(item) //item이라면 아이템 획득 및 삭제
        {
            Item _item = new Item(item.item);
            Debug.Log(_item.Id);
            inventory.AddItem(_item, 1);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            //equipment.Save();
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            //equipment.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        //equipment.Clear();
    }
}
