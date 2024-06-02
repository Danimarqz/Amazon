namespace Amazon.Models.Repository
{
    public interface IUsuariosRepository
    {
        public Task<IEnumerable<Usuarios>> GetAll();
        public Task<Usuarios> GetById(int id);
        public Task<Usuarios> GetByEmail(string email);
        public Task Add(Usuarios usuarios);
        public Task Update(Usuarios usuarios);
        public Task Delete(int id);
    }
}
