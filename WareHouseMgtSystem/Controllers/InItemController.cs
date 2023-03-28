using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ShamisDateTime;
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
    public class InItemController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            var model = new InItemModel()
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
                sql.Query("dbo.AddInItem", parameters, commandType: CommandType.StoredProcedure);
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
            var ViewModel = new InItemViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.InItemList = sql.Query<InItemModel>("dbo.GetInItemList", commandType: CommandType.StoredProcedure);

            return View(ViewModel);
        }

        public IActionResult Delete(int id)
        {
            var template = new { InItemId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteInItem", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var template = new
            {
                InItemId = id
            };

            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            var ViewModel = new InItemModel() { };

            ViewModel = sql.Query<InItemModel>("dbo.SelectInItemToEdit", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
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
        public IActionResult Edit(InItemModel model)
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
                    model.InItemId

                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.EditInItem", parameters, commandType: CommandType.StoredProcedure);
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
