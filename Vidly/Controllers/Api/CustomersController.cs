using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.Include(c=>c.MembershipType).ToList();
        }

        // GET api/<Customers>/5
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(x => x.Id == id);
            if(customer==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return customer;
        }

        // POST api/<Customers>
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        // PUT api/<Customers>/5
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.SingleOrDefault(c=>c.Id == id);
            if( customerInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            customerInDb.Name = customer.Name;
            customerInDb.Birthday = customer.Birthday;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            _context.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c=> c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}