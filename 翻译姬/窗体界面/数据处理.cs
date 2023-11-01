using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
public partial class 数据处理 : 自定义Form {

    #region 拖动panel移动窗口
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();

    [DllImport("user32.DLL", EntryPoint = "PostMessage")]
    private extern static void PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    //Panel控件,鼠标点击时移动窗口位置,MouseDown事件
    private void 移动窗口_MouseDown(object sender, MouseEventArgs e) { ReleaseCapture(); PostMessage(this.Handle, 0x112, 0xf012, 0); }
    #endregion

    private 文件结构[] 处理中文件结构 = new 文件结构[0];
    private 文本组[] 显示中文本组 = new 文本组[0];
    private int 显示中文本组下标 {
        get => _显示中文本组下标;
        set {
            _显示中文本组下标 = value;
            表格显示(_显示中文本组下标);
            页数显示Label.Text = $"第{显示中文本组下标 + 1}/{显示中文本组.Length}页";
        }
    }
    private int _显示中文本组下标;

    public 数据处理(文件结构[] 处理中文件结构) {
        this.处理中文件结构 = 处理中文件结构;
        InitializeComponent();
    }

    private void 数据处理_Load(object sender, EventArgs e) {
        //居中显示
        var rec = Screen.GetWorkingArea(this);
        int W = rec.Width;
        int H = rec.Height;
        Location = new Point {
            X = (W - Width) / 2,
            Y = (H - Height) / 2
        };
        数据刷新();
    }

    private void 数据刷新() {
        if (处理中文件结构 == null) {
            return;
        }
        表格刷新(new 文本组[0]);
        //文件列表刷新
        文件列表Panel.Clear();
        foreach (var 文件 in 处理中文件结构) {
            var 文本 = 文件.文本组.FirstOrDefault(t => !t.完成状态);
            if (!只显示未完成数据Switch.Active && 文本 == null) {//已完成
                文件列表Panel.Add(获取按钮(文件, true));
            } else {
                文件列表Panel.Add(获取按钮(文件, false));
            }
        }
        if (文件列表Panel.Controls.Count > 0) {
            var btn = 文件列表Panel.Get(0) as UIButton;
            btn.PerformClick();
        }
    }

    private void 表格刷新(文本组[] 文本组) {
        if (只显示未完成数据Switch.Active) {
            显示中文本组 = 文本组.Where(t => !t.完成状态).ToArray();
        } else {
            显示中文本组 = 文本组;
        }
        if (显示中文本组.Length > 0) {
            显示中文本组下标 = 0;
        } else {
            显示中文本组下标 = -1;
        }
    }

    private void 表格显示(int 显示下标) {
        表格.DataTable?.Rows.Clear();
        if (显示下标 < 0 || 显示下标 >= 显示中文本组.Length) {
            按钮状态检测();
            return;
        }
        文本组 文本 = 显示中文本组[显示下标];
        DataTable dt = new DataTable();
        dt.Columns.Add("原文");
        dt.Columns.Add("译文");
        int max = 文本.原文.Length;
        if (max < 文本.译文?.Length) {
            max = 文本.译文.Length;
        }
        for (int i = 0; i < max; i++) {
            DataRow dr = dt.NewRow();
            dr["原文"] = 文本.原文.ElementAtOrDefault(i);
            dr["译文"] = 文本.译文?.ElementAtOrDefault(i);
            dt.Rows.Add(dr);
        }
        表格.DataTable = dt;
        //异常状态显示
        if (文本.异常状态.Count > 0) {
            foreach (var item in 文本.异常状态) {
                if (item.Item2 != -1) {
                    表格.Rows[item.Item2].DefaultCellStyle.BackColor = 全局字符串.不可用时颜色;
                } else {
                    for (int i = 0; i < 表格.Rows.Count; i++) {
                        表格.Rows[i].DefaultCellStyle.BackColor = 全局字符串.不可用时颜色;
                    }
                    break;
                }
            }
        }
        按钮状态检测();
    }

    private void 表格数据保存() {
        文本组 文本 = 显示中文本组.ElementAtOrDefault(显示中文本组下标);
        if (文本 == null) {
            return;
        }
        var 译文list = new List<string>();
        for (int i = 0; i < 表格.Rows.Count; i++) {
            string 译文 = 表格.Rows[i].Cells["译文"].EditedFormattedValue.ToString();
            译文list.Add(译文);
        }
        文本.译文 = 译文list.ToArray();
    }

    private void 上一页() {
        表格数据保存();
        显示中文本组下标 -= 1;
        按钮状态检测();
    }

    private void 下一页() {
        表格数据保存();
        显示中文本组下标 += 1;
        按钮状态检测();
    }

