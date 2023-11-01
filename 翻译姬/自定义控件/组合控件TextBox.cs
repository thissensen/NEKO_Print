using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sunny.UI.UITextBox;

namespace 翻译姬 {
    [ToolboxItem(true)]
    public partial class 组合控件TextBox : 组合控件基类<UITextBox> {

        public 组合控件TextBox() {
            InitializeComponent();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Type 控件数据类型 => typeof(string);

        [Category("控件绑定"), Description("指示所有字符应当大写还是小写")]
        public CharacterCasing CharacterCasing {
            get {
                return TextBox.CharacterCasing;
            }
            set {
                TextBox.CharacterCasing = value;
            }
        }
        [Category("控件绑定"), Description("TextBox设置为多行")]
        public bool Multiline {
            get {
                return TextBox.Multiline;
            }
            set {
                TextBox.Multiline = value;
            }
        }
        [Category("控件绑定"), Description("TextBox行内文字上限")]
        public int MaxLength {
            get {
                return TextBox.MaxLength;
            }
            set {
                TextBox.MaxLength = value;
            }
        }
        [Category("控件绑定"), Description("输入模式为数字时，输入的上限")]
        public double Maximum {
            get {
                return TextBox.Maximum;
            }
            set {
                TextBox.Maximum = value;
            }
        }
        [Category("控件绑定"), Description("输入模式为数字时，输入的下限")]
        public double Minimum {
            get {
                return TextBox.Minimum;
            }
            set {
                TextBox.Minimum = value;
            }
        }
        [Category("控件绑定"), Description("水印文字")]
        public string Watermark {
            get {
                return TextBox.Watermark;
            }
            set {
                TextBox.Watermark = value;
            }
        }
        [Category("控件绑定"), Description("浮点或整形输入时是否允许空值")]
        public bool CanEmpty {
            get {
                return TextBox.CanEmpty;
            }
            set {
                TextBox.CanEmpty = value;
            }
        }
        [Category("控件绑定"), Description("浮点输入时，小数的位数")]
        public int DecimalPlaces {
            get {
                return TextBox.DecimalPlaces;
            }
            set {
                TextBox.DecimalPlaces = value;
            }
        }
        [Category("控件绑定"), Description("文本输入类型")]
        public UIEditType Type {
            get {
                return TextBox.Type;
            }
            set {
                TextBox.Type = value;
            }
        }
        [Category("行为")]
        public bool ReadOnly {
            get {
                return TextBox.ReadOnly;
            }
            set {
                TextBox.ReadOnly = value;
            }
        }
        [Category("数据库"), Description("数值类型时为0是否抛出异常")]
        public bool 是否允许为零 { get; set; } = true;

        public new event KeyPressEventHandler KeyPress {
            add {
                TextBox.KeyPress += value;
            }
            remove {
                TextBox.KeyPress -= value;
            }
        }

        public new void Focus() {
            TextBox.Focus();
        }
        protected override async void OnEnter(EventArgs e) {
            base.OnEnter(e);
            if (this.FindPage()?.IsShown ?? false) {
                await Task.Delay(50);
                TextBox.Focus();
            }
        }

    }
}
