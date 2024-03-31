using System;
using System.Collections.Generic;
using System.Text;

namespace 翻译姬;
internal interface GPT数据处理接口 {

    GPT设置数据 GPT设置数据 { get; set; }
    Dictionary<string, string> 上文内容 { get; set; }

    List<dynamic> 获取请求内容(params dynamic[] data);

    /// <summary>
    /// 传入GPT返回.Choices[0].Message.Content
    /// </summary>
    dynamic 返回值解析(string content);

}