    private void 上下移动(bool 上移) {
        var cells = 表格.获取所选单元格().OrderBy(cell => cell.RowIndex).ToList();
        if (cells.Count() == 0) {
            return;
        }
        try {
            int 当前行下标 = -1;
            foreach (DataGridViewCell cell in cells) {
                if (表格.Columns[cell.ColumnIndex].HeaderText == "原文") {
                    throw new Exception("只能选择译文进行上下移动");
                }
                if (当前行下标 != -1 && 当前行下标 != cell.OwningRow.Index - 1) {
                    throw new Exception("只能移动连续的单元格");
                }
                当前行下标 = cell.RowIndex;
            }
            if (上移) {
                if (cells[0].RowIndex == 0) {
                    return;
                }
                if (表格.Rows[cells[0].RowIndex - 1].Cells["译文"].EditedFormattedValue.ToString() != "") {
                    throw new Exception("上方已有译文，无法移动");
                }
                表格.取消选中();
                foreach (DataGridViewCell cell in cells) {
                    表格.Rows[cell.RowIndex - 1].Cells["译文"].Selected = true;
                    表格.Rows[cell.RowIndex - 1].Cells["译文"].Value = cell.EditedFormattedValue;
                    cell.Value = "";
                }
            } else {
                int 最下行下标 = cells[cells.Count - 1].RowIndex;
                if (表格.Rows.Count - 1 < 最下行下标 + 1) {
                    return;
                }
                if (表格.Rows[最下行下标 + 1].Cells["译文"].EditedFormattedValue.ToString() != "") {
                    throw new Exception("下方已有译文，无法移动");
                }
                表格.取消选中();
                for (int i = cells.Count - 1; i >= 0; i--) {
                    DataGridViewCell cell = cells[i];
                    表格.Rows[cell.RowIndex + 1].Cells["译文"].Selected = true;
                    表格.Rows[cell.RowIndex + 1].Cells["译文"].Value = cell.EditedFormattedValue;
                    cell.Value = "";
                }
            }
            表格数据保存();
        } catch (Exception ex) {
            消息框帮助.轻便消息(ex.Message, this);
        }
    }

    private void 按钮状态检测() {
        //上一页检测
        if (显示中文本组.ElementAtOrDefault(显示中文本组下标 - 1) == null) {
            上一页Btn.Enabled = false;
        } else {
            上一页Btn.Enabled = true;
        }
        //下一页检测
        if (显示中文本组.ElementAtOrDefault(显示中文本组下标 + 1) == null) {
            下一页Btn.Enabled = false;
        } else {
            下一页Btn.Enabled = true;
        }
        //异常行检测
        var res = from 文件 in 处理中文件结构
                  let 文本 = 文件.文本组.FirstOrDefault(文本 => 文本.异常状态.Count > 0)
                  where 文本 != null
                  select 文本;
        if (!下一页Btn.Enabled && 显示中文本组下标 != -1) {//最后一页，额外检测
            文本组 文本 = 显示中文本组[显示中文本组下标];
            int 最大异常下标 = (from 异常状态 in 文本.异常状态
                          orderby 异常状态.Item2 descending
                          select 异常状态.Item2).FirstOrDefault();
            if (表格.FirstDisplayedScrollingRowIndex + 1 > 最大异常下标) {
                异常行Btn.Enabled = false;
            } else {
                异常行Btn.Enabled = true;
            }
        } else {
            if (res.Count() == 0) {
                异常行Btn.Enabled = false;
            } else {
                异常行Btn.Enabled = true;
            }
        }
    }

    private UISymbolButton 获取按钮(文件结构 文件, bool 完成状态) {
        var btn = new UISymbolButton();
        btn.Width = 文件列表Panel.Width - 50;
        btn.Height *= 2;
        if (!完成状态) {
            btn.Symbol = 57441;
        }
        btn.SymbolOffset = new Point(btn.Width / -2 + btn.SymbolSize, btn.SymbolOffset.Y);
        btn.TextAlign = ContentAlignment.MiddleRight;
        btn.Text = 文件.文件名;
        btn.Tag = 文件;
        btn.主题设置();
        btn.Click += (_, _) => 表格刷新(文件.文本组);
        btn.Click += 文件按钮_Click;
        return btn;
    }

    private UIButton 获取所选Btn() {
        foreach (var con in 文件列表Panel.Panel.Controls) {
            var btn = con as UIButton;
            if (btn.Selected) {
                return btn;
            }
        }
        return null;
    }

    private void 文件按钮_Click(object sender, EventArgs e) {
        文件列表Panel.Panel.全部取消选中();
        (sender as UIButton).Selected = true;
    }

