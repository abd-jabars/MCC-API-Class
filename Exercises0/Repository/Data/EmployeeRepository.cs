using Exercises0.Context;
using Exercises0.Models;
using Exercises0.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(Register register)
        {
            //DateTime regDate = DateTime.Now;
            //string regYear = regDate.Year.ToString();
            string yearNow = DateTime.Now.Year.ToString();
            var countEmp = myContext.Employees.ToList().Count;
            var incEmp = countEmp + 1;
            string formatedNik = yearNow + "0" + incEmp;

            var checkPhone = myContext.Employees.Where(emp => emp.Phone == register.Phone).FirstOrDefault();
            var checkEmail = myContext.Employees.Where(emp => emp.Email == register.Email).FirstOrDefault();
            if (checkPhone != null && checkEmail != null)
            {
                return 1;
            }
            else if (checkEmail != null)
            {
                return 2;
            }
            else if (checkPhone != null)
            {
                return 3;
            }
            else
            {
                var employee = new Employee
                {
                    //NIK = regYear + "0" + incEmp,
                    NIK = formatedNik,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    BirthDate = register.BirthDate,
                    Phone = register.Phone,
                    Email = register.Email,
                    Salary = register.Salary,
                    Gender = register.Gender
                };
                myContext.Employees.Add(employee);
                myContext.SaveChanges();

                var account = new Account
                {
                    NIK = employee.NIK,
                    Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
                };
                myContext.Accounts.Add(account);
                myContext.SaveChanges();

                var accountRole = new AccountRole
                {
                    AccountNIK = employee.NIK,
                    RoleId = 3
                };
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();

                var education = new Education
                {
                    Degree = register.Degree,
                    GPA = register.GPA,
                    UniversityId = register.UniversityId
                };
                myContext.Educations.Add(education);
                myContext.SaveChanges();

                var profiling = new Profiling
                {
                    NIK = employee.NIK,
                    EducationId = education.EducationId
                };
                myContext.Profilings.Add(profiling);
                myContext.SaveChanges();

                return 0;
            }
        }

        public IEnumerable<Object> GetRegisteredData()
        {
            var registeredData = from employees in myContext.Employees
                                 join accounts in myContext.Accounts
                                    on employees.NIK equals accounts.NIK
                                 join profilings in myContext.Profilings
                                    on accounts.NIK equals profilings.NIK
                                 join educations in myContext.Educations
                                    on profilings.EducationId equals educations.EducationId
                                 join universities in myContext.Universities
                                    on educations.UniversityId equals universities.UniversityId
                                 join accountRoles in myContext.AccountRoles
                                    on accounts.NIK equals accountRoles.AccountNIK
                                 join roles in myContext.Roles
                                    on accountRoles.RoleId equals roles.Id
                                 //from universities in myContext.Universities
                                 //join educations in myContext.Educations
                                 //   on universities.UniversityId equals educations.UniversityId
                                 //join profilingEdId in myContext.Profilings
                                 //   on educations.EducationId equals profilingEdId.EducationId
                                 select new
                                    {
                                        FullName = employees.FirstName + " " + employees.LastName,
                                        Phone = employees.Phone,
                                        BirthDate = employees.BirthDate,
                                        Salary = employees.Salary,
                                        Email = employees.Email,
                                        Gender = employees.Gender,
                                        Degree = educations.Degree,
                                        Gpa = educations.GPA,
                                        UniversityName = universities.UniversityName,
                                        RoleName = roles.Name
                                    };
            return registeredData;
        }

        public IEnumerable<Object> GetRegisteredDataEagerly()
        {
            var eagerLoading = myContext.Employees
                .Include(ac => ac.Account)
                .ThenInclude(p => p.Profiling)
                .ThenInclude(ed => ed.Education)
                .ThenInclude(univ => univ.University);
            return eagerLoading;
        }

        public int InsertEmp(Employee employee)
        {
            //if (CheckEmail(employee) != 1 && CheckPhone(employee) != 1)
            //{
            //    base.Insert(employee);
            //    return 1;
            //}
            if (CheckEmail(employee) == 1 && CheckPhone(employee) == 1)
            {
                return 1;
            }
            else if (CheckEmail(employee) == 1)
            {
                return 2;
            }
            else if (CheckPhone(employee) == 1)
            {
                return 3;
            }
            else
            {
                //base.Insert(employee);
                return 0;
            }
        }

        public int UpdateEmp(Employee employee)
        {
            if (CheckPhoneMail(employee) == 1 || CheckPhoneMail(employee) == 7) // phone already used
            {
                return 1;
            }
            else if (CheckPhoneMail(employee) == 3 || CheckPhoneMail(employee) == 5) // email already used
            {
                return 2;
            }
            else if (CheckPhoneMail(employee) == 4) // phone & email already used
            {
                return 3;
            }
            else
            {
                //myContext.Entry(employee).State = EntityState.Modified;
                //myContext.SaveChanges();
                //base.Update(employee);
                return 0;
            }
        }

        public int CheckEmail(Employee employee)
        {
            var checkEmail = myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
            if (checkEmail != null)
                return 1;
            else
                return 0;
        }

        public int CheckPhone(Employee employee)
        {
            var checkPhone = myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
            if (checkPhone != null)
                return 1;
            else
                return 0;
        }

        //public int CheckPhoneMail(Employee employee)
        //{
        //    var checkData = myContext.Employees.Find(employee.NIK);
        //    var listPhone = myContext.Employees.ToList().Where(e => e.Phone == employee.Phone);
        //    var listEmail = myContext.Employees.ToList().Where(e => e.Email == employee.Email);

        //    if (checkData != null)
        //    {
        //        myContext.Entry(checkData).State = EntityState.Detached;
        //        foreach (var phone in listPhone)
        //        {
        //            if (phone.NIK != employee.NIK)
        //            {
        //                return 1;
        //            }
        //        }
        //        foreach (var email in listEmail)
        //        {
        //            if (email.NIK != employee.NIK)
        //            {
        //                return 2;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return 3;
        //    }
        //    return 4;
        //}

        public int CheckPhoneMail(Employee employee)
        {
            var checkData = myContext.Employees.Where(e => e.NIK == employee.NIK).FirstOrDefault();
            if (checkData != null)
            {
                myContext.Entry(checkData).State = EntityState.Detached;
            }
            if (checkData.Email == employee.Email)
            {
                if (checkData.Phone == employee.Phone) // email & phone still the same => update
                {
                    return 0;
                }
                else
                {
                    if (CheckPhone(employee) == 1) // same email, different phone but already used => can't update, phone already used
                    {
                        return 1;
                    }
                    else // same email, different phone but never used => update
                    {
                        return 2;
                    }
                }
            }
            else // change email
            {
                if (CheckEmail(employee) == 1) // email already used
                {
                    if (checkData.Phone == employee.Phone) // even though phone still the same => can't update, email already used
                    {
                        return 3;
                    }
                    else //either phone is different
                    {
                        if (CheckPhone(employee) == 1) // phone already used => can't update, email & phone already used
                        {
                            return 4;
                        }
                        else // phone never used => can't update, email already used
                        {
                            return 5;
                        }
                    }
                }
                else // email never used
                {
                    if (checkData.Phone == employee.Phone) // phone still the same => update
                    {
                        return 6;
                    }
                    else // change phone
                    {
                        if (CheckPhone(employee) == 1) // phone already used => can't update, phone already used
                        {
                            return 7;
                        }
                        else // phone never used => update
                        {
                            return 8;
                        }
                    }
                }
            }
        }

    }
}
