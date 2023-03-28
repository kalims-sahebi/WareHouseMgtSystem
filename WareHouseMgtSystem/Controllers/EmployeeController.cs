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
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            var model = new EmployeeModel() 
            {
                Position = PositionTitle.GetAll().Select(a => new SelectListItem
                {
                    Value = a.PositionId.ToString(),
                    Text = a.Title
                }),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(EmployeeModel model)
        {
           if(ModelState.IsValid)
            {
                model.User = User.Identity.Name;

                var template = new
                {
                    model.FullName,
                    model.Phone,
                    model.Address,
                    model.Salary,
                    model.PositionId,
                    model.User
                    
                };

                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.AddEmployee", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");
                
            }
            else
            {
                model.Position = PositionTitle.GetAll().Select(a => new SelectListItem
                {
                    Value = a.PositionId.ToString(),
                    Text = a.Title
                });
                return View(model);
            }
            
        }

        [HttpGet]
        public IActionResult List()
        {
            var ViewModel = new EmployeeListViewModel() { };
            using SqlConnection sql = new(Startup.ConnectionString);
            ViewModel.Employee = sql.Query<EmployeeModel>("dbo.GetEmployeeList", commandType: CommandType.StoredProcedure);

            return View(ViewModel);
        }

        public IActionResult Delete(int id)
        {
            var template = new { EmployeeId = id };
            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            sql.Query("dbo.DeleteEmployee", parameters, commandType: CommandType.StoredProcedure);
            TempData["Message"] = parameters.Get<string>("Msg");
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var template = new
            {
                EmployeeId = id
            };

            using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
            DynamicParameters parameters = new DynamicParameters(template);
            parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            var ViewModel = new EmployeeModel() { };

            ViewModel = sql.Query<EmployeeModel>("dbo.SelectEmployeeToEdit", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            ViewModel.Position = PositionTitle.GetAll().Select(a => new SelectListItem
            {
                Value = a.PositionId.ToString(),
                Text = a.Title,
                Selected = a.PositionId == ViewModel.PositionId
            });
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                model.User = User.Identity.Name;

                var template = new
                {
                    model.FullName,
                    model.Phone,
                    model.Address,
                    model.Salary,
                    model.PositionId,
                    model.User,
                    model.EmployeeId

                };

                using SqlConnection sql = new SqlConnection(Startup.ConnectionString);
                DynamicParameters parameters = new DynamicParameters(template);
                parameters.Add("@Msg", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                sql.Query("dbo.EditEmployee", parameters, commandType: CommandType.StoredProcedure);
                TempData["Message"] = parameters.Get<string>("Msg");
                return RedirectToAction("List");

            }
            else
            {
                model.Position = PositionTitle.GetAll().Select(a => new SelectListItem
                {
                    Value = a.PositionId.ToString(),
                    Text = a.Title,
                    Selected = a.PositionId == model.PositionId
                });
                return View(model);
            }

        }
    }
}
