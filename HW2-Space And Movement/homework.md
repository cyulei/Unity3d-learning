# 空间与运动

---

1.简答并用程序验证
     
 - 游戏对象运动的本质是什么？
 
 游戏对象运动的本质是游戏对象跟随每一帧的变化，Transform组件的属性值的变化。

 - 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）
    - 第一种方法是修改Transform的position属性但不创建新的Vector3来实现抛物线运动，水平方向匀速移动，竖直方向有一定的加速度，从而实现抛物线运动，代码如下：
    
<pre><code>
public class parabola1 : MonoBehaviour
{
    public float verticalspeed = 1;
    // Use this for initialization  
    void Start()
    {
    }
    // Update is called once per frame  
    void Update()
    {
        this.transform.position += Vector3.down * Time.deltaTime * verticalspeed;
        this.transform.position += Vector3.right * Time.deltaTime * 10;
        verticalspeed++;
    }
}
</code></pre>


- 第二种方法是创建一个Vector3变量并且加到Transform的position上，代码如下：
<pre><code>
public class parabola2 : MonoBehaviour {
    public float verticalspeed = 1;
    // Use this for initialization  
    void Start()
    {
    }
    // Update is called once per frame  
    void Update()
    {
        Vector3 vec = new Vector3(Time.deltaTime * 10, -Time.deltaTime * verticalspeed, 0);
        this.transform.position += vec;
        verticalspeed++;
    }
}

</code></pre>

- 第二种方法是利用translate函数来改变position,代码如下：
<pre><code>
public class parabola3 : MonoBehaviour {  
    public float verticalspeed = 1;  
    // Use this for initialization  
    void Start () 
    {  
    }  
    // Update is called once per frame  
    void Update () 
    {  
        Vector3 vec = new Vector3(Time.deltaTime * 10, -Time.deltaTime * verticalspeed, 0);  
        transform.Translate(vec);  
        verticalspeed++;  
    }  
}  
</code></pre>
