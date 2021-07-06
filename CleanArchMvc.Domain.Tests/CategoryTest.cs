using System;
using Xunit;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using CleanArchMvc.Domain.Tests._Utils;


namespace CleanArchMvc.Domain.Tests
{
    public class CategoryTest
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState ()
        {            
            var newCategory = new Category(1, "Name Category");
            Assert.IsType<Category>(newCategory);
        }


        [Fact]
        public void CreateCategory_WithInvalidID_ReturnsThrowException()
        {
            Assert.Throws<DomainExceptionValidation>( () => 
                new Category(-1, "Name Category")).WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateCategory_WithInvalidName_ReturnsThrowException()
        {
            Assert.Throws<DomainExceptionValidation>( () => 
                new Category(1, "Na")).WithMessage("Invalid name, too short, minimum 3 charecters");
        }


        [Fact]
        public void CreateCategory_WithNameIsNull_ReturnsThrowException()
        {
            Assert.Throws<DomainExceptionValidation>( () => 
                new Category(1, null)).WithMessage("Invalid name. Name is required");
        }

    } 
}
