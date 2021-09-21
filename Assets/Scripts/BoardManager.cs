using System.Collections;
using System.Collections.Generic;//用List所需要的头文件
using UnityEngine;//黄金矿工
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;//用unity的random函数来替代系统自带的函数，更好用

public class BoardManager : MonoBehaviour
{
    public GameObject[] objTiles;
    public Text targetText;
    public Text levelText;
    public Player player;

    public int totalScore = 0;
    public int needScore = 0;
    private int[]targetScores = new int[] { 2000, 3000, 4000, 6000, 8000, 11000, 14000, 19000, 24000 };
    public int targetScore;
    public int nowScore;
    public int bufferScore=2000;
    public Transform boardHolder;

    //private Transform boardHolder;

    public GameController gameController;
    



    private List<Vector3> gridPosition = new List<Vector3>();//用List来存储三维坐标，便于抽取使用
    public void Awake()
    {
        //targetScores = new int[] { 2000, 3000, 4000, 6000, 8000, 11000, 14000, 19000, 24000 };
    }
    public void Start()
    {
        
        gameController = GetComponent<GameController>();
        //boardHolder = new GameObject("Player").transform;
    }
    void InitialiseList()//生成List以便后续抽取坐标使用
    {
        gridPosition.Clear();//每次刷新地图版，需要清除之前剩余的没用的坐标
        for (int x = -6; x < 7; x++)//通过双循环把所有坐标输入List
        {
            for (int y = -4; y < 3; y++)
            {
                gridPosition.Add(new Vector3(x, y, 1.0f));
            }
        }
    }
    Vector3 RandomPosition()//从List中随机抽取一个坐标，并删除该坐标以免重复抽取
    {
        int randomIndex = Random.Range(0, gridPosition.Count);//闭区间0，开区间数量，>=0,<count
        Vector3 randomPosition = gridPosition[randomIndex];//开奖
        gridPosition.RemoveAt(randomIndex);//移除该地址，为了防止重复
        return randomPosition;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray,int ndScore)//生成地图物品元素，本质上类似于生成地板
    {
       
        int objScore=0;
        totalScore = 0;//目前场景中已经生成的物品总计分数
        while(totalScore<ndScore)//当总价值还未达到要求的分数时，继续随机生成
        {
            
            Vector3 randomPosition = RandomPosition();
            Quaternion tempQuat = RandomRotate();
            
            GameObject objChoice = tileArray[Random.Range(0, tileArray.Length)];
            GameObject instance=Instantiate(objChoice, randomPosition, tempQuat);
            instance.transform.SetParent(boardHolder);
            
            if (objChoice.tag == "Rock")
                objScore = 500;
            else if(objChoice.tag == "Enemy")
                objScore = 300;
            else if (objChoice.tag == "Spider")
                objScore = 150;
            else 
                objScore = 100;
            totalScore += objScore;
        }
    }
    public Quaternion RandomRotate()//随机旋转
    {
        float angle = Random.Range(0, 360);
        Quaternion tempQuat = Quaternion.AngleAxis(angle, Vector3.forward);
        return tempQuat;
    }
    public void SetupScene(int level)//唯一的public函数，因为它作为接口被GameManager调用
    {//主函数，一个个调用以上子函数

        //Debug.Log("1");
        if (level >= 1 && level <= 3)
            bufferScore = 2000;
        else if (level >= 4 && level <= 6)
            bufferScore = 3000;
        else if (level >= 7 && level <= 9)
            bufferScore = 4000;
        int reallevel = level - 1;
        targetScore = targetScores[reallevel];
        nowScore = player.score;
        if ((targetScore - nowScore) > 0)
            needScore = targetScore - nowScore + bufferScore;
        else
            needScore = bufferScore;
        InitialiseList();
        LayoutObjectAtRandom(objTiles,needScore);
        targetText.text = "" + targetScore;
        levelText.text = "" + level;
        player.flag = false;

        

    }
}
