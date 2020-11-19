# Log
## 11/18
immersion做的是，用uniform的weights，然后用validationg set 上 activation的值当做charge.

我觉得我可以有两种approach
1. 和他一样，就 validation 上的 activation。
2. weights我们用真实的weights，当然可能要取个平均值或者，standard deviation 啥的。

* 但都可以加的是，用颜色代表weights的强度 （那就意味着我们势必需要想办法去计算一下weights了）

下面是一个典型的W b的数据，长这样
```
{'W1': array([[ 0.02991222,  0.23027405,  0.02223037, -0.0489001 ],
       [-0.10385251,  0.01670007,  0.06376674,  0.08876977],
       [ 0.09534236, -0.05980569, -0.01975641,  0.00634322],
       ...,
       [ 0.00747293,  0.07096836,  0.07692409, -0.03340201],
       [ 0.2409679 , -0.27487527, -0.10204242,  0.22496413],
       [ 0.18790773,  0.12227524,  0.02368217, -0.06131757]]), 'W2': array([[-0.09737694,  0.01529091, -0.01999152, -0.19353598,  0.05958552,
        -0.00700677, -0.0402821 , -0.06426189],
       [-0.11966758,  0.138093  ,  0.24409011, -0.22689445,  0.12693956,
        -0.00075496, -0.05442496, -0.05466859],
       [ 0.20005676, -0.01960047,  0.00947262, -0.01000532,  0.13004015,
        -0.07429122,  0.08031108,  0.07916078],
       [-0.00206434,  0.11612551,  0.07750933, -0.2545503 , -0.03003584,
         0.14839018,  0.02456559,  0.00419167]]), 'W3': array([[-0.00556847,  0.00774664, -0.00917872, -0.01363326,  0.00188678,
        -0.00561909,  0.00119905],
       [ 0.00475954,  0.004395  , -0.00887902, -0.00464057, -0.02408308,
        -0.00361246,  0.01683175],
       [-0.00156803,  0.00170786, -0.00693402,  0.01481984, -0.00386996,
         0.00420602, -0.00072568],
       [ 0.00706443, -0.00336153, -0.02113919, -0.00534157, -0.0008336 ,
        -0.00721442,  0.01990753],
       [ 0.00723184,  0.00672783,  0.00614536,  0.00528026,  0.0018166 ,
        -0.00683265,  0.01100522],
       [ 0.00146868,  0.01065809, -0.00996594,  0.00646702, -0.016983  ,
        -0.00992602,  0.00199408],
       [ 0.00451215, -0.00535034,  0.01224677, -0.00431076,  0.00080349,
        -0.01170311, -0.00807161],
       [ 0.00316659,  0.01955397, -0.00341098,  0.00309523,  0.01679207,
         0.00748337, -0.0002614 ]]), 'b1': array([5.72896833e-08, 6.14257062e-05, 0.00000000e+00, 1.63776344e-07]), 'b2': array([ 0.00000000e+00,  5.48782049e-04, -1.17276997e-04,  0.00000000e+00,
        1.19554543e-04,  2.13177182e-06, -1.62851808e-06, -1.02879831e-06]), 'b3': array([-0.00897107, -0.00754504, -0.00823201,  0.00799539, -0.00755673,
       -0.00739353,  0.03170299]), 'dW1': array([[ 1.32899858e-08,  2.88731846e-06,  0.00000000e+00,
         0.00000000e+00],
       [ 2.04978099e-08,  3.18786695e-06,  0.00000000e+00,
        -0.00000000e+00],
       [ 2.34070693e-08,  3.62264437e-06,  0.00000000e+00,
         0.00000000e+00],
       ...,
       [ 2.25459875e-08,  2.14601124e-06,  0.00000000e+00,
         0.00000000e+00],
       [ 2.22926618e-08,  2.41008860e-06,  0.00000000e+00,
         0.00000000e+00],
       [ 2.14060219e-08,  2.92926073e-06,  0.00000000e+00,
         0.00000000e+00]]), 'dW2': array([[ 0.00000000e+00,  5.13963026e-07, -1.84560136e-07,
         0.00000000e+00,  2.08078453e-07,  0.00000000e+00,
        -0.00000000e+00,  0.00000000e+00],
       [ 0.00000000e+00,  4.84752255e-05,  3.18752399e-05,
         0.00000000e+00,  1.57316542e-06, -0.00000000e+00,
         0.00000000e+00,  0.00000000e+00],
       [ 0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00],
       [ 0.00000000e+00, -0.00000000e+00, -0.00000000e+00,
         0.00000000e+00,  0.00000000e+00, -0.00000000e+00,
        -0.00000000e+00, -0.00000000e+00]]), 'dW3': array([[ 0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00],
       [-1.08129280e-04, -3.31227282e-05, -1.92141674e-04,
         1.71734300e-04, -1.33600676e-04,  7.66353265e-05,
         2.18624732e-04],
       [-1.93833498e-04, -5.77318919e-05, -3.38364796e-04,
         3.03549258e-04, -2.35077851e-04,  1.35791539e-04,
         3.85667240e-04],
       [ 0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00],
       [-9.64182676e-05, -3.08229057e-05, -1.76871640e-04,
         1.57233100e-04, -1.23112543e-04,  6.99256704e-05,
         2.00066586e-04],
       [-0.00000000e+00, -0.00000000e+00,  0.00000000e+00,
        -0.00000000e+00, -0.00000000e+00, -0.00000000e+00,
         0.00000000e+00],
       [-0.00000000e+00, -0.00000000e+00, -0.00000000e+00,
        -0.00000000e+00, -0.00000000e+00, -0.00000000e+00,
         0.00000000e+00],
       [-0.00000000e+00, -0.00000000e+00, -0.00000000e+00,
        -0.00000000e+00, -0.00000000e+00, -0.00000000e+00,
         0.00000000e+00]]), 'db1': array([6.45382622e-08, 9.38459932e-06, 0.00000000e+00, 0.00000000e+00]), 'db2': array([ 0.00000000e+00,  3.72571879e-05,  1.47656355e-05,  0.00000000e+00,
        5.02429207e-06,  0.00000000e+00, -0.00000000e+00, -0.00000000e+00]), 'db3': array([-0.00031771, -0.00032115, -0.00081217,  0.00055417, -0.00060852,
        0.00018448,  0.0013209 ]), 'dE_dW1': array([[-1.32899858e-06, -2.88731846e-04,  0.00000000e+00,
         0.00000000e+00],
       [-2.04978099e-06, -3.18786695e-04,  0.00000000e+00,
         0.00000000e+00],
       [-2.34070693e-06, -3.62264437e-04,  0.00000000e+00,
         0.00000000e+00],
       ...,
       [-2.25459875e-06, -2.14601124e-04,  0.00000000e+00,
         0.00000000e+00],
       [-2.22926618e-06, -2.41008860e-04,  0.00000000e+00,
         0.00000000e+00],
       [-2.14060219e-06, -2.92926073e-04,  0.00000000e+00,
         0.00000000e+00]]), 'dE_dW2': array([[ 0.00000000e+00, -5.13963026e-05,  1.84560136e-05,
         0.00000000e+00, -2.08078453e-05,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00],
       [ 0.00000000e+00, -4.84752255e-03, -3.18752399e-03,
         0.00000000e+00, -1.57316542e-04,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00],
       [ 0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00],
       [ 0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00,  0.00000000e+00,
         0.00000000e+00,  0.00000000e+00]]), 'dE_dW3': array([[ 0.        ,  0.        ,  0.        ,  0.        ,  0.        ,
         0.        ,  0.        ],
       [ 0.01081293,  0.00331227,  0.01921417, -0.01717343,  0.01336007,
        -0.00766353, -0.02186247],
       [ 0.01938335,  0.00577319,  0.03383648, -0.03035493,  0.02350779,
        -0.01357915, -0.03856672],
       [ 0.        ,  0.        ,  0.        ,  0.        ,  0.        ,
         0.        ,  0.        ],
       [ 0.00964183,  0.00308229,  0.01768716, -0.01572331,  0.01231125,
        -0.00699257, -0.02000666],
       [ 0.        ,  0.        ,  0.        ,  0.        ,  0.        ,
         0.        ,  0.        ],
       [ 0.        ,  0.        ,  0.        ,  0.        ,  0.        ,
         0.        ,  0.        ],
       [ 0.        ,  0.        ,  0.        ,  0.        ,  0.        ,
         0.        ,  0.        ]]), 'dE_db1': array([-6.45382622e-06, -9.38459932e-04,  0.00000000e+00,  0.00000000e+00]), 'dE_db2': array([ 0.        , -0.00372572, -0.00147656,  0.        , -0.00050243,
        0.        ,  0.        ,  0.        ]), 'dE_db3': array([ 0.03177115,  0.03211517,  0.08121687, -0.05541695,  0.06085182,
       -0.01844797, -0.13209009])}
Epoch   0 Step 24 Train CE 1.93876 Train Acc 0.23000
here is the model
```

