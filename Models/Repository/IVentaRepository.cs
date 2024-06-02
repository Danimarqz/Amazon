namespace Amazon.Models.Repository
{
    public interface IVentaRepository
    {

        public Task<IEnumerable<Ventas>> GetVentasAdmin();
        public Task<int> GetVentaID(int userID);
        public Task<IEnumerable<DetallesVenta>> GetDetallesVenta(int ventaID);
        public Task AddVenta(int productoID, int userID);
        public Task RmVenta(int productoID, int userID);
        public Task EditVenta(DetallesVenta detallesVenta);
    }
}
