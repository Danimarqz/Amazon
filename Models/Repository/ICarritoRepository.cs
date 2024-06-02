namespace Amazon.Models.Repository
{
    public interface ICarritoRepository
    {
        public Task<Carrito> GetCart(int userID);
        public Task<IEnumerable<Carrito>> GetCartAdmin();
        public Task<int> GetCartID(int userID);
        public Task<IEnumerable<DetallesCarrito>> GetAllCart(int userID);
        public Task Add(int userID);
        public Task UpdateCart(int cartID);
        public Task AddProducto(int productoID, int userID);
        public Task RmProducto(int productoID, int userID);
        public Task EditCarrito(Carrito cart);
        public Task EditDetallesProductoCarrito(DetallesCarrito detallesCarrito);
        public Task DeleteProducto(int productID, int usuarioID);
    }
}
