using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Queries.GetCarById;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Queries.GetCars;
using UnluCo.NetBootcamp.Odev4.DBOperations;
using UnluCo.NetBootcamp.Odev4.Entities.Concrete;
using static UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.CreateCar.CreateCarCommand;
using static UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.PatchCar.IsActivePatchCommand;
using static UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.UpdateCar.UpdateCarCommand;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.CreateCar;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.DeleteCar;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.PatchCar;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.UpdateCar;
using Microsoft.AspNetCore.Authorization;

namespace UnluCo.NetBootcamp.Odev4.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarSystemDbContext _context;
        private readonly IMapper _mapper;

        public CarController(CarSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            GetCarsQuery query = new GetCarsQuery(_context, _mapper);
            var carsList = query.Handle();
            return Ok(carsList);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetCarByIdQuery query = new GetCarByIdQuery(_context, _mapper);
            query.carId = id;
            GetCarByIdQueryValidator validator = new GetCarByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var brand = query.Handle();
            return Ok(brand);
        }
        //Araclari model yilina gore artan sirada getirir
        [HttpGet("listModelYearAsc")]
        public IActionResult GetListBrandNameAsc()
        {
            GetCarsQuery query = new GetCarsQuery(_context, _mapper);
            var carsList = query.Handle().OrderBy(x => x.ModelYear).ToList();
            return Ok(carsList);
        }
        //Araclari model yilina gore azalan sirada getirir
        [HttpGet("listBrandNameDesc")]
        public IActionResult GetListBrandNameDesc()
        {
            GetCarsQuery query = new GetCarsQuery(_context, _mapper);
            var carsList = query.Handle().OrderByDescending(x => x.ModelYear).ToList();
            return Ok(carsList);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCarModel car)
        {
            if (ModelState.IsValid)
            {
                CreateCarCommand command = new CreateCarCommand(_context, _mapper);
                command.Model = car;
                CreateCarCommandValidator validator = new CreateCarCommandValidator();
                validator.ValidateAndThrow(command);//Fluentvalidation kutuphanesinden kontrol edip varsa hata firlatan bir fonksiyon
                command.Handle();
                return StatusCode(201);
            }
            else
            {
                return StatusCode(401);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCarModel car)
        {
            UpdateCarCommand command = new UpdateCarCommand(_context);
            command.carId = id;
            command.Model = car;
            UpdateCarCommandValidator validator = new UpdateCarCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                DeleteCarCommand command = new DeleteCarCommand(_context);
                command.carId = id;
                DeleteCarCommandValidator validator = new DeleteCarCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
            else
            {
                return StatusCode(401);
            }
        }
        [HttpPatch("{id}")]
        public IActionResult IsActiceUpdate(int id, [FromBody] IsActiveCarModel car)
        {
            IsActivePatchCommand command = new IsActivePatchCommand(_context);
            command.carId = id;
            command.Model = car;
            IsActivePatchCommandValidator validator = new IsActivePatchCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
    }
}
