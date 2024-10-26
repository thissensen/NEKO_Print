using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬;
public class GPT设置数据 : INotifyPropertyChanged {

    public bool 是否Https {
        get => _是否Https;
        set {
            if (_是否Https != value) {
                _是否Https = value;
                通知更改(() => 连接域名);
            }
        }
    }
    private bool _是否Https = true;

    public string 连接域名 {
        get => _连接域名;
        set {
            if (_连接域名 != value) {
                _连接域名 = value?.Trim();
                通知更改(() => 连接域名);
            }
        }
    }
    private string _连接域名;

    public string 连接路由 {
        get => _连接路由;
        set {
            if (_连接路由 != value) {
                _连接路由 = value?.Trim();
                通知更改(() => 连接路由);
            }
        }
    }
    private string _连接路由 = "/v1/chat/completions";

    public string 使用模型 {
        get => _使用模型;
        set {
            if (_使用模型 != value) {
                _使用模型 = value;
                通知更改(() => 使用模型);
            }
        }
    }
    private string _使用模型 = "gpt-3.5-turbo";

    public int 次数限制 {
        get => _次数限制;
        set {
            if (_次数限制 != value) {
                _次数限制 = value;
                通知更改(() => 次数限制);
            }
        }
    }
    private int _次数限制 = 3;

    public int Token限制 {
        get => _Token限制;
        set {
            if (_Token限制 != value) {
                _Token限制 = value;
                通知更改(() => Token限制);
            }
        }
    }
    private int _Token限制 = 40000;

    public int 请求等待延迟 {
        get => _请求等待延迟;
        set {
            if (_请求等待延迟 != value) {
                _请求等待延迟 = value;
                通知更改(() => 请求等待延迟);
            }
        }
    }
    private int _请求等待延迟 = 60;

    public int 单次机翻行 {
        get => _单次机翻行;
        set {
            if (_单次机翻行 != value) {
                _单次机翻行 = value;
                通知更改(() => 单次机翻行);
            }
        }
    }
    private int _单次机翻行 = 12;

    public bool 上下文提示 {
        get => _上下文提示;
        set {
            if (_上下文提示 != value) {
                _上下文提示 = value;
                通知更改(() => 上下文提示);
            }
        }
    }
    private bool _上下文提示 = true;

    public int 上下文深度 {
        get => _上下文深度;
        set {
            if (_上下文深度 != value) {
                _上下文深度 = value;
                通知更改(() => 上下文深度);
            }
        }
    }
    private int _上下文深度 = 1;

    public int 错行重试数 {
        get => _错行重试数;
        set {
            if (_错行重试数 != value) {
                _错行重试数 = value;
                通知更改(() => 错行重试数);
            }
        }
    }
    private int _错行重试数 = 10;

    public int 异常重试上限 {
        get => _异常重试上限;
        set {
            if (_异常重试上限 != value) {
                _异常重试上限 = value;
                通知更改(() => 异常重试上限);
            }
        }
    }
    private int _异常重试上限 = 10;

    public bool 漏翻检测 {
        get => _漏翻检测;
        set {
            if (_漏翻检测 != value) {
                _漏翻检测 = value;
                通知更改(() => 漏翻检测);
            }
        }
    }
    private bool _漏翻检测 = true;

    public bool 错误跳过 {
        get => _错误跳过;
        set {
            if (_错误跳过 != value) {
                _错误跳过 = value;
                通知更改(() => _错误跳过);
            }
        }
    }
    private bool _错误跳过;

    public int 漏翻重试次数 {
        get => _漏翻重试次数;
        set {
            if (_漏翻重试次数 != value) {
                _漏翻重试次数 = value;
                通知更改(() => 漏翻重试次数);
            }
        }
    }
    private int _漏翻重试次数 = 3;

    public bool 连续对话合并 {
        get => _连续对话合并;
        set {
            if (_连续对话合并 != value) {
                _连续对话合并 = value;
                通知更改(() => 连续对话合并);
            }
        }
    }
    private bool _连续对话合并 = true;

    public bool 相邻对话合并 {
        get => _相邻对话合并;
        set {
            if (_相邻对话合并 != value) {
                _相邻对话合并 = value;
                通知更改(() => 相邻对话合并);
            }
        }
    }
    private bool _相邻对话合并 = true;

    public bool 输出人名优先词汇表 {
        get => _输出人名优先词汇表;
        set {
            if (_输出人名优先词汇表 != value) {
                _输出人名优先词汇表 = value;
                通知更改(() => 输出人名优先词汇表);
            }
        }
    }
    private bool _输出人名优先词汇表 = true;

    //原文-译文-备注
    public DataTable GPT词汇表 { get; set; } = new DataTable();

    public string 合并分隔符 {
        get => _合并分隔符;
        set {
            if (_合并分隔符 != value) {
                _合并分隔符 = value;
                通知更改(() => 合并分隔符);
            }
        }
    }
    private string _合并分隔符 = @"\r\n";

    public string 语境 {
        get => _语境;
        set {
            if (_语境 != value) {
                _语境 = value;
                通知更改(() => 语境);
            }
        }
    }
    private string _语境;

    public bool 简易模式 {
        get => _简易模式;
        set {
            if (_简易模式 != value) {
                _简易模式 = value;
                通知更改(() => 简易模式);
            }
        }
    }
    private bool _简易模式;

    public bool 翻后润色 {
        get => _翻后润色;
        set {
            if (_翻后润色 != value) {
                _翻后润色 = value;
                通知更改(() => 翻后润色);
            }
        }
    }
    private bool _翻后润色;

    public string 润色语境 {
        get => _润色语境;
        set {
            if (_润色语境 != value) {
                _润色语境 = value;
                通知更改(() => 润色语境);
            }
        }
    }
    private string _润色语境;


    public void 必要数据验证() {
        if (string.IsNullOrWhiteSpace(连接域名)) {
            throw new Exception("请前往GPT设置填写连接域名");
        }
        if (string.IsNullOrWhiteSpace(使用模型)) {
            throw new Exception("请前往GPT设置填写使用模型");
        }
        if (string.IsNullOrWhiteSpace(语境)) {
            throw new Exception("请前往GPT设置填写语境，使GPT返回Json格式");
        }
        if (连续对话合并 && string.IsNullOrWhiteSpace(合并分隔符)) {
            throw new Exception("连续对话合并情况下必须设置合并分隔符");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void 通知更改<T>(Expression<Func<T>> property) {
        if (PropertyChanged == null)
            return;

        var memberExpression = property.Body as MemberExpression;
        if (memberExpression == null)
            return;

        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
    }
}
