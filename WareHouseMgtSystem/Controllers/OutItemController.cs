using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using WareHouseMgtSystem.ViewModels;
using ShamisDateTime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;
using static WareHouseMgtSystem.DataAccess.MyTables;

namespace WareHouseMgtSystem.Controllers
{
    public class OutItemController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            var model = new OutItemModel()
            {
                ItemList = Items.GetAll().Select(a => new SelectListItem
                {
                    Value = a.ItemId.ToString(),
                    Text = a.Name
                })
        };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(OutItemModel model)
        {

            if (!string.IsNullOrEmpty(model.DateString))
                model.Date = PersianDateTime.Parse(model.DateString).ToDateTime();
            model.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var template = new
                {
                    model.ItemId,
                    model.Qty,
                    model.TotalPrice,
                    model.Date,
                    model.User

                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddOutItem", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");
            }

            else
            {
                model.ItemList = Items.GetAll().Select(a => new SelectListItem
                {
                    Value = a.ItemId.ToString(),
                    Text = a.Name
                });

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            var ViewModel = new OutItemViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.OutItemList = sql.Query<OutItemModel>("dbo.GetOutItemList", commandType: CommandType.StoredProcedure);

            return View(ViewModel);
        }

        public IActionResult Delete(int id)
        {
            var template = new { OutItemId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteOutItem", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var template = new
            {
                OutItemId = id
            };

            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            var ViewModel = new OutItemModel() { };

            ViewModel = sql.Query<OutItemModel>("dbo.SelectOutItemToEdit", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            ViewModel.ItemList = Items.GetAll().Select(a => new SelectListItem
            {
                Value = a.ItemId.ToString(),
                Text = a.Name,
                Selected = a.ItemId == ViewModel.ItemId
            });
            if (ViewModel != null)
                ViewModel.DateString = new ShamisDateTime.PersianDateTime(ViewModel.Date)
                    .ToShortDateString();
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Edit(OutItemModel model)
        {

            if (!string.IsNullOrEmpty(model.DateString))
                model.Date = PersianDateTime.Parse(model.DateString).ToDateTime();
            model.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var template = new
                {
                    model.ItemId,
                    model.Qty,
                    model.TotalPrice,
                    model.Date,
                    model.User,
                    model.OutItemId

                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.EditOutItem", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");
            }

            else
            {
                model.ItemList = Items.GetAll().Select(a => new SelectListItem
                {
                    Value = a.ItemId.ToString(),
                    Text = a.Name
                });

                return View(model);
            }
        }

    }
}
