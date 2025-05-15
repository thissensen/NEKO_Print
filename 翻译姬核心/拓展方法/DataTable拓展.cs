using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace 翻译姬 {
    public static class DataTable拓展 {

        public static void 设置列名(this DataTable dt, List<string> 列名) {
            int count = dt.Columns.Count < 列名.Count ? dt.Columns.Count : 列名.Count;
            for (int i = 0; i < count; i++) {
                dt.Columns[i].ColumnName = 列名[i];
            }
        }

        /// <summary>
        /// 将DataTable其中一列转为List
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="列名"></param>
        /// <returns></returns>
        public static IEnumerable<string> 获取值集合(this DataTable dt, int index = 0) {
            foreach (DataRow row in dt.Rows) {
                yield return row[index].ToString();
            }
        }
        public static IEnumerable<string> 获取值集合(this DataTable dt, string 列名) {
            foreach (DataRow row in dt.Rows) {
                yield return row[列名].ToString();
            }
        }

        /// <summary>
        /// 将DataTable的列名转为List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string> ToColumnList(this DataTable dt) {
            List<string> res = new List<string>();
            for (int i = 0; i < dt.Columns.Count; i++) {
                res.Add(dt.Columns[i].ColumnName);
            }
            return res;
        }

        /// <summary>
        /// 寻找指定行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="列下标"></param>
        /// <param name="值"></param>
        /// <returns></returns>
        public static DataRow 寻找指定行(this DataTable dt, int 列下标, string 值) {
            DataRow res = null;
            foreach (DataRow row in dt.Rows) {
                if (row[列下标].ToString() == 值) {
                    res = row;
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// 寻找指定行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="列名"></param>
        /// <param name="值"></param>
        /// <returns></returns>
        public static DataRow 寻找指定行(this DataTable dt, string 列名, string 值) {
            DataRow res = null;
            foreach (DataRow row in dt.Rows) {
                if (row[列名].ToString() == 值) {
                    res = row;
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// 查询相同列，并追加数据
        /// dt:被追加的表
        /// data:数据表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="data"></param>
        /// <returns>追加的行</returns>
        public static List<DataRow> 数据追加(this DataTable dt, DataTable data) {
            List<DataRow> res = new List<DataRow>();
            List<string> 相同列名 = new List<string>();
            foreach (DataColumn col in data.Columns) {
                if (dt.Columns.Contains(col.ColumnName)) {
                    相同列名.Add(col.ColumnName);
                }
            }
            foreach (DataRow row in data.Rows) {
                DataRow dr = dt.NewRow();
                foreach (string 列名 in 相同列名) {
                    dr[列名] = row[列名];
                }
                dt.Rows.Add(dr);
                res.Add(dr);
            }
            return res;
        }
        public static void 数据追加覆盖(this DataTable dt, DataTable data, string 主键) {
            List<string> 相同列名 = new List<string>();
            foreach (DataColumn col in data.Columns) {
                if (dt.Columns.Contains(col.ColumnName)) {
                    相同列名.Add(col.ColumnName);
                }
            }
            foreach (DataRow row in data.Rows) {
                DataRow[] rows = dt.AsEnumerable().Where(r => r[主键].ToString() == 主键).Select(r => r).ToArray();
                if (rows.Count() == 0) {//不存在追加
                    DataRow dr = dt.NewRow();
                    foreach (string 列名 in 相同列名) {
                        dr[列名] = row[列名];
                    }
                    dt.Rows.Add(dr);
                } else if (rows.Count() == 1) {//存在覆盖
                    DataRow dr = rows[0];
                    foreach (string 列名 in 相同列名) {
                        dr[列名] = row[列名];
                    }
                } else {
                    throw new Exception("主键填写错误，原表存在重复主键");
                }
            }
        }

    }
}