1. 问MIT和Stanford推荐信能不能迟交(我觉得这个可能要打电话去 然后MIT的那个顺带问一下他们到底招收怎样的学生 明天就Email过去)
2. 如果可以的话，MIT也申请上吧...算不后悔。（地里看一下有没有攻略)

我真的觉得我就是个废物，活着有什么意义真的，就是好高骛远，心存幻想，也不撒泡尿自己照照。唯一问题就是，这样一走了之不知道爸妈，外婆会伤心多久，当初小姨的事情外婆都那样...哎。

## 11/17
其实还有一个思考，其实如果后面还要PhD的话，这样并不节省时间，反而是多出一年，本科直博应该是最节省时间的。(其实真的是，如果要探索科研是否适合自己， 是应该用本科时间去探索的。其实这样的话，后面申请tovi的PhD也可啊...) 但今年就只思考Master的事情了，申请不到的话，明年申就考虑PhD了，嗯嗯，今年绝对不会放弃斯坦福的MST的，必须。

周四给Tovi看前需要有的
1. 需要Python连接上，Fa是根据w, Fr是根据activation(这个需要去他paper里看一下). link是透明还是白色，根据weights, 注意所有的变化都要lerp一下，尤其是白线亮度变化

其实1完成就可以看一下了，会不会图像很混沌？很难看？如果那样3就不用做了。

