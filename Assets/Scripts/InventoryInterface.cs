using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryInterface : UserInterface
{
    public GameObject inventoryPrefab;

    public int X_START; //�κ��丮 x���� ��ǥ
    public int Y_START; //�κ��丮 y���� ��ǥ

    public int X_SPACE_BETWEEN_ITEM; // ������ ���� x ����
    public int NUMBER_OF_COLUMN; // �� ���� �ִ� ����
    public int Y_SPACE_BETWEEN_ITEMS; // ������ ���� y ����

    public override void CreateSlots()
    {
        itemDisplay = new Dictionary<GameObject, InventorySlot>();
        //tooltip = inventoryPrefab.GetComponent<Tooltip>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); }); //���콺�� ���� �ȿ� �� ����� �̺�Ʈ
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); }); //���콺�� ���� ���� ���� ����� �̺�Ʈ
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); }); //�巡�׸� ������ ����� �̺�Ʈ
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); }); //�巡�װ� ���� ����� �̺�Ʈ
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); }); //�巡�װ� ����ǰ� �ִ� ����� �̺�Ʈ

            itemDisplay.Add(obj, inventory.Container.Items[i]);
        }
    }

    private Vector3 GetPosition(int i) // ������ �����յ��� ��ġ�� ���ϴ� �Լ�
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
