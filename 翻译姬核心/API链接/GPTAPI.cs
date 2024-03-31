using System;
using System.Collections.Generic;
using System.Text;

namespace 翻译姬;
public class GPTAPI : API接口模板 {

    private static GPT设置数据 GPT设置数据 => 全局数据.GPT设置数据;

    public GPTAPI(API信息 data) : base(data) {
        data.QPS = GPT设置数据.次数限制;
        GPT设置数据.必要数据验证();
    }

    protected override string[] 机翻(string[] 传入文本) {
        throw new NotImplementedException();
    }
    public string[] GPT机翻(文本[] 待机翻) {
        return null;
    }
}
