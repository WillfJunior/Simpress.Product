using AutoMapper;
using Moq;
using Simpress.Product.Application.AutoMapperConfig;
using Simpress.Product.Application.Services;
using Simpress.Product.Domain.Adapters;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Entities.Models;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.Test
{
    public class CategoryServiceTest
    {

        Mock<ICategoryRepository> mockRepository;
        Mock<INotificator> mockNotificator;
        Mock<IUnitOfWork> mockUoW;
        private readonly IMapper mockMapper;

        public CategoryServiceTest()
        {
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new AutoMapperConfiguration());
            });

            mockMapper = config.CreateMapper();
        }

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<ICategoryRepository>();
            mockNotificator = new Mock<INotificator>();
            
            mockUoW = new Mock<IUnitOfWork>();

        }

        [Test]
        [Category("Category Test")]
        public async Task WhenCreateCategoryItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var category =
                new Category(1,"Teste");
           
            

            mockRepository.Setup(x => x.Add(category)).Returns(Task.FromResult(category));
            var categoryDto = mockMapper.Map<CategoryDto>(category);


            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);
            
            var result = await service.Add(categoryDto);


            //Assert
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Never());
            Assert.True(result.Success);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenCreateCategoryItShouldReturnNotificationFail()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var category =
                new Category(1, "Tes");



            mockRepository.Setup(x => x.Add(category)).Returns(Task.FromResult(category));
            var categoryDto = mockMapper.Map<CategoryDto>(category);


            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Add(categoryDto);


            //Assert
            mockNotificator.Verify(n => n.GetAllNotifications(),Times.Once());
            Assert.False(result.Success);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenUpdateCategoryItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var category =
                new Category(1, "Teste");



            mockRepository.Setup(x => x.Update(category)).Returns(Task.FromResult(category));
            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(category));
            var categoryDto = mockMapper.Map<CategoryDto>(category);


            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Update(categoryDto, 1);


            //Assert
            mockRepository.Verify(x => x.GetById(1),Times.Once());
            mockRepository.Verify(x => x.Update(category), Times.Once());
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Never());
            Assert.True(result.Success);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenUpdateCategoryItShouldReturnNotificationFail()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var category =
                new Category(1, "Tes");



            mockRepository.Setup(x => x.Update(category)).Returns(Task.FromResult(category));
            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(category));
            var categoryDto = mockMapper.Map<CategoryDto>(category);


            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.Update(categoryDto, 1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once());
            mockRepository.Verify(x => x.Update(category), Times.Never());
            mockNotificator.Verify(n => n.GetAllNotifications(), Times.Once());
            Assert.False(result.Success);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenListingAllCategoriesItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var Categorys = new List<Domain.Entities.Models.Category>
            {
                new Category("Teste")
            };

            mockRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(Categorys));



            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.GetAll();


            //Assert
            mockRepository.Verify(x => x.GetAll(), Times.Once);


        }



        [Test]
        [Category("Category Test")]
        public async Task WhenListingCategoryByIdItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var Category =
                new Category(1, "Teste");


            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(Category));



            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.GetById(1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenListingCategoryByIdItShouldReturnfail()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var Category =
                new Category(1, "Teste");


            mockRepository.Setup(x => x.GetById(0)).Returns(Task.FromResult(Category));



            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = await service.GetById(0);


            //Assert
            mockRepository.Verify(x => x.GetById(0), Times.Once);
            Assert.False(!result.Success);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenDeleteCategoryByIdItShouldReturnSuccess()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            var Category =
                new Category(1, "Teste");

            mockRepository.Setup(x => x.GetById(1)).Returns(Task.FromResult(Category));
            mockRepository.Setup(x => x.Remove(Category)).Returns(Task.FromResult(Category));



            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.Remove(1);


            //Assert
            mockRepository.Verify(x => x.GetById(1), Times.Once);
            mockRepository.Verify(x => x.Remove(Category), Times.Once);


        }

        [Test]
        [Category("Category Test")]
        public async Task WhenDeleteCategoryByIdItShouldReturnFail()
        {

            //Arrange
            mockUoW.Setup(x => x.CategoryRepository).Returns(mockRepository.Object);

            Domain.Entities.Models.Category Category = null;


            mockRepository.Setup(x => x.GetById(0)).Returns(Task.FromResult(Category));
            mockRepository.Setup(x => x.Remove(Category)).Returns(Task.FromResult(Category));



            //Act
            CategoryService service = new CategoryService(mockNotificator.Object, mockUoW.Object, mockMapper, mockRepository.Object);

            var result = service.Remove(0);


            //Assert
            mockRepository.Verify(x => x.GetById(0), Times.Once);
            mockRepository.Verify(x => x.Remove(Category), Times.Never);


        }

    }
}
