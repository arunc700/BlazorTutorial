using LearningModels;
using LearningWebApi.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [Authorize]
        [Route("GetEmployee")]
        [HttpGet]
        public async Task<ActionResult> GetEmployee()
        {
            try
            {
                var result = await employeeRepository.GetEmployee();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in db");
            }

        }

        [Route("GetEmployeeById/{id:int}")]
        [HttpGet]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployeeByID(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in db");
            }

        }

        [Route("CreateEmployee")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                //Check duplicate email
                var emailList = await employeeRepository.GetEmployeeByEmail(employee.Email);
                if (emailList != null)
                {
                    ModelState.AddModelError("Email", "Email is already exist in database");
                    return BadRequest(ModelState);
                }
                //Add employee
                var result = await employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = result.EmployeeId }, result);


                // return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in db");
            }

        }

        [Route("UpdateEmployee")]
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] Employee employee)
        {
            try
            { 
                if (employee == null)
                {
                    return NotFound($"Employee details not found");
                }

                return await employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in db");
            }

        }

        [Route("DeleteEmployee/{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {

                var updatedEmployee = await employeeRepository.GetEmployeeByID(id);
                if (updatedEmployee == null)
                {
                    return NotFound($"Employee with id ={id} not found");
                }

                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in db");
            }

        }

        [Route("SearchData/{search}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {

                var searchList = await employeeRepository.Search(name, gender);
                if (searchList.Any())
                {
                    return Ok(searchList);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in db");
            }

        }
    }
}
