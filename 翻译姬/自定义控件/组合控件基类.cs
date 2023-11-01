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

namespace 翻译姬 {
    [ToolboxItem(false)]
    public partial class 组合控件基类<T> : UserControl, 组合控件基类接口 where T : Control {


        public 组合控件基类() {
            InitializeComponent();
            LabelPanel.Name = "";
        }

        private void 数据库控件基类_Load(object sender, EventArgs e) {
            Label方向修改();
            if (DesignMode) {
                return;
            }
            this.DataBindings.Add(new Binding("Text", 获取控件(), "Text"));
        }

        private void Label方向修改() {
            T t = 获取控件();
            if (t == null) {
                return;
            }
            Size 原始大小 = t.Size;
            if (_Label方向 == 数据库控件Label方向.左) {
                //上到左
                t.Dock = DockStyle.None;
                LabelPanel.Dock = DockStyle.None;
                t.Size = 原始大小;
                Width = Label.Width + t.Width;
                Height = t.Height;
                LabelPanel.Width = Label.Width;
                LabelPanel.Dock = DockStyle.Left;
                Label.Location = new Point(0, 4);//定位到左边
                t.Dock = DockStyle.Fill;

            } else {//左到上

                t.Dock = DockStyle.None;
                LabelPanel.Dock = DockStyle.None;
                t.Size = 原始大小;
                Width = t.Width;
                Height = Label.Height + t.Height;
                LabelPanel.Height = Label.Height;
                LabelPanel.Dock = DockStyle.Top;
                Label.Location = new Point(0, 0);
                t.Dock = DockStyle.Fill;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual object 默认值 { get; set; } = "";
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Type 控件数据类型 { get; }
        [Description("选择Label在控件左边还是上面")]
        public 数据库控件Label方向 Label方向 {
            get {
                return _Label方向;
            }
            set {
                _Label方向 = value;
                Label方向修改();
            }
        }
        private 数据库控件Label方向 _Label方向 = 数据库控件Label方向.左;
        [Description("Label的Text")]
        public string LabelText {
            get {
                return Label.Text;
            }
            set {
                Label.Text = value;
                if (Label方向 == 数据库控件Label方向.左) {
                    int 差值 = Label.Width - LabelPanel.Width;
                    if (差值 == 0) {
                        return;
                    }
                    LabelPanel.Width += 差值;
                    Width += 差值;
                }
            }
        }
        [Description("Label的Font")]
        public Font LabelFont {
            get {
                return Label.Font;
            }
            set {
                Label.Font = value;
            }
        }
        [Description("主控件的Font")]
        public Font 主控件Font {
            get {
                return 获取控件()?.Font;
            }
            set {
                T t = 获取控件();
                if (t != null) {
                    t.Font = value;
                }
            }
        }
        [Description("主控件的Name")]
        public new string Name {
            get {
                return base.Name;
            }
            set {
                base.Name = value;
                T t = 获取控件();
                if (t != null) {
                    t.Name = value + "_主控件";
                }
                Label.Name = value + "_Label";
                LabelPanel.Name = value + "_LabelPanel";
            }
        }
        [Browsable(true), Description("主控件的Text")]
        public virtual string 主控件Text {
            get {
                return 获取控件()?.Text;
            }
            set {
                T t = 获取控件();
                if (t != null) {
                    t.Text = value;
                }
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text {
            get {
                return 主控件Text;
            }
            set {
                主控件Text = value;
            }
        }
        [Description("该控件是否作为判断条件")]
        public bool 是否判断条件 { get; set; }
        [Description("该控件是否序列化")]
        public bool 是否序列化 { get; set; }
        public Control 主控件 => 获取控件();
        public UIPanel LabelPanel控件 => LabelPanel;

        private T 获取控件() {
            if (Controls.Count > 0) {
                return Controls[0] as T;
            }  else {
                return null;
            }
        }
    }
    public enum 数据库控件Label方向 {
        左,
        上
    }
}
