using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.DAL
{
    public class WorkOrderDL
    {

        #region Constructor
        private IConfiguration _config;

        public WorkOrderDL(IConfiguration config)
        {
            _config = config;
        }
        #endregion

        #region CreateWorkOrder
        public async Task<WorkOrder> CreateWorkOrder(WorkOrderForCreateDto workOrderForCreate)
        {
            WorkOrder workOrder = new WorkOrder();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_createWorkOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderTypeId", workOrderForCreate.WorkOrderTypeId));
                cmd.Parameters.Add(new SqlParameter("@title", workOrderForCreate.Title));
                cmd.Parameters.Add(new SqlParameter("@description", workOrderForCreate.Description));
                if (workOrderForCreate.AssignedTo > 0)
                    cmd.Parameters.Add(new SqlParameter("@assignedTo", workOrderForCreate.AssignedTo));
                else
                    cmd.Parameters.Add(new SqlParameter("@assignedTo", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@requestFor", workOrderForCreate.RequestFor));
                cmd.Parameters.Add(new SqlParameter("@submittedBy", workOrderForCreate.SubmittedBy));
                cmd.Parameters.Add("@new_id", SqlDbType.Int).Direction = ParameterDirection.Output;


                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                workOrder.Id = Convert.ToInt32(cmd.Parameters["@new_id"].Value);
            }

            return workOrder;
        }
        #endregion

        #region GetAllOpen
        public async Task<List<WorkOrder>> GetAllOpen()
        {
            List<WorkOrder> workOrders = new List<WorkOrder>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getAllOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var workOrder = new WorkOrder();

                        workOrder.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        workOrder.RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));

                        workOrder.WorkOrderType.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderTypeId"));
                        workOrder.WorkOrderType.Name = reader.GetString(reader.GetOrdinal("WorkOrderTypeName"));

                        workOrder.Title = reader.GetString(reader.GetOrdinal("Title"));
                        workOrder.Description = reader.GetString(reader.GetOrdinal("Description"));

                        if (!reader.IsDBNull("AssignedToId"))
                            workOrder.AssignedTo.Id = reader.GetInt32(reader.GetOrdinal("AssignedToId"));
                        if (!reader.IsDBNull("AssignedToFirstName"))
                            workOrder.AssignedTo.FirstName = reader.GetString(reader.GetOrdinal("AssignedToFirstName"));
                        if (!reader.IsDBNull("AssignedToLastName"))
                            workOrder.AssignedTo.LastName = reader.GetString(reader.GetOrdinal("AssignedToLastName"));
                        if (!reader.IsDBNull("AssignedToUserName"))
                            workOrder.AssignedTo.UserName = reader.GetString(reader.GetOrdinal("AssignedToUserName"));

                        workOrder.RequestFor.ApartmentNo = reader.GetString(reader.GetOrdinal("RequestForApartmentNo"));
                        workOrder.RequestFor.FirstName = reader.GetString(reader.GetOrdinal("RequestForFirstName"));
                        workOrder.RequestFor.LastName = reader.GetString(reader.GetOrdinal("RequestForLastName"));

                        if (!reader.IsDBNull("SubmittedByFirstName"))
                            workOrder.SubmittedBy.FirstName = reader.GetString(reader.GetOrdinal("SubmittedByFirstName"));
                        if (!reader.IsDBNull("SubmittedByLastName"))
                            workOrder.SubmittedBy.LastName = reader.GetString(reader.GetOrdinal("SubmittedByLastName"));
                        workOrder.SubmittedBy.UserName = reader.GetString(reader.GetOrdinal("SubmittedByUserName"));

                        workOrder.Status.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderStatusCodeId"));
                        workOrder.Status.Name = reader.GetString(reader.GetOrdinal("WorkOrderStatusCodeName"));

                        workOrders.Add(workOrder);
                    }
                }
            }

            return workOrders;
        }
        #endregion

        #region GetAllByUser
        public async Task<List<WorkOrder>> GetAllByUser(int userId)
        {
            List<WorkOrder> workOrders = new List<WorkOrder>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getAllByUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userId", userId));

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var workOrder = new WorkOrder();

                        workOrder.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        workOrder.RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));

                        workOrder.WorkOrderType.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderTypeId"));
                        workOrder.WorkOrderType.Name = reader.GetString(reader.GetOrdinal("WorkOrderTypeName"));

                        workOrder.Title = reader.GetString(reader.GetOrdinal("Title"));
                        workOrder.Description = reader.GetString(reader.GetOrdinal("Description"));

                        if (!reader.IsDBNull("AssignedToId"))
                            workOrder.AssignedTo.Id = reader.GetInt32(reader.GetOrdinal("AssignedToId"));
                        if (!reader.IsDBNull("AssignedToFirstName"))
                            workOrder.AssignedTo.FirstName = reader.GetString(reader.GetOrdinal("AssignedToFirstName"));
                        if (!reader.IsDBNull("AssignedToLastName"))
                            workOrder.AssignedTo.LastName = reader.GetString(reader.GetOrdinal("AssignedToLastName"));

                        workOrder.RequestFor.ApartmentNo = reader.GetString(reader.GetOrdinal("RequestForApartmentNo"));
                        workOrder.RequestFor.FirstName = reader.GetString(reader.GetOrdinal("RequestForFirstName"));
                        workOrder.RequestFor.LastName = reader.GetString(reader.GetOrdinal("RequestForLastName"));

                        if (!reader.IsDBNull("SubmittedByFirstName"))
                            workOrder.SubmittedBy.FirstName = reader.GetString(reader.GetOrdinal("SubmittedByFirstName"));
                        if (!reader.IsDBNull("SubmittedByLastName"))
                            workOrder.SubmittedBy.LastName = reader.GetString(reader.GetOrdinal("SubmittedByLastName"));
                        workOrder.SubmittedBy.UserName = reader.GetString(reader.GetOrdinal("SubmittedByUserName"));

                        workOrder.Status.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderStatusCodeId"));
                        workOrder.Status.Name = reader.GetString(reader.GetOrdinal("WorkOrderStatusCodeName"));

                        workOrders.Add(workOrder);
                    }
                }
            }

            return workOrders;
        }
        #endregion

        #region GetAllOpenByUser
        public async Task<List<WorkOrder>> GetAllOpenByUser(int userId)
        {
            List<WorkOrder> workOrders = new List<WorkOrder>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getAllOpenByUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userId", userId));

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var workOrder = new WorkOrder();

                        workOrder.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        workOrder.RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));

                        workOrder.WorkOrderType.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderTypeId"));
                        workOrder.WorkOrderType.Name = reader.GetString(reader.GetOrdinal("WorkOrderTypeName"));

                        workOrder.Title = reader.GetString(reader.GetOrdinal("Title"));
                        workOrder.Description = reader.GetString(reader.GetOrdinal("Description"));

                        if (!reader.IsDBNull("AssignedToId"))
                            workOrder.AssignedTo.Id = reader.GetInt32(reader.GetOrdinal("AssignedToId"));
                        if (!reader.IsDBNull("AssignedToFirstName"))
                            workOrder.AssignedTo.FirstName = reader.GetString(reader.GetOrdinal("AssignedToFirstName"));
                        if (!reader.IsDBNull("AssignedToLastName"))
                            workOrder.AssignedTo.LastName = reader.GetString(reader.GetOrdinal("AssignedToLastName"));

                        workOrder.RequestFor.ApartmentNo = reader.GetString(reader.GetOrdinal("RequestForApartmentNo"));
                        workOrder.RequestFor.FirstName = reader.GetString(reader.GetOrdinal("RequestForFirstName"));
                        workOrder.RequestFor.LastName = reader.GetString(reader.GetOrdinal("RequestForLastName"));

                        if (!reader.IsDBNull("SubmittedByFirstName"))
                            workOrder.SubmittedBy.FirstName = reader.GetString(reader.GetOrdinal("SubmittedByFirstName"));
                        if (!reader.IsDBNull("SubmittedByLastName"))
                            workOrder.SubmittedBy.LastName = reader.GetString(reader.GetOrdinal("SubmittedByLastName"));
                        workOrder.SubmittedBy.UserName = reader.GetString(reader.GetOrdinal("SubmittedByUserName"));

                        workOrder.Status.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderStatusCodeId"));
                        workOrder.Status.Name = reader.GetString(reader.GetOrdinal("WorkOrderStatusCodeName"));

                        workOrders.Add(workOrder);
                    }
                }
            }

            return workOrders;
        }
        #endregion

        #region GetById
        public async Task<WorkOrder> GetById(int id)
        {
            WorkOrder workOrder = new WorkOrder();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", id));

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        workOrder.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        workOrder.RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));

                        workOrder.WorkOrderType.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderTypeId"));
                        workOrder.WorkOrderType.Name = reader.GetString(reader.GetOrdinal("WorkOrderTypeName"));

                        workOrder.Title = reader.GetString(reader.GetOrdinal("Title"));
                        workOrder.Description = reader.GetString(reader.GetOrdinal("Description"));

                        if (!reader.IsDBNull("AssignedToId"))
                            workOrder.AssignedTo.Id = reader.GetInt32(reader.GetOrdinal("AssignedToId"));
                        if (!reader.IsDBNull("AssignedToFirstName"))
                            workOrder.AssignedTo.FirstName = reader.GetString(reader.GetOrdinal("AssignedToFirstName"));
                        if (!reader.IsDBNull("AssignedToLastName"))
                            workOrder.AssignedTo.LastName = reader.GetString(reader.GetOrdinal("AssignedToLastName"));

                        workOrder.RequestFor.ApartmentNo = reader.GetString(reader.GetOrdinal("RequestForApartmentNo"));
                        workOrder.RequestFor.FirstName = reader.GetString(reader.GetOrdinal("RequestForFirstName"));
                        workOrder.RequestFor.LastName = reader.GetString(reader.GetOrdinal("RequestForLastName"));

                        if (!reader.IsDBNull("SubmittedByFirstName"))
                            workOrder.SubmittedBy.FirstName = reader.GetString(reader.GetOrdinal("SubmittedByFirstName"));
                        if (!reader.IsDBNull("SubmittedByLastName"))
                            workOrder.SubmittedBy.LastName = reader.GetString(reader.GetOrdinal("SubmittedByLastName"));
                        workOrder.SubmittedBy.UserName = reader.GetString(reader.GetOrdinal("SubmittedByUserName"));

                        workOrder.Status.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderStatusCodeId"));
                        workOrder.Status.Name = reader.GetString(reader.GetOrdinal("WorkOrderStatusCodeName"));
                    }
                }
            }

            return workOrder;
        }
        #endregion

        #region GetWorkOrderForEdit
        public async Task<WorkOrderForEditDto> GetWorkOrderForEdit(int id)
        {
            WorkOrderForEditDto workOrder = new WorkOrderForEditDto();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getWorkOrderForEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", id));

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        workOrder.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        workOrder.RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate"));

                        workOrder.WorkOrderType.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderTypeId"));
                        workOrder.WorkOrderType.Name = reader.GetString(reader.GetOrdinal("WorkOrderTypeName"));

                        workOrder.Title = reader.GetString(reader.GetOrdinal("Title"));
                        workOrder.Description = reader.GetString(reader.GetOrdinal("Description"));

                        workOrder.AssignedTo.FirstName = reader.GetString(reader.GetOrdinal("AssignedToFirstName"));
                        workOrder.AssignedTo.LastName = reader.GetString(reader.GetOrdinal("AssignedToLastName"));

                        workOrder.RequestFor.ApartmentNo = reader.GetString(reader.GetOrdinal("RequestForApartmentNo"));
                        workOrder.RequestFor.FirstName = reader.GetString(reader.GetOrdinal("RequestForFirstName"));
                        workOrder.RequestFor.LastName = reader.GetString(reader.GetOrdinal("RequestForLastName"));

                        if (!reader.IsDBNull("SubmittedByFirstName"))
                            workOrder.SubmittedBy.FirstName = reader.GetString(reader.GetOrdinal("SubmittedByFirstName"));
                        if (!reader.IsDBNull("SubmittedByLastName"))
                            workOrder.SubmittedBy.LastName = reader.GetString(reader.GetOrdinal("SubmittedByLastName"));
                        workOrder.SubmittedBy.UserName = reader.GetString(reader.GetOrdinal("SubmittedByUserName"));

                        workOrder.Status.Id = reader.GetInt32(reader.GetOrdinal("WorkOrderStatusCodeId"));
                        workOrder.Status.Name = reader.GetString(reader.GetOrdinal("WorkOrderStatusCodeName"));
                    }
                }

                return workOrder;
            }
        }
        #endregion

        #region GetNotesForWorkOrder
        public async Task<List<WorkOrderNote>> GetNotesForWorkOrder(int id)
        {
            List<WorkOrderNote> notes = new List<WorkOrderNote>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getWorkOrderNotesByWorkOrderId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", id));

                conn.Open();
                await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        WorkOrderNote noteToAdd = new WorkOrderNote();

                        noteToAdd.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        noteToAdd.Note = reader.GetString(reader.GetOrdinal("Note"));
                        noteToAdd.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
                        noteToAdd.CreatedBy.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        noteToAdd.CreatedBy.LastName = reader.GetString(reader.GetOrdinal("LastName"));

                        notes.Add(noteToAdd);
                    }
                }
            }

            return notes;
        }
        #endregion

        #region GetCountsForDashboard
        public async Task<DashboardCountDto> GetCountsForDashboard(int userId)
        {
            DashboardCountDto dashboardCountDto = new DashboardCountDto();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getCountsForDashboard", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userId", userId));

                conn.Open();
                await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        dashboardCountDto.TotalWorkOrders = reader.GetInt32(reader.GetOrdinal("TotalWorkOrders"));
                        dashboardCountDto.AssignedWorkOrders = reader.GetInt32(reader.GetOrdinal("AssignedWorkOrders"));
                        dashboardCountDto.AgeTwoWeeks = reader.GetInt32(reader.GetOrdinal("AgeTwoWeeks"));
                        dashboardCountDto.Unassigned = reader.GetInt32(reader.GetOrdinal("Unassigned"));
                    }
                }
            }

            return dashboardCountDto;
        }
        #endregion

        #region GetWorkOrderTypes
        public async Task<List<WorkOrderType>> GetWorkOrderTypes()
        {
            List<WorkOrderType> workOrderTypes = new List<WorkOrderType>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getWorkOrderTypes", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        workOrderTypes.Add(new WorkOrderType()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        });
                    }
                }
            }

            return workOrderTypes;
        }
        #endregion

        #region GetWorkOrderStatusCode
        public async Task<int> GetWorkOrderStatusCode(int workOrderId)
        {
            int statusId = 0;

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getWorkOrderStatusCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", workOrderId));

                conn.Open();
                await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        statusId = reader.GetInt32(reader.GetOrdinal("Status"));

                    }
                }
            }

            return statusId;
        }
        #endregion

        #region GetWorkOrderStatusCodes
        public async Task<List<WorkOrderStatusCode>> GetWorkOrderStatusCodes()
        {
            List<WorkOrderStatusCode> workOrderTypes = new List<WorkOrderStatusCode>();

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_getWorkOrderStatusCodes", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        workOrderTypes.Add(new WorkOrderStatusCode()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        });
                    }
                }
            }

            return workOrderTypes;
        }
        #endregion

        #region AddWorkOrderNote
        public async Task<HttpStatusCode> AddWorkOrderNote(WorkOrderNote workOrderNote)
        {
            HttpStatusCode httpStatus = (HttpStatusCode)500;

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_addWorkOrderNote", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", workOrderNote.WorkOrderId));
                cmd.Parameters.Add(new SqlParameter("@note", workOrderNote.Note));
                cmd.Parameters.Add(new SqlParameter("@createdBy", workOrderNote.CreatedBy.Id));

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                httpStatus = HttpStatusCode.Created;
            }

            return httpStatus;
        }
        #endregion

        #region UpdateWorkOrder
        public async Task<HttpStatusCode> UpdateWorkOrder(WorkOrder workOrder)
        {
            HttpStatusCode httpStatus = (HttpStatusCode)500;

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_updateWorkOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", workOrder.Id));
                cmd.Parameters.Add(new SqlParameter("@workOrderTypeId", workOrder.WorkOrderType.Id));
                cmd.Parameters.Add(new SqlParameter("@description", workOrder.Description));
                cmd.Parameters.Add(new SqlParameter("@status", workOrder.Status.Id));

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                httpStatus = HttpStatusCode.NoContent;
            }

            return httpStatus;
        }
        #endregion

        #region LogStatusChange
        public async Task<HttpStatusCode> LogStatusChange(WorkOrderStatusLog log)
        {
            HttpStatusCode httpStatus = HttpStatusCode.BadRequest;

            using (var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_logStatusChange", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", log.WorkOrderId));
                cmd.Parameters.Add(new SqlParameter("@userId", log.UserId));
                cmd.Parameters.Add(new SqlParameter("@statusId", log.StatusId));

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                httpStatus = HttpStatusCode.NoContent;
            }

            return httpStatus;
        }
        #endregion

        #region AssignToLoggedInUser
        public async Task<HttpStatusCode> AssignToLoggedInUser(WorkOrderAssignDto workOrder)
        {
            HttpStatusCode httpStatus = HttpStatusCode.BadRequest;

            using (var conn = new SqlConnection(_config["ConnectionSTrings:DefaultConnection"]))
            {
                var cmd = new SqlCommand("pr_assignToLoggedInUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@workOrderId", workOrder.Id));
                cmd.Parameters.Add(new SqlParameter("@userId", workOrder.AssignToId));

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                httpStatus = HttpStatusCode.NoContent;
            }

            return httpStatus;
        }
        #endregion
    }



}
