# Sensing AI: physicalizaiton(Materilization) and sonification of Artificial Neural Network
备选项："Feel" the AI: auditory and visual approach for ....


## Abstract / Intro
人们关注AI的产出，但是没有人关注AI在学习的时候是什么样子的，什么声音。

我觉得这是一个很重要的问题，AI这么火，人们基本都关注的是它的结果，它的accuracy。但没有人思考过
1. 关注AI本身
2. 关注除了visualization之外别的sense (听觉，触觉（我觉得我可以说VR是一种动态现实的模拟，未来触觉研究（这里引用一下那批触觉研究paper) 就可以加上触觉），3D，美感，引发的情感).

这样的好处是：
1. 能激起正确的情绪，尤其是对大众，这种浅显的方式不用太多科学知识，可以有助于AI的普及
2. 对于残障人士，视觉或者听觉障碍的，也可以有特殊的帮助。
3. 引导一种对话，关注别的知觉，这里我就提供了听觉和3D视觉的结合


其实我只要保证我上面这几点意思传达到位了，我不用考虑投什么会议的，真的，该说的说就OK了、
## Related Work

### Visualization
1. AutoAI，可以放那个NodeSphere的图。


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

### 科学家（障碍）
科学家的话我们可以提供更make sense的架构。需要量化

### 大众
通俗，简单

只是我没想好该怎么呈现这个，
1. 如果只是引导一种对话，我只要有一个简单的demo就可以了
2. 如果说是要严谨的研究，我需要去思考不同的interaction techniques，或者，对于不同的model需要有不同的measure量度。


# 模板paper是
1. Jack的那个Chunity，那个只是把Unity和Chunck连接了一下，然后说了很多玄而又玄的东西就完事了。
2. Raul Altosaar 的这个 https://dl-acm-org.myaccess.library.utoronto.ca/doi/pdf/10.1145/3294109.3301256，真的就是一个贼鸡儿短的poster。（就是这个TEL会议下的一个performance track而已）不过他演奏时的执着是可以看出来的。我觉得我可以录制一个视频讲解我的系统，如果真的申请的话，嗯嗯（用好我的声音）

目前想到的就是，看一下有没有现成可以用的VR visualization.

或者就，只是声音?

然后这个paper目前是合并的，我觉得未来可以做成两篇单独的paper 1. VR下的AI  2.单独声音 分别单独提升。