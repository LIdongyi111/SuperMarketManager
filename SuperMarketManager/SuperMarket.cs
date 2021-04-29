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
                    Console.WriteLine("谢谢使用，已退出!");
                    break;
                default:
                    Console.WriteLine("输出错误,请重试");
                    Mean();
                    return;

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
            while (r != null && r.HasRows && r.Read())
            {
                string gid = r["GID"].ToString();
                string name = r["Name"].ToString();
                string price = r["Price"].ToString();
                string stock = r["Stock"].ToString();
                string typeName = r["TypeName"].ToString();
                Console.WriteLine($"{gid}\t{name}\t{price}\t{stock}\t{typeName}");
            }
            if (r != null)
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
            string u_sql = string.Format("SELECT COUNT(0) FROM [dbo].[GoodsType] WHERE GTID = {0}", bh);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(u_sql));
            if (res == 0)
            {
                Console.WriteLine("编号不存在！请重试");
                NewCommodity();
                return;
            }
            string r_sql = string.Format("SELECT COUNT(0) FROM [dbo].[Goods] WHERE Name='{0}'", name);
            int ras = Convert.ToInt32(DBHelper.ExecuteScalar(r_sql));
            if (ras != 0)
            {
                Console.WriteLine("商品已存在！请重新输入！");
                Mean();
                return;
            }
            string no_sql = string.Format("INSERT INTO [dbo].[Goods] VALUES ('{0}','{1}','{2}','{3}')", name, bh, kucun, money);
            bool sv = DBHelper.ExecuteNonQuery(no_sql);
            if (sv)
            {
                Console.WriteLine("新增商品成功!");
            }
            else
            {
                Console.WriteLine("新增商品失败,请稍后再试!");
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
            string sa_sql = string.Format("SELECT COUNT(0) FROM [dbo].[Goods] WHERE GID='{0}';", bh);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(sa_sql));
            if (res == 0)
            {
                Console.WriteLine("对不起,您输入的商品编号不存在,请重试！");
                Mean();
                return;
            }
            Console.Write("请输入商品的最新售价:");
            string money = Console.ReadLine();
            string u_sql = string.Format("SELECT Name,Price FROM [dbo].[Goods]	WHERE GID = '{0}'", bh);
            SqlDataReader r = DBHelper.ExecuteReader(u_sql);
            if (r != null && r.HasRows && r.Read())
            {
                string name = r["Name"].ToString();
                string price = r["Price"].ToString();
                Console.WriteLine("您将要修改”{0}”的商品售价,该商品原售价为:{1}元, 现在的售价为:{2}元", name, price, money);
            }
            Random ex = new Random();
            int num = ex.Next(1000, 9999);
            Console.Write("请输入验证码”{0}”继续您的操作:", num);
            string yzm = Console.ReadLine();
            if (yzm == num.ToString())
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
            Console.Write("\n请输入需要修改售价的商品编号:");
            string bh = Console.ReadLine();
            string sa_sql = string.Format("SELECT COUNT(0) FROM [dbo].[Goods] WHERE GID='{0}';", bh);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(sa_sql));
            if (res == 0)
            {
                Console.WriteLine("对不起,您输入的商品编号不存在,请重试！");
                Mean();
                return;
            }
            string u_sql = string.Format("SELECT Name,Price,Stock FROM [dbo].[Goods] WHERE GID = '{0}'", bh);
            SqlDataReader r = DBHelper.ExecuteReader(u_sql);
            if (r != null && r.HasRows && r.Read())
            {
                string name = r["Name"].ToString();
                string price = r["Price"].ToString();
                string stock = r["Stock"].ToString();
                Console.WriteLine("您将要删除商品:{0},商品售价:{1}元, 商品库存:{2}", name, price, stock);
            }
            Random ex = new Random();
            int num = ex.Next(1000, 9999);
            Console.Write("请输入验证码”{0}”继续您的操作:", num);
            string yzm = Console.ReadLine();
            if (yzm == num.ToString())
            {
                string s_sql = string.Format("SELECT COUNT(0) FROM [dbo].[OrderDetails] O INNER JOIN [dbo].[Goods] G ON O.GID=G.GID WHERE O.GID='{0}';", bh);
                int ress = Convert.ToInt32(DBHelper.ExecuteScalar(s_sql));
                if (ress == 0)
                {
                    string no_sql = string.Format("DElETE FROM [dbo].[Goods] WHERE GID ='{0}'", bh);
                    bool sv = DBHelper.ExecuteNonQuery(no_sql);
                    if (sv)
                    {
                        Console.WriteLine("删除商品成功!");
                    }
                    else
                    {
                        Console.WriteLine("删除商品失败,请稍后再试!");
                    }
                }
                else
                {
                    Console.WriteLine("商品已被客户购买过,则不允许被删除!");
                }
            }
            else
            {
                Console.WriteLine("验证码输入错误,删除操作已被终止!");
            }
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
        /// <summary>
        /// 在线购物
        /// </summary>
        private void OnlineShopping()
        {
            //输出主菜单，新增随机订单编号
            string s = DateTime.Now.ToLongDateString().ToString();
            string ss = DateTime.Now.ToString("hh:mm:ss");
            string dqsj = s + ss;
            Console.WriteLine("请选择您需要购买的商品编号:");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("商品编号\t商品名称\t单价\t库存\t所属分类");
            Console.WriteLine("----------------------------------------------------------------");
            string time = DateTime.Now.ToString("yyyyMMdd");
            Random ex = new Random();
            string num = ex.Next(100000, 999999).ToString();
            string numtime = time + num;
            string no_sql = string.Format("INSERT INTO [dbo].[Order] (OID, OrderDate, TotalPrice, CustmoerName, Phone, Address) VALUES ('{0}','','{1}','','','')", numtime, 0);
            bool sv = DBHelper.ExecuteNonQuery(no_sql);

            if (sv)
            {
                //输出商品内容
                string sql = "SELECT G.GID,G.Name,G.Price,G.Stock,T.TypeName FROM [dbo].[GoodsType] T INNER JOIN [dbo].[Goods] G ON T.GTID = G.GTID";
                SqlDataReader r = DBHelper.ExecuteReader(sql);
                while (r != null && r.HasRows && r.Read())
                {
                    string gid = r["GID"].ToString();
                    string name = r["Name"].ToString();
                    string price = r["Price"].ToString();
                    string stock = r["Stock"].ToString();
                    string typeName = r["TypeName"].ToString();
                    Console.WriteLine($"{gid}\t{name}\t{price}\t{stock}\t{typeName}");
                }
                if (r != null)
                {
                    r.Close();
                }
                //购买商品
                Console.WriteLine("----------------------------------------------------------------");
                while (true)
                {
                    Console.Write("请选择商品编号:");
                    string bh = Console.ReadLine();
                    Console.Write("请输入购买数量:");
                    string sl = Console.ReadLine();
                    string ns_sql = string.Format("UPDATE [dbo].[Goods] SET [Stock] = Stock-'{0}' WHERE [GID] ='{1}'", sl, bh);
                    bool svv = DBHelper.ExecuteNonQuery(ns_sql);
                    if (svv)
                    {

                        decimal xiaoji = 0;
                        string ra_sql = string.Format("SELECT Price,Stock FROM [dbo].[Goods] where GID='{0}'", bh);
                        SqlDataReader rs = DBHelper.ExecuteReader(ra_sql);
                        if (rs != null && rs.HasRows && rs.Read())
                        {
                            decimal price = Convert.ToDecimal(rs["Price"]);
                            int stock = (int)rs["Stock"];
                            int u = Convert.ToInt32(sl);
                            xiaoji = price * u;
                        }
                        if (rs != null)
                        {
                            rs.Close();
                        }
                        string us_sql = string.Format("INSERT INTO [dbo].[OrderDetails] (OID, GID, Number, Subtotal)  VALUES('{0}','{1}','{2}','{3}')", numtime, bh, sl, xiaoji);//往订单详情表添加新增的数据
                        bool sv1 = DBHelper.ExecuteNonQuery(us_sql);
                        if (sv1)
                        {
                            Console.Write("输入 N 结束购物,按任意键,继续您的购物:");
                            string jx = Console.ReadLine();
                            if (jx.ToLower() == "n")
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("程序异常，请重试");
                            Mean();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("库存不足！");
                    }
                }
                Console.WriteLine("购物结束,请确认您的订单信息:");
                Console.Write("请输入您的姓名: ");
                string xm = Console.ReadLine();
                Console.Write("请输入您的联系电话:");
                string dh = Console.ReadLine();
                Console.Write("请输入您的配送地址:");
                string add = Console.ReadLine();
                Console.WriteLine("请稍后,系统正在结算,结算后,我们将为您打印购物小票,请稍后…\n");
                string gg_sql = string.Format("UPDATE [dbo].[Order] SET CustmoerName ='{0}',Phone ='{1}',Address='{2}'  WHERE OID = '{3}'", xm, dh, add, numtime);
                bool sv3 = DBHelper.ExecuteNonQuery(gg_sql);
                if (sv3)
                {
                    Console.WriteLine("***********************************************************");
                    string xs_sql = string.Format("SELECT OID, OrderDate,  CustmoerName, Phone, Address FROM [dbo].[Order] where OID='{0}'", numtime);
                    SqlDataReader ree = DBHelper.ExecuteReader(xs_sql);
                    if (ree != null && ree.HasRows && ree.Read())
                    {
                        string ddbh = ree["OID"].ToString();
                        string kfname = ree["CustmoerName"].ToString();
                        string khlxdh = ree["Phone"].ToString();
                        string lxdz = ree["Address"].ToString();
                        Console.WriteLine("订单编号: " + ddbh);
                        Console.WriteLine("下单时间: " + dqsj);
                        Console.WriteLine("客户姓名:{0}\t 客户联系电话:{1}", kfname, khlxdh);
                        Console.WriteLine("客户联系地址: " + lxdz);
                    }
                    if (ree != null)
                    {
                        ree.Close();
                    }
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("商品名称\t单价\t 购买数量\t 小计");
                    Console.WriteLine("-------------------------------------------");
                    string xj_sql = string.Format("SElECT G.Name,G.Price,O.Number,O.Subtotal FROM [dbo].[OrderDetails] O INNER JOIN [dbo].[Goods] G ON G.GID = O.GID WHERE O.OID = '{0}'", numtime);
                    SqlDataReader r1 = DBHelper.ExecuteReader(xj_sql);
                    decimal zj = 0;
                    while (r1 != null && r1.HasRows && r1.Read())
                    {
                        string name = r1["Name"].ToString();
                        decimal price = Convert.ToDecimal(r1["Price"]);
                        int number = (int)r1["Number"];
                        string subtotal = r1["Subtotal"].ToString();
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", name, price, number, subtotal);
                        zj += price * number;
                    }
                    if (r1 != null)
                    {
                        r1.Close();
                    }
                    string gr_sql = string.Format("UPDATE [dbo].[Order] SET [TotalPrice] ='{0}' WHERE OID = '{1}'", zj, numtime);
                    bool sv4 = DBHelper.ExecuteNonQuery(gr_sql);
                    if (sv4)
                    {
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine("总价:" + zj + "元");
                        Console.WriteLine("感谢您对本店的支持,欢迎下次再来,祝您生活愉快!");
                        Console.WriteLine("***********************************************************");
                    }
                    else
                    {
                        Console.WriteLine("程序异常");
                        Mean();
                    }
                }
                else
                {
                    Console.WriteLine("程序异常，请重试");
                    Mean();
                }

            }
            else
            {
                Console.WriteLine("程序异常，请重试");
                Mean();
            }
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
        /// <summary>
        /// 订单管理
        /// </summary>
        private void OrderManagement()
        {
            string s = DateTime.Now.ToLongDateString().ToString();
            string ss = DateTime.Now.ToString("hh:mm:ss");
            string dqsj = s + ss;
            Console.Write("请输入订单编号:");
            string bh = Console.ReadLine();
            string u_sql = string.Format("SELECT COUNT(0) FROM [dbo].[OrderDetails] WHERE OID='{0}'", bh);
            int res = Convert.ToInt32(DBHelper.ExecuteScalar(u_sql));
            if (res == 0)
            {
                Console.WriteLine("很抱歉,没有查询到相关的订单信息,请稍后再试!");
                Mean();
            }
            Console.Write("为保证您的数据安全,现在对您的信息进行查验,请输入该订单的联系人电话:");
            string ph = Console.ReadLine();
            string us_sql = string.Format("SELECT COUNT(0) FROM [dbo].[Order] WHERE OID='{0}' AND Phone ={1}", bh, ph);
            int re1 = Convert.ToInt32(DBHelper.ExecuteScalar(us_sql));
            if (re1 == 0)
            {
                Console.WriteLine("您输入的电话与订单不匹配,查询信息失败!");
                Mean();
            }
            string xs_sql = string.Format("SELECT OID, OrderDate,  CustmoerName, Phone, Address FROM [dbo].[Order] where OID='{0}'", bh);
            SqlDataReader ree = DBHelper.ExecuteReader(xs_sql);
            if (ree != null && ree.HasRows && ree.Read())
            {
                string ddbh = ree["OID"].ToString();
                string kfname = ree["CustmoerName"].ToString();
                string khlxdh = ree["Phone"].ToString();
                string lxdz = ree["Address"].ToString();
                Console.WriteLine("订单编号: " + ddbh);
                Console.WriteLine("下单时间: " + dqsj);
                Console.WriteLine("客户姓名:{0}\t 客户联系电话:{1}", kfname, khlxdh);
                Console.WriteLine("客户联系地址: " + lxdz);
            }
            if (ree != null)
            {
                ree.Close();
            }
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("商品名称\t单价\t 购买数量\t 小计");
            Console.WriteLine("-------------------------------------------");
            string xj_sql = string.Format("SElECT G.Name,G.Price,O.Number,O.Subtotal FROM [dbo].[OrderDetails] O INNER JOIN [dbo].[Goods] G ON G.GID = O.GID WHERE O.OID = '{0}'", bh);
            SqlDataReader r1 = DBHelper.ExecuteReader(xj_sql);

            while (r1 != null && r1.HasRows && r1.Read())
            {
                string name = r1["Name"].ToString();
                decimal price = Convert.ToDecimal(r1["Price"]);
                int number = (int)r1["Number"];
                string subtotal = r1["Subtotal"].ToString();
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", name, price, number, subtotal);

            }
            if (r1 != null)
            {
                r1.Close();
            }

            Console.WriteLine("-------------------------------------------");
            string sql = string.Format("SELECT [TotalPrice] FROM [dbo].[Order] WHERE OID = '{0}'", bh);
            SqlDataReader r = DBHelper.ExecuteReader(sql);
            if (r != null && r.HasRows && r.Read())
            {
                string zj = r["TotalPrice"].ToString();
                Console.WriteLine("总价:" + zj + "元");
            }

            Console.WriteLine("***********************************************************");
            Console.Write("请输入任意键,返回主菜单:");
            Console.ReadLine();
            Mean();
        }
    }
}
