﻿using Entities.Models;

namespace Repository.Extensions
{
    public static class EmployeeRepositoryExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge)
        {
            return employees.Where(e => e.Age >= minAge && e.Age <= maxAge);
        }

        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return employees;
            }

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return employees.Where(e => e.FirstName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