2. 左右耳朵听sonification, 就，其实可以是在一定距离内线性的，就，只要在一个小球的距离之内了，就约近声音越响。
3. 移动小球，反向计算weights，输送给Python，然后听一下变化

Bonus:
* 看一下能够怎样精简一下计算，能够做到至少[5,10]这样的size吧...
* 甚至可以手动添加小球，但这个会比较难操作..(可以paper里面瞎鸡儿说一下？截个图就好，code不一定需要有)

## 11/16
找到MKGlow在VR中不能用的原因了，是我们需要在OVRCameraRig下面的CenterEyeAnchor加上MLGlow的script，这个script原先是加在MainCamera上的，现在这个变成了maincamera，所以需要加上才可。
下一步需要去连接真实的training数据然后update weights了

找到MKGlow在VR中不能用的原因了，是我们需要在OVRCameraRig下面的CenterEyeAnchor加上MLGlow的script，这个script原先是加在MainCamera上的，现在这个变成了maincamera，所以需要加上才可。

* 其实想到一个事情，其实可以看一下那个斯坦福去耶鲁的小朋友是去了什么program，没准我可以搞上那个program？还不用钱，舒适了？


## 11/15
直接从mac上clone到windows，
1. 会发现里面所有的script都是断的，需要手动连上去。（包括Node和Link的prefab和Controller object，在MainScene里面）
2. 需要import Chunity，在asset store
3. 需要MK glow，并且是完整的这个，我自己project直接clone下来的不行，会缺少东西。 https://github.com/epyon1/Unity_C-_basic/tree/45ebe9e38c685cba20df3eeb40695e9a51856b4f/BreakOut/Assets/_MK
4. 然后发现所有mk的material的shader也是断的，所以需要找到 Diffuse (Normal/Diffuse) 加上去
5. 如果发现还是不对，都是白色或者都是粉色，就确保一下3做了，然后重启unity就ok了。

要连接Oculus的话
1. 需要import那个Oculus integration的pkg
2. 需要安装Android Build Support!!
https://developer.oculus.com/documentation/unity/book-unity-gsg/
这个support可以在hub里面，对应版本选择add module即可。
3. 并且需要在player setting里面装上XR Plug-in Management, 然后把Oculus勾上。似乎我们是没有XR Setting 或者 VR setting的，不知道，网上说要在Other setting里面找，但似乎我找不到。

## 11/14
Tut, how to make this glowing effect:
https://youtu.be/6SKHDaSe768

Configure Universal PR
https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/InstallURPIntoAProject.html

有mk glow
下面这个日本链接的mk glow比较好
https://github.com/epyon1/Unity_C-_basic/tree/45ebe9e38c685cba20df3eeb40695e9a51856b4f/BreakOut/Assets/_MK
https://github.com/Podepi/FPSTD2/tree/a930b674b38b8702f8c8a050572fa889d0f5e543/Assets/Graphics


其实看tensorflow 的 playground

我觉得下一步的话：
http://arborjs.org/docs/barnes-hut
问题在于，其实大于10个nodes，他就会十分卡顿了，怎么办哇...哎其实2千多个的情况真的有点蓝的。

