﻿using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ClientsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public List<Client> GetClients()
        {
            return context.Clients.OrderByDescending(c=>c.Id).ToList();
        }
        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = context.Clients.Find(id);
            if(client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        [HttpPost]
        public IActionResult CreateClient(ClientDto clientDto) 
        {
            var otherclient = context.Clients.FirstOrDefault(c=>c.Email == clientDto.Email);
            if(otherclient != null)
            {
                ModelState.AddModelError("Email", "Dia chi email da duoc su dung");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }
            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Phone = clientDto.Phone ?? "",
                Address = clientDto.Address ?? "",
                Status = clientDto.Status,
                CreateAt = DateTime.Now
            };
            context.Clients.Add(client);
            return Ok(client);
        }
    }
}
