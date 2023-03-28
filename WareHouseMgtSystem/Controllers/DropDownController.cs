using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;
using WareHouseMgtSystem.ViewModels;

namespace WareHouseMgtSystem.Controllers
{
    public class DropDownController : Controller
    {

        /*category start*/
        #region category
        [HttpGet]
        public IActionResult AddCat()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddCat(CategoryModel model)
        {
            if (ModelState.IsValid) 
            {

                var template = new
                {
                    model.Name
                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddCat", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("ListCat");
            }
            else
            return View(model);
        }

        public IActionResult ListCat()
        {
            var ViewModel = new CatListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.CategoryList = sql.Query<CategoryModel>("dbo.GetCategory", commandType: CommandType.StoredProcedure);
            return View(ViewModel);
        }

        public IActionResult DeleteCat(int id)
        {
            var template = new { CategoryId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteCat", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("ListCat");
        }
        #endregion
        /*category End*/

        /*Measure start*/
        #region category
        [HttpGet]
        public IActionResult AddMeasure()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddMeasure(MeasurementModel model)
        {
            if (ModelState.IsValid)
            {

                var template = new
                {
                    model.Measure
                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddMeasure", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("ListMeasure");
            }
            else
                return View(model);
        }

        public IActionResult ListMeasure()
        {
            var ViewModel = new MeasureListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.MeasureList = sql.Query<MeasurementModel>("dbo.GetMeasure", commandType: CommandType.StoredProcedure);
            return View(ViewModel);
        }

        public IActionResult DeleteMeasure(int id)
        {
            var template = new { MeasurementId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteMeasure", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("ListMeasure");
        }
        #endregion
        /*Measure start*/


        /*Supplier start*/
        #region category
        [HttpGet]
        public IActionResult AddSupp()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddSupp(SupplierModel model)
        {
            if (ModelState.IsValid)
            {

                var template = new
                {
                    model.SupplierName,
                    model.Brand
                };
                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddSupp", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("ListSupp");
            }
            else
                return View(model);
        }

        public IActionResult ListSupp()
        {
            var ViewModel = new SuppListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.SuppList = sql.Query<SupplierModel>("dbo.GetSupp", commandType: CommandType.StoredProcedure);
            return View(ViewModel);
        }

        public IActionResult DeleteSupp(int id)
        {
            var template = new { SupplierId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteSupp", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("ListSupp");
        }


        #endregion
        /*Supplier End*/
    }
}
