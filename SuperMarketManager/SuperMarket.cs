using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace SuperMarketManager
{
    class SuperMarket
    {
        public void Mean()
        {
            Console.WriteLine("-----------------------欢迎使用超市购物管理系统-----------------------");
            Console.WriteLine("1. 显示所有商品");
            Console.WriteLine("2. 显示所有商品分类");
            Console.WriteLine("3. 新增商品");
            Console.WriteLine("4. 修改商品售价");
            Console.WriteLine("5. 删除商品信息");
            Console.WriteLine("6. 在线购物");
            Console.WriteLine("7. 订单管理");
            Console.WriteLine("0. 退出");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.Write("请输入您的操作:");
            string xz = Console.ReadLine();
            switch (xz)
            {
                case "1":
                    // 显示所有商品
                    ShowAllProducts();
                    break;
                case "2":
                    //显示所有商品分类
                    ShowAllCommodityClassifications();
                    break;
                case "3":
                    //新增商品
                    NewCommodity();
                    break;
                case "4":
                    //修改商品售价
                    ModifyTheSellingPriceOfGoods();
                    break;
                case "5":
                    //删除商品信息
                    DeleteCommodity();
                    break;
                case "6":
                    //在线购物
                    OnlineShopping();
                    break;
                case "7":
                    //订单管理
                    OrderManagement();
                    break;
                case "0":
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 显示所有商品
        /// </summary>
        private void ShowAllProducts()
        {
            Console.WriteLine("\n----------------------------------------------------------------");
            Console.WriteLine("商品编号\t商品名称\t单价\t库存\t所属分类");
            Console.WriteLine("----------------------------------------------------------------");
            string sql = "SELECT G.GID,G.Name,G.Price,G.Stock,T.TypeName FROM [dbo].[GoodsType] T INNER JOIN [dbo].[Goods] G ON T.GTID = G.GTID";
            SqlDataReader r = DBHelper.ExecuteReader(sql);
            while (r!=null && r.HasRows && r.Read())
            {
                string gid = r["GID"].ToString();
                string name = r["Name"].ToString();
                string price = r["Price"].ToString();
                string stock = r["Stock"].ToString();
                string typeName = r["TypeName"].ToString();
                Console.WriteLine($"{gid}\t{name}\t{price}\t{stock}\t{typeName}");
            }
            if(r != null)
            {
                r.Close();
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
        /// <summary>
        /// 显示所有商品分类
        /// </summary>
        private void ShowAllCommodityClassifications()
        {

            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("分类编号\t商品分类名称 ");
            Console.WriteLine("---------------------------------");
            string sql = "SELECT GTID,TypeName FROM [dbo].[GoodsType]";
            SqlDataReader r = DBHelper.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                string gid = r["GTID"].ToString();
                string name = r["TypeName"].ToString();
               
                Console.WriteLine($"{gid}\t\t{name}");
            }
            if (r != null)
            {
                r.Close();
            }
            Console.WriteLine("---------------------------------");
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
        /// <summary>
        /// 新增商品
        /// </summary>
        private void NewCommodity()
        {
            Console.Write("\n请输入商品名称:");
            string name = Console.ReadLine();
            Console.Write("请输入商品售价:");
            string money = Console.ReadLine();
            Console.Write("请输入商品入库库存:");
            string kucun = Console.ReadLine();
            Console.WriteLine("请选择商品所属分类:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("分类编号\t商品分类名称 ");
            Console.WriteLine("---------------------------------");
            string sql = "SELECT GTID,TypeName FROM [dbo].[GoodsType]";
            SqlDataReader r = DBHelper.ExecuteReader(sql);
            while (r != null && r.HasRows && r.Read())
            {
                string gid = r["GTID"].ToString();
                string naame = r["TypeName"].ToString();

                Console.WriteLine($"{gid}\t\t{naame}");
            }
            if (r != null)
            {
                r.Close();
            }
            Console.Write("请输入商品分类编号:");
            int bh = Convert.ToInt32(Console.ReadLine());
            string u_sql = string.Format("SELECT MAX(GTID) FROM [dbo].[GoodsType]");
            string us_sql = string.Format("SELECT MIN(GTID) FROM [dbo].[GoodsType]");
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(u_sql));
            int ress = Convert.ToInt32(DBHelper.ExecuteScalar(us_sql));
            if(bh<ress || bh > res)
            {
                Console.WriteLine("编号不存在！请重试");
                NewCommodity();
                return;
            }
            string r_sql = string.Format("SELECT COUNT(0) FROM [dbo].[Goods] WHERE Name='{0}'",name);
            int ras = Convert.ToInt32(DBHelper.ExecuteScalar(r_sql));
            if(ras != 0)
            {
                Console.WriteLine("商品已存在！请重新输入！");
                Mean();
                return;
            }
            else
            {
                string no_sql = string.Format("INSERT INTO [dbo].[Goods] VALUES ('{0}','{1}','{2}','{3}')", name,bh,kucun, money);
                bool sv = DBHelper.ExecuteNonQuery(no_sql);
                if (sv)
                {
                    Console.WriteLine("新增商品成功!");
                }
                else
                {
                    Console.WriteLine("新增商品失败,请稍后再试!");
                }
                
            }
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
        /// <summary>
        /// 修改商品售价
        /// </summary>
        private void ModifyTheSellingPriceOfGoods()
        {
            Console.Write("\n请输入需要修改售价的商品编号:");
            string bh = Console.ReadLine();
            string sa_sql = string.Format("SELECT COUNT(0) FROM [dbo].[Goods] WHERE GID='{0}';",bh);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(sa_sql));
            if(res == 0)
            {
                Console.WriteLine("对不起,您输入的商品编号不存在,请重试！");
                Mean();
                return;
            }
            Console.Write("请输入商品的最新售价:");
            string money = Console.ReadLine();
            string u_sql = string.Format("SELECT Name,Price FROM [dbo].[Goods]	WHERE GID = '{0}'",bh);
            SqlDataReader r = DBHelper.ExecuteReader(u_sql);
            if(r!=null && r.HasRows && r.Read())
            {
                string name = r["Name"].ToString();
                string price = r["Price"].ToString();
                Console.WriteLine("您将要修改”{0}”的商品售价,该商品原售价为:{1}元, 现在的售价为:{2}元",name,price,money);
            }
            Random ex = new Random();
            int num = ex.Next(1000, 9999);
            Console.Write("请输入验证码”{0}”继续您的操作:",num);
            string yzm = Console.ReadLine();
            if(yzm == num.ToString())
            {
                string no_sql = string.Format("UPDATE [dbo].[Goods] SET [Price]='{0}' WHERE GID='{1}'", money, bh);
                bool sv = DBHelper.ExecuteNonQuery(no_sql);
                if (sv)
                {
                    Console.WriteLine("修改商品售价成功!");
                }
                else
                {
                    Console.WriteLine("修改商品售价失败,请稍后再试!");
                }
            }
            else
            {
                Console.WriteLine("验证码输入错误,修改售价操作已被终止!");
            }
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
        /// <summary>
        /// 删除商品信息
        /// </summary>
        private void DeleteCommodity()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 在线购物
        /// </summary>
        private void OnlineShopping()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 订单管理
        /// </summary>
        private void OrderManagement()
        {
            throw new NotImplementedException();
        }
    }
}
