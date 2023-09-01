using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System;
using MediatR;
using CleanArchMVC.Application.Products.Queries;
using System.Collections.Generic;

namespace CleanArchMVC.Application.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductsAsync();
        }
    }
}