﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 翻译姬 {
    /// <summary>
    /// Update提交：
    /// DataGridView.Rows[0] as DataGridRow对象.Delete()后，或DataTable.Rows[0].Delete
    /// 注意事项：
    /// DataGridView.Rows删除后不影响DataTable的Rows数量
    /// </summary>
    public interface 数据库连接 {        

        string 连接地址 { get; }
        string 数据库名 { get; }

        bool 是否能连接();
        bool 是否存在表(string 表名);
        List<string> 获取列名(string 表名, bool 排除主键 = false, params string[] 排除列名);
        string 获取主键(string 表名);

        DataTable 分页查询(string sql, int 行数, int 页数, out int 总数, Dictionary<string, object> dic = null);
        DataTable Select(string sql, Dictionary<string, object> dic = null);//返回DataTable
        Task<DataTable> SelectAsync(string sql, Dictionary<string, object> dic = null, int 超时时间 = 10);//异步查询
        int Select(string sql, DataTable dt, Dictionary<string, object> dic = null);//查询到表，可用于更新
        int Insert(string sql, Dictionary<string, object> dic = null);//插入并返回自增ID的值
        int Update(Update数据 data);
        int Update(string sql, DataTable dt, Dictionary<string, object> dic = null);//根据DataTable增删改
        int Update_批量(params Update数据[] datas);//事务批量执行Update提交
        int Update_混合(Update数据 data, string[] sqls, Dictionary<string, object> dic = null);//Update与SQL同时提交，先执行Update
        int Execute(string sql, Dictionary<string, object> dic = null);//执行sql
        int Work(string[] sqls, Dictionary<string, object> dic = null, bool 忽略零条影响 = false);//事务批量

    }
    public struct Update数据 {
        public string SQL;
        public DataTable DataTable;
        public Dictionary<string, object> SQL数据;
    }
    public static class 数据库连接拓展 {
        public static void AddRange(this SqlParameterCollection sqlParameterCollection, Dictionary<string, object> dic) {
            if (dic == null) {
                return;
            }
            foreach (KeyValuePair<string, object> item in dic) {
                sqlParameterCollection.AddWithValue(item.Key, item.Value ?? DBNull.Value);
            }
        }
        public static void AddRange(this SQLiteParameterCollection sqlParameterCollection, Dictionary<string, object> dic) {
            if (dic == null) {
                return;
            }
            foreach (KeyValuePair<string, object> item in dic) {
                sqlParameterCollection.AddWithValue(item.Key, item.Value ?? DBNull.Value);
            }
        }
        public static string 获取表名(string sql) {
            return Regex.Match(sql.Trim(), @"^(?:select [^ ]+ from|update|insert into|delete from) ([^ ]+).*$", RegexOptions.IgnoreCase).Groups[1].Value;
        }
        /// <summary>
        /// 新表数据有差异的，把原表数据覆盖掉
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static void 数据覆盖(string 主键, DataTable 新表, DataTable 原表) {
            if (新表 == null || 原表 == null) {
                throw new Exception("数据库连接.数据覆盖:原表新表不能为空");
            }
            if (新表.Columns.Count != 原表.Columns.Count) {
                throw new Exception("数据库连接.数据覆盖:原表新表的字段需一致");
            }
            if (主键 == null) {
                return;
            }
            var rows = 新表.AsEnumerable().Except(原表.AsEnumerable(), DataRowComparer.Default);
            if (rows.Count() == 0) {
                return;
            }
            try {
                foreach (DataRow dr in rows) {
                    DataRow row = 原表.Select($"{主键}='{dr[主键]}'").SingleOrDefault();
                    if (row == null) {
                        原表.Rows.Add(dr.ItemArray);
                    } else {
                        row.ItemArray = dr.ItemArray;
                    }
                }
            } catch { }

        }

    }
}
