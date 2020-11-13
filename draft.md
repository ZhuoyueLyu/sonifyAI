# Sensing AI: physicalizaiton(Materilization) and sonification of Artificial Neural Network
备选项："Feel" the AI: auditory and visual approach for ....


## Abstract / Intro
人们关注AI的产出，但是没有人关注AI在学习的时候是什么样子的，什么声音。

我觉得这是一个很重要的问题，AI这么火，人们基本都关注的是它的结果，它的accuracy。但没有人思考过
1. 关注AI的training本身，是一个beautiful的process，就像人类在学习的感觉一样，就这个需要给人一种感觉。
2. 关注除了visualization之外别的sense (听觉，触觉（我觉得我可以说VR是一种动态现实的模拟，未来触觉研究（这里引用一下那批触觉研究paper) 就可以加上触觉），3D，美感，引发的情感).
3. training 本身可以在VR中实现，可感可触，就，拉扯，而不是手动输入数据。

这样的好处是：
0. 其实让AI变的concrete，like a really object/creature, instead of a concept. AI需要有自己的形象
1. 能激起正确的情绪，尤其是对大众，这种浅显的方式不用太多科学知识，可以有助于AI的普及
2. 对于残障人士，视觉或者听觉障碍的，也可以有特殊的帮助。
3. 引导一种对话，关注别的知觉，这里我就提供了听觉和3D视觉的结合

`这是一种和AI interact的全新方式` 就，在VR里面可以这样去train ML model.


并且一个ML/DL algo的关键就是optimization (gradient descent之类的，这里可以引用一堆paper，讲optimization的) 然后我们可以说，well，怎么让人们感知这个核心的idea? 图像是一个方面，声音可以从另一个角度开启我们的认知。
（如果这样的话，我想把momentum什么的都做上去，就可以听到这些小的变化对ML train的影响）

并且VR，用手操作，不需要任何知识，真的，零基础入门，我们没有任何复杂的东西。


其实我只要保证我上面这几点意思传达到位了，我不用考虑投什么会议的，真的，该说的说就OK了、
## Related Work

### Visualization in 2D
1. AutoAI，可以放那个NodeSphere的图。


### Visualization in 3d
1. TensorSpace.js
https://tensorspace.org/html/playground/
这个其实挺好玩的，但是同样，也是train结束之后的东西，不是活着的

2.Deep Learning Development Environment in
Virtual Reality
专业，但是同样，略过了training的过程。
https://arxiv.org/pdf/1906.05925.pdf

## Implementation
我们使用的是一个简单的network, 用于UofT Machine learning 教学的NN。

### Physicalization
`对，我觉得，可以去拉扯它，就可以改变weights，可以怎么搓一下，就可以改变什么weights（比如用中间的距离代表weights)，就，不用看着数字，可以直接拖来拖去，捏来捏去。但我不知道这个在2D中，人们有没有做过，如果有的话，可以直接拿进来。然后捏完之后，松开，网络又会慢慢回复，是因为它又train回去了。`



就，其实visualization，这个人已经做了，Unity里面，
但是问题是
1. 他这个不是real time，就，我感觉是数据提前有的，然后他只是设计了一个界面而已。
https://vimeo.com/274236414
2. 他这个不可以用手扯来扯去。

https://youtu.be/r8pmKYmsOv8
这个是那个手写数字的训练集，但这个是，结果的visualization，而不是过程的。


### Sonification



觉得最简单的做法

`左手听左声道，右手听右声道 (就，靠近左边的耳朵变成单左声道，靠近右边的耳朵变成单右声道)`。

远处抓进来一个小球(可以两个颜色，代表accuracy和CE loss)，可以代表声音。

面前放一个肚兜一样的东西，可以存储着想要的音高。

对，这样是最快的，因为一旦加入visualization，似乎就变复杂了，比如，我们要用什么去呈现不同的weights，颜色吗?graph长什么样？


### 科学家（障碍）
科学家的话我们可以提供更make sense的架构。需要量化

### 大众
通俗，简单

只是我没想好该怎么呈现这个，
1. 如果只是引导一种对话，我只要有一个简单的demo就可以了
2. 如果说是要严谨的研究，我需要去思考不同的interaction techniques，或者，对于不同的model需要有不同的measure量度。




## Conclusion / Limitation
1. 我们只是在一个简单的network上，对于更加复杂的network我们还没有探索过
1+. 但是呢，这种简单的example其实很有教学意义。


# 模板paper是
1. Jack的那个Chunity，那个只是把Unity和Chunck连接了一下，然后说了很多玄而又玄的东西就完事了。
2. Raul Altosaar 的这个 https://dl-acm-org.myaccess.library.utoronto.ca/doi/pdf/10.1145/3294109.3301256，真的就是一个贼鸡儿短的poster。（就是这个TEL会议下的一个performance track而已）不过他演奏时的执着是可以看出来的。我觉得我可以录制一个视频讲解我的系统，如果真的申请的话，嗯嗯（用好我的声音）

目前想到的就是，看一下有没有现成可以用的VR visualization.

或者就，只是声音?

然后这个paper目前是合并的，我觉得未来可以做成两篇单独的paper 1. VR下的AI  2.单独声音 分别单独提升。