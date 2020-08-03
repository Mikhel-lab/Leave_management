using Leave_management.Contracts;
using Leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.Repository
{
	public class LeaveAllocationRepository : ILeaveAllocationRepository
	{
		private readonly ApplicationDbContext _db;
		public LeaveAllocationRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public bool CheckAllocation(int leavetypeid, string employeeid )
		{
			var period = DateTime.Now.Year;
			return FindAll().Where(x => x.EmployeeId == employeeid && x.LeaveTypeId == leavetypeid && x.Period == period).Any();
		}

		public bool Create(LeaveAllocation entity)
		{
			_db.LeaveAllocations.Add(entity);
			return Save();
		}

		public bool Delete(LeaveAllocation entity)
		{
			_db.LeaveAllocations.Remove(entity);
			return Save();
		}

		public ICollection<LeaveAllocation> FindAll()
		{
			return _db.LeaveAllocations.ToList();
		}

		public LeaveAllocation FindById(int id)
		{
			var leaveallocation = _db.LeaveAllocations.Find(id);
			return leaveallocation;
		}

		public bool isExist(int id)
		{
			var exists = _db.LeaveAllocations.Any(x => x.Id == id);
			return exists;
		}

		public bool Save()
		{
			var changes = _db.SaveChanges();
			return changes > 0;
		}

		public bool Update(LeaveAllocation entity)
		{
			_db.LeaveAllocations.Update(entity);
			return Save();
		}
	}
}
