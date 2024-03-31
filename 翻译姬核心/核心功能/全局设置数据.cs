using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬;
public class 全局设置数据 : INotifyPropertyChanged {


    public string 使用机翻 {
        get => _使用机翻;
        set {
            if (_使用机翻 != value) {
                _使用机翻 = value;
                通知更改(() => 使用机翻);
            }
        }
    }
    private string _使用机翻;

    public string 使用正则 {
        get => _使用正则;
        set {
            if (_使用正则 != value) {
                _使用正则 = value;
                通知更改(() => 使用正则);
            }
        }
    }
    private string _使用正则;

    public string 指令集名称 {
        get => _指令集名称;
        set {
            if (_指令集名称 != value) {
                _指令集名称 = value;
                通知更改(() => 指令集名称);
            }
        }
    }
    private string _指令集名称;

    public string Xml指令集名称 {
        get => _Xml指令集名称;
        set {
            if (_Xml指令集名称 != value) {
                _Xml指令集名称 = value;
                通知更改(() => Xml指令集名称);
            }
        }
    }
    private string _Xml指令集名称;

    public string 读取目录 {
        get => _读取目录;
        set {
            if (_读取目录 != value) {
                _读取目录 = value;
                通知更改(() => 读取目录);
            }
        }
    }
    private string _读取目录;

    public string 读取编码 {
        get => _读取编码;
        set {
            if (_读取编码 != value) {
                _读取编码 = value;
                通知更改(() => 读取编码);
            }
        }
    }
    private string _读取编码;

    public string 读取后缀 {
        get => _读取后缀;
        set {
            if (_读取后缀 != value) {
                _读取后缀 = value;
                通知更改(() => 读取后缀);
            }
        }
    }
    private string _读取后缀 = "*.txt|*.json|*.ks|*.lua";

    public string 源语言 {
        get => _源语言;
        set {
            if (_源语言 != value) {
                _源语言 = value;
                通知更改(() => 源语言);
            }
        }
    }
    private string _源语言;

    public string 目标语言 {
        get => _目标语言;
        set {
            if (_目标语言 != value) {
                _目标语言 = value;
                通知更改(() => 目标语言);
            }
        }
    }
    private string _目标语言;

    public bool 机翻空值使用原文 {
        get => _机翻空值使用原文;
        set {
            if (_机翻空值使用原文 != value) {
                _机翻空值使用原文 = value;
                通知更改(() => 机翻空值使用原文);
            }
        }
    }
    private bool _机翻空值使用原文;

    public bool 无视返回空值 {
        get => _无视返回空值;
        set {
            if (_无视返回空值 != value) {
                _无视返回空值 = value;
                通知更改(() => 无视返回空值);
            }
        }
    }
    private bool _无视返回空值;

    public bool 重复内容跳过 {
        get => _重复内容跳过;
        set {
            if (_重复内容跳过 != value) {
                _重复内容跳过 = value;
                通知更改(() => 重复内容跳过);
            }
        }
    }
    private bool _重复内容跳过;

    public bool 无视返回原文 {
        get => _无视返回原文;
        set {
            if (_无视返回原文 != value) {
                _无视返回原文 = value;
                通知更改(() => 无视返回原文);
            }
        }
    }
    private bool _无视返回原文;

    public bool 文本级多线程 {
        get => _文本级多线程;
        set {
            if (_文本级多线程 != value) {
                _文本级多线程 = value;
                通知更改(() => 文本级多线程);
            }
        }
    }
    private bool _文本级多线程;

    public bool 启用单组上限 {
        get => _启用单组上限;
        set {
            if (_启用单组上限 != value) {
                _启用单组上限 = value;
                通知更改(() => 启用单组上限);
            }
        }
    }
    private bool _启用单组上限;

    public int API单组上限 {
        get => _API单组上限;
        set {
            if (_API单组上限 != value) {
                _API单组上限 = value;
                通知更改(() => API单组上限);
            }
        }
    }
    private int _API单组上限 = 50;

    public string 写出目录 {
        get => _写出目录;
        set {
            if (_写出目录 != value) {
                _写出目录 = value;
                通知更改(() => 写出目录);
            }
        }
    }
    private string _写出目录;

    public string 写出编码 {
        get => _写出编码;
        set {
            if (_写出编码 != value) {
                _写出编码 = value;
                通知更改(() => 写出编码);
            }
        }
    }
    private string _写出编码;

    public string 写出格式 {
        get => _写出格式;
        set {
            if (_写出格式 != value) {
                _写出格式 = value;
                通知更改(() => 写出格式);
            }
        }
    }
    private string _写出格式;

    public bool 写出后删除源文件 {
        get => _写出后删除源文件;
        set {
            if (_写出后删除源文件 != value) {
                _写出后删除源文件 = value;
                通知更改(() => 写出后删除源文件);
            }
        }
    }
    private bool _写出后删除源文件;

    public bool 内置中括号过滤 {
        get => _内置中括号过滤;
        set {
            if (_内置中括号过滤 != value) {
                _内置中括号过滤 = value;
                通知更改(() => _内置中括号过滤);
            }
        }
    }
    private bool _内置中括号过滤;

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
