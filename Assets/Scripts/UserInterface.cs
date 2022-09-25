using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    public Player player;
    //public Tooltip tooltip;

    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> itemDisplay = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        for(int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }
        CreateSlots();

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }


    void Update()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemDisplay)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        player.mouseItem.hoverobj = obj;
        if (itemDisplay.ContainsKey(obj))
            player.mouseItem.hoverItem = itemDisplay[obj];
        //tooltip.Activate(player.mouseItem.hoverItem.parent.itemDisplay[player.mouseItem.hoverobj]);
    }

    public void OnExit(GameObject obj)
    {
        player.mouseItem.hoverobj = null;
        player.mouseItem.hoverItem = null;
    }

    public void OnEnterInterface(GameObject obj)
    {
        player.mouseItem.ui = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        player.mouseItem.ui = null;
    }

    public void OnStartDrag(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);

        if (itemDisplay[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemDisplay[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }

        player.mouseItem.obj = mouseObject;
        player.mouseItem.item = itemDisplay[obj];
    }

    public void OnEndDrag(GameObject obj)
    {
        var itemOnMouse = player.mouseItem;
        var mouseHoverItem = itemOnMouse.hoverItem;
        var mouseHoverObj = itemOnMouse.hoverobj;
        var GetItemObject = inventory.database.GetItem;

        if (mouseHoverObj)
        {
            if(mouseHoverItem.CanPlaceInSlot(GetItemObject[itemDisplay[obj].ID]))
            inventory.MoveItem(itemDisplay[obj], mouseHoverItem.parent.itemDisplay[itemOnMouse.hoverobj]);
        }   
        else
        {
            //inventory.RemoveItem(itemDisplay[obj].item);
        }
        Destroy(itemOnMouse.obj);
        itemOnMouse.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        if (player.mouseItem.obj != null)
            player.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }

}

public class MouseItem
{
    public UserInterface ui;
    public GameObject obj; 
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverobj;
}
