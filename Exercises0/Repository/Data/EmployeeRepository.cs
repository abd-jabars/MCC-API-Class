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
            int increment = myContext.Employees.ToList().Count;
            string formatedNik = "";
            if (increment == 0)
            {
                formatedNik = DateTime.Now.ToString("yyyy") + "0" + increment.ToString();
            }
            else
            {
                string increment2 = myContext.Employees.ToList().Max(e => e.NIK);
                int formula = Int32.Parse(increment2) + 1;
                formatedNik = formula.ToString();
            }

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

        public IEnumerable<Object> GetRegisteredVM()
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
                                 select new
                                 {
                                     NIK = employees.NIK,
                                     FirstName = employees.FirstName,
                                     LastName = employees.LastName,
                                     Gender = employees.Gender,
                                     BirthDate = employees.BirthDate,
                                     Phone = employees.Phone,
                                     Email = employees.Email,
                                     Salary = employees.Salary,
                                     Password = accounts.Password,
                                     Degree = educations.Degree,
                                     Gpa = educations.GPA,
                                     UniversityId = universities.UniversityId,
                                     RoleName = myContext.AccountRoles.Where(ar => ar.AccountNIK == employees.NIK).Select(ar => ar.Role.Name).ToList()
                                 };
            return registeredData;
        }

        public Object GetRegisteredVM(string NIK)
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
                                 where employees.NIK == NIK
                                 select new
                                 {
                                     NIK = employees.NIK,
                                     FirstName = employees.FirstName,
                                     LastName = employees.LastName,
                                     Gender = employees.Gender,
                                     BirthDate = employees.BirthDate,
                                     Phone = employees.Phone,
                                     Email = employees.Email,
                                     Salary = employees.Salary,
                                     Password = accounts.Password,
                                     Degree = educations.Degree,
                                     GPA = educations.GPA,
                                     UniversityId = universities.UniversityId,
                                     RoleName = myContext.AccountRoles.Where(ar => ar.AccountNIK == employees.NIK).Select(ar => ar.Role.Name).ToList()
                                 };
            return registeredData;
        }

        public int DeleteRegisteredData(string NIK)
        {
            var getProfiling = myContext.Profilings.Where(p => p.NIK == NIK).FirstOrDefault();
            var education = myContext.Educations.Where(e => e.EducationId == getProfiling.EducationId).FirstOrDefault();
            myContext.Remove(education);
            var result = myContext.SaveChanges();
            return result;
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
                                 select new
                                 {
                                     NIK = employees.NIK,
                                     FullName = employees.FirstName + " " + employees.LastName,
                                     Phone = employees.Phone,
                                     BirthDate = employees.BirthDate.ToString("dddd, dd MMMM yyyy"),
                                     Salary = employees.Salary,
                                     Email = employees.Email,
                                     Gender = employees.Gender,
                                     Degree = educations.Degree,
                                     Gpa = educations.GPA,
                                     UniversityId = universities.UniversityId,
                                     UniversityName = universities.UniversityName,
                                     RoleName = myContext.AccountRoles.Where(acr => acr.AccountNIK == employees.NIK).Select(acr => acr.Role.Name).ToList()
                                 };
            return registeredData;
        }

        public Register GetRegisteredData(string NIK)
        {
            var query = myContext.Employees.Where(e => e.NIK == NIK)
                                    .Include(e => e.Account)
                                        .ThenInclude(a => a.Profiling)
                                        .ThenInclude(p => p.Education)
                                        .ThenInclude(e => e.University)
                                            .FirstOrDefault();
            if (query == null)
                return null;

            var registeredData = new Register
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Gender = query.Gender,
                BirthDate = query.BirthDate,
                Phone = query.Phone,
                Salary = query.Salary,
                Email = query.Email,
                Degree = query.Account.Profiling.Education.Degree,
                GPA = query.Account.Profiling.Education.GPA,
                UniversityId = query.Account.Profiling.Education.University.UniversityId,
                RoleName = myContext.AccountRoles.Where(acr => acr.AccountNIK == query.NIK).Select(acr => acr.Role.Name).ToList()
            };

            return registeredData;
        }

        public int UpdateRegisteredData(Register register)
        {
            if (CheckPhoneMailRegisteredData(register) == 1 || CheckPhoneMailRegisteredData(register) == 7)
            {
                return 1;
            }
            else if (CheckPhoneMailRegisteredData(register) == 3 || CheckPhoneMailRegisteredData(register) == 5)
            {
                return 2;
            }
            else if (CheckPhoneMailRegisteredData(register) == 4)
            {
                return 3;
            }
            else
            { 
                var employee = new Employee
                {
                    NIK = register.NIK,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    BirthDate = register.BirthDate,
                    Phone = register.Phone,
                    Email = register.Email,
                    Salary = register.Salary,
                    Gender = register.Gender
                };
                myContext.Entry(employee).State = EntityState.Modified;
                myContext.SaveChanges();

                var getProfiling = myContext.Profilings.Find(register.NIK);
                //var getProfiling = myContext.Profilings.Find(employee.NIK);
                var education = new Education
                {
                    EducationId = getProfiling.EducationId,
                    Degree = register.Degree,
                    GPA = register.GPA,
                    UniversityId = register.UniversityId
                };
                myContext.Entry(education).State = EntityState.Modified;
                myContext.SaveChanges();

                return 0;
            }

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
                return 0;
            }
        }

        public int CheckEmailRegisteredData(Register register)
        {
            var checkEmail = myContext.Employees.Where(e => e.Email == register.Email).FirstOrDefault();
            if (checkEmail != null)
                return 1;
            else
                return 0;
        }

        public int CheckPhoneRegisteredData(Register register)
        {
            var checkPhone = myContext.Employees.Where(e => e.Phone == register.Phone).FirstOrDefault();
            if (checkPhone != null)
                return 1;
            else
                return 0;
        }

        public int CheckPhoneMailRegisteredData(Register register)
        {
            var checkData = myContext.Employees.Where(e => e.NIK == register.NIK).FirstOrDefault();
            if (checkData != null)
            {
                myContext.Entry(checkData).State = EntityState.Detached;
            }
            if (checkData.Email == register.Email)
            {
                if (checkData.Phone == register.Phone) // email & phone still the same => update
                {
                    return 0;
                }
                else
                {
                    if (CheckPhoneRegisteredData(register) == 1) // same email, different phone but already used => can't update, phone already used
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
                if (CheckEmailRegisteredData(register) == 1) // email already used
                {
                    if (checkData.Phone == register.Phone) // even though phone still the same => can't update, email already used
                    {
                        return 3;
                    }
                    else //either phone is different
                    {
                        if (CheckPhoneRegisteredData(register) == 1) // phone already used => can't update, email & phone already used
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
                    if (checkData.Phone == register.Phone) // phone still the same => update
                    {
                        return 6;
                    }
                    else // change phone
                    {
                        if (CheckPhoneRegisteredData(register) == 1) // phone already used => can't update, phone already used
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
