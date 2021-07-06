using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Tests._Utils;
using CleanArchMvc.Domain.Validation;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductTest
    {

        [Fact]
        public void CreateProduct_WithValidParameters_ResultVaidObject()
        {
            var newProduct =
                new Product(1, "Product Name", "Short description", 50, 30, "local image");

            Assert.IsType<Product>(newProduct);
        }

        [Fact]
        public void CreateProduct_WithNegativeId_ReturnDomainExceptionValidation()
        {
            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(-1, "Product Name", "Short description", 50, 30, "local image"))
                .WithMessage("Invalid Id value");
        }



        [Fact]
        public void CreateProduct_WithNullName_ReturnDomainExceptionValidation()
        {
            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(1, null, "Short description", 50, 30, "local image"))
                .WithMessage("Invalid name, Name is required");
        }


        [Fact]
        public void CreateProduct_WithNullDescription_ReturnDomainExceptionValidation()
        {
            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(1, "Product name", null, 50, 30, "local image"))
                .WithMessage("Invalid description. Description is required");
        }

        [Fact]
        public void CreateProduct_WithShortDescription_ReturnDomainExceptionValidation()
        {
            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(1, "Product name", "sts", 50, 30, "local image"))
                .WithMessage("Invalid description, too short, minimun 5 characters");
        }

        
        [Theory]
        [InlineData(-10)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-200)]
        public void CreateProduct_WithInvalidPrice_ReturnDomainExceptionValidation( decimal invalidPrice)
        {

            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(1, "Product name", "short description product", invalidPrice, 5, "local image"))
                .WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-5)]
        [InlineData(-2)]
        public void CreateProduct_WithStockInvalid_ReturnDomainExceptionValidation(int invalidStock)
        {
            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(1, "Product name", "short description product", 50, invalidStock, "local image"))
                .WithMessage("Invalid stock value");
        }



        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CreateProduct_WithImageIsNull_ReturnValidObject(string nullImage)
        {
            Assert.IsType<Product>(
                new Product(1, "Product name", "short description product", 50, 10, nullImage));
        }

        [Fact]
        public void CreateProduct_WithLongImageValue_ReturnDomainExceptionValidation()
        {
            Assert.Throws<DomainExceptionValidation>(() =>
                new Product(1, "Product name", "short description product", 50, 30,
                    "product image to looooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                    "ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                    "ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                    "ooooooooooooooooooooooooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnng "))
                .WithMessage("Invalid image name, too long, maximum 250 characters");
        }

    }
}