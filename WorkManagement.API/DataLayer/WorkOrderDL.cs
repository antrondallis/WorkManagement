using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.DAL
{
    public class WorkOrderDL
    {
        private IConfiguration _config;

        public WorkOrderDL(IConfiguration config)
        {
            _config = config;
        }
        public List<WorkOrderForListDto> GetAllByUser(int userId)
        {
            List<WorkOrderForListDto> workOrders = new List<WorkOrderForListDto>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getAllByUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userId", userId));

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workOrders.Add(new WorkOrderForListDto()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                            WorkOrderType = reader.GetString(reader.GetOrdinal("WorkOrderType")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            AssignedTo = reader.GetString(reader.GetOrdinal("AssignedTo")),
                            RequestFor = reader.GetString(reader.GetOrdinal("RequestFor")),
                            ApartmentNo = reader.GetString(reader.GetOrdinal("ApartmentNo")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return workOrders;
        }

        public async Task<WorkOrderForDetailDto> GetById(int id)
        {
            WorkOrderForDetailDto workOrder = new WorkOrderForDetailDto();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", id));

                conn.Open();
                await using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workOrder.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        workOrder.RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));
                        workOrder.WorkOrderType = reader.GetString(reader.GetOrdinal("WorkOrderType"));
                        workOrder.Title = reader.GetString(reader.GetOrdinal("Title"));
                        workOrder.Description= reader.GetString(reader.GetOrdinal("Description"));
                        workOrder.AssignedTo = reader.GetString(reader.GetOrdinal("AssignedTo"));
                        workOrder.ApartmentNo = reader.GetString(reader.GetOrdinal("ApartmentNo"));
                        workOrder.RequestFor = reader.GetString(reader.GetOrdinal("RequestFor"));
                        workOrder.SubmittedBy= reader.GetString(reader.GetOrdinal("SubmittedBy"));
                        workOrder.Status = reader.GetString(reader.GetOrdinal("Status"));
                    }
                }
            }

            return workOrder;
        }
    }
}
