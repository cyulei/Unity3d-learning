# Unity3d-基础概念

---

**1、简答题**

 - 解释游戏对象（GameObjects） 和 资源（Assets）的区别与联系。
 答：游戏对象是一种容器。它们是空盒,能够容纳组件。根据要创建的对象类型，可以添加不同的组件组合到游戏对象中。
 资源可以添加到游戏对象上，例如材质和动画运用到游戏对象上，有些资源作为模板，可实例化成游戏中具体的对象。

 - 下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）
答：每个Unity的项目包含一个资源文件夹。此文件夹的内容呈现在项目视图。这里存放着游戏的所有资源，在资源文件夹中，通常有对象、材质、场景、声音、预设、贴图、脚本、动作，在这些文件夹下可以继续进行划分。
游戏对象树层次视图包含了每一个当前场景的所有游戏对象。其中一些是资源文件的实例，如3D模型和其他预制物体的实例。可以在层次结构视图中选择对象或者生成对象。当在场景中增加或者删除对象，层次结构视图中相应的对象则会出现或消失。想让一个游戏对象成为另一个的子对象，只需在层次视图中把它拖到另一个上即可。一个子对象将继承其父对象的移动和旋转属性。

 - 编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件
   - 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
   - 常用事件包括 OnGUI() OnDisable() OnEnable()

<pre><code>
public class NewBehaviourScript : MonoBehaviour {
    void Awake() {
        Debug.Log ("Awake");
    }
    void Start () {
        Debug.Log ("Start");
    }
    void Update () {
        Debug.Log ("Update");
    }
    void FixedUpdate() {
        Debug.Log ("FixedUpdate");
    }
    void LateUpdate() {
        Debug.Log ("LateUpdate");
    }
    void OnGUI() {
        Debug.Log ("OnGUI");
    }
    void OnDisable() {
        Debug.Log ("OnDisable");
    }
    void OnEnable() {
        Debug.Log ("OnEnable");
    }
}
</code></pre>

 - 查找脚本手册，了解 GameObject，Transform，Component 对象
   - 分别翻译官方对三个对象的描述（Description）
 答：GameObject是代表人物、道具和风景的基本对象在Unity中。它们本身并没有实现多少功能，但是它们充当Component的容器，这些Component实现了真正的功能。
Transform决定场景中每个对象的位置、旋转和缩放比例。每个GameObject都有一个Transform。
一个GameObject包含一个Component,可以通过 Inspector 来查看游戏对象的组件

   - 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件
      - 本题目要求是把可视化图形编程界面与 Unity API 对应起来，当你在 Inspector 面板上每一个内容，应该知道对应 API。
      - 例如：table 的对象是 GameObject，第一个选择框是 activeSelf 属性。
答：table 的对象是 GameObject，第一个选择框是 activeSelf,table是name属性 属性,旁边的Static的选择框是isStatic属性，Tag对应tag属性，Layer对应layer属性。
Transform属性有position属性对应图中的Position，rotation属性对应Rotation,要修改Scale使用localScale属性。
table 的部件有Box Collider，Mesh Renderer以及Default-Material。

 ![picture](https://pmlpml.github.io/unity3d-learning/images/ch02/ch02-homework.png)
 
   - 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）
 ![picture](https://wx4.sinaimg.cn/mw690/c184249cly1fpql2e55jrj20gk06274c.jpg)
   
 - 整理相关学习资料，编写简单代码验证以下技术的实现：
   - 查找对象
     - 通过名字查找：public static GameObject Find(string name)
     - 通过标签查找单个对象：public static GameObject FindWithTag(string tag)
     - 通过标签查找多个对象：public static GameObject[] FindGameObjectsWithTag(string tag)
   - 添加子对象：public static GameObect CreatePrimitive(PrimitiveTypetype)
   - 遍历对象树：foreach (Transform child in transform) {};
   - 清除所有子对象：foreach (Transform child in transform) { Destroy(child.gameObject);}
   
 - 资源预设（Prefabs）与 对象克隆 (clone)
   - 预设（Prefabs）有什么好处？
   答：预设体完整储存了对象的组件和属性，相当于模板，方便重复使用。
   - 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
   答：预设与对象克隆不同的是，预设与实例化的对象有关联，比如预设有组件变化，那么实例化对也会有组件变化，而对象克隆本体和克隆出的对象是不相影响的。
   - 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象
   答：将table预制体放到 Resource 文件夹下
 使用GameObject.Instantiate(Resource.Load("table"))；或者
GameObject instance = Instantiate(Resources.Load<GameObject>("table"));

 - 尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法
   - 向子对象发送消息
   答：组合模式将对象组合成树形结构来表现”部分-整体”的层次结构，可以用一致的方式处理单个对象以及对象的组合。组合模式实现的关键地方是——简单对象和复合对象必须实现相同的接口，所以能够将组合对象和简单对象进行一致处理。

父对象：
<pre><code>
public class NewBehaviourScript : MonoBehaviour {
    void Start () {
        this.BroadcastMessage("test");
    }
}
</code></pre>
子对象：
<pre><code>
public class NewBehaviourScript1 : MonoBehaviour {
    void test() {
        Debug.Log("Child!");
    }
}
</code></pre>