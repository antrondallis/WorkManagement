import pyodbc

# create a new ticket directly to the database 
# fill in necessary parameters 

# Work Order Request parameters 
request_date = '20200328 10:38:09 AM'
work_order_type_id = 3
title = 'AC Temperature'
description = 'Tenant says apartment is not getting cool. Has been running all night.'
assigned_to = 1
requested_for = 148
submitted_by = 9999
status = 1

# initialize database params and connection
sql = ("insert into workorder(requestdate, workordertypeid, title, description, assignedto, requestfor, "
    "submittedby, status) values ('{}', {}, '{}', '{}', {}, {}, {}, {})"
    .format(request_date, work_order_type_id, title, description,
    assigned_to, requested_for, submitted_by, status))
conn = pyodbc.connect('driver={SQL Server}; server=192.168.1.134,49170;database=WorkManagement;uid=workmanagement;pwd=management1')
cursor = conn.cursor()
cursor.execute(sql)
conn.commit()

print('Request Created')