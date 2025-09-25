﻿using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        Task<List<TipoUsuario>> GetAllAsync();
        //stand by
        Task<TipoUsuario> GetByIdAsync(int id);
        Task AddAsync(TipoUsuario tipoUsuario);
        Task UpdateAsync(TipoUsuario tipoUsuario);
        Task DeleteAsync(int id);
    }
}
