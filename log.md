# 待实现的idea
1. 提供标高线，就，提供 freq = 100, 200, 300...的声音，这样人们在听的时候就可以实时拿出标高线听一下，我这个声音在哪里了，是否到达了某个值。
2. 熟悉一下我的NN的model的结构和概念，研究一下怎么share电脑声音，到时候demo该怎么搞。
3. 剪头发
* 好像后面电脑运行缓慢了，声音出现也会缓慢



# Link
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


## Paper/Github
* [Deep Learning Development Environment in Virtual Reality ](http://github.com/Cobanoglu-Lab/VR4DL)
	* 发现这个 `UpdateProgressText` 是核心，每次需要更新UI上的accuracy值都会用到他。
	* `EvaluateSimple.cs` 里面的 `Evaluator.Evaluator.EvaluatorClient client` 可以被这样call `client.EvaluateAsync(req).ResponseAsync.Result.Accuracy`就可以拿到accuracy.
	* 然后发现`CEvaluator.cs`里面有 `const string PYTHON = "C:/Users/VR_Demo/Anaconda3/envs/VR_Env_GPU/python.exe"` 其实是在那边call python, 让Python去跑script. 所以他这边的逻辑是，用Unity来主导一切的， 就是Unity来发号施令的。
* [Immersions Visualizing and sonifying how an artificial ear hears music](https://github.com/vincentherrmann/neural-layout)

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
* [how to pronounce Chafe](https://youtu.be/zMaJKLRfhew)
	* 听起来有点像chase (没有se) + f
* [ccrmalite1](https://youtu.be/BkucUIiiXac)
* [YouTube-A listening tour of musical portraits and sonic landscapes](https://youtu.be/Y_d1A2Ehjrc)


# Knowledge
## Difference between Epoch and Step?
* Epoch: A training epoch represents a complete use of all training data for gradients calculation and optimizations(train the model).
* Step: A training step means using one batch size of training data to train the model.

* Bitcrusher: 就是指那种让音频变得很低品质的方式，比如变成radio啊，就，很次的声音。比如这里的一个[YouTube链接](https://youtu.be/jRzU2TO8tO0)

# 可以问Chris的问题
* 可以投什么会议？ICMC吗？还是说有什么建议？然后你们什么时候审核啊，如果我中了你们能看见吗？
* 在CCRMA有没有可能去学习唱歌呢？美声，音乐剧，歌剧这种（这个我需要自己先查询一下） 真的，我觉得CCRMA这个项目可以用进我所有的才华，是目前最perfect的项目。



# Log
## 11/01

5:45 - 7:45
* 看一下Chafe的那个视频，总结一下他干了啥
* 去仔细研究一下他的work，除了sonification他还干了啥
* 研究一下他的Music 153a是干嘛的 (好像是一个network的啥的)

* 想一下咋丰富我们的project
* 也看一下别的sonification到底咋做的（比如看一下Chris那个文章引用的那些sonification是做了啥）




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

