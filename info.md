# 待实现的idea


我从 看淡金钱 -> 大二 发现赚钱 重视钱 ->现在 想做喜欢做的事情。
CS就业很好，但是我想做我想做的事情。

其实这个项目有没有用，CC帮不了，他只能提供music sonify的帮助。用没有用需要问ML的人。真的，不能期待CC能给什么ML的见解。ML的见解需要我自己想明白 + 问ML的Prof问明白。


0. `查一下他那个脑波项目后来成功没有，因为如果成功的话，那也可以算是科技和音乐的极好结合。并且很严谨，就，可以学习一波。`
. Chafe and Parvizi are now going through the rigorous research process that would pass US Food and Drug Administration standards in the hope of introducing a commercial medical device.
0. ML知识
	* 对，其实最初我的gradient descent idea是可以表示不同维度的声音变化，如果是好几个维度的，就应该有好几条时间轴啊！！！
	* 要知道用loss有什么意义，除了overfit，就，能不能找到别的有趣的数据，variable来做这个，让它更有意义（或者看一下常人是怎么去train ML model的，根据什么。其实Model的结构也需要考虑一下啊，有没有声音从一层过渡到另一层的效果呢？）
0. 可能需要恶补一下音乐知识，专有名词啥的 | 可以说我的女票是lsq? Play cell?
vocabulary with phrases such as ‘it’s a gas’ and ‘off on a jag’, along with the more conventional musical jargon of keys and scales.
0. 看一下Chris 的sonification怎么搞的（我觉得他应该是用了更高级的软件？没..就是ChucK`而且我发现他擅长用不同的音效和左右声道`），因为他似乎自己有一套algo，并且声音也很丰富，就那种，马林巴（不知道）的声音？http://sevenairs.com/data.html 可能有哪里介绍这个的（去他的网站看一下他咋搞的？）
1. 提供标高线，就，提供 freq = 100, 200, 300...的声音，这样人们在听的时候就可以实时拿出标高线听一下，我这个声音在哪里了，是否到达了某个值。
2. 熟悉一下我的NN的model的结构和概念，研究一下怎么share电脑声音，到时候demo该怎么搞。
3. 剪头发
* 好像后面电脑运行缓慢了，声音出现也会缓慢



对，就如果是面向大众的
可以华丽一些
1. 每一个train的ce acc都放出来，用不同的间隔
2. 然后不

learning rate(eps 可以)
batch size （可以每个step的train数据给一个声音）
hidden unit （可以#hidden units个 freq的声音叠加在一起）
momentum (大提，中提，小提琴？)


面向科学家
就那两个音就够了
1. 好几个不同hyperparamenter的放在一起跑，每一个pre set用不同的timbre
2. 提供几个标尺 （左手三个光柱，hover可以听到音高，并且语音会说是多高），可以听一下高度。正中间是一个play，hover左侧听acc，hover右侧听ce. 右手边可以存某个时间点的声音
3. 但这个问题极有可能不存在，就其实有一个软件直接回放一下，告知一下最低点就好。



# Paper
https://youtu.be/UY7sVKJPTMA
写paper的建议，我觉得太对了！
真的就，开始胜过一切。先列outline，然后直接写，不要抠细节(包括那种，需要reference的位置，先不要停下找，继续写，后面再回去找)，现在先关注的是完成一个paper。然后intro应该最后写，先写实验部分。
![](pic/2020-11-07-03-04-47.png)
![](pic/2020-11-07-03-03-25.png)
![](pic/2020-11-07-03-06-42.png)
最后再写abstract，intro，ack.

Intro需要cover两点：
1. why was the study done, what is it's purpose
2. give sufficient relevant background to understand what you did


