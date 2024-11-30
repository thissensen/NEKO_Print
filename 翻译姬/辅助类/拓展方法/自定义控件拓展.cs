using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 翻译姬 {
    public static class 自定义控件拓展 {

        public static void 全部取消选中(this Panel panel) {
            Stack<Control> stack = new Stack<Control>();
            stack.Push(panel);
            while (stack.Count > 0) {
                Control con = stack.Pop();
                foreach (Control c in con.Controls) {
                    if (c is UIButton btn) {
                        btn.Selected = false;
                    } else if (c is UIImageButton imgBtn) {
                        imgBtn.Selected = false;
                    } else if (c is UISymbolButton symbolButton) {
                        symbolButton.Selected = false;
                    }
                    if (c.Controls.Count > 0) {
                        stack.Push(c);
                    }
                }
            }
        }

        public static IEnumerable<T> 获取全部控件<T>(this Control control) where T : Control {
            Stack<Control> stack = new Stack<Control>();
            stack.Push(control);
            while (stack.Count > 0) {
                Control con = stack.Pop();
                foreach (Control c in con.Controls) {
                    if (c is T t) {
                        yield return t;
                    } 
                    if (c.Controls.Count > 0) {
                        stack.Push(c);
                    }
                }
            }
        }

        public static 自定义Page FindPage(this Control con) {
            return con.FindForm() as 自定义Page;
        }
        public static void DPI自适应(this 自定义Form form, Dictionary<string, Control> Name_Control) {
            DPI设置(form, Name_Control);
        }
        public static void DPI自适应(this 自定义Page page, Dictionary<string, Control> Name_Control) {
            DPI设置(page, Name_Control);
        }

        private static void 主题设置(Dictionary<string, Control> Name_Control) {
            /*
             * 其他设置点：列控件Switch、自定义Swutch、主界面、文本翻译
             */
            foreach (var kv in Name_Control) {
                kv.Value.主题设置();
            }
        }
        public static void 主题设置(this Control con) {
            if (!主界面.主题设置控件.Contains(con)) {
                主界面.主题设置控件.Add(con);
            }
            if (con.IsDisposed) {
                return;
            }
            //con.Disposed += (_, _) => 主界面.主题设置控件.Remove(con);
            //设置全局字体
            float 字体大小 = con.Font.Size;
            con.Font = new Font(全局字符串.主字体, 字体大小);
            if (con is 自定义Page page) {
                page.AutoScaleMode = AutoScaleMode.None;
                page.BackColor = 全局字符串.背景色;
                主题设置(page.Name_Control);
            } else if (con is 自定义Form form) {
                form.AutoScaleMode = AutoScaleMode.None;
                form.BackColor = 全局字符串.背景色;
                form.RectColor = 全局字符串.主题色;
                form.TitleColor = 全局字符串.次级主题色;//标题背景色
                form.TitleForeColor = 全局字符串.主题色;//标题字颜色
                form.ControlBoxForeColor = 全局字符串.主题色;//按钮字颜色
                主题设置(form.Name_Control);
            }
            if (con is UIUserControl userControl) {
                userControl.BackColor = 全局字符串.背景色;
                userControl.FillColor = 全局字符串.背景色;
                userControl.FillColor2 = 全局字符串.背景色;
                userControl.FillDisableColor = 全局字符串.背景色;
                userControl.ForeColor = 全局字符串.主题色;
                userControl.RectColor = 全局字符串.主题色;
                userControl.RectDisableColor = 全局字符串.不可用时颜色;
                
            } else if (con is UIProcessBar processBar) {
                processBar.FillColor = 全局字符串.背景色;
                processBar.ForeColor = 全局字符串.主题色;
                processBar.RectColor = 全局字符串.主题色;

            } else if (con is UINavMenu navMenu) {
                navMenu.BackColor = 全局字符串.背景色;
                navMenu.FillColor = 全局字符串.背景色;
                navMenu.ForeColor = 全局字符串.主题色;
                navMenu.HoverColor = 全局字符串.次级主题色;//鼠标移上色
                navMenu.ScrollBarColor = 全局字符串.主题色;
                navMenu.ScrollBarHoverColor = 全局字符串.背景色;//鼠标移上填充色
                navMenu.ScrollBarPressColor = 全局字符串.背景色;//鼠标按下填充色
                navMenu.ScrollFillColor = 全局字符串.背景色;//填充色
                navMenu.SecondBackColor = 全局字符串.背景色;
                navMenu.SelectedColor = 全局字符串.次级主题色;//选中背景色
                navMenu.SelectedColor2 = 全局字符串.次级主题色;//填充色
                navMenu.SelectedForeColor = 全局字符串.主题色;//选中字体色
                navMenu.SelectedHighColor = 全局字符串.主题色;//选中高亮色，左边色块
                navMenu.TipsForeColor = 全局字符串.主题色;

            } else if (con is UILabel label) {
                label.ForeColor = 全局字符串.主题色;
                label.BackColor = 全局字符串.背景色;

                label.UseCompatibleTextRendering = true;//使用自定义字体

            } else if (con is UISymbolLabel symbolLabel) {
                symbolLabel.ForeColor = 全局字符串.主题色;
                symbolLabel.BackColor = 全局字符串.背景色;
                symbolLabel.SymbolColor = 全局字符串.主题色;

            } else if (con is UISwitch uiSwitch) {
                uiSwitch.ActiveColor = 全局字符串.主题色;
                uiSwitch.BackColor = 全局字符串.背景色;
                uiSwitch.ButtonColor = 全局字符串.主题色;
                uiSwitch.ForeColor = 全局字符串.主题色;
                uiSwitch.InActiveColor = 全局字符串.次级主题色;

            } else if (con is UIButton button) {
                button.FillColor = 全局字符串.背景色;
                button.ForeColor = 全局字符串.主题色;
                button.RectColor = 全局字符串.次级主题色;
                button.ForeHoverColor = 全局字符串.主题色;//鼠标移上字体色
                button.FillHoverColor = 全局字符串.背景色;//鼠标移上填充色
                button.RectHoverColor = 全局字符串.主题色;//鼠标移上边框色
                button.FillPressColor = 全局字符串.背景色;//按下时背景色
                button.RectPressColor = 全局字符串.主题色;//按下时边框色
                button.ForePressColor = 全局字符串.主题色;//按下时字体色
                button.FillDisableColor = 全局字符串.背景色;//不可用时
                button.RectDisableColor = 全局字符串.不可用时颜色;//不可用时
                button.ForeDisableColor = 全局字符串.不可用时颜色;//不可用时
                button.FillSelectedColor = 全局字符串.次级主题色;
                button.ForeSelectedColor = 全局字符串.主题色;
                button.RectSelectedColor = 全局字符串.主题色;

            } else if (con is UILine line) {
                line.FillColor = Color.Transparent;
                line.BackColor = Color.Transparent;
                line.LineColor = 全局字符串.主题色;

            } else if (con is 自定义DataGridView view) {
                view.BackgroundColor = 全局字符串.背景色;
                //奇偶行背景色
                //view.StripeEvenColor = 全局字符串.背景色;//偶数行背景色
                //view.StripeOddColor = 全局字符串.背景色;//奇数行背景色
                ////滚动条
                //view.ScrollBarColor = 全局字符串.主题色;
                //view.ScrollBarBackColor = 全局字符串.背景色;
                //view.ScrollBarRectColor = 全局字符串.主题色;
                //标题行
                view.ColumnHeadersDefaultCellStyle.BackColor = 全局字符串.背景色;//标题行背景色
                view.ColumnHeadersDefaultCellStyle.ForeColor = 全局字符串.主题色;
                view.ColumnHeadersDefaultCellStyle.Font = new Font(全局字符串.主字体, view.ColumnHeadersDefaultCellStyle.Font.Size);
                //单元格线条
                view.GridColor = 全局字符串.主题色;//单元格线条色
                //奇数行
                view.AlternatingRowsDefaultCellStyle.BackColor = 全局字符串.背景色;//奇数行背景色
                view.AlternatingRowsDefaultCellStyle.ForeColor = 全局字符串.主题色;//奇数行文字色
                view.AlternatingRowsDefaultCellStyle.SelectionBackColor = 全局字符串.背景色;
                view.AlternatingRowsDefaultCellStyle.SelectionForeColor = 全局字符串.主题色;
                if (view.AlternatingRowsDefaultCellStyle.Font != null) {
                    view.AlternatingRowsDefaultCellStyle.Font = new Font(全局字符串.主字体, view.AlternatingRowsDefaultCellStyle.Font.Size);
                }
                //默认行
                view.RowsDefaultCellStyle.BackColor = 全局字符串.背景色;//单元格背景色
                view.RowsDefaultCellStyle.ForeColor = 全局字符串.主题色;//单元格文字色
                view.RowsDefaultCellStyle.SelectionForeColor = 全局字符串.主题色;//选中字颜色
                view.RowsDefaultCellStyle.SelectionBackColor = 全局字符串.次级主题色;//选中背景色
                if (view.RowsDefaultCellStyle.Font != null) {
                    view.RowsDefaultCellStyle.Font = new Font(全局字符串.主字体, view.RowsDefaultCellStyle.Font.Size);
                }
                //默认新增行
                view.RowTemplate.DefaultCellStyle.BackColor = 全局字符串.背景色;
                view.RowTemplate.DefaultCellStyle.ForeColor = 全局字符串.主题色;
                view.RowTemplate.DefaultCellStyle.SelectionForeColor = 全局字符串.主题色;//选中字颜色
                view.RowTemplate.DefaultCellStyle.SelectionBackColor = 全局字符串.深级主题色;//选中背景色
                if (view.RowTemplate.DefaultCellStyle.Font != null) {
                    view.RowTemplate.DefaultCellStyle.Font = new Font(全局字符串.主字体, view.RowTemplate.DefaultCellStyle.Font.Size);
                }
                //行标题
                view.RowHeadersDefaultCellStyle.BackColor = 全局字符串.背景色;
                view.RowHeadersDefaultCellStyle.ForeColor = 全局字符串.主题色;
                view.RowHeadersDefaultCellStyle.SelectionBackColor = 全局字符串.背景色;
                view.RowHeadersDefaultCellStyle.SelectionForeColor = 全局字符串.主题色;
                if (view.RowHeadersDefaultCellStyle.Font != null) {
                    view.RowHeadersDefaultCellStyle.Font = new Font(全局字符串.主字体, view.RowHeadersDefaultCellStyle.Font.Size);
                }
                //滚动条相关
                view.VBar.BackColor = 全局字符串.背景色;
                view.VBar.FillColor = 全局字符串.背景色;
                view.VBar.ForeColor = 全局字符串.主题色;
                view.VBar.HoverColor = 全局字符串.主题色;
                view.VBar.PressColor = 全局字符串.主题色;
                view.VBar.RectColor = 全局字符串.主题色;

                view.HBar.BackColor = 全局字符串.背景色;
                view.HBar.FillColor = 全局字符串.背景色;
                view.HBar.ForeColor = 全局字符串.主题色;
                view.HBar.HoverColor = 全局字符串.主题色;
                view.HBar.PressColor = 全局字符串.主题色;
            }

            if (con is UITrackBar trackBar) {
                trackBar.RectColor = 全局字符串.主题色;
                trackBar.ForeColor = 全局字符串.主题色;
                trackBar.FillColor = 全局字符串.背景色;
            }
            //UIUserControl的子类
            if (con is UIPanel panel) {
                panel.ForeDisableColor = 全局字符串.不可用时颜色;
            }
            //UIButton子类
            if (con is UISymbolButton symbolButton) {//SymbolButton的额外设置
                symbolButton.SymbolColor = 全局字符串.深级主题色;
                symbolButton.SymbolHoverColor = 全局字符串.主题色;//鼠标移上
                symbolButton.SymbolPressColor = 全局字符串.深级主题色;//鼠标按下
                symbolButton.SymbolSelectedColor = 全局字符串.主题色;
                symbolButton.ForeColor = 全局字符串.深级主题色;
            }
            //UIPanel子类
            if (con is UITextBox textBox) {

                textBox.ScrollBarBackColor = 全局字符串.背景色;//滚动条背景色
                textBox.ScrollBarColor = 全局字符串.主题色;//滚动条颜色
                textBox.FillReadOnlyColor = 全局字符串.背景色;
                textBox.ForeReadOnlyColor = 全局字符串.主题色;
                textBox.RectReadOnlyColor = 全局字符串.主题色;
                textBox.WatermarkActiveColor = 全局字符串.次级主题色;
                textBox.WatermarkColor = 全局字符串.次级主题色;

            } else if (con is UIRichTextBox richBox) {
                richBox.ScrollBarBackColor = 全局字符串.背景色;//滚动条背景色
                richBox.ScrollBarColor = 全局字符串.主题色;//滚动条颜色
            } else if (con is UIListBox listBox) {
                listBox.ScrollBarBackColor = 全局字符串.背景色;
                listBox.ScrollBarColor = 全局字符串.主题色;
                listBox.HoverColor = 全局字符串.次级主题色;
                listBox.ItemSelectBackColor = 全局字符串.次级主题色;
                listBox.ItemSelectForeColor = 全局字符串.主题色;
            } else if (con is UIComboBox comboBox) {
                comboBox.ItemFillColor = 全局字符串.背景色;
                comboBox.ItemForeColor = 全局字符串.主题色;
                comboBox.ItemHoverColor = 全局字符串.次级主题色;
                comboBox.ItemRectColor = 全局字符串.主题色;
                comboBox.ItemSelectBackColor = 全局字符串.次级主题色;
                comboBox.ItemSelectForeColor = 全局字符串.主题色;

            } else if (con is UIFlowLayoutPanel flowLayoutPanel) {
                flowLayoutPanel.ScrollBarBackColor = 全局字符串.背景色;
                flowLayoutPanel.ScrollBarColor = 全局字符串.主题色;
            }
            //UILabel子类
            if (con is UIMarkLabel markLabel) {
                markLabel.MarkColor = 全局字符串.主题色;
            }
        }
        private static void DPI设置(Form form, Dictionary<string, Control> Name_Control) {
            form.Width = (int)(form.Width * 全局字符串.屏幕缩放比);
            form.Height = (int)(form.Height * 全局字符串.屏幕缩放比);
            if (form is UIForm uiForm) {
                uiForm.TitleHeight = (int)(uiForm.TitleHeight * 全局字符串.屏幕缩放比);
            }
            foreach (var kv in Name_Control) {
                Control con = kv.Value;
                //自定义控件中的2个控件不放大
                if (con.Name.EndsWith("_LabelPanel") || con.Name.EndsWith("_Label")) {
                    continue;
                }
                con.DPI设置();
            }
        }
        public static void DPI设置(this Control con) {
            Point 修改后坐标 = con.Location;
            修改后坐标.X = (int)(修改后坐标.X * 全局字符串.屏幕缩放比);
            修改后坐标.Y = (int)(修改后坐标.Y * 全局字符串.屏幕缩放比);
            con.Location = 修改后坐标;
            Size 修改后大小 = con.Size;
            修改后大小.Width = (int)(修改后大小.Width * 全局字符串.屏幕缩放比);
            修改后大小.Height = (int)(修改后大小.Height * 全局字符串.屏幕缩放比);
            con.Size = 修改后大小;

            if (con is ISymbol symbol) {//字体图片额外设置
                symbol.SymbolSize = (int)(symbol.SymbolSize * 全局字符串.屏幕缩放比);

            } else if (con is UIComboBox comboBox) {
                comboBox.ItemHeight = (int)(comboBox.ItemHeight * 全局字符串.屏幕缩放比);

            } else if (con is 自定义DataGridView view) {
                //列宽
                foreach (DataGridViewColumn column in view.Columns) {
                    if (column.AutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet) {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                    }
                    if (column.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet) {
                        column.Width = (int)(column.Width * 全局字符串.屏幕缩放比);
                    }
                }
                view.RowHeadersWidth = (int)(view.RowHeadersWidth * 全局字符串.屏幕缩放比);
                view.ColumnHeadersHeight = (int)(view.ColumnHeadersHeight * 全局字符串.屏幕缩放比);
                view.RowTemplate.Height = (int)(view.RowTemplate.Height * 全局字符串.屏幕缩放比);


            } else if (con is UISplitContainer sc) {
                sc.SplitterWidth = (int)(sc.SplitterWidth * 全局字符串.屏幕缩放比);

            } else if (con is UIMarkLabel markLabel) {
                markLabel.MarkSize = (int)(markLabel.MarkSize * 全局字符串.屏幕缩放比);
            } else if (con is UIListBox listBox) {
                listBox.ItemHeight = (int)(listBox.ItemHeight * 全局字符串.屏幕缩放比);
            }

            if (con is UIColorTable colorTable) {
                colorTable.Width = (int)(colorTable.Width * 全局字符串.屏幕缩放比);
                colorTable.Height = (int)(colorTable.Height * 全局字符串.屏幕缩放比);
                /*colorTable.MinimumSize = new Size {
                    Width = (int)(colorTable.Width * 全局字符串.屏幕缩放比),
                    Height = (int)(colorTable.Height * 全局字符串.屏幕缩放比)
                };*/
            }
            if (con is UIControl uiControl) {
                uiControl.RectSize = (int)(uiControl.RectSize * 全局字符串.屏幕缩放比);
            } else if (con is UIUserControl uiUserControl) {
                uiUserControl.RectSize = (int)(uiUserControl.RectSize * 全局字符串.屏幕缩放比);
            }
        }
    }
}
