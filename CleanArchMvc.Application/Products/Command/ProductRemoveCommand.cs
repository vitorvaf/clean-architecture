using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Command
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public ProductRemoveCommand(int id)
        {
            Id = id;
            
        }

        public int Id { get; set; }
        
    }
}