namespace Amazon.Models.Repository
{
    public interface ICarritoRepository
    {
        public Task<Carrito> GetCart(int userID);
        public void Add(Carrito cart);
        public void Edit(Carrito cart);
        public void Delete(Carrito cart);
    }
}
