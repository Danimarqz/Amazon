namespace Amazon.Models.Repository
{
    public interface ICarritoRepository
    {
        public Task<IEnumerable<Carrito>> GetCartAdmin();
        public Task<int> GetCartID(int userID);
        public Task<IEnumerable<DetallesCarrito>> GetAllCart(int userID);
        public Task AddProducto(int productoID, int userID);
        public Task RmProducto(int productoID, int userID);
        public Task RmAllProducto(int productoID, int userID);
        public Task EditDetallesProductoCarrito(DetallesCarrito detallesCarrito);
        public Task RmCarrito(int userID);
        public Task<int> ProductosCarrito(int userID);
    }
}
