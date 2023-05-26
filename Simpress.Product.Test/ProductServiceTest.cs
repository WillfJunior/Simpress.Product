using AutoMapper;
using Moq;
using Simpress.Product.Application.AutoMapperConfig;
using Simpress.Product.Application.Services;
using Simpress.Product.Domain.Adapters;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Entities.Models;
using Simpress.Product.Domain.Entities.Models.Request;
using Simpress.Product.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simpress.Product.Test
{

    public class ProductServiceTest
    {
        Mock<IProductRepository> mockRepository;
        Mock<INotificator> mockNotificator;
        Mock<IUnitOfWork> mockUoW;
        private readonly IMapper mockMapper;

        public ProductServiceTest()
        {
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new AutoMapperConfiguration());
            });

            mockMapper = config.CreateMapper();
        }

        [SetUp] public void Setup()
        {
            mockRepository = new Mock<IProductRepository>();
            mockNotificator = new Mock<INotificator>();
            mockUoW = new Mock<IUnitOfWork>();
            
        }

        [Test]
        [Category("Product Test")]
        public async Task WhenCreateProductItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Teste", 5, 1);



            mockRepository.Setup(x => x.Add(product)).Returns(Task.FromResult(product));
            var productDto = mockMapper.Map<ProductDto>(product);


            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Add(productDto);


            //Assert
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Never());
            Assert.True(result.Success);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenCreateProductItShouldReturnNotificationFail()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Tes", 5, 1);



            mockRepository.Setup(x => x.Add(product)).Returns(Task.FromResult(product));
            var productDto = mockMapper.Map<ProductDto>(product);


            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Add(productDto);


            //Assert
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Once());
            Assert.False(result.Success);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenUpdateProductItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Teste", 5, 1);



            mockRepository.Setup(x => x.Update(product)).Returns(Task.FromResult(product));
            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(product));
            var productDto = mockMapper.Map<ProductDto>(product);


            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Update(productDto, 1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once());
            mockRepository.Verify(x => x.Update(product), Times.Once());
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Never());
            Assert.True(result.Success);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenUpdateProductItShouldReturnNotificationFail()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Tes", 5, 1);



            mockRepository.Setup(x => x.Update(product)).Returns(Task.FromResult(product));
            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(product));
            var productDto = mockMapper.Map<ProductDto>(product);


            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Update(productDto, 1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once());
            mockRepository.Verify(x => x.Update(product), Times.Never());
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Once());
            Assert.False(result.Success);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenListingAllProductsItShouldReturnSuccess()
        {
            
            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var products = new List<Domain.Entities.Models.Product>
            {
                new Domain.Entities.Models.Product("Teste",5,1)
            };

            mockRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(products));
            


            //Act
            ProductService service = new ProductService(mockNotificator.Object,mockUoW.Object,mockMapper,mockRepository.Object);

            var result = service.GetAll();


            //Assert
            mockRepository.Verify(x => x.GetAll(), Times.Once); 

            
        }

        [Test]
        [Category("Product Test")]
        public async Task WhenListingProductByIdItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Teste", 5, 1);
            

            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(product));



            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.GetById(1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenListingProductByIdItShouldReturnfail()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Teste", 5, 1);


            mockRepository.Setup(x => x.GetById(0)).Returns(Task.FromResult(product));



            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.GetById(0);


            //Assert
            mockRepository.Verify(x => x.GetById(0), Times.Once);
            Assert.False(!result.Success);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenDeleteProductByIdItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            var product =
                new Domain.Entities.Models.Product(1, "Teste", 5, 1);

            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(product));
            mockRepository.Setup(x => x.Remove(product)).Returns(Task.FromResult(product));



            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.Remove(1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once);
            mockRepository.Verify(x => x.Remove(product), Times.Once);


        }

        [Test]
        [Category("Product Test")]
        public async Task WhenDeleteProductByIdItShouldReturnFail()
        {

            //Arrange
            mockUoW.Setup(x => x.ProductRepository).Returns(mockRepository.Object);

            Domain.Entities.Models.Product product = null;
                

            mockRepository.Setup(x => x.GetById(0)).Returns(Task.FromResult(product));
            mockRepository.Setup(x => x.Remove(product)).Returns(Task.FromResult(product));



            //Act
            ProductService service = new ProductService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.Remove(0);


            //Assert
            mockRepository.Verify(x => x.GetById(0), Times.Once);
            mockRepository.Verify(x => x.Remove(product), Times.Never);


        }
    }
}
