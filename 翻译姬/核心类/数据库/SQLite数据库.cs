using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using Sunny.UI;

namespace 翻译姬 {
    
    public class SQLite数据库 : 数据库连接 {

        public string 连接地址 => _连接地址;
        private string _连接地址;
        public string 数据库名 => con.Database;
        private string 连接字符串;
        public SQLiteConnection con;

        /// <summary>
        /// 默认内存数据库
        /// </summary>
        public SQLite数据库() {
            con = new SQLiteConnection("Data Source=:memory:");
        }
        public SQLite数据库(string 路径) {
            //Data Source=路径;Pooling=true;FailIfMissing=false
            连接字符串 = $"Data Source={路径};Pooling=true;FailIfMissing=false";
            con = new SQLiteConnection(连接字符串);
            _连接地址 = con.Database;
        }
        ~SQLite数据库() {
            try {
                if (con?.State == ConnectionState.Open)
                    con.Close();
            } catch { }
        }

        public bool 是否能连接() {
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
            } catch {
                return false;
            } finally {
                con.Close();
            }
            return true;
        }

        public bool 是否存在表(string 表名) {
            string val = Select($"SELECT count(*) FROM sqlite_master WHERE type='table' AND name='{表名}'").AsEnumerable().ElementAtOrDefault(0)?[0].ToString();
            return int.Parse(val) == 1;
        }

        public List<string> 获取列名(string 表名, bool 排除主键 = false, params string[] 排除列名) {
            if (排除主键) {
                DataTable dt = Select($"pragma table_info('{表名}')");
                return (from row in dt.AsEnumerable()
                           where row["pk"].ToString() != "1"
                           select row["name"].ToString()).ToList();
            } else {
                return Select($"pragma table_info('{表名}')").获取值集合(1).Except(排除列名).ToList();
            }
        }

        public string 获取主键(string 表名) {
            DataTable dt = new DataTable();
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }

