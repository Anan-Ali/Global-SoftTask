using AutoMapper;
using Context;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Service.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration mapper { get; set; }
        public static MapperConfiguration mapper2 { get; set; }

        public static void RegisterMappers()
        {

             mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeModel>());
             mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeModel, Employee>());
        }
    }
}
