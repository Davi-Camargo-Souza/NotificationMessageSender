﻿using Dapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Infraestructure.Context;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;

namespace NotificationMessageSender.Infraestructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DapperContext _dapper;

        public BaseRepository(AppDbContext context, DapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async void Create(T entity, CancellationToken cancellationToken)
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        //public async void Inativar(Guid id, string tabela, CancellationToken cancellationToken)
        //{
        //    using (var connection = _dapper.CreateConnection())
        //    {
        //        connection.Open();
        //        string sql = $"SELECT * " +
        //                     $"FROM \"{tabela}\" " +
        //                     $"WHERE \"Id\" = '{id}'";
        //        var queryResult = await connection.QueryFirstOrDefaultAsync<T>(sql, cancellationToken);
        //        connection.Close();
        //        if (queryResult == null) throw new Exception("Registro não encontrado");
        //        queryResult.Ativo = false;
        //        Update(queryResult);
        //    }
        //}

        //public async void Ativar(Guid id, string tabela, CancellationToken cancellationToken)
        //{
        //    using (var connection = _dapper.CreateConnection())
        //    {
        //        connection.Open();
        //        string sql = $"SELECT * " +
        //                     $"FROM \"{tabela}\" " +
        //                     $"WHERE \"Id\" = '{id}'";
        //        var queryResult = await connection.QueryFirstOrDefaultAsync<T>(sql, cancellationToken);
        //        connection.Close();
        //        if (queryResult == null) throw new Exception("Registro não encontrado");
        //        queryResult.Ativo = true;
        //        Update(queryResult);
        //    }
        //}

        public async Task<T> Get(Guid id, string tabela, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                string sql = $"SELECT * " +
                             $"FROM \"{tabela}\" " +
                             $"WHERE \"Id\" = '{id}'";
                var queryResult = await connection.QueryFirstOrDefaultAsync<T>(sql, cancellationToken);
                connection.Close();
                return queryResult;
            }
        }

        public async Task<List<T>> GetAll(string tabela, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                string sql = $"SELECT * " +
                             $"FROM \"{tabela}\"";
                var queryResult = await connection.QueryAsync<T>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }

        public async Task<List<T>> GetAllAtivos(string tabela, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                string sql = $"SELECT * " +
                             $"FROM \"{tabela}\" " +
                             $"WHERE \"Ativo\" = '1'";
                var queryResult = await connection.QueryAsync<T>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }

        public async Task<List<T>> GetAllInativos(string tabela, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                string sql = $"SELECT * " +
                             $"FROM \"{tabela}\" " +
                             $"WHERE \"Ativo\" = '0'";
                var queryResult = await connection.QueryAsync<T>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }

        //public void Update(T entity)
        //{
        //    entity.UpdatedAt = DateTime.Now.ToUniversalTime();
        //    _context.Update(entity);
        //}

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }
    }
}
