using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace SuperMarketManager
{
    class DBHelper
    {
        private const string constr = "Data Source=DESKTOP-AF20NAF;Initial Catalog=SuperMarketManager;Persist Security Info=True;User ID=sa;Password=cssl#123";
 /// <summary>
 /// 返回单个值
 /// </summary>
 /// <param name="sql"></param>
 /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序执行过程中出现错误，错误信息为\n{0}", ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }
    /// <summary>
    /// 返回一行
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteReader( CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序执行过程中出现错误，错误信息为\n{0}", ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 增/删/改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序执行过程中出现错误，错误信息为\n{0}", ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }
    }
}
