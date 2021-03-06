﻿using Leave_management.Contracts;
using Leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.Repository
{
	public class LeaveHistoryRepository : ILeaveHistoryRepository
	{
		private readonly ApplicationDbContext _db;
		public LeaveHistoryRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public bool Create(LeaveHistory entity)
		{
			_db.LeaveHistories.Add(entity);
			return Save();
		}

		public bool Delete(LeaveHistory entity)
		{
			_db.LeaveHistories.Remove(entity);
			return Save();
		}

		public ICollection<LeaveHistory> FindAll()
		{
			return _db.LeaveHistories.ToList();
		}

		public LeaveHistory FindById(int id)
		{
			var leaveHistoris = _db.LeaveHistories.Find(id);
			return leaveHistoris;

		}

		public bool isExist(int id)
		{
			var exists = _db.LeaveHistories.Any(x => x.Id == id);
			return exists;
		}

		public bool Save()
		{
			var changes = _db.SaveChanges();
			return changes > 0;
		}

		public bool Update(LeaveHistory entity)
		{
			_db.LeaveHistories.Update(entity);
			return Save();

		}
	}
}
