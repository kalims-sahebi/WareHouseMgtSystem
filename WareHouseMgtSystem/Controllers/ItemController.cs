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
    public class ItemController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            var model = new ItemModel()
            {
                Category = Category.GetAll().Select(a => new SelectListItem
                {
                    Value = a.CategoryId.ToString(),
                    Text = a.Name
                }),
                Supplier = Supplier.GetAll().Select(a => new SelectListItem
                {
                    Value = a.SupplierId.ToString(),
                    Text = a.SupplierName
                }),
                Measurement = Measurement.GetAll().Select(a => new SelectListItem
                {
                    Value = a.MeasurementId.ToString(),
                    Text = a.Measure
                })
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult Add(ItemModel model)
        {
            model.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var template = new
                {
                    model.Name,
                    model.Code,
                    model.Price,
                    model.Description,
                    model.Qty,
                    model.SupplierId,
                    model.CategoryId,
                    model.MeasurementId,
                    model.User

                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddItem", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");
            }
            else
            {
                model.Category = Category.GetAll().Select(a => new SelectListItem
                {
                    Value = a.CategoryId.ToString(),
                    Text = a.Name
                });
                model.Supplier = Supplier.GetAll().Select(a => new SelectListItem
                {
                    Value = a.SupplierId.ToString(),
                    Text = a.SupplierName
                });
                model.Measurement = Measurement.GetAll().Select(a => new SelectListItem
                {
                    Value = a.MeasurementId.ToString(),
                    Text = a.Measure
                });
                return View(model);
            }


        }


        [HttpGet]
        public IActionResult List()
        {
            var ViewModel = new ItemListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.ItemList = sql.Query<ItemListModel>("dbo.GetItemList", commandType: CommandType.StoredProcedure);
            return View(ViewModel);

        }

        public IActionResult Delete(int id)
        {
            var template = new { ItemId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteItem", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var template = new
            {
                ItemId = id
            };

            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            var ViewModel = new ItemModel() { };

            ViewModel = sql.Query<ItemModel>("dbo.SelectItemToEdit", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

            ViewModel.Category = Category.GetAll().Select(a => new SelectListItem
            {
                Value = a.CategoryId.ToString(),
                Text = a.Name,
                Selected = a.CategoryId == ViewModel.CategoryId
            });
            ViewModel.Supplier = Supplier.GetAll().Select(a => new SelectListItem
            {
                Value = a.SupplierId.ToString(),
                Text = a.SupplierName,
                Selected = a.SupplierId == ViewModel.SupplierId
            });
            ViewModel.Measurement = Measurement.GetAll().Select(a => new SelectListItem
            {
                Value = a.MeasurementId.ToString(),
                Text = a.Measure,
                Selected = a.MeasurementId == ViewModel.MeasurementId
            });

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ItemModel model)
        {
            model.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var template = new
                {
                    model.ItemId,
                    model.Name,
                    model.Code,
                    model.Price,
                    model.Description,
                    model.Qty,
                    model.SupplierId,
                    model.CategoryId,
                    model.MeasurementId,
                    model.User

                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.EditItem", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");
            }
            else
            {
                model.Category = Category.GetAll().Select(a => new SelectListItem
                {
                    Value = a.CategoryId.ToString(),
                    Text = a.Name
                });
                model.Supplier = Supplier.GetAll().Select(a => new SelectListItem
                {
                    Value = a.SupplierId.ToString(),
                    Text = a.SupplierName
                });
                model.Measurement = Measurement.GetAll().Select(a => new SelectListItem
                {
                    Value = a.MeasurementId.ToString(),
                    Text = a.Measure
                });
                return View(model);
            }

        }

    }
}
