using AutoMapper;
using Context;
using DataModel;
using Repository;
using Service.AutoMapper;
using Service.SharedService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public class EmployeeService:BaseService
    {
        private readonly Repository<Employee> _EmployeeRepository;
        private MapperConfiguration config;
        private IMapper mapper;
        public EmployeeService()
        {
            _EmployeeRepository = new Repository<Employee>(_unitOfWork);
            config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeModel>();
            });
            mapper = config.CreateMapper();
        }



        public IEnumerable<EmployeeModel> GetAll()
        {
            IEnumerable<Employee> Employees = _EmployeeRepository.GetList();
           
 
            var EmployeesList = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(Employees);
            return EmployeesList;
        }

        public EmployeeModel Create(EmployeeModel Employee)
        {
            var Employee1 = mapper.Map<EmployeeModel, Employee>(Employee);
            _EmployeeRepository.Insert(Employee1);
            _unitOfWork.Submit();
            return Employee;
        }

        //for edit and delete
        public EmployeeModel GetById(int id)
        {
            Employee Employee = _EmployeeRepository.GetById(id);
            var model = mapper.Map<Employee, EmployeeModel>(Employee);
            return model;
        }

        public IEnumerable<EmployeeModel> GetByName(string name)
        {
            var Employee = _EmployeeRepository.GetList().Where(a => a.Name.Contains(name));
            var model = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(Employee);
            return model;
        }


        public EmployeeModel Edit(EmployeeModel employee)
        {
            var employee1 = mapper.Map<EmployeeModel, Employee>(employee);
            _EmployeeRepository.Update(employee1);
            _unitOfWork.Submit();
            return employee;
        }

        public void Delete(int id)
        {
            _EmployeeRepository.Delete(id);
            _unitOfWork.Submit();
        }
    }
}