# Link
## Music Theory
* [Learn music theory in half an hour](https://youtu.be/rgaTLrZGlk0)
* [Complete Piano Theory Course: Chords, Intervals, Scales & More!](https://youtu.be/Ud9CpGOG1GE)
* ![ADSR](pic/ADSR_parameter.svg)
￼

## Unity
* [ML Agent getting started guide](https://github.com/Unity-Technologies/ml-agents/blob/release_8_docs/docs/Getting-Started.md)
* [Unity ML-Agents Toolkit Documentation, Release 8](https://github.com/Unity-Technologies/ml-agents/blob/release_8_docs/docs/Readme.md)
	* 这个主要想搞明白，里面有用到colab，是不是Unity和colab是可以无缝接合的？
* [How to install this toolkit?](https://github.com/Unity-Technologies/ml-agents/blob/release_8_docs/docs/Installation.md)
* [How to run ML-Agents on google colab-blog](https://medium.com/@dhyeythumar/training-ml-agents-with-google-colab-cb166c3dca46)
* [How to run ML-Agents on google colab-github](https://github.com/Dhyeythumar/ML-Agents-with-Google-Colab)
* [How to Use t-SNE Effectively](https://distill.pub/2016/misread-tsne/)
* [Unity/Python communication](https://github.com/off99555/Unity3D-Python-Communication)
	* 这个可以Unity/Python之间实现通信。并且在README里面也提到了他也想要做ML，所以想要这种沟通。
	* 他也是找了一圈code发现没有合适的，然后就手撸了！！woc，我第一次看到那个Sending Hello的时候好开心啊。
	* 注意跑它之前要确保 `pip install pyzmq` 这个我在FY的电脑上要再做一次

* [Graph And Chart, 就是Unity里面visualize数据的一个pkg, 70刀](https://assetstore.unity.com/packages/tools/gui/graph-and-chart-78488)
* [Fource directed graph](https://github.com/l-l/UnityNetworkGraph)


## Paper/Github
* [Deep Learning Development Environment in Virtual Reality ](http://github.com/Cobanoglu-Lab/VR4DL)
	* 发现这个 `UpdateProgressText` 是核心，每次需要更新UI上的accuracy值都会用到他。
	* `EvaluateSimple.cs` 里面的 `Evaluator.Evaluator.EvaluatorClient client` 可以被这样call `client.EvaluateAsync(req).ResponseAsync.Result.Accuracy`就可以拿到accuracy.
	* 然后发现`CEvaluator.cs`里面有 `const string PYTHON = "C:/Users/VR_Demo/Anaconda3/envs/VR_Env_GPU/python.exe"` 其实是在那边call python, 让Python去跑script. 所以他这边的逻辑是，用Unity来主导一切的， 就是Unity来发号施令的。
* [Immersions Visualizing and sonifying how an artificial ear hears music](https://github.com/vincentherrmann/neural-layout)
	* 我好像看懂了一丢丢，就是，我感觉一开始所有的点都是重合在一起的，都是没有出现，或者neuron没有被激活，后面根据weight来算forces，weight被更新上了就会出现这种图像。
	* 我不确定这个从2D转到3D需要多少代价，主要还是声音吧。(但主要有3D就会很炫酷)
	* 我觉得他的局限性一定是，他只是针对audio的NN做的，但是Loss几乎对所有ML都试用。然后我可以Visual 和 Sound两个部分分开去说
	* 跑他的code
		* python setup.py build
		* python setup.py install
		* PyQt5, imageio-ffmpeg, PyOpenGL 都需要到 Project Interpreter里面去install, 直接cmd做install是不行的！我觉得应该是virtual env的问题，pip install是到全局的（我本地的全局是3.7), 但是Interpreter是local的virtual env.
		![](pic/2020-11-05-21-03-13.png)
		* 出现 IndexError: tensors used as indices must be long, byte or bool tensors 就 xxx.type(torch.long)，其中xxx就是它说需要是这个type的值。
		* 出现 TypeError: expected Long (got Float), 就xxx.type(torch.long）
		* IndexError: only integers, slices (`:`), ellipsis (`...`), numpy.newaxis (`None`) and integer or boolean arrays are valid indices  就 xxx.type(torch.int)
	* [Barnes-Hut N-Body Simulations with MPI](https://youtu.be/0eKQXPAcQK8)其实就是把一个n^2的计算简化成了nlogn，做法是等分区域，如果某个区域有多于两个的再等分，直到最后每个区域都只有一个particle为止，这样其他partical给他的force就可以近似为所在区域center of mass给他的。
	* [Simple Force Directed Graph in Unity](https://youtu.be/T69V_d_XmUw)!!!已经有现成的algo了
	* [Force Directed Graphs in Unity](https://youtu.be/5HFVQQZ5GGg)
	* [Building and Drawing a Graph in Unity using Gizmos](https://youtu.be/zdHvM6XU4rY)

这个sample-env是必须的，每次run前都需要打开
source ~/python-envs/sample-env/bin/activate
然后再在任意文件夹中跑 mlagents-learn
mlagents-learn /Users/zhuoyue/Documents/School/Stanford/ml-agents/config/ppo/3DBall.yaml --run-id=first3DBallRun --force


## Chunity
* [Music 256](https://ccrma.stanford.edu/courses/256a/)
* [总的link](http://chuck.stanford.edu/chunity/)
	* Spatialization: is the technique of processing a stream of audio so that it is perceived to be originating from a particular place in the environment.
* [Plug-in mode](http://chuck.stanford.edu/chunity/with-plugins/)
	* Jack的tut很细致，就，把 (Note: @"" denotes a multi-line string.) 这种都写出来了，就，我们可能出问题的东西都写出来了。
	* 最底下还讲解了unit generators(UGens)就可以变成不同的声音效果
* [Chicken sequencer](https://youtu.be/LZMLEfjq8Ns)
	* 这个Ge Wang提到，所有和时间控制有关的（比如一个drum loop之类的）都要放到Chuck里面去，因为时间会很准确，然后用chuck给的时间去update Unity里面的动画。
* [Audio visualizer](https://youtu.be/nMeF2W2gv7E)
	* 这个是可以visualize我们实时说话的声音，但这个似乎是通过unity内建的module来实现的，和Chuck木有关系。
* [Youtube-Chunity Tut](https://youtu.be/gpcqd5rSOhI)
	* 是用subInstance的，然后就，每一次触发声音都用一个broadcast去触发，我觉得这才是正道
	* 也提到 [这个document] (http://chuck.stanford.edu/doc/program/ugen.html) 给了很多音色！
* [Free wave source](https://www.wavsource.com/sfx/sfx.htm)
	* 这里有一些短的wave音效


## Prof. Chris Chafe
* 今年好像67岁了
* (both Chafe and [Greg Niemeye, 他说data artist](https://youtu.be/UTYiENfN8Ak?t=5) are Swiss- born 瑞士人哈哈哈
* [how to pronounce Chafe](https://youtu.be/zMaJKLRfhew)
	* 听起来有点像chase (没有se) + f
* [ccrmalite1](https://youtu.be/BkucUIiiXac)
* [YouTube-A listening tour of musical portraits and sonic landscapes](https://youtu.be/Y_d1A2Ehjrc)
	* 这个的笔记我放在safari里面了
* [The Sound Stage of the Mind: Imagined Sounds and In ner Voices](https://youtu.be/Sr_j0O2WWCs)
* [Online Jamming and Concert Technology](https://www.kadenze.com/courses/online-jamming-and-concert-technology-x/info)
* [Quarantine Sessions, 就6和musician在 California, Berlin (DE), and Ghent (BE) 一起演奏，concert](https://chrischafe.net/quarantine-sessions/)

* [LISTEN: 1,200 Years of Earth's Climate, Transformed into Sound](https://www.kqed.org/science/1918660/listen-1200-years-of-earths-climate-transformed-into-sound)
![](pic/2020-11-02-01-40-51.png)
When you sonify data, you experience time in a way you can’t when you look at a chart.
-Hal Gordon, Graduate student
 ping-pong sound: global temperature averages. 另外一个声音是CO2的水平。
 是19，20世纪开始，直线飙升，最后是ends in this kind of ambulance (frightening) sound (其实确实，会让人揪心，会给人一种提醒。)

* [Sonifying the world, 一个aeon的小短文！](https://aeon.co/essays/how-the-sounds-of-data-and-nature-join-to-make-sweet-music)
	*  but he wouldn’t have `felt` the way in which economic progress was so tightly bound to pollution levels. (确实，音乐可以带来这种 感觉，但是其他是无法带来的。)
	* sonifying our world has a way of wrenching our guts (绞尽脑汁), producing visceral(内脏的) reactions that are frequently missing from the merely visual.
	* ‘Carl Sagan had a real nice insight about this,’ Chafe told me. ‘ e effect of using your ears is the easiest way to achieve, for him, teleportation.’



## 偶然碰到的很美的音乐
* [Frédéric Chopin: Piano Concerto No. 1 e-minor (Olga Scheps live)](https://youtu.be/2bFo65szAP0) 这个红衣妹子 Olga Scheps 太美了
* [Encoding the Fibonacci Sequence Into Music](https://youtu.be/IGJeGOw8TzQ) 这个把斐波那契数列变成Music.

# Knowledge

## Fundamentals of NN
https://www.wandb.com/articles/fundamentals-of-neural-networks

## Loss / Epoch and Step / Early stopping?
* Epoch: A training epoch represents a complete use of all training data for gradients calculation and optimizations(train the model).
* Step: A training step means using one batch size of training data to train the model.

Loss (Punish the incorrect value):
1. Squared loss
2. Cross-entropy loss (-log y)
3. Hinge loss....

Backprob:Loss function对某一个variable的求导

Early stopping
![](pic/2020-11-02-19-33-45.png)

Tunning:
就是在Validation set 上看表现 （validation error），然后给不同hidden units, learning rate 看一下。有grid search和random search两种给法。


## ML & AI?
其实有争议，ML不能完全说是AI。
Judea Pearl:
* ML learns and predicts based on passive observations (methods and models borrowed from statistics and probability theory);
* AI implies an agent interacting with the environment to learn and take actions that maximize its chance of successfully achieving its goals. (symbolic/knowledge-based learning, inductive logic programming)

Supervised learning:
1. KNN
2. Decision tree

Unspervised
1. K Means (假设背后是gaussian distribution, 一次次update，轮流update，其实有一个cost function，就是所有点距离centroid的平均距离。)
2. EM
3. PCA

不管是哪个，最后都会变成一个optimization problem，有一个function我们要让他变小




## Variable
1. learning rate
2. W, b
3. activation function
4. Optimizers (基于Loss function)


* Bitcrusher: 就是指那种让音频变得很低品质的方式，比如变成radio啊，就，很次的声音。比如这里的一个[YouTube链接](https://youtu.be/jRzU2TO8tO0)


## Stanford
* 这里提到 CS课人很多，Prof很强但讲课很烂，基本靠TA。因为基本大家都学CS。
* 但是人文学课，比如英语，6个人一节课，就，很开心。那些Prof也很友好。
* flake, 放鸽子？ A flake is someone who generally makes plans with you, promises to do things with or for you but can never seem to follow
