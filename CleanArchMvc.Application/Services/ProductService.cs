using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Command;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        const string entityError = "Entity could not be loaded";

        public ProductService(IProductRepository productRepository, IMapper mapper, IMediator mediator)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;            
            _mediator = mediator;
        }

        public async Task Add(ProductDTO productDTO)
        {
            
            var productCreate = _mapper.Map<ProductCreateCommand>(productDTO);

            if(productCreate is null)
                throw new ApplicationException(entityError);
            
            var result = await _mediator.Send(productCreate);


        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if(productByIdQuery is null)
                throw new ApplicationException(entityError);
            
            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);

        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if(productByIdQuery is null)
                throw new ApplicationException(entityError);
            
            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();
            
            if(productsQuery is null)
                throw new ApplicationException(entityError);
            
            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if(productRemoveCommand is null)
                throw new ApplicationException(entityError);
            
            await _mediator.Send(productRemoveCommand);
            
        }

        public async Task Update(ProductDTO productDTO)
        {

            var productCreate = _mapper.Map<ProductUpdateCommand>(productDTO);

            if(productCreate is null)
                throw new ApplicationException(entityError);
            
            var result = await _mediator.Send(productCreate);
        }
    }
}