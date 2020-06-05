using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Helpers;
using WorkManagement.API.Models;

namespace WorkManagement.API.DataLayer
{
    public class TenantDL
    {
        #region Constructor
        private IConfiguration _config;

        public TenantDL(IConfiguration config)
        {
            _config = config;
        }
        #endregion

        #region Lookup Tenant
        public async Task<List<Tenant>> LookupTenant(Tenant tenant)
        {
            List<Tenant> tenants = new List<Tenant>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_lookUpTenant", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@apartmentNo", DBHelper.NullHandler(tenant.ApartmentNo)));
                cmd.Parameters.Add(new SqlParameter("@firstName", DBHelper.NullHandler(tenant.FirstName)));
                cmd.Parameters.Add(new SqlParameter("@lastName", DBHelper.NullHandler(tenant.LastName)));

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        tenants.Add(new Tenant()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            ApartmentNo = reader.GetString(reader.GetOrdinal("ApartmentNumber")),
                        });

                    }
                }
            }

            return tenants;
        }
        #endregion
    }
}
