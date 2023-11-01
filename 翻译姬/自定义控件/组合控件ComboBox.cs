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
using static Sunny.UI.UIComboBox;

namespace 翻译姬 {
    [ToolboxItem(true)]
    public partial class 组合控件ComboBox : 组合控件基类<自定义ComboBox> {

        public 组合控件ComboBox() {
            InitializeComponent();
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Type 控件数据类型 => typeof(string);

        [Category("控件绑定"), Description("打开文本筛选，输文字自动模糊查询")]
        public bool ShowFilter {
            get {
                return ComboBox.ShowFilter;
            }
            set {
                ComboBox.ShowFilter = value;
            }
        }

        /// <summary>
        /// 获取选中值，没设置绑定默认返回Text
        /// </summary>
        [Category("控件绑定"), Description("获取所选值，有DataSource时获取ValueMember绑定值，否则返回Text")]
        public object SelectedValue {
            get {
                if (ComboBox.SelectedValue == null) {
                    return ComboBox.Text;
                }
                return ComboBox.SelectedValue;
            }
            set {
                ComboBox.SelectedValue = value;
            }
        }

        [Category("控件绑定"), Description("显示值")]
        public string DisplayMember {
            get {
                return ComboBox.DisplayMember;
            }
            set {
                ComboBox.DisplayMember = value;
            }
        }

        [Category("控件绑定"), Description("内部取值")]
        public string ValueMember {
            get {
                return ComboBox.ValueMember;
            }
            set {
                ComboBox.ValueMember = value;
            }
        }

        [Category("控件绑定"), Description("数据源")]
        public object DataSource {
            get {
                return ComboBox.DataSource;
            }
            set {
                ComboBox.DataSource = value;
            }
        }

        [Category("控件绑定"), Description("文字对齐方向")]
        public ContentAlignment TextAlignment {
            get {
                return ComboBox.TextAlignment;
            }
            set {
                ComboBox.TextAlignment = value;
            }
        }
        [Category("控件绑定"), Description("水印文字")]
        public string Watermark {
            get {
                return ComboBox.Watermark;
            }
            set {
                ComboBox.Watermark = value;
            }
        }

        [Category("控件绑定"), Description("下拉框显示样式")]
        public UIDropDownStyle DropDownStyle {
            get {
                return ComboBox.DropDownStyle;
            }
            set {
                ComboBox.DropDownStyle = value;
            }
        }

        public new event KeyPressEventHandler KeyPress {
            add {
                ComboBox.KeyPress += value;
            }
            remove {
                ComboBox.KeyPress -= value;
            }
        }

    }
}
