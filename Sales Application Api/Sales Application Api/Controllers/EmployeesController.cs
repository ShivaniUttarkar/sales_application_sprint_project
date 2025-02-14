﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales_Application_Api.Models;

namespace Sales_Application_Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'NorthwindContext.Employees'  is null.");
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees.ToListAsync();
        }

        // PUT: api/employee/edit/5
        [HttpPut("edit{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PATCH: api/employee/edit/5
        [HttpPatch("edit/{EmployeeID}")]
        public async Task<IActionResult> PatchEmployee(int EmployeeID, JsonPatchDocument<Employee> employeePatch)
        {
            var employee = await this._context.Employees.FindAsync(EmployeeID);

            if (employee != null)
            {
                employeePatch.ApplyTo(employee);
                _context.SaveChanges();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // GET: api/employees/title/{title}
        [HttpGet("title/{title}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByTitle(string title)
        {

            var employees = await _context.Employees.Where(c => c.Title == title).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // GET: api/employee/City/{City}
        [HttpGet("City/{City}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByCity(string City)
        {

            var employee = await _context.Employees.Where(c => c.City == City).ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/Employees/Region/{RegionDescription}
        [HttpGet("Region/{RegionDescription}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByRegion(string RegionDescription)
        {

            var employee = await _context.Employees.Where(c => c.Region == RegionDescription).ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        //GET: api/Employees/HireDate
        [HttpGet("HireDate/{HireDate}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByDate(string HireDate)
        {
            DateTime date = DateTime.Parse(HireDate);
            var employees = await _context.Employees.Where(c => c.HireDate == date).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        ////GET: api/Employees/highestsalebyemployee/{date}
        //[HttpGet("highestsalebyemployee/{date}")]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee()
        //{

        //    var salesTotalByAmount = _context.SalesTotalsByAmounts.OrderByDescending(sta => sta.SaleAmount);

        //    var 
    
        //}

        /*
       //GET: api/Employees/highestsalebyemployee/{year}
       [HttpGet("highestsalebyemployee/{date}")]
       public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee(DateTime HireDate)
       {

           var employees = await _context.Employees.Where(c => c.HireDate == HireDate).ToListAsync();

           if (employees == null)
           {
               return NotFound();
           }

           return employees;
       }*/

        /*
      //GET: /api/employee/lowestsalebyemployee/{date}
      [HttpGet("highestsalebyemployee/{date}")]
      public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee(DateTime HireDate)
      {

          var employees = await _context.Employees.Where(c => c.HireDate == HireDate).ToListAsync();

          if (employees == null)
          {
              return NotFound();
          }

          return employees;
      }*/

        /*
     //GET: /api/employee/lowestsalebyemployee/{year}
     [HttpGet("highestsalebyemployee/{date}")]
     public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee(DateTime HireDate)
     {

         var employees = await _context.Employees.Where(c => c.HireDate == HireDate).ToListAsync();

         if (employees == null)
         {
             return NotFound();
         }

         return employees;
     }*/

        /*
     //GET: /api/employee/salemadebyanemployee/{Employeeid}/{date}
     [HttpGet("highestsalebyemployee/{date}")]
     public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee(DateTime HireDate)
     {

         var employees = await _context.Employees.Where(c => c.HireDate == HireDate).ToListAsync();

         if (employees == null)
         {
             return NotFound();
         }

         return employees;
     }*/

        /*
    //GET: /api/employee/Salemadebyanemployeebetweendates/{EmployeeId}{fromdate}/{todate}
    [HttpGet("highestsalebyemployee/{date}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee(DateTime HireDate)
    {

        var employees = await _context.Employees.Where(c => c.HireDate == HireDate).ToListAsync();

        if (employees == null)
        {
            return NotFound();
        }

        return employees;
    }*/

        /*
    //GET: /api/employee/companyname/{EmployeeID}
    [HttpGet("highestsalebyemployee/{date}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetHeightSaleByEmployee(DateTime HireDate)
    {

        var employees = await _context.Employees.Where(c => c.HireDate == HireDate).ToListAsync();

        if (employees == null)
        {
            return NotFound();
        }

        return employees;
    }*/


        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
