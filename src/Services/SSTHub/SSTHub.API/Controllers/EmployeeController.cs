﻿using Microsoft.AspNetCore.Mvc;
using SSTHub.Domain.Interfaces;
using SSTHub.Domain.ViewModels.Employee;
using System.Net;

namespace SSTHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<EmployeeListItemViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IReadOnlyCollection<EmployeeListItemViewModel>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] int amount = 20, [FromQuery] int page = 0)
        {
            try
            {
                var employees = await _employeeService.GetAsync(amount, page);
                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDetailsViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(EmployeeDetailsViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee != null)
                return StatusCode(StatusCodes.Status200OK, employee);
                
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateViewModel createViewModel)
        {
            try
            {
                var employeeId = await _employeeService.CreateAsync(createViewModel);
                return StatusCode(StatusCodes.Status200OK, employeeId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody] EmployeeEditItemViewModel employeeEditItemViewModel)
        {
            try
            {
                await _employeeService.UpdateAsync(id, employeeEditItemViewModel);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("{id}/ActiveStatus")]
        public async Task<IActionResult> ChangeActiveStatus([FromRoute] int id)
        {
            try
            {
                await _employeeService.ChangeActiveStatusAsync(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
