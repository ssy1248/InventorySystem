using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory; //�κ��丮 ��ũ���ͺ� ������Ʈ ����
    //public InventoryObject equipment; //��� ��ũ���ͺ� ������Ʈ ����

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if(item) //item�̶�� ������ ȹ�� �� ����
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
