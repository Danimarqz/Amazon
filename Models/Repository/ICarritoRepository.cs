namespace Amazon.Models.Repository
{
    public interface ICarritoRepository
    {
        public Task<Carrito> GetCart(int userID);
        public int GetCartID(int userID);
        public Task<DetallesCarrito> GetAllCart(int userID);
        public void Add(Carrito cart);
        public void AddDetalles(DetallesCarrito detallesCarrito);
        public void EditCarrito(Carrito cart);
        public void EditDetallesCarrito(DetallesCarrito detallesCarrito);
    }
}
