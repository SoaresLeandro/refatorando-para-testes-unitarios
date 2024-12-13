using Store.Domain.Entities;
using Store.Domain.Queries;
using Store.Tests.Repositories;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private readonly FakeProductRepository _fakeProductRepository = new FakeProductRepository();
    IList<Product> _products = new List<Product>();

    [TestMethod]
    [TestCategory("Queries")]
    public void DadaUmaConsultaDeProdutosAtivosDeveRetornarTres()
    {
        var expected = 3;

        var _query = ProductQueries.GetActiveProducts();
        var activesProducts = _fakeProductRepository.GetAll().AsQueryable().Where(_query);
        var result = activesProducts.Count();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void DadaUmaConsultaDeProdutosInativosDeveRetornarDois()
    {
        var expected = 2;

        var _query = ProductQueries.GetInactiveProducts();
        var inactiveProducts = _fakeProductRepository.GetAll().AsQueryable().Where(_query);
        var result = inactiveProducts.Count();

        Assert.AreEqual(expected, result);
    }
}