其实就是个取舍了，我觉得需要想一下，没准用一个更简单的NN模型（但那样说明不了问题啊）

似乎可以用这个 DOTS package?
https://unity.com/dots?_ga=2.34474214.672803161.1605378543-555137330.1604411757

今天目标，需要有一个work的demo (就，weights什么的需要能传输进去，然后能跟着training更新，律动)
我觉得没准就，变简单一点好了？就先别考虑那么大的model的visualization了？

哦其实那个tree的idea我也可以这么理解，就是每一层自己内部考虑内部的force就好，对，就，每40个自己内部render一波这样？

或者就，真的一个48*48的定死就完事了...

或者比如每20个小球当成一组，然后只要把大球直接的关系render清楚就好？就那种，球中球的感觉...但这样的idea似乎不大对。

shortcut for Unity (Play etc.)
```
Ctrl/Cmd+P	Play
Ctrl/Cmd+Shift+P	Pause
Ctrl/Cmd+Alt+P	Step
```
并且按住option再三指Trackpad可以移动view.

## 11/13
should first try rendering 2304 objects. (就，其实可以render, 但是会非常卡，真的，非常卡) 我觉得没准我那里面，48*48可以作为一个固定的形状，比如就一个48*48的小正方形就完事了？

```
 class Attracted : MonoBehaviour
 {
     public GameObject attractedTo;
     public float strengthOfAttraction = 5.0f;
     void Start {}
     void Update
     {
         Vector3 direction = attractedTo.transform.position - transform.position;
         rigidBody.AddForce(strengthOfAttraction * direction);

     }
 }
```

下面这是他对于作用力的选择。
```
There are many possibilities on how to determine the weights of the edges and the charges of the vertices. I chose to use uniform weights for the edges and use the variance of the activation of each neuron across the validation set as its charge.
```

## 11/12
Change gravity
```
Edit -> Project Settings -> Physics
```
or in Code
```c#
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    void Example()
    {
        Physics.gravity = new Vector3(0, -1.0F, 0);
    }
}
```
生成作用力
```
transform.rigidbody.AddForce(Vector3.up *FloatStrenght);
```

## 11/11
发现他那个似乎还是不行，还是会坍缩成2D。然后网上找到了一个force graph, 有基本的控制，但是没有那种，动态的force.
无论如何，明天的demo似乎是来不及了。

1. 看一下那个怎么变成3D数据（或者怎么变成不探索成一层的3D数据）
2. 有了那个数据之后，就可以连接Unity了（其实我觉得只要有一个简单的visualization就可以去说明问题了

## 11/10
但我觉得那样的问题就是，
这个project和音乐的关联太小了，就，signal processing 的部分太少了。（但是如果改变某个数值就会影响声音我感觉，也挺好的。

我在想，就几个小球，然后
## 11/09

其实就是，周四（如果他不OK就下周）带给Tovi看一下，一个简单的demo, 他如果觉得不可成为research，我就放成project就完事了, 自己写一个小文章。就不要想research的事情了。
我觉得我想得太多，做的太少。其实就
Next Step:
1. 用immersion的algo，试一下在我那个简单的小NNmodel上出来效果咋样。因为我看它应该是分层的
2. 如果1还OK，就尝试把它调整成3D的。
3. 然后就尝试把data数据传回到Unity试一下。


说实话我有种感觉，这个只能作为一个，艺术作品了，而不是一个Research。因为太不完整了。

就，其实也别想的太高深，感觉，就真的，自己想做啥，然后，写一个东西来说明一下就完事了，别为了research 而 research。

如果给盲人用，我至少需要去调研一下，现在盲人在training的时候用的是什么。

如果这样的话，没准可以把那个，唱歌自动确定音高的软件也搞出来。

`这就可以强调我是一个 emotional的人，我在乎一个东西的感情，我拒绝冷冰冰的东西。` 然后假如AI拥有了这种

这样的话，题目就真的可以变成

对，人们都在talk about how AI can faciliate 啥啥的，但是AI本身的样子是什么呢？（这里引用一下那个Memory的work)


Next Step (我也录到Voice Memo里了，下次继续的时候可以听一下先):
* 我觉得，详细看一下那个Sensing Data的paper（尤其是声音和material的部分，看看会不会有什么灵感）, 然后看一下他的引用
* 再仔细看一下MIT的paper，看一他的贡献是什么（我猜是，把data mapping到3D的algo)?
* 找到那种，把Art work describe到paper里去的paper，看看他们怎么诌出来一篇paper的 `主要看一下能不能找到一两篇模板paper`


