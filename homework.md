# 游戏对象与图形基础
---
1、操作与总结
 
- **参考 Fantasy Skybox FREE 构建自己的游戏场景**
  - 在Asset Store下载TGU Skybox Pack（Fantasy Skybox FREE没有找到），这个包里面有四个已经制作好的天空盒和对应的场景。如下图所示：

 ![photo1][1]
 
  - 在Nostalgia1场景中放置Terrain，并且使用Height Tools使地面高低起伏，为地形添加纹理图案，然后为地形添加树和细节（草），如图下所示：

![photo2][2]

![photo3][3]



---
- **写一个简单的总结，总结游戏对象的使用**

- 游戏对象是什么：
游戏对象是所有其他组件的容器。
- 怎样使用游戏对象
  - 组件 
  
游戏对象可以容纳很多组件，比如Transform组件，我们可以改变Transform的各个参数的值来改变游戏对象的位置。我们改变游戏对象的状态，其实就是对游戏对象身上的组件进行更改。获得一个游戏对象的一个组件我们可以使用GetComponent方法，然后对组件的值进行修改。用AddComponent方法，在游戏对象上添加一个组件。
 - 属性
 
游戏对象也有自己的Tag,Layer,Name等，让我们在场景中寻找到它。比如可以使用Find方法去找到叫做Name的游戏对象
 - 静态方法
 
 我们也可以实例化游戏对象用Instantiate方法让它出现在游戏中，也可以使用Destroy方法让游戏对象销毁消失在游戏场景中。


  [1]: https://wx1.sinaimg.cn/mw690/c184249cly1fq4y0ly2owj20tn0inqc5.jpg
  [2]: https://wx4.sinaimg.cn/mw690/c184249cly1fq4y0m5xq0j20r20fctun.jpg
  [3]: https://wx2.sinaimg.cn/mw690/c184249cly1fq4y0m79e6j20ts0f3b04.jpg
