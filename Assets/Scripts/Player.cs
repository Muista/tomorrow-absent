using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum State//三种状态
    {
        Rock,Stretch,Shorten//摇摆，拉伸，缩短
    }
public class Player : MonoBehaviour
{
    private State state;
    private Vector3 dir;
    public Transform rope;
    public Transform ropeCord;
    public Transform boardHolder;
    public ShopManager shopManager;
    public int playerNum=0;
    public int[] gameTimes;
    public int timeIndex;

    public float length;
    public int speed=5;
    public int backspeed ;
    public float time;
    public int score;
    public int target;
    public int boomNum=0;
    //public int level;
    public Text timeText;
    public Text scoreText;
    public Text targetText;
    public Text levelText;
    public Text shopMoneyText;
    public Text boomText;
    public bool Pause = false;
    public bool ifBuy = false;
    public bool ifDouble = false;
    public bool ifFast = false;
    public bool ifDizzy = false;

    public GameObject shock1;
    public GameObject shock2;
    public GameObject dizzy1;
    public GameObject dizzy2;
    public GameObject shop;
    public GameObject switchPanel;
    public GameObject ggPanel;
    public GameObject winPanel;
    public BoardManager boardManager;
    public bool flag ;//用于判断是否结算过本关
    public GameController gameController;
    public GameObject startPanel;
    public Image image2;
    public Text text2;



