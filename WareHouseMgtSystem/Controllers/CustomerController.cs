using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;
using WareHouseMgtSystem.ViewModels;
using static WareHouseMgtSystem.DataAccess.MyTables;

namespace WareHouseMgtSystem.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                model.User = User.Identity.Name;
                model.Printed = 0;

                var template = new
                {
                    model.FullName,
                    model.Phone,
                    model.Address,
                    model.Balance,
                    model.User,
                    model.Printed

                };

                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddCustomer", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");

            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult List()
        {
            var ViewModel = new CustomerListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.Customer = sql.Query<CustomerModel>("dbo.GetCustomerList", commandType: CommandType.StoredProcedure);
            ViewModel.Item = Items.GetAll().Select(a => new SelectListItem
            {
                Value = a.ItemId.ToString(),
                Text = a.Name
            });
            return View(ViewModel);
        }
        [HttpGet]
        public IActionResult PrintedList()
        {
            var ViewModel = new CustomerListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.Customer = sql.Query<CustomerModel>("dbo.GetCustomerPrintedList", commandType: CommandType.StoredProcedure);
            ViewModel.Item = Items.GetAll().Select(a => new SelectListItem
            {
                Value = a.ItemId.ToString(),
                Text = a.Name
            });
            return View(ViewModel);
        }

        public IActionResult Delete(int id)
        {
            var template = new { CustomerId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteCustomer", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var template = new
            {
                CustomerId = id
            };

            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            var ViewModel = new CustomerModel() { };

            ViewModel = sql.Query<CustomerModel>("dbo.SelectCustomerToEdit", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                model.User = User.Identity.Name;

                var template = new
                {
                    model.CustomerId,
                    model.FullName,
                    model.Phone,
                    model.Address,
                    model.Balance,
                    model.User

                };

                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.EditCustomer", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");

            }
            else
            {
                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] AjaxOrderModel model)
        {
            if (ModelState.IsValid)
            {
                model.User = User.Identity.Name;
                model.Printed = 0;
                var template = new
                {
                    model.ItemId,
                    model.Qty,
                    model.Description,
                    model.CustomerId,
                    model.User,
                    model.Printed
                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                await sql.QueryAsync("AddOrder", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult OrderList(int id)
        {
            var template = new
            {
                CustomerId = id
            };

            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@FullName", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

            var ViewModel = new OrderListViewModel() { };
            ViewModel.OrderList = sql.Query<AjaxOrderModel>("dbo.GetOrderList", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("FullName");
            return View(ViewModel);
        }

        public IActionResult DeleteOrder(int id)
        {
            var template = new { OrderId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteOrder", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("List");
        }

        public async Task<IActionResult> PrintBill(int id) 
        {
            var template = new
            {
                CustomerId = id
            };
            var parameters = new DynamicParameters(template);
            parameters.Add("@FullName", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

            var model = new BillViewModel() { };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            model.Bill = await sql.QueryAsync<BillModel>("dbo.GetBill", parameters, commandType: CommandType.StoredProcedure);
            
            TempData["Message"] = parameters.Get<string>("FullName");

            return View(model);
        }
    }
}
