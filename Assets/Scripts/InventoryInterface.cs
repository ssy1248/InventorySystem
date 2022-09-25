using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryInterface : UserInterface
{
    public GameObject inventoryPrefab;

    public int X_START; //인벤토리 x시작 좌표
    public int Y_START; //인벤토리 y시작 좌표

    public int X_SPACE_BETWEEN_ITEM; // 프리팹 간의 x 간격
    public int NUMBER_OF_COLUMN; // 한 열의 최대 갯수
    public int Y_SPACE_BETWEEN_ITEMS; // 프리팹 간의 y 간격

    public override void CreateSlots()
    {
        itemDisplay = new Dictionary<GameObject, InventorySlot>();
        //tooltip = inventoryPrefab.GetComponent<Tooltip>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); }); //마우스가 영역 안에 들어갈 경우의 이벤트
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); }); //마우스가 영역 밖을 나갈 경우의 이벤트
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); }); //드래그를 시작한 경우의 이벤트
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); }); //드래그가 끝날 경우의 이벤트
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); }); //드래그가 진행되고 있는 경우의 이벤트

            itemDisplay.Add(obj, inventory.Container.Items[i]);
        }
    }

    private Vector3 GetPosition(int i) // 생성될 프리팹들의 위치를 구하는 함수
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
