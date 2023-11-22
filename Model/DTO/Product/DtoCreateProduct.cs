namespace Model.DTO.Product;

public class DtoCreateProduct
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string SupplierId { get; set; }
    public string CategoryId { get; set; }
}
