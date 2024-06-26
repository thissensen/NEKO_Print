﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Sunny.UI;

namespace 翻译姬 {
    public class SqlServer数据库 : 数据库连接 {

        public string 连接地址 => _连接地址;
        private string _连接地址;
        public string 数据库名 => con.Database;
        private string 连接字符串;
        public SqlConnection con;

        public SqlServer数据库(string 连接字符串) {
            this.连接字符串 = 连接字符串;
            con = new SqlConnection(连接字符串);
        }

        public SqlServer数据库(string IP, string 数据库名, string 用户名, string 密码, int 超时时间 = 5) {
            连接字符串 = $"Data Source={IP};Initial Catalog={数据库名};User ID={用户名};Password={密码};Connect Timeout={超时时间}";
            _连接地址 = IP;
            con = new SqlConnection(连接字符串);
        }

        public bool 是否能连接() {
            try {
                con.Open();
            } catch {
                return false;
            } finally {
                con.Close();
            }
            return true;
        }

        public bool 是否存在表(string 表名) {
            var row = Select($"select top 1 * from sysObjects where Id=OBJECT_ID(N'{表名}') and xtype='U'").AsEnumerable().ElementAtOrDefault(0);
            return row != null;
        }

        public List<string> 获取列名(string 表名, bool 排除主键 = false, params string[] 排除列名) {
            string sql = $"select Name from SysColumns where id=Object_Id('{表名}') ";
            if (排除主键) {
                sql += $" and Name<>(select  COLUMN_NAME  FROM  INFORMATION_SCHEMA.KEY_COLUMN_USAGE  WHERE  TABLE_NAME='{表名}') ";
            }
            sql += " order by colorder";
            return Select(sql).获取值集合().Except(排除列名).ToList();
        }

        public string 获取主键(string 表名) {
            string sql = $"select  COLUMN_NAME  FROM  INFORMATION_SCHEMA.KEY_COLUMN_USAGE  WHERE  TABLE_NAME='{表名}'";
            DataTable dt = new DataTable();
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                SqlDataAdapter dat = new SqlDataAdapter(sql, con);
                dat.Fill(dt);
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt.AsEnumerable().ElementAtOrDefault(0)?[0].ToString();
            //return Select($"select  COLUMN_NAME  FROM  INFORMATION_SCHEMA.KEY_COLUMN_USAGE  WHERE  TABLE_NAME='{表名}'").AsEnumerable().ElementAtOrDefault(0)?[0].ToString();
        }

        
        public DataTable Select(string sql, Dictionary<string, object> dic = null) {
            DataTable dt = new DataTable();
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                SqlDataAdapter dat = new SqlDataAdapter(sql, con);
                if (dic != null && dic.Count > 0) {
                    dat.SelectCommand.Parameters.AddRange(dic);
                }
                dat.Fill(dt);
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }

        public async Task<DataTable> SelectAsync(string sql, Dictionary<string, object> dic = null, int 超时时间 = 10) {
            DataTable dt = new DataTable();
            try {
                SqlConnection con = new SqlConnection(连接字符串);
                if (con.State != ConnectionState.Open) {
                    await con.OpenAsync();
                }
                SqlDataAdapter dat = new SqlDataAdapter(sql, con);
                dat.SelectCommand.CommandTimeout = 超时时间;
                if (dic != null && dic.Count > 0) {
                    dat.SelectCommand.Parameters.AddRange(dic);
                }
                dat.Fill(dt);
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }

        public int Insert(string sql, Dictionary<string, object> dic = null) {
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                //执行语句，字段错误抛出异常，值错误不抛
                string 表名 = 数据库连接拓展.获取表名(sql);
                SqlCommand cmd = new SqlCommand(sql + $";SELECT IDENT_CURRENT('{表名}');", con);
                if (dic != null && dic.Count > 0) {
                    cmd.Parameters.AddRange(dic);
                }
                //只会返回INT类型ID自增的值
                string str = cmd.ExecuteScalar().ToString();
                if (str.IsNullOrEmpty()) {
                    return -1;
                }
                return int.Parse(str);
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int Update(string sql, DataTable dt, Dictionary<string, object> dic = null) {
            try {
                int num = -1;
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                if (dic != null && dic.Count > 0) {
                    adp.SelectCommand.Parameters.AddRange(dic);
                }
                new SqlCommandBuilder(adp);//必须加
                num = adp.Update(dt);
                return num;
            } catch (Exception ex) {
                if (ex.Message.Contains("对于不返回任何键列信息的 SelectCommand，不支持 UpdateCommand 的动态 SQL 生成")) {
                    throw new Exception("操作的表没有主键");
                }
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private int Update_非提交(string sql, DataTable dt, Dictionary<string, object> dic, SqlConnection con, SqlTransaction transaction) {
            try {
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                adp.SelectCommand.Transaction = transaction;
                if (dic != null && dic.Count > 0) {
                    adp.SelectCommand.Parameters.AddRange(dic);
                }
                new SqlCommandBuilder(adp);//必须加
                return adp.Update(dt);
            } catch (Exception ex) {
                if (ex.Message.Contains("对于不返回任何键列信息的 SelectCommand，不支持 UpdateCommand 的动态 SQL 生成")) {
                    throw new Exception("操作的表没有主键");
                }
                throw ex;
            }
        }

        public int Execute(string sql, Dictionary<string, object> dic = null) {
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                //执行语句，字段错误抛出异常，值错误不抛
                SqlCommand cmd = new SqlCommand(sql, con);
                if (dic != null && dic.Count > 0) {
                    cmd.Parameters.AddRange(dic);
                }
                return cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int Work(string[] sqls, Dictionary<string, object> dic = null, bool 忽略零条影响 = false) {
            int num = 0;
            SqlTransaction transaction = null;
            using (SqlCommand cmd = con.CreateCommand()) {
                try {
                    if (con.State != ConnectionState.Open) {
                        con.Open();
                    }
                    transaction = con.BeginTransaction("SampleTransaction");
                    cmd.Transaction = transaction;
                    if (dic != null && dic.Count > 0) {
                        cmd.Parameters.AddRange(dic);
                    }
                    foreach (string sql in sqls) {
                        cmd.CommandText = sql;
                        int n = cmd.ExecuteNonQuery();
                        if (忽略零条影响) {
                            num += n;
                        } else {
                            if (n > 0) {
                                num += n;
                            } else {
                                throw new Exception("影响了0行,SQL:" + sql);
                            }
                        }
                    }
                    transaction.Commit();//执行
                } catch (Exception e) {
                    transaction?.Rollback();
                    throw e;
                } finally {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                return num;
            }
        }

        /// <summary>
        /// 快速插入整表，要求与目标表一一对应(除自增主键)
        /// </summary>
        /// <param name="表名">数据库表名</param>
        /// <param name="dt">表</param>
        /// <returns></returns>
        public bool 插入整表(string 表名, DataTable dt) {
            bool flag = true;
            SqlTransaction tran = null;
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                using (tran = con.BeginTransaction()) {
                    using (SqlBulkCopy sqlCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)) {
                        sqlCopy.DestinationTableName = 表名;//表名
                        sqlCopy.WriteToServer(dt);
                        tran.Commit();
                    }
                }
            } catch (Exception ex) {
                tran?.Rollback();
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return flag;
        }

    }
}
