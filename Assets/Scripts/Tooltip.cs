using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private ItemObject item; //������ ������Ʈ 
    private string descript; //���� ���� 
    private GameObject tooltip; //����

    void Start()
    {
        tooltip = GameObject.Find("Tooltip"); //�̸����� ã�Ƽ� ���ӿ�����Ʈ�� �ο�
        tooltip.SetActive(false); //�ϴ� ��Ƽ�� �����´�
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

    public void ConstructDescriptString() //���� ���ӿ�����Ʈ�� ������ �ִ� �Լ�
    {
        descript = item.description + "\n" + item.buffs.ToString();
        tooltip.transform.GetChild(0).GetComponent<Text>().text = descript;
    }
}