                SQLiteDataAdapter dat = new SQLiteDataAdapter($"pragma table_info ('{表名}')", con);
                dat.Fill(dt);
            } catch (Exception ex) {
                throw ex;
            }
            //DataTable dt = Select($"pragma table_info ('{表名}')");
            var rows = dt?.Select("pk=1");
            if (rows.Length > 0) {
                return rows?[0]["name"].ToString();
            } else {
                return string.Empty;
            }
        }

        /// <summary>
        /// 如 行数=5，页数=2，则取11-15
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="行数">一页取几行</param>
        /// <param name="页数">第几页开始</param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable 分页查询(string sql, int 行数, int 页数, out int 总数, Dictionary<string, object> dic = null) {
            总数 = 0;
            DataTable dt = new DataTable();
            string 处理sql = Regex.Replace(sql, "select ", $"select (select count() from ({sql})) as 总数量,", RegexOptions.IgnoreCase);
            处理sql += $" Limit {行数} Offset {行数 * 页数}";
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                SQLiteDataAdapter dat = new SQLiteDataAdapter(处理sql, con);
                if (dic != null && dic.Count > 0) {
                    dat.SelectCommand.Parameters.AddRange(dic);
                }
                dat.Fill(dt);
                if (dt.Rows.Count > 0) {
                    总数 = Convert.ToInt32(dt.Rows[0]["总数量"].ToString());
                }
                dt.Columns.Remove("总数量");
            } catch (Exception ex) {
                throw ex;
            }
            return dt;
        }

        public DataTable Select(string sql, Dictionary<string, object> dic = null) {
            DataTable dt = new DataTable();

            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }

                SQLiteDataAdapter dat = new SQLiteDataAdapter(sql, con);
                if (dic != null && dic.Count > 0) {
                    dat.SelectCommand.Parameters.AddRange(dic);
                }
                dat.Fill(dt);
                string 主键 = 获取主键(数据库连接拓展.获取表名(sql));
            } catch (Exception ex) {
                throw ex;
            }
            return dt;
        }

        public async Task<DataTable> SelectAsync(string sql, Dictionary<string, object> dic = null, int 超时时间 = 10) {
            DataTable dt = new DataTable();

            try {
                SQLiteConnection con = new SQLiteConnection(连接字符串);
                if (con.State != ConnectionState.Open) {
                    await con.OpenAsync();
                }

                SQLiteDataAdapter dat = new SQLiteDataAdapter(sql, con);
                dat.SelectCommand.CommandTimeout = 超时时间;
                if (dic != null && dic.Count > 0) {
                    dat.SelectCommand.Parameters.AddRange(dic);
                }
                dat.Fill(dt);
                string 主键 = 获取主键(数据库连接拓展.获取表名(sql));
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }

        public int Select(string sql, DataTable dt, Dictionary<string, object> dic = null) {
            DataTable newDt = new DataTable();
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }

                SQLiteDataAdapter dat = new SQLiteDataAdapter(sql, con);
                if (dic != null && dic.Count > 0) {
                    dat.SelectCommand.Parameters.AddRange(dic);
                }
                int num = dat.Fill(newDt);
                string 主键 = 获取主键(数据库连接拓展.获取表名(sql));
                数据库连接拓展.数据覆盖(主键, newDt, dt);
                return num;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int Insert(string sql, Dictionary<string, object> dic = null) {
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                //执行语句，字段错误抛出异常，值错误不抛
                string 表名 = 数据库连接拓展.获取表名(sql);
                SQLiteCommand cmd = new SQLiteCommand(sql + $";select last_insert_rowid() from {表名};", con);
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

        public int Update(Update数据 data) {
            return Update(data.SQL, data.DataTable, data.SQL数据);
        }

        public int Update(string sql, DataTable dt, Dictionary<string, object> dic = null) {
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, con);
                if (dic != null && dic.Count > 0) {
                    adp.SelectCommand.Parameters.AddRange(dic);
                }
                new SQLiteCommandBuilder(adp);//必须加
                return adp.Update(dt);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private int Update_非提交(string sql, DataTable dt, Dictionary<string, object> dic, SQLiteConnection con, SQLiteTransaction transaction) {
            try {
                SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, con);
                adp.SelectCommand.Transaction = transaction;
                if (dic != null && dic.Count > 0) {
                    adp.SelectCommand.Parameters.AddRange(dic);
                }
                new SQLiteCommandBuilder(adp);//必须加
                return adp.Update(dt);
            } catch (Exception ex) {
                if (ex.Message.Contains("对于不返回任何键列信息的 SelectCommand，不支持 UpdateCommand 的动态 SQL 生成")) {
                    throw new Exception("操作的表没有主键");
                }
                throw ex;
            }
        }

        public int Update_批量(params Update数据[] 提交数据) {
            int num = 0;
            SQLiteTransaction transaction = null;
            using (SQLiteCommand cmd = con.CreateCommand()) {
                try {
                    if (con.State != ConnectionState.Open) {
                        con.Open();
                    }
                    transaction = con.BeginTransaction();
                    foreach (Update数据 data in 提交数据) {
                        num += Update_非提交(data.SQL, data.DataTable, data.SQL数据, con, transaction);
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

        public int Update_混合(Update数据 data, string[] sqls, Dictionary<string, object> dic = null) {
            int num = 0;
            SQLiteTransaction transaction = null;
            using (SQLiteCommand cmd = con.CreateCommand()) {
                try {
                    if (con.State != ConnectionState.Open) {
                        con.Open();
                    }
                    transaction = con.BeginTransaction();
                    cmd.Transaction = transaction;

                    SQLiteDataAdapter adp = new SQLiteDataAdapter(data.SQL, con);
                    adp.SelectCommand.Transaction = transaction;
                    if (data.SQL数据 != null && data.SQL数据.Count > 0) {
                        adp.SelectCommand.Parameters.AddRange(data.SQL数据);
                    }
                    new SQLiteCommandBuilder(adp);//必须加
                    num += adp.Update(data.DataTable);

                    if (dic != null && dic.Count > 0) {
                        cmd.Parameters.AddRange(dic);
                    }
                    foreach (string sql in sqls) {
                        cmd.CommandText = sql;
                        int n = cmd.ExecuteNonQuery();
                        if (n > 0) {
                            num += n;
                        } else {
                            throw new Exception("影响了0行,SQL:" + sql);
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


        public int Execute(string sql, Dictionary<string, object> dic = null) {
            try {
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                //执行语句，字段错误抛出异常，值错误不抛
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                if (dic != null && dic.Count > 0) {
                    cmd.Parameters.AddRange(dic);
                }
                return  cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int Work(string[] sqls, Dictionary<string, object> dic = null, bool 忽略零条影响 = false) {
            int num = 0;
            SQLiteTransaction transaction = null;
            using (SQLiteCommand cmd = con.CreateCommand()) {
                try {
                    if (con.State != ConnectionState.Open) {
                        con.Open();
                    }
                    transaction = con.BeginTransaction();
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
                } catch (Exception ex) {
                    transaction?.Rollback();
                    throw ex;
                }
                return num;
            }
        }

    }
    
}
