﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentAServ.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _db;

        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }

        

        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
