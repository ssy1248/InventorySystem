using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _camera;
    private void LateUpdate()
    {
        transform.forward = _camera.transform.forward; //생성되는 아이템들을 카메라 바라보는 방향으로 설정
    }
}