    private void 返回Btn_Click(object sender, EventArgs e) {
        Close();
    }

    private void 只显示未完成数据Switch_ActiveChanged(object sender, EventArgs e) => 数据刷新();

    private void 上一页Btn_Click(object sender, EventArgs e) => 上一页();

    private void 下一页Btn_Click(object sender, EventArgs e) => 下一页();

    private void 上移Btn_Click(object sender, EventArgs e) {
        上下移动(true);
    }

    private void 下移Btn_Click(object sender, EventArgs e) {
        上下移动(false);
    }

    private void 保存Btn_Click(object sender, EventArgs e) {
        var btn = 获取所选Btn();
        if (btn == null) {
            return;
        }
        var 文件 = btn.Tag as 文件结构;
        if (MessageBoxEx.Show($"是否将【{文件.文件名}】保存到输出目录？", "显示按钮", 提示窗按钮.确认取消)) {
            表格数据保存();
            保存文件(文件);
            消息框帮助.轻便消息("保存成功", this);
        }
    }

    private void 全部保存Btn_Click(object sender, EventArgs e) {
        if (处理中文件结构.Length == 0) {
            return;
        }
        if (MessageBoxEx.Show("是否将所有文件保存到输出目录？", "显示按钮", 提示窗按钮.确认取消)) {
            表格数据保存();
            foreach (UIButton btn in 文件列表Panel.Panel.Controls) {
                var 文件 = btn.Tag as 文件结构;
                保存文件(文件);
            }
            消息框帮助.轻便消息("保存成功", this);
        }
    }

    private void 保存文件(文件结构 文件) {
        foreach (var 文本 in 文件.文本组) {
            文本.异常状态.Clear();
        }
        if (文件.Json文本 == null) {
            调用管理.文本写出(写出方式.本地写出, 文件);
        } else {
            调用管理.文本写出(写出方式.Json写出, 文件);
        }
    }

    private void 重翻Btn_Click(object sender, EventArgs e) {
        if (表格.Rows.Count == 0) {
            return;
        }
        var btn = 获取所选Btn();
        if (btn == null) {
            return;
        }
        try {
            var 文件 = btn.Tag as 文件结构;
            Type API类型 = 文件.处理数据.API类型;
            string API名称 = API类型.Name.Substring(0, API类型.Name.Length - 3);
            DataRow 明细row = 数据库.Select($"select * from API明细 where 是否启用=1 and 类型='{API名称}'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (明细row == null) {
                throw new Exception($"【{API名称}】没有可用的账号，请前往添加并开启");
            }
            var 待机翻 = new List<string>();
            for (int i = 0; i < 表格.Rows.Count; i++) {
                string text = 表格.Rows[i].Cells["原文"].FormattedValue.ToString();
                if (text.Trim() != "") {
                    待机翻.Add(text);
                }
            }
            API信息 data = API信息.Parse(明细row);
            API接口模板 api = Activator.CreateInstance(API类型, data) as API接口模板;
            string[] 机翻完 = api.文本机翻(待机翻.ToArray());
            if (机翻完.Length != 待机翻.Count) {
                消息框帮助.轻便消息("机翻内容依旧错行", this);
            }
            if (机翻完.Length <= 待机翻.Count) {
                for (int i = 0; i < 表格.Rows.Count; i++) {
                    表格.Rows[i].Cells["译文"].Value = 机翻完.ElementAtOrDefault(i);
                }
            } else {
                //扩充表格行
                for (int i = 0; i < 机翻完.Length - 待机翻.Count; i++) {
                    DataRow dr = 表格.DataTable.NewRow();
                    表格.DataTable.Rows.Add(dr);
                }
                for (int i = 0; i < 表格.Rows.Count; i++) {
                    表格.Rows[i].Cells["译文"].Value = 机翻完[i];
                }
            }
            消息框帮助.轻便消息("重翻成功", this);
        } catch (Exception ex) {
            MessageBoxEx.Show(ex.Message);
        }
    }

    private void 异常行Btn_Click(object sender, EventArgs e) {
异常行开始:
        文本组 文本 = 显示中文本组[显示中文本组下标];
        var res = from 异常状态 in 文本.异常状态
                where 异常状态.Item2 > 表格.FirstDisplayedScrollingRowIndex + 1
                select 异常状态.Item2;
        if (res.Count() == 0) {//当前文本组没有了
            if (显示中文本组下标 != 显示中文本组.Length - 1) {
                下一页();
                goto 异常行开始;
            }
        } else {//定位到当前文本组的下一个异常
            表格.FirstDisplayedScrollingRowIndex = res.First();
        }
    }
}
