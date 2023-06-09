﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity.Infrastructure.MappingViews;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class CustomersController : Controller
	{
		private readonly ApplicationDbContext _context;

        public CustomersController()
        {
			_context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
			_context.Dispose();
            base.Dispose(disposing);
        }
        
        // GET: /Customers/
        public ViewResult Index()
		{
			return View();
		}

		[Route("customers/details/{id}")]
		public ActionResult Details(int id)
		{
			var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
			return View(customer);
		}

		public ActionResult New()
		{
			var membershipTypes = _context.MembershipTypes.ToList();
			var viewModel = new CustomerFormViewModel()
			{
				MembershipTypes = membershipTypes,
				Customer = new Customer()
			};
			return View("CustomerForm",viewModel);
		}

		[HttpPost] //post request
		[ValidateAntiForgeryToken]
		public ActionResult Save(Customer customer) //model binding
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new CustomerFormViewModel
				{
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList(),
				};
				return View("CustomerForm",viewModel);
			}
			if(customer.Id == 0)
			{
				_context.Customers.Add(customer);
			}
			else
			{
				var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
				//TryUpdateModel(customerInDb); // error prone
				customerInDb.Name = customer.Name;
				customerInDb.Birthday = customer.Birthday;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
			}
			_context.SaveChanges();
			return RedirectToAction("Index", "Customers");
		}

		public ActionResult Edit(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if(customer == null)
			{
				return HttpNotFound();
			}
			var viewModel = new CustomerFormViewModel()
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList(),
			};
			return View("CustomerForm",viewModel);
		}
	}
}