using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace Fb
{
    class Program
    {
        public static readonly string ConnString = @"Database=bin\Debug\FbTest.FDB;ServerType=1;username=SYSDBA";

        static void Main(string[] args)
        {
            File.Delete(@"bin\Debug\FbTest.FDB");
            FbConnection.CreateDatabase(ConnString);

            using(IDbConnection cn = new FbConnection(ConnString))
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"create table FbTest (
                                    Id INTEGER not null,
                                    TestColumn VARCHAR(255) not null,
                                    primary key (Id)
                                    );";

                cmd.ExecuteNonQuery();
            }
            
            using(IDbConnection cn = new FbConnection(ConnString))
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"insert into FbTest (Id, TestColumn) values (@p0, @p1);";

                IDataParameter p0 = cmd.CreateParameter();
                p0.ParameterName = "@p0";
                p0.DbType = DbType.Int32;
                p0.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p0);

                IDataParameter p1 = cmd.CreateParameter();
                p1.ParameterName = "@p1";
                p1.DbType = DbType.String;
                p1.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p1);

                p0.Value = 1;
                p1.Value = "test1";
                cmd.ExecuteNonQuery();

                p0.Value = 2;
                p1.Value = "test10";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
