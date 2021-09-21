using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform startTrans;    //起始点
    LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;//修改线条宽度
    }
    void Update()
    {
        UpdataLine();
    }
    public void UpdataLine()
    {
        lineRenderer.SetPosition(0, startTrans.position);
        lineRenderer.SetPosition(1, transform.position);//设置线条2点的位置
    }
}
