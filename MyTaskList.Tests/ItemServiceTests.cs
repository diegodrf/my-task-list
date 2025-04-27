using FakeItEasy;
using MyTaskList.Application.Abstractions.Ports.Driven;
using MyTaskList.Application.Entities;
using MyTaskList.Application.Services;

namespace MyTaskList.Tests;

public class ItemServiceTests
{
    [Fact]
    public async Task Should_Create_An_Item()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        var sut = new ItemService(fakeRepository);
        
        // Act
        var result = await sut.CreateAsync(A.Dummy<string>(), TestContext.Current.CancellationToken);
        
        // Assert
        Assert.IsType<Guid>(result.Id);
        A.CallTo(() => fakeRepository.SaveAsync(A<Item>.Ignored, A<CancellationToken>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task When_Items_Exists_Should_Return_A_List()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        A.CallTo(() => fakeRepository.GetAllAsync(A<CancellationToken>.Ignored))
            .Returns([A.Dummy<Item>()]);
        
        var sut = new ItemService(fakeRepository);
        
        // Act
        var result = await sut.GetAllAsync(TestContext.Current.CancellationToken);
        
        // Assert
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public async Task When_Items_Not_Exists_Should_Return_An_Empty_List()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        A.CallTo(() => fakeRepository.GetAllAsync(A<CancellationToken>.Ignored))
            .Returns(Enumerable.Empty<Item>().ToList());
        
        var sut = new ItemService(fakeRepository);
        
        // Act
        var result = await sut.GetAllAsync(TestContext.Current.CancellationToken);
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task When_Item_Not_Exists_Should_Return_Null()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        A.CallTo(() => fakeRepository.GetByIdAsync(A<Guid>.Ignored, A<CancellationToken>.Ignored))
            .Returns(null as Item);
        
        var sut = new ItemService(fakeRepository);
        
        // Act
        var result = await sut.GetByIdAsync(Guid.CreateVersion7(), TestContext.Current.CancellationToken);
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task When_Item_Exists_Should_Return_An_Object()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        A.CallTo(() => fakeRepository.GetByIdAsync(A<Guid>.Ignored, A<CancellationToken>.Ignored))
            .Returns(A.Dummy<Item>());
        
        var sut = new ItemService(fakeRepository);
        
        // Act
        var result = await sut.GetByIdAsync(Guid.CreateVersion7(), TestContext.Current.CancellationToken);
        
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Should_Update_An_Item()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        
        var sut = new ItemService(fakeRepository);
        
        // Act
        var result = await sut.UpdateAsyncAsync(
            A.Dummy<Guid>(), 
            A.Dummy<string>(), 
            A.Dummy<bool>(), 
            TestContext.Current.CancellationToken);
        
        // Assert
        Assert.NotNull(result);
        A.CallTo(() => fakeRepository.SaveAsync(A<Item>.Ignored, A<CancellationToken>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task Should_Remove_An_Item()
    {
        // Arrange
        var fakeRepository = A.Fake<IRepository>();
        
        var sut = new ItemService(fakeRepository);
        
        // Act
        await sut.DeleteAsync(A.Dummy<Guid>(), TestContext.Current.CancellationToken);
        
        // Assert
        A.CallTo(() => fakeRepository.DeleteAsync(A<Guid>.Ignored, A<CancellationToken>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
}
