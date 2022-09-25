using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    private void Awake()
    {
        //스크립터블 오브젝트의 아이디에 접근하여 아이디에 관련해서 아이템 타입을 준다.
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
