using System;
using System.Collections.Generic;
using System.Text;

namespace 翻译姬;
internal interface GPT数据处理接口 {

    GPT设置数据 GPT设置数据 { get; set; }
    Dictionary<string, string> 上文内容 { get; set; }
    int 错行重试次数 { get; set; }

    List<dynamic> 获取请求内容(bool 是否润色, dynamic 原请求);
    void 请求写入文本(dynamic 返回值, 文本[] 文本arr);
    
    dynamic 返回值解析(string content, dynamic 原请求);

    dynamic 文本转请求(文本[] 文本arr);

    dynamic 获取待润色数据(dynamic 原请求, dynamic 返回值);

    bool 漏翻检测(dynamic 返回值);

    void 添加上文内容(dynamic 原请求, dynamic 返回值);

    void 清空上文内容();

}
