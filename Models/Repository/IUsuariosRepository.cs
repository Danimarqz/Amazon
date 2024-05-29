namespace Amazon.Models.Repository
{
    public interface IUsuariosRepository
    {
        public Task<IEnumerable<Usuarios>> GetAll();
        public Task<Usuarios> GetById(int id);
        public void Add(Usuarios usuarios);
        public void Update(Usuarios usuarios);
        public void Delete(Usuarios usuarios);
    }
}
