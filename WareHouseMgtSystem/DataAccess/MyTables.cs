using Dapper;
using Microsoft.Data.SqlClient;
using WareHouseMgtSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.DataAccess
{
    public static class MyTables
    {
        public static class PositionTitle
        {
            public static IEnumerable<PositionModel> GetAll()
            {
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                return sql.Query<PositionModel>("dbo.GetPosition");
            }
        }
        public static class Category
        {
            public static IEnumerable<CategoryModel> GetAll()
            {
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                return sql.Query<CategoryModel>("dbo.GetCategory");
            }
        } 
        public static class Supplier
        {
            public static IEnumerable<SupplierModel> GetAll()
            {
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                return sql.Query<SupplierModel>("dbo.GetSupplier");
            }
        } 
        public static class Measurement
        {
            public static IEnumerable<MeasurementModel> GetAll()
            {
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                return sql.Query<MeasurementModel>("dbo.GetMeasurement");
            }
        }
        public static class Items
        {
            public static IEnumerable<OrderItemModel> GetAll()
            {
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                return sql.Query<OrderItemModel>("dbo.OrderItem");
            }
        }


    }
}
