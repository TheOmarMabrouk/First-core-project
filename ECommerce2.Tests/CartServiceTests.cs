using Moq;
using First_core_project.Data;
using First_core_project.Models;
using First_core_project.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace First_core_projectTests
{
    public class CartServiceTests
    {
        [Fact]
        public async Task AddToCartAsync_Should_Call_Add_And_Save()
        {
            // 1. Arrange
            // بنعمل Mock للـ DbSet والـ Context
            var mockSet = new Mock<DbSet<Cart>>();
            var mockContext = new Mock<SouqcomContext>();

            // بنربط جدول الكارت بالـ Mock
            mockContext.Setup(c => c.Carts).Returns(mockSet.Object);

            var service = new CartService(mockContext.Object);
            int productId = 1;
            string userId = "omar-123";

            // 2. Act
            await service.AddToCartAsync(productId, userId);

            // 3. Assert (التأكد إن الأوامر اتبعتت لقاعدة البيانات)

            // هل الكود حاول يضيف عنصر جديد؟
            mockSet.Verify(m => m.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());

            // هل الكود عمل SaveChanges؟
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}