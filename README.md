 **翻译姬是用于机翻各类游戏脚本、小说、文档等文本类型的机翻工具。** 

核心特色：
- 支持任意游戏文本、字幕文件、小说、Json/Xml等能直接一切能使用文本工具打开的文本
- 内置强大的双向正则系统，让翻译姬几乎适配了所有的游戏文本，同时支持mtool、SExtractor等常用工具提取的文本
- 支持GPT3.5、GPT-4、claude-3、Sakura等大语言模型，以及阿里云、腾讯云、百度、火山等可白嫖的普通机翻
- 支持多种多线程模式，可处理单个大文件、多个小文件等，发挥最大机翻效率
- 机翻完毕后可手动查看漏翻、原文返回、多换行等异常，一键定位，一键补翻
- 支持断点续翻、配置文件保存，方便一键打开指定模式，方便不同场景的切换
- 文本翻前翻后字典+GPT人名字典，提升机翻质量，也可实现快速的文本替换，例如繁简转换
- 完善的Json/xml机翻，只要是个Json/xml，均可实现
- 每个账号支持字符消耗检测，防止超额导致欠费

更多详细功能请参考使用教程/wiki：https://gitee.com/this_sensen/NEKO_Print/wikis

下载地址：https://gitee.com/this_sensen/NEKO_Print/releases

使用教程/wiki：https://gitee.com/this_sensen/NEKO_Print/wikis

交流群(QQ)：866373258

TG群：[TG群链接(不常用)](https://t.me/neko_print)

**使用须知** 
- 如果为了机翻游戏，则游戏拆包封包有一定门槛，如果你是想机翻给自己玩，个人推荐使用VNR类实时翻译工具，例如团子翻译器、yuki。
- 本工具作为纯文本机翻工具，请确保您的文本能够使用文本工具打开查看(记事本打开)。
- 软件需要正则基础，如果不会，进群免费帮写。
- 使用该软件机翻后公开发布游戏的，请标记本软件并注明是机翻。(机翻就是机翻，请勿使用精翻、汉化等词汇)
- 纯免费软件，禁止商用。如有特殊需求例如linux系统、docker部署、汉化对照辅助等请在群咨询
- BUG/建议提交最好去QQ群，其次TG和issues(git玩的不熟)

 **所需环境：** 

win10、win11可直接使用，win7则需要 .Net Framework 4.6.2环境，部分精简、Ghost系统需要额外安装环境
1. NET4.6.2离线安装包下载地址：http://go.microsoft.com/fwlink/?linkid=780600
2. DirectX修复工具：https://blog.csdn.net/vbcom/article/details/6962388 工具-选项-高级中开启C++强力修复

 **相关链接** 

界面UI：https://gitee.com/yhuse/SunnyUI

字体[猫啃珠圆体]：https://www.maoken.com/freefonts/17948.html

GPT参考(已获得作者授权)：https://github.com/cx2333-gal/GalTransl

 **界面预览** 

可在[关于翻译姬]界面进行主题更改
![文本翻译](https://foruda.gitee.com/images/1714463215960716668/3518df98_10364928.jpeg "文本翻译.jpg")
这里可对机翻后文本进行修改、重翻、导出/导入其他格式等
![数据处理](https://foruda.gitee.com/images/1714462638578270479/13f0f6bc_10364928.jpeg "数据处理.jpg")
这里保存了每个api账号，并可手动设置额度上限防止超额
![API设置界面](https://foruda.gitee.com/images/1698737641872288175/76ab6a4c_10364928.jpeg "API设置.jpg")
这里是更深层次的设定
![全局设置](https://foruda.gitee.com/images/1714463245281152268/189dd091_10364928.jpeg "全局设置.jpg")
GPT专属的设置
![GPT设置](https://foruda.gitee.com/images/1730306426068730894/6516ad31_10364928.jpeg "GPT设置.jpg")
翻译姬的运行核心，可在这里进行正则的测试，看是否提取到想要机翻的文本
![正则设置](https://foruda.gitee.com/images/1698740336574517631/c57f5b83_10364928.jpeg "正则设置.jpg")
高效的多线程替换的数据来源，支持正则替换，可以实现更多的功能
![替换列表](https://foruda.gitee.com/images/1698813678607926739/cfb9f529_10364928.jpeg "替换列表.jpg")
简单的Json/Xml指令生成界面，查询，然后点一点即可生成对应提取
![Json和Xml指令](https://foruda.gitee.com/images/1698819502910042916/d1a22f27_10364928.jpeg "Json和Xml指令.jpg")
