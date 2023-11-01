using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译姬.Properties;

namespace 翻译姬 {
    public class 控件序列化 {

        private bool 是否序列化;
        private string 窗体名;
        private Dictionary<string, Control> Name_Control;
        private 数据库连接 数据库 => 全局数据.数据库;

        public 控件序列化(自定义窗体 form) {
            Name_Control = form.Name_Control;
            窗体名 = form.GetType().Name;
            是否序列化 = form.是否序列化;
        }

        public void 序列化() {
            if (!是否序列化) {
                return;
            }
            JObject obj = new JObject();
            foreach (var kv in Name_Control) {
                Control con = kv.Value;
                if (con.Parent is 组合控件基类接口 temp && !temp.是否序列化) {
                    continue;
                }
                if (con is UITextBox box) {
                    obj.Add(box.Name, box.Text);
                } else if (con is UIComboBox combobox) {
                    obj.Add(combobox.Name, combobox.Text);
                } else if (con is UISwitch sw) {
                    obj.Add(sw.Name, sw.Active);
                }
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("值", obj.ToString());
            DataRow row = 数据库.Select($"select 窗体名 from 窗体序列化 where 窗体名='{窗体名}'")?.AsEnumerable()?.ElementAtOrDefault(0);
            if (row == null) {
                数据库.Execute($"insert into 窗体序列化(窗体名,序列化值) values('{窗体名}',@值)", dic);
            } else {
                数据库.Execute($"update 窗体序列化 set 序列化值=@值 where 窗体名='{窗体名}'", dic);
            }

        }

        public void 反序列化() {
            if (!是否序列化) {
                return;
            }
            if (!全局数据.窗体名_序列化数据.ContainsKey(窗体名)) {
                return;
            }
            JObject obj = 全局数据.窗体名_序列化数据[窗体名];
            for (int i = 0; i < Name_Control.Count; i++) {
                var kv = Name_Control.ElementAt(i);
                Control con = kv.Value;
                if (con.Parent is 组合控件基类接口 temp && !temp.是否序列化) {
                    continue;
                }
                if (!obj.ContainsKey(con.Name)) {
                    continue;
                }
                if (con is UITextBox box) {
                    box.Text = obj[box.Name].Value<string>();
                } else if (con is UIComboBox combobox) {
                    if (combobox.Name == "读取目录Box") {
                        var a = 0;
                    }
                    string val = obj[combobox.Name].Value<string>();
                    combobox.Text = val ?? "";
                } else if (con is UISwitch sw) {
                    sw.Active = obj[sw.Name].Value<bool>();
                }
            }
        }

    }
}
