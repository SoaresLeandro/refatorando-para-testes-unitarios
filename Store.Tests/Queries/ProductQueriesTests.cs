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

        _products.Add(new Product("Produto 01", 10, true));
        _products.Add(new Product("Produto 02", 10, true));
        _products.Add(new Product("Produto 03", 10, false));
        _products.Add(new Product("Produto 04", 10, false));
        _products.Add(new Product("Produto 05", 10, true));
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void DadaUmaConsultaDeProdutosAtivosDeveRetornarTres()
    {
        var expected = 3;

        var _query = ProductQueries.GetActiveProducts();
        var activesProducts = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
        var result = activesProducts.Count();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void DadaUmaConsultaDeProdutosInativosDeveRetornarDois()
    {
        var expected = 2;

        var _query = ProductQueries.GetActiveProducts();
        var inactiveProducts = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        var result = inactiveProducts.Count();

        Assert.AreEqual(expected, result);
    }
}