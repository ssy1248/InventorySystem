using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    private void Awake()
    {
        //��ũ���ͺ� ������Ʈ�� ���̵� �����Ͽ� ���̵� �����ؼ� ������ Ÿ���� �ش�.
        if(id == 6)
        {
            type = ItemType.Boots;
        }
        if(id == 8)
        {
            type = ItemType.Helmet;
        }
        if(id == 2)
        {
            type = ItemType.Shield;
        }
        if(id == 3 || id == 4 || id == 5)
        {
            type = ItemType.Weapon;
        }
        if(id == 7)
        {
            type = ItemType.Chest;
        }
    }
}
