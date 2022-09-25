using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private ItemObject item; //아이템 오브젝트 
    private string descript; //툴팁 설명 
    private GameObject tooltip; //툴팁

    void Start()
    {
        tooltip = GameObject.Find("Tooltip"); //이름으로 찾아서 게임오브젝트에 부여
        tooltip.SetActive(false); //일단 액티브 꺼놓는다
    }


    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(ItemObject item)
    {
        this.item = item;
        ConstructDescriptString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDescriptString() //툴팁 게임오브젝트에 설명을 넣는 함수
    {
        descript = item.description + "\n" + item.buffs.ToString();
        tooltip.transform.GetChild(0).GetComponent<Text>().text = descript;
    }
}
