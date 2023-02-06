﻿using AutoMapper;
using CustomerService.DataAccess.Models;
using CustomerService.DataAccess.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.BusinessLogic
{
    internal class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Customer, Guid> _customerRepository;
        public CustomerService(IMapper mapper, IRepository<Customer, Guid> customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public Guid Create(CustomerDto customerDto)
        {
            var entity = _mapper.Map<CustomerDto, Customer>(customerDto);
            var res = _customerRepository.Add(entity);
            _customerRepository.SaveChanges();
            return res.Id;
        }

        public void Delete(Guid id)
        {
            var entity = _customerRepository.GetByPrimaryKey(id);
            _customerRepository.Delete(entity);
            _customerRepository.SaveChanges();
        }

        public ICollection<CustomerDto> GetAll()
        {
            ICollection<Customer> entities = _customerRepository.GetAll(true).ToList();
            return _mapper.Map<ICollection<Customer>, ICollection<CustomerDto>>(entities);
        }

        public CustomerDto GetById(Guid id)
        {
            return _mapper.Map<CustomerDto>(_customerRepository.GetByPrimaryKey(id));
        }

        public void Update(CustomerDto cDto)
        {
            var entity = _mapper.Map<CustomerDto, Customer>(cDto); 
            _customerRepository.Update(entity);
            _customerRepository.SaveChanges();
        }
    }
}
