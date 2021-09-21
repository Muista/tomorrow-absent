using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BoardManager boardScripts;
    public static GameController instance = null;

   

    public int level;
    public void Awake()
    {
        //Screen.SetResolution(1280, 921, false);
        if (instance == null)//保证不会存在两个gameManager
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);//切换scene时不摧毁，一旦摧毁了，就无法保存分数了
        boardScripts = GetComponent<BoardManager>();//因为这两个脚本挂在一个对象上，故可以直接通过该命令获取面板生成脚本
        
        level = 1;
        //InitGame();
    }
    //private void OnLevelWasLoaded(int index)//unity自带，每次场景加载Restar时自动调用
    //{
    //    level++;
    //    InitGame();
    //}
    public void InitGame()
    {
        
        boardScripts.SetupScene(level);//命令面板脚本根据层数生成地图   
    }
}
