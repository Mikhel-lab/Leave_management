﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leave_management.Contracts;
using Leave_management.Data;
using Leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leave_management.Controllers
{
	[Authorize(Roles = "Administrator, Employee")]
	public class LeaveTypesController : Controller
	{

		private readonly ILeaveTypeRepository _repo;
		private readonly IMapper _mapper;

		public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper )
		{
			_repo = repo;
			_mapper = mapper;
		}

	
		// GET: LeaveTypesController
		public ActionResult Index()
		{

			var leavestypes = _repo.FindAll().ToList();
			var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavestypes);
			return View(model);
		}

		// GET: LeaveTypesController/Details/5
		public ActionResult Details(int id)
		{
			if (!_repo.isExist(id))
			{
				return NotFound();
			}

			var leavetype = _repo.FindById(id);
			var model = _mapper.Map<LeaveTypeVM>(leavetype);
			return View(model);
		}

		// GET: LeaveTypesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: LeaveTypesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(LeaveTypeVM model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var leavetype = _mapper.Map<LeaveType>(model);
				leavetype.DateCreated = DateTime.Now;
				var isSuccess = _repo.Create(leavetype);

				if (!isSuccess)
				{
					ModelState.AddModelError("", "Something went error");
					return View(model);
				}
				return RedirectToAction(nameof(Index));
			}

			catch
			{
				ModelState.AddModelError("", "Something went error");
				return View(model);
			}
		}

		// GET: LeaveTypesController/Edit/5
		public ActionResult Edit(int id)
		{
			if (!_repo.isExist(id))
			{
				return NotFound();
			}

			var leavetype = _repo.FindById(id);
			var model = _mapper.Map<LeaveTypeVM>(leavetype);
			return View(model);
		}

		// POST: LeaveTypesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit( LeaveTypeVM model)
		{
			try
			{

				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var leavetype = _mapper.Map<LeaveType>(model);
				var isSuccess = _repo.Update(leavetype);

				if (!isSuccess)
				{
					ModelState.AddModelError("", "Something went error");
					return View(model);
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Something went error");
				return View(model);
			}
		}

		// GET: LeaveTypesController/Delete/5
		public ActionResult Delete(int id)
		{
			//if (!_repo.isExist(id))
			//{
			//	return NotFound();
			//}

			//var leavetype = _repo.FindById(id);
			//var model = _mapper.Map<LeaveTypeVM>(leavetype);
			//return View(model);

			var leavetype = _repo.FindById(id);
			if (leavetype == null)
			{
				return NotFound();
			}
			var isSuccess = _repo.Delete(leavetype);

			if (!isSuccess)
			{
				return BadRequest();
			}
			return RedirectToAction(nameof(Index));

		}

		// POST: LeaveTypesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, LeaveTypeVM model)
		{
			try
			{
				var leavetype = _repo.FindById(id);
				if (leavetype == null)
				{
					return NotFound();
				}
				var isSuccess = _repo.Delete(leavetype);

				if (!isSuccess)
				{
					return View(model);
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Something went error");
				return View(model);
			}
		}
	}
}