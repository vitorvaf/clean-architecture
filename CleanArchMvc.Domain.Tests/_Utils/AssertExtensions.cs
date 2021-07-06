using CleanArchMvc.Domain.Validation;
using Xunit;

namespace CleanArchMvc.Domain.Tests._Utils
{
    public static class AssertExtensions
    {
        public static void WithMessage(this DomainExceptionValidation exception, string message)
        {
            if(exception.Message == message)
                Assert.True(true);
            else
                Assert.False(true, $"Expected message '{message}'");
            
        }
        
    }
}