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
            throw new NotImplementedException();
        }
        /// <summary>
        /// 修改商品售价
        /// </summary>
        private void ModifyTheSellingPriceOfGoods()
        {
            throw new NotImplementedException();
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