其实或者，有没有可能写成一篇文科的paper
`A theoretical or methodological contribution that provokes novel conversations for the discipline`
就，我只是为了provoke大家关注一下过程本身（但感觉这样的文章应该很难写...)，

我觉得 Tovi可以给Visual的建议，Chris给声音的建议，一定是需要他们的input的，纯自己搞几乎不可能的。


## 11/08
其实有一个感想，没准真的，呈现自己就够了。我觉得最后做不成research paper的话，`做成一个很有艺术感觉的音乐project也是很好的，然后稍微正式一点地介绍一下背景`，related work之类的（其实那样没准无心之中就出了一篇paper）。就真的，不要太难为自己，就跟着自己的感觉去做就可以了，成不了research也可以成一个很棒的project的。

就，真的，可以发挥自己美感的一面，在这个project上，我有完全的自由，可以信马由缰，我觉得什么对就做什么就好。

然后自己可以多去录制一些歌曲（因为也确实喜欢）

其实没准真的就，只focus在声音上就好了？不

## 11/07
其实今天真的就，很不想搞，也很迷茫，觉得那个要是是一步步走的，就和我之前idea毫无关联了。
但其实也可搞就，一开始先一步步build起那个model，然后后面再一步步run它的时候，相应的edges和nodes能发光就好。但就觉得，1.真的很难做完感觉 2.真的不知道这个是否有意义。

不管是否有意义，我觉得还是挺有新意的，也挺好看的，并且真的是do or die, 这可能是进stanford的唯一办法。


## 11/06
搞懂了怎么加上去的，但是觉得好傻逼啊，这个...一点点画出来算个啥。毫无意义啊。但我觉得今晚要把visualization搞定

* 真的，最懵逼的还是neuron是怎么加上去的，就为啥每次step都会多出一堆neuron，他真的是边train边加吗。我觉得需要开debugger，一直追踪那个positions的shape，然后估计就知道是哪里增加了。
* 上面这个主要是确定，他确实是随着training生长的，而不是说是自己一层层画出来而已。如果不是的话，我就要去用别的办法了。
* 确定是随着training生长的话，就可以尝试调整成3D坐标，然后可以发送数据到Unity了。
* 有了visualize 和 sonif 就可以开始写paper了。无论怎样这一周 + 双休日，一个粗稿一定要拿出来的。


## 11/05
下一步就是思考怎么转移 / 升华。
[x]他的visualization我已经可以看到了

## 11/03
主要目的是吸引他和其他Faculty，同时证明学术能力，

他很nice, 我一定要impress他 （他说放在网站上就OK）我是觉得，哪怕申请不上，我自己觉得这个project有意义的话（他说到了really impressive, 还是说用了个什么词语）

他也说到，我捕捉到了他的project的一些精华思想。

可以华丽一些
1. 每一个train的ce acc都放出来，用不同的间隔
2. 然后不

learning rate(eps 可以)
batch size （可以每个step的train数据给一个声音）
hidden unit （可以#hidden units个 freq的声音叠加在一起）
momentum (大提，中提，小提琴？)



## 11/02
6: 45 - 7:45
0. ML知识
	* 对，其实最初我的gradient descent idea是可以表示不同维度的声音变化，如果是好几个维度的，就应该有好几条时间轴啊！！！
	* `要知道用loss有什么意义` 音为他可以给music的见解，但是意义是需要我去赋予的，除了overfit，就，能不能找到别的有趣的数据，variable来做这个，让它更有意义（或者看一下常人是怎么去train ML model的，根据什么。其实Model的结构也需要考虑一下啊，有没有声音从一层过渡到另一层的效果呢？）

1. `查一下他那个脑波项目后来成功没有，因为如果成功的话，那也可以算是科技和音乐的极好结合。并且很严谨，就，可以学习一波。`
. Chafe and Parvizi are now going through the rigorous research process that would pass US Food and Drug Administration standards in the hope of introducing a commercial medical device.