    public State GetState
    {
        set { state = value; }
        get { return state; }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameTimes = new int[] { 30, 40, 45, 50, 55, 65, 65, 70, 70 };
        backspeed = speed;//初始化返回速度默认为出发速度
        state = State.Rock;//默认为摇摆状态
        dir = Vector3.back;//顺时针
        //rope = transform.GetChild(0);
        //ropeCord = transform.GetChild(0);
        length = 1;
        //score = 300000;
        //time = 2;
        Pause = true;
        shock1.SetActive(false);
        shock2.SetActive(false);
        dizzy1.SetActive(false);
        dizzy2.SetActive(false);
        shop.SetActive(false);
        winPanel.SetActive(false);
        ggPanel.SetActive(false);
        
        //startPanel.SetActive(false);
        switchPanel.SetActive(false);
        ggPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Pause==false)
        {
            if (time <= 0 && flag == false)//时间用光游戏结束
            {
                Pause = true;
                flag = true;//已经结算过了
                time = 0;
                length = 1;
                state = State.Rock;

                if (ropeCord.childCount != 0)//如果结束时勾着东西
                {
                    
                    Destroy(ropeCord.GetChild(0).gameObject);
                }

                ropeCord.GetComponent<Collider2D>().enabled = true;
                backspeed = speed;
                if (shock1.activeInHierarchy == true)//震惊符号
                    shock1.SetActive(false);
                if (shock2.activeInHierarchy == true)
                    shock2.SetActive(false);
                rope.localScale = new Vector3(rope.localScale.x, length, rope.localScale.z);
                ropeCord.localScale = new Vector3(ropeCord.localScale.x, 1 / length, ropeCord.localScale.z);
                if (boardManager.targetScore <= score)//通关
                {
                    if(gameController.level==9)
                    {
                        winPanel.SetActive(true);
                        return;
                    }
                    switchPanel.SetActive(true);
                }
                    
                else//gg
                    ggPanel.SetActive(true);

                return;
            }
            if (state == State.Rock)
            {
                Rock();
                if (Input.GetKeyDown("space")&&ifDizzy==false)
                    state = State.Stretch;
            }
            else if (state == State.Stretch)
            {
                Stretch();
            }
            else
            {
                Shorten();
            }
            if (time > 0)
                time -= Time.deltaTime;
            timeText.text = "00:" + ((int)time).ToString();
            scoreText.text = (score).ToString();
            boomText.text = "炸药数：" + boomNum;
        }
        
    }
    void Rock()
    {
        if (rope.localRotation.z <= -0.6f)
            dir = Vector3.forward;//逆时针
        else if (rope.localRotation.z >= 0.6f)
            dir = Vector3.back;
        rope.Rotate(dir * 60*Time.deltaTime);
    }
    void Stretch()
    {
        if(length>=9.5f)
        {
            state = State.Shorten;
            return;
        }
        length += Time.deltaTime*speed;
        rope.localScale = new Vector3(rope.localScale.x, length, rope.localScale.z);
        ropeCord.localScale = new Vector3(ropeCord.localScale.x,1/length, ropeCord.localScale.z);
    }
    void Shorten()
    {
        if(length<=1)//钩子归位后
        {
            length = 1;
            state = State.Rock;
            
            if(ropeCord.childCount!=0)
            {
                if(ropeCord.GetChild(0).tag=="Enemy")
                {
                    dizzy1.SetActive(true);
                    dizzy2.SetActive(true);
                    ifDizzy = true;
                    StartCoroutine(StopDizzy());
                }
                score += GetScore(ropeCord.GetChild(0).tag);
                Destroy(ropeCord.GetChild(0).gameObject);
                
            }
            
            ropeCord.GetComponent<Collider2D>().enabled = true;
            backspeed = speed;
            if (shock1.activeInHierarchy == true)//震惊符号
                shock1.SetActive(false);
            if (shock2.activeInHierarchy == true)
                shock2.SetActive(false);
            return;
        }
        if (ropeCord.childCount != 0)//勾到了！
        {
            if(ropeCord.GetChild(0).gameObject.tag=="Enemy"|| ropeCord.GetChild(0).gameObject.tag == "Spider")
            {
                shock1.SetActive(true);
                shock2.SetActive(true);
            }
            backspeed = GetSpeed(ropeCord.GetChild(0).gameObject.tag);
            if(Input.GetKeyDown("up")&&boomNum>0)
            {
                score += GetScore(ropeCord.GetChild(0).tag);
                Destroy(ropeCord.GetChild(0).gameObject);
                backspeed = speed;
                boomNum--;
            }
        }
            
        length -= Time.deltaTime*backspeed;
        rope.localScale = new Vector3(rope.localScale.x, length, rope.localScale.z);
        ropeCord.localScale = new Vector3(ropeCord.localScale.x, 1 / length, ropeCord.localScale.z);

    }
    private int GetScore(string tag)//得分
    {
        int num = 0;
        if(ifDouble==true)
        {
            switch (tag)
            {
                case "Rock":
                    num = 1000;
                    break;
                case "Enemy":
                    num = 300;
                    break;
                case "Spider":
                    num = 150;
                    break;
                case "Bug":
                    num = 100;
                    break;

            }
        }
        else
        {
            switch (tag)
            {
                case "Rock":
                    num = 500;
                    break;
                case "Enemy":
                    num = 300;
                    break;
                case "Spider":
                    num = 150;
                    break;
                case "Bug":
                    num = 100;
                    break;

            }
        }
        
       
            return num;
    }
    private int GetSpeed(string tag)//得速度
    {
        int num = 1;
        if(ifFast==true)
        {
            switch (tag)
            {
                case "Rock":
                    num = 6;
                    break;
                case "Enemy":
                    num = 2;
                    break;
                case "Spider":
                    num = 3;
                    break;
                case "Bug":
                    num =6;
                    break;

            }
        }
        else
        {
            switch (tag)
            {
                case "Rock":
                    num = 5;
                    break;
                case "Enemy":
                    num = 1;
                    break;
                case "Spider":
                    num = 2;
                    break;
                case "Bug":
                    num = 5;
                    break;

            }
        }
        
        return num;
    }
    //private void Restart()//场景重载
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    //}
    public void Shop()
    {
        for(int i=0;i<boardHolder.childCount;i++)
        {

            Destroy(boardHolder.GetChild(i).gameObject);//注意，destroy函数在这里是在一帧内先把所有要删的obj打上标记，然后在下一帧删除，故即使用循环，也是不停的给第一个打标记。所以一定要用for循环不能while循环，否则会死循环
        }
        ifDouble = false;
        speed = 5;
        ifFast = false;
        switchPanel.SetActive(false);
        shop.SetActive(true);
        shopMoneyText.text = "金钱：" + score;
        shopManager.ShopInit();
        
        
    }
    public void NextLevel()
    {
        if(ifBuy==false)
        {
            image2.color = new Color(255, 255, 255, 255);
            text2.color = new Color(0, 0, 0, 255);
            ifBuy = true;
            return;
        }
        ifBuy = false;
        image2.color = new Color(255, 255, 255, 0);
        text2.color = new Color(255, 255, 255, 0);
        shop.SetActive(false);
        Pause = false;
        gameController.level++;
        timeIndex = gameController.level - 1;
        time =30;
        gameController.InitGame();
        
    }
    public void GO()
    {
        Pause = true;
        startPanel.SetActive(true);
        ggPanel.SetActive(false);
        for (int i = 0; i < boardHolder.childCount; i++)
        {

            Destroy(boardHolder.GetChild(i).gameObject);//注意，destroy函数在这里是在一帧内先把所有要删的obj打上标记，然后在下一帧删除，故即使用循环，也是不停的给第一个打标记。所以一定要用for循环不能while循环，否则会死循环
        }
    }
    public void StartGame()
    {
        
        Pause = false;
        time = 30;
        score =0;
        gameController.level = 1;
        gameController.InitGame();
        startPanel.SetActive(false);
        if (playerNum == 0)
            gameObject.transform.position = new Vector3(0,0,1);
        else
            gameObject.transform.position = new Vector3(1.24f, 0, 1);
    }
    public void Win()
    {
        Pause = true;
        winPanel.SetActive(false);
        startPanel.SetActive(true);
        for (int i = 0; i < boardHolder.childCount; i++)
        {

            Destroy(boardHolder.GetChild(i).gameObject);//注意，destroy函数在这里是在一帧内先把所有要删的obj打上标记，然后在下一帧删除，故即使用循环，也是不停的给第一个打标记。所以一定要用for循环不能while循环，否则会死循环
        }
    }
    public void Buy()
    {
        ifBuy = true;
        int i = shopManager.choice;
        shopManager.DestroyProduct();
        if(i==1)
        {
            score = score - 300;
            shopMoneyText.text = "金钱：" + score;
            boomNum++;
        }
        else if(i==2)
        {
            score = score - 500;
            shopMoneyText.text = "金钱：" + score;
            speed = 6;
            ifFast = true;
        }
        else if (i == 3)
        {
            score = score - 1000;
            shopMoneyText.text = "金钱：" + score;
            ifDouble = true;
        }
    }
    public void ExitLevel()
    {
        length = 1;
        state = State.Rock;

        if (ropeCord.childCount != 0)
        {
            Destroy(ropeCord.GetChild(0).gameObject);
        }
        Pause = true;
        startPanel.SetActive(true);
        ggPanel.SetActive(false);
        rope.localScale = new Vector3(rope.localScale.x, length, rope.localScale.z);
        ropeCord.localScale = new Vector3(ropeCord.localScale.x, 1 / length, ropeCord.localScale.z);
        for (int i = 0; i < boardHolder.childCount; i++)
        {

            Destroy(boardHolder.GetChild(i).gameObject);//注意，destroy函数在这里是在一帧内先把所有要删的obj打上标记，然后在下一帧删除，故即使用循环，也是不停的给第一个打标记。所以一定要用for循环不能while循环，否则会死循环
        }
    }
    IEnumerator StopDizzy()
    {
        yield return new WaitForSeconds(3);
        ifDizzy = false;
        dizzy1.SetActive(false);
        dizzy2.SetActive(false);
    }
}
