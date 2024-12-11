using Store.Domain.Entities;
using Store.Domain.Queries;
using Store.Tests.Repositories;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private readonly FakeProductRepository _fakeProductRepository;
    IList<Product> _products = new List<Product>();
    public ProductQueriesTests()
    {
        var _fakeProductRepository = new FakeProductRepository();
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void DadaUmaConsultaDeProdutosAtivosDeveRetornarTres()
    {
        var expected = 3;

        var _query = ProductQueries.GetActiveProducts();
        var activesProducts = _fakeProductRepository.GetAll().AsQueryable().Where(ProductQueries.GetActiveProducts());
        var result = activesProducts.Count();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void DadaUmaConsultaDeProdutosInativosDeveRetornarDois()
    {
        var expected = 2;

        var _query = ProductQueries.GetActiveProducts();
        var inactiveProducts = _fakeProductRepository.GetAll().AsQueryable().Where(ProductQueries.GetInactiveProducts());
        var result = inactiveProducts.Count();

        Assert.AreEqual(expected, result);
    }
}