* 我觉得要尝试把.wav加进去，或者加上UGen，那样声音会丰富很多
	* (我觉得无论如何都得到达他做的那个水准，否则没法impress他，直接mapping确实太简单了。（我觉得没准真的可以把每个维度的sound都mapping上去。）
	* 看一下他有没有paper或者啥的介绍自己怎么做的
* 继续探索他的网站，看他sonification做了些啥。

[x]aeon那个小短文看完 +

## 11/01

5:45 - 7:45
[x] 看一下Chafe的那个视频，总结一下他干了啥
* 去仔细研究一下他的work，除了sonification他还干了啥
* 研究一下他的Music 153a是干嘛的 (好像是一个network的啥的)

* 想一下咋丰富我们的project
* 也看一下别的sonification到底咋做的（比如看一下Chris那个文章引用的那些sonification是做了啥）包括他自己的sonification是怎么做的，



3:45 - 5:45
几个问题
[x]每次放完声音都会有一个断点，有点难受，能不能不要有断的声音
[x]把另外几个metrics也map上试一下
[x]能不能好听一点的声音啊！梦幻一点的，科幻一点的。
[x]尝试每个step都map上来？会不会更密集一些？ 答：不行，听起来十分难受


13:07 Update: Sonification worked!

白天
11 - 1 (2h)
* 搞定连接Chunity，至少能出声音
* 看一下使用不同的metric是什么感觉，比如用accuracy？用loss？用MCC？(看一下能不能让它好听，或者，能够传递科学的信息)



## 10/31
白天做了一个小时（我记得连接到Unity里面已经搞定了，就差取出数据连接chunity了），然后去超市，给爸妈打电话，开会，一天时间就没了。真的
其实晚上心然也提到了，就，这个idea确实会给人简单的感觉。我只是mapping到声音确实不一定能说明什么。所以需要做更多。说实话好像除了对盲人之外确实没有什么帮助了。

凌晨：
1. 其实用nn.py和中间那个桥梁就OK了，现在string已经可以send了，就要搞定float的send和decode。
2. 然后在unity中decode出来就可以去连接Chunity了。

## 10/30
### 晚上
Chris Chafe回复了，所以在周二之前必须完成几件事情
1. 必须有一个像样的可以跑的demo (就，至少已经要能提供training的sonification了，并且要能选择，是loss还是MCC还是啥的，Visual可以先没有)
2. 去仔细研究一下他的work，除了sonification他还干了啥
3. 研究一下他的Music 153a是干嘛的 (好像是一个network的啥的)

今天目标：
`无论如何，实现在Unity中的sonification，什么值都可以，loss还是啥`
1. 先看那个VR trainning 是怎么把数值搞进去的，或者先看colab怎么连接unity的
2. 只要colab能连接进去 （或者电脑本地的script也可以）就可以考虑借鉴那个visualize的东西到Unity里面了。

其实现在主要问题就是，how to send data from python/terminal to Unity. 因为我们如果用GPU去train的话，data也会出现在terminal里面，而且一定会是python script的形式。


### 凌晨
其实思路还是要把colab连接到Unity里面才比较对

觉得需要思考的是，声音能够传递什么信息？能不能指引人们去调参呢？那样就要看visualization提供了什么信息。

1. 我觉得可以这样，先去看一下提到的三个工具，看一下他们肿么样，然后看加上声音是啥效果。
Three interactive visualizations aimed at non-experts to teach deep learning concepts. From left to right: Teachable Machines, TensorFlow Playground, and GAN Lab.

2. 然后需要考虑，从colab怎么到Unity去，其实我是需要把那些值都传进去，那样才能变成声音；或者ML agents本来就能传值？我知道他能连接colab，其实那就够了，尝试连上！

## 10/29
第一次跑的Report


ML-Agents里面 `stats.py`里面有一行 `log_info.append(f"Time Elapsed: {elapsed_time:0.3f} s")` 就是log出这个时间，loss信息的。
然后下面的`logger.info(". ".join(log_info))`就把这个信息在terminal里面显示出来了。



```
2020-10-29 21:58:16 INFO [stats.py:126] Hyperparameters for behavior name 3DBall:
	trainer_type:	ppo
	hyperparameters:
	  batch_size:	64
	  buffer_size:	12000
	  learning_rate:	0.0003
	  beta:	0.001
	  epsilon:	0.2
	  lambd:	0.99
	  num_epoch:	3
	  learning_rate_schedule:	linear
	network_settings:
	  normalize:	True
	  hidden_units:	128
	  num_layers:	2
	  vis_encode_type:	simple
	  memory:	None
	reward_signals:
	  extrinsic:
	    gamma:	0.99
	    strength:	1.0
	init_path:	None
	keep_checkpoints:	5
	checkpoint_interval:	500000
	max_steps:	500000
	time_horizon:	1000
	summary_freq:	12000
	threaded:	True
	self_play:	None
	behavioral_cloning:	None
	framework:	tensorflow
2020-10-29 21:58:25 INFO [stats.py:118] 3DBall. Step: 12000. Time Elapsed: 26.085 s. Mean Reward: 1.215. Std of Reward: 0.729. Training.
2020-10-29 21:58:34 INFO [stats.py:118] 3DBall. Step: 24000. Time Elapsed: 35.425 s. Mean Reward: 1.345. Std of Reward: 0.832. Training.
2020-10-29 21:58:43 INFO [stats.py:118] 3DBall. Step: 36000. Time Elapsed: 44.107 s. Mean Reward: 1.674. Std of Reward: 1.048. Training.
2020-10-29 21:58:52 INFO [stats.py:118] 3DBall. Step: 48000. Time Elapsed: 52.883 s. Mean Reward: 2.300. Std of Reward: 1.633. Training.
2020-10-29 21:59:01 INFO [stats.py:118] 3DBall. Step: 60000. Time Elapsed: 61.581 s. Mean Reward: 3.981. Std of Reward: 3.050. Training.
2020-10-29 21:59:13 INFO [stats.py:118] 3DBall. Step: 72000. Time Elapsed: 73.572 s. Mean Reward: 7.557. Std of Reward: 6.722. Training.
2020-10-29 21:59:23 INFO [stats.py:118] 3DBall. Step: 84000. Time Elapsed: 84.245 s. Mean Reward: 16.474. Std of Reward: 16.629. Training.
2020-10-29 21:59:35 INFO [stats.py:118] 3DBall. Step: 96000. Time Elapsed: 95.803 s. Mean Reward: 35.863. Std of Reward: 26.855. Training.
2020-10-29 21:59:47 INFO [stats.py:118] 3DBall. Step: 108000. Time Elapsed: 108.281 s. Mean Reward: 67.388. Std of Reward: 38.094. Training.
2020-10-29 22:00:00 INFO [stats.py:118] 3DBall. Step: 120000. Time Elapsed: 120.727 s. Mean Reward: 70.671. Std of Reward: 31.025. Training.
2020-10-29 22:00:11 INFO [stats.py:118] 3DBall. Step: 132000. Time Elapsed: 131.785 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:00:21 INFO [stats.py:118] 3DBall. Step: 144000. Time Elapsed: 141.982 s. Mean Reward: 93.554. Std of Reward: 22.330. Training.
2020-10-29 22:00:31 INFO [stats.py:118] 3DBall. Step: 156000. Time Elapsed: 151.616 s. Mean Reward: 92.269. Std of Reward: 19.640. Training.
2020-10-29 22:00:39 INFO [stats.py:118] 3DBall. Step: 168000. Time Elapsed: 160.344 s. Mean Reward: 90.823. Std of Reward: 25.730. Training.
2020-10-29 22:00:48 INFO [stats.py:118] 3DBall. Step: 180000. Time Elapsed: 169.055 s. Mean Reward: 93.623. Std of Reward: 18.801. Training.
2020-10-29 22:00:57 INFO [stats.py:118] 3DBall. Step: 192000. Time Elapsed: 178.480 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:01:07 INFO [stats.py:118] 3DBall. Step: 204000. Time Elapsed: 188.032 s. Mean Reward: 89.700. Std of Reward: 25.848. Training.
2020-10-29 22:01:16 INFO [stats.py:118] 3DBall. Step: 216000. Time Elapsed: 197.180 s. Mean Reward: 92.462. Std of Reward: 26.114. Training.
2020-10-29 22:01:27 INFO [stats.py:118] 3DBall. Step: 228000. Time Elapsed: 207.602 s. Mean Reward: 92.500. Std of Reward: 25.981. Training.
2020-10-29 22:01:38 INFO [stats.py:118] 3DBall. Step: 240000. Time Elapsed: 218.614 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:01:50 INFO [stats.py:118] 3DBall. Step: 252000. Time Elapsed: 231.310 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:02:03 INFO [stats.py:118] 3DBall. Step: 264000. Time Elapsed: 244.295 s. Mean Reward: 95.362. Std of Reward: 16.068. Training.
2020-10-29 22:02:13 INFO [stats.py:118] 3DBall. Step: 276000. Time Elapsed: 254.490 s. Mean Reward: 95.608. Std of Reward: 14.566. Training.
2020-10-29 22:02:23 INFO [stats.py:118] 3DBall. Step: 288000. Time Elapsed: 264.264 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:02:33 INFO [stats.py:118] 3DBall. Step: 300000. Time Elapsed: 274.328 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:02:43 INFO [stats.py:118] 3DBall. Step: 312000. Time Elapsed: 284.412 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:02:52 INFO [stats.py:118] 3DBall. Step: 324000. Time Elapsed: 293.511 s. Mean Reward: 92.508. Std of Reward: 25.954. Training.
2020-10-29 22:03:01 INFO [stats.py:118] 3DBall. Step: 336000. Time Elapsed: 302.180 s. Mean Reward: 94.508. Std of Reward: 19.026. Training.
2020-10-29 22:03:08 INFO [stats.py:118] 3DBall. Step: 348000. Time Elapsed: 309.447 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:03:17 INFO [stats.py:118] 3DBall. Step: 360000. Time Elapsed: 318.310 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:03:26 INFO [stats.py:118] 3DBall. Step: 372000. Time Elapsed: 327.265 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:03:35 INFO [stats.py:118] 3DBall. Step: 384000. Time Elapsed: 336.270 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:03:44 INFO [stats.py:118] 3DBall. Step: 396000. Time Elapsed: 345.301 s. Mean Reward: 92.862. Std of Reward: 24.728. Training.
2020-10-29 22:03:53 INFO [stats.py:118] 3DBall. Step: 408000. Time Elapsed: 354.441 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:04:03 INFO [stats.py:118] 3DBall. Step: 420000. Time Elapsed: 363.949 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:04:13 INFO [stats.py:118] 3DBall. Step: 432000. Time Elapsed: 373.830 s. Mean Reward: 96.717. Std of Reward: 10.890. Training.
2020-10-29 22:04:24 INFO [stats.py:118] 3DBall. Step: 444000. Time Elapsed: 384.570 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:04:34 INFO [stats.py:118] 3DBall. Step: 456000. Time Elapsed: 394.653 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:04:42 INFO [stats.py:118] 3DBall. Step: 468000. Time Elapsed: 403.323 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:04:51 INFO [stats.py:118] 3DBall. Step: 480000. Time Elapsed: 411.924 s. Mean Reward: 100.000. Std of Reward: 0.000. Training.
2020-10-29 22:05:00 INFO [stats.py:118] 3DBall. Step: 492000. Time Elapsed: 421.007 s. Mean Reward: 92.438. Std of Reward: 26.194. Training.

```


其实Unity自己的ML Agent没准就可行，但是那个是RL的，不是DL和ML，所以就很头大。

但说实话，其实training的部分人们一般都是忽略的，因为并没有什么信息，但是其实也可以关注，因为有一些美感和有趣的东西在里面。

我觉得没准可以先尝试把ML Agent的那些值map到ChucK上，试试看先。

## 10/28
今天下班之后5个多小时都扑在这个上面，但是效率不是很高。发现，刚好只有6周少一天了。严格来讲最好这两周就能写完，因为后面还有369的A2，test 2之类的。

真的，内心会十分动摇，到底还要不要申请。我看那些去的中国学生本科都很一般，北邮，天津大学这种（虽然可能人家也是高考落榜的，没有必要瞧不起别人，因为当初我要是不出国和他们的状况是一样的吧）

同时又觉得我这个算哪门子research，担心 1.做不完 2.方向错。

方向问题其实要找那边和Tovi确定一下是最好的。但是现在这个状况，Ge Wang不理我 我有什么办法呢。

说实话，可以先听一下sonify之后的声音怎么样，然后找另外那个Prof聊一下也OK的。

就算打水漂了，也可以继续添补，再去投下一个会议啊，总好过没有哇啊。实在不行放到多大的期刊上也可啊，放在网络上当自己的project也可啊。

说实话，这是我一次试错的机会，我就算所有努力打水漂，也要去做的，否则对不起
* 一个暑假的纠结煎熬
* 今年申请人数变少的优势
* 放弃TA offer做这个
* 那么多关于CCRMA的资料查询，networking

下一步：
1. 要把model training搞到Unity里面，要有办法去拿到loss的值。（看一下那个VR里面装model的东西，是怎么把python搞到unity里面的）
2. 把loss的值map到chuck上 (现在主要有点担心声音的问题. 那个机械的风格实在难听，是怎么变好听的呢)
3. 要和Data sonify的那个Prof取得联系。

## 10/27
所以就，看一下CHI LWB 和 ICMC往年的paper（或者他们有没有best paper之类的）看一下大致长度，找三两篇类似的模板，就可以直接开始写了（LWB可以直接找CHI的paper我觉得。）我觉得没准在写related work的时候会有新的体会和感悟。

可以先从斯坦福那个教授的paper开始整理，这样有问题之后也可以快速去问

因为我最终是一定要有一篇paper投出去的，哪怕多大那个也行。

可以看一下CHI LWB投稿的流程，是不是谁都可以瞎鸡儿投的